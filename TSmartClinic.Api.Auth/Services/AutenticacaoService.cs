﻿using TSmartClinic.Api.Auth.DTOs;
using TSmartClinic.Api.Auth.Interfaces.Services;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Models;
using AutoMapper;
using TSmartClinic.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TSmartClinic.Core.Infra.Security.Services;

namespace TSmartClinic.Api.Auth.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioService? _usuarioService;
        private readonly IUsuarioClientePerfilService? _usuarioClinicaPerfilService;
        private readonly ICriptografiaProvider _criptografiaProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AutenticacaoService(IUsuarioService? usuarioService, IUsuarioClientePerfilService usuarioClinicaPerfil, ICriptografiaProvider criptografiaProvider = null, ITokenService tokenService = null, IMapper mapper = null, IHttpContextAccessor httpContextAccessor = null)
        {
            _usuarioService = usuarioService;
            _usuarioClinicaPerfilService = usuarioClinicaPerfil;
            _criptografiaProvider = criptografiaProvider;
            _tokenService = tokenService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        //public LoginResponseDto Login(LoginRequestDto loginRequestDto)
        //{
        //    try
        //    {
        //        if (loginRequestDto == null)
        //            throw new ArgumentNullException(nameof(loginRequestDto), "A requisição de login não pode ser nula.");

        //        if (string.IsNullOrWhiteSpace(loginRequestDto.Email) || string.IsNullOrWhiteSpace(loginRequestDto.Senha))
        //            throw new ArgumentException("E-mail e senha são obrigatórios.");

        //        var usuario = _usuarioService?.ObterPorEmail(loginRequestDto.Email);

        //        if (usuario == null)
        //            return null;



        //        // var senhaCifrada = _criptografiaProvider.Criptografar(usuario.Senha);
        //        var senhaDecifrada = _criptografiaProvider.Decriptografar(usuario.Senha);

        //        if (!senhaDecifrada.Equals(loginRequestDto.Senha))
        //            return null;

        //        var usuarioAutenticacao = _mapper.Map<AutenticacaoModel>(usuario);

        //        // Garantindo os campos essenciais
        //        usuarioAutenticacao.Id = usuario.Id;
        //        usuarioAutenticacao.ClienteId = usuario.ClienteId;
        //        usuarioAutenticacao.TipoUsuario = usuario.TipoUsuario; // 'M' ou 'C'

        //        //var usuarioSistema = usuario.UsuariosSistema.FirstOrDefault(); REVER

        //        //var permissoes = _usuarioService.ObterPermissaoUsuario(usuario.Id, loginRequestDto.ClinicaId, loginRequestDto.ModuloId);

        //        var clinicasUsuario = _usuarioClinicaPerfilService.ObterClinicasDoUsuario(usuario.Id);

        //        var permissoes = _usuarioService.ObterPermissaoUsuario(usuario.Id, clinicasUsuario);

        //        var accessToken = _tokenService.GerarToken(usuarioAutenticacao, permissoes);

        //        var response = new LoginResponseDto
        //        {
        //            AccessToken = accessToken,
        //            Nome = usuario.Nome,
        //            Email = usuario.Email,
        //            IdUsuario = usuarioAutenticacao.Id,
        //            TipoUsuario = usuarioAutenticacao.TipoUsuario.ToString(),
        //            ListClientes = clinicasUsuario,// loginRequestDto.ClinicaId,
        //            PrimeiroAcesso = usuario.PrimeiroAcesso
        //        };

        //        return response;
        //    }
        //    catch (AcessoNegadoException adx)
        //    {
        //        throw new ApplicationException("Acesso negado: " + adx.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Erro inesperado ao realizar login: " + ex.Message);
        //    }
        //}


        public LoginResponseDto? Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                if (loginRequestDto == null)
                    throw new ArgumentNullException(nameof(loginRequestDto), "A requisição de login não pode ser nula.");

                var email = loginRequestDto.Email?.Trim().ToLowerInvariant();
                var senha = loginRequestDto.Senha;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                    throw new ArgumentException("E-mail e senha são obrigatórios.");

                var usuario = _usuarioService?.ObterPorEmail(email);

                if (usuario == null)
                    return null;

                // Se ainda não definiu senha (primeiro acesso)
                if (string.IsNullOrEmpty(usuario.Senha))
                {
                    return new LoginResponseDto
                    {
                        PrimeiroAcesso = true,
                        Email = usuario.Email,
                        Nome = usuario.Nome,
                        IdUsuario = usuario.Id
                    };
                }

                var hasher = new PasswordHasher<Usuario>();
                var vr = hasher.VerifyHashedPassword(usuario, usuario.Senha, senha);

                if (vr != PasswordVerificationResult.Success &&
                    vr != PasswordVerificationResult.SuccessRehashNeeded)
                    return null;

                // Monta dados do token
                var usuarioAutenticacao = _mapper.Map<AutenticacaoModel>(usuario);
                usuarioAutenticacao.Id = usuario.Id;
                usuarioAutenticacao.ClienteId = usuario.ClienteId;
                usuarioAutenticacao.TipoUsuario = usuario.TipoUsuario;

                var clinicasUsuario = _usuarioClinicaPerfilService.ObterClinicasDoUsuario(usuario.Id);
                var permissoes = _usuarioService.ObterPermissaoUsuario(usuario.Id, clinicasUsuario);

                var accessToken = _tokenService.GerarToken(usuarioAutenticacao, permissoes);

                return new LoginResponseDto
                {
                    AccessToken = accessToken,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    IdUsuario = usuarioAutenticacao.Id,
                    TipoUsuario = usuarioAutenticacao.TipoUsuario.ToString(),
                    ListClientes = clinicasUsuario,
                    PrimeiroAcesso = usuario.PrimeiroAcesso
                };
            }
            catch (AcessoNegadoException adx)
            {
                throw new ApplicationException("Acesso negado: " + adx.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao realizar login: " + ex.Message);
            }
        }

        public void Logout(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public LoginResponseDto RefreshToken(int usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
