CREATE TABLE Modulo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Descricao VARCHAR(255)
);

CREATE TABLE Funcionalidade (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NomeTabela VARCHAR(100) NOT NULL,
    Descricao VARCHAR(255),
    ModuloId INT NOT NULL,
    CONSTRAINT FK_Funcionalidade_Modulo FOREIGN KEY (ModuloId) REFERENCES Modulo(Id)
);

CREATE TABLE Operacao (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,            -- Ex: 'Visualizar', 'Editar'
    Descricao VARCHAR(255),
    FuncionalidadeId INT NOT NULL,
    CONSTRAINT FK_Operacao_Funcionalidade FOREIGN KEY (FuncionalidadeId) REFERENCES Funcionalidade(Id)
);

INSERT INTO Modulo (Nome, Descricao)
VALUES
    ('Basic', 'Módulo básico com funcionalidades essenciais para iniciar.'),
    ('Essential', 'Módulo intermediário com recursos importantes para uso comum.'),
    ('Expert', 'Módulo avançado com funcionalidades completas e avançadas.');

	INSERT INTO Funcionalidade (NomeTabela, Descricao, ModuloId)
VALUES
-- Basic
('Agendamento', 'Gerenciamento de tarefas, compromissos ou eventos.', 1),
('Dashboard', 'Visão geral com informações resumidas do sistema.', 1),
('Usuarios', 'Cadastro e gerenciamento básico de usuários.', 1),
('Categoria', 'Gestão de categorias ou classificações.', 1),

-- Essential
('Relatorios', 'Emissão de relatórios para análise de dados.', 2),
('Integracoes', 'Integração com sistemas externos ou APIs.', 2),
('Documento', 'Organização e controle de documentos do sistema.', 2),
('AuditoriaSimples', 'Registro básico de ações realizadas no sistema.', 2),

-- Expert
('ControleAcesso', 'Gerenciamento de permissões avançadas.', 3),
('AuditoriaCompleta', 'Rastreamento detalhado de atividades no sistema.', 3),
('Indicadores', 'Visualização de KPIs e gráficos de performance.', 3),
('Automacoes', 'Automatização de fluxos e processos.', 3);


-- Inserir operações padrão (Acessar, Incluir, Editar, Excluir) para cada funcionalidade

DECLARE @FuncionalidadeId INT;

DECLARE cur CURSOR FOR
    SELECT Id FROM Funcionalidade;

OPEN cur;
FETCH NEXT FROM cur INTO @FuncionalidadeId;

WHILE @@FETCH_STATUS = 0
BEGIN
    INSERT INTO Operacao (Nome, Descricao, FuncionalidadeId)
    VALUES 
        ('Acessar', 'Permite acessar a funcionalidade.', @FuncionalidadeId),
        ('Incluir', 'Permite incluir novos registros.', @FuncionalidadeId),
        ('Editar', 'Permite editar registros existentes.', @FuncionalidadeId),
        ('Excluir', 'Permite excluir registros.', @FuncionalidadeId);

    FETCH NEXT FROM cur INTO @FuncionalidadeId;
END;

CLOSE cur;
DEALLOCATE cur;
