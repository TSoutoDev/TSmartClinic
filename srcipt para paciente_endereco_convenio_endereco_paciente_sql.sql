CREATE TABLE Endereco (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Logradouro NVARCHAR(150) NOT NULL,
    Numero NVARCHAR(20),
    Complemento NVARCHAR(100),
    Bairro NVARCHAR(100),
    Cidade NVARCHAR(100),
    Estado NVARCHAR(50),
    CEP VARCHAR(10)
);

CREATE TABLE Convenio (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    CNPJ VARCHAR(18)    NULL,
    Telefone VARCHAR(20) NULL,
    Email NVARCHAR(100)  NULL,
    Ativo BIT            DEFAULT 1,
    DataCadastro DATETIME DEFAULT GETDATE()
);

-- Opcional: garantir CNPJ único (caso seja obrigatório para cada convênio)
ALTER TABLE Convenio
ADD CONSTRAINT UQ_Convenio_CNPJ UNIQUE (CNPJ);

CREATE TABLE Paciente (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    DataNascimento DATE,
    CPF VARCHAR(14),
    Telefone VARCHAR(20),
    Email VARCHAR(100),
    Observacoes NVARCHAR(MAX),
    Ativo BIT        DEFAULT 1,
    DataCadastro DATETIME DEFAULT GETDATE(),
    
    ConvenioId INT NULL,  
    CONSTRAINT FK_Paciente_Convenio FOREIGN KEY (ConvenioId)
        REFERENCES Convenio(Id)
);

-- Opcional: evitar CPF duplicado
ALTER TABLE Paciente
ADD CONSTRAINT UQ_Paciente_CPF UNIQUE (CPF);


CREATE TABLE PacienteEndereco (
    PacienteId INT NOT NULL,
    EnderecoId  INT NOT NULL,
    Tipo        VARCHAR(20)   -- ex: 'Residencial', 'Comercial'
    CONSTRAINT PK_PacienteEndereco PRIMARY KEY (PacienteId, EnderecoId),
    CONSTRAINT FK_Paciente_Endereco_Paciente FOREIGN KEY (PacienteId)
        REFERENCES Paciente(Id),
    CONSTRAINT FK_Paciente_Endereco_Endereco FOREIGN KEY (EnderecoId)
        REFERENCES Endereco(Id)
);

CREATE TABLE Clinica (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(150)    NOT NULL,
    RazaoSocial NVARCHAR(150) NULL,
    CNPJ VARCHAR(18)      NULL,
    Telefone VARCHAR(20)  NULL,
    Email NVARCHAR(100)   NULL,
    Ativo BIT             DEFAULT 1,
    DataCadastro DATETIME DEFAULT GETDATE()
);

-- Opcional: garantir CNPJ único
ALTER TABLE Clinica
ADD CONSTRAINT UQ_Clinica_CNPJ UNIQUE (CNPJ);

CREATE TABLE ClinicaEndereco (
    ClinicaId  INT       NOT NULL,
    EnderecoId INT       NOT NULL,
    Tipo       VARCHAR(20) NULL,  -- ex: 'Matriz', 'Filial', 'Comercial'

    CONSTRAINT PK_ClinicaEndereco PRIMARY KEY (ClinicaId, EnderecoId),
    CONSTRAINT FK_Clinica_Endereco_Clinica FOREIGN KEY (ClinicaId)
        REFERENCES Clinica(Id),
    CONSTRAINT FK_Clinica_Endereco_Endereco FOREIGN KEY (EnderecoId)
        REFERENCES Endereco(Id)
);
