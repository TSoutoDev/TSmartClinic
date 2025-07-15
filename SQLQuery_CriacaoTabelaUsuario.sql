CREATE TABLE Usuario (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),

    Login NVARCHAR(100) NOT NULL,
    Senha NVARCHAR(255) NOT NULL,           -- Senha codificada (de preferência com hash seguro)
    Nome NVARCHAR(150) NOT NULL,

    DataCriacao DATETIME DEFAULT GETDATE(), -- Data em que o usuário foi criado
    LoginInclusao NVARCHAR(100),            -- Quem cadastrou esse usuário

    DataInicio DATETIME,                    -- Data de ativação
    DataFim DATETIME,                       -- Data de desativação (expiração do acesso)

    FlagBloqueado BIT DEFAULT 0,            -- 1 = bloqueado / 0 = normal
    DataBloqueio DATETIME,                  -- Quando o bloqueio ocorreu

    UltimoAcesso DATETIME,                  -- Última vez que o usuário acessou
    PrimeiroAcesso BIT DEFAULT 1,           -- True se ainda não acessou pela primeira vez

    ExpiracaoSenha DATETIME,                -- Data de expiração da senha

    Email NVARCHAR(150),
    Celular NVARCHAR(20),

    Empresa NVARCHAR(100),
    TipoUsuario NVARCHAR(50),               -- Ex: Admin, Suporte, Cliente...

    Foto VARBINARY(MAX),                    -- Opcional: Armazena a imagem em binário

    Ativo BIT DEFAULT 1,                    -- Usuário ativo?
    Tipo CHAR(1) DEFAULT 'H'                -- Tipo: 'H' (Humano), 'T' (Técnico), etc
);

INSERT INTO Usuario (
    Login, Senha, Nome, LoginInclusao, DataInicio, DataFim,
    FlagBloqueado, PrimeiroAcesso, ExpiracaoSenha, Email, Celular,
    Empresa, TipoUsuario, Ativo
)
VALUES (
    'tsouto.dev@gmail.com',
    'MTIzNDU2', -- 123456 em base64 (somente para testes)
    'Tiago Souto',
    'admin@gmail.com.br',
    GETDATE(),
    DATEADD(YEAR, 1, GETDATE()),   -- Expira em 1 ano
    0,                             -- Não está bloqueado
    1,                             -- Primeiro acesso = true
    DATEADD(MONTH, 6, GETDATE()),  -- Senha expira em 6 meses
    'tsouto.dev@gmail.com',
    '(21) 980560123',
    'sidetech',
    'Administrador',
    1
);
