INSERT INTO TipoDocumento (Nome, FlagSituacao, DataCriacao, UsuarioCriacao)
VALUES 
('Contrato', 1, GETDATE(), 'admin'),
('Recibo', 1, GETDATE(), 'admin'),
('Declaração', 1, GETDATE(), 'admin'),
('Atestado', 1, GETDATE(), 'admin'),
('Relatório', 1, GETDATE(), 'admin');


select * from TipoDocumento