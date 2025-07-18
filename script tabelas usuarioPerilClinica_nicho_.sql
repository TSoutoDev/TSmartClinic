select * from Modulo
select * from Funcionalidade
select * from Perfil
select * from Operacao
select * from OperacaoPerfil
select * from [dbo].[Nicho]
select * from [dbo].[Convenio]
select * from [dbo].Clinica
select * from [dbo].[Usuario]
select * from [dbo].[Paciente]
sp_help'Clinica'


CREATE TABLE UsuarioClinicaPerfil (
  UsuarioId INT NOT NULL
    CONSTRAINT FK_UCP_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
  ClinicaId INT NOT NULL
    CONSTRAINT FK_UCP_Clinica FOREIGN KEY (ClinicaId) REFERENCES Clinica(Id),
  PerfilId INT NOT NULL
    CONSTRAINT FK_UCP_Perfil FOREIGN KEY (PerfilId) REFERENCES Perfil(Id),
  CONSTRAINT PK_UsuarioClinicaPerfil PRIMARY KEY (UsuarioId, ClinicaId, PerfilId)
);

--exemplo :Exemplo de SQL para levantar permissões de um usuário na clínica atual
SELECT op.Codigo
FROM UsuarioClinicaPerfil ucp
JOIN OperacaoPerfil   opf ON opf.PerfilId = ucp.PerfilId
JOIN Operacao         op  ON op.Id = opf.OperacaoId
WHERE ucp.UsuarioId = @usuarioId
  AND ucp.ClinicaId = @clinicaId;

