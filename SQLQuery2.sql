select * from Categoria

select * from Tarefa
INSERT INTO Tarefa 
    (Nome, Data, Hora, Prioridade, FlagSituacao, DataCriacao, UsuarioCriacao, DataAlteracao, UsuarioAlteracao, CategoriaId)
VALUES 
    ('Comprar Material', '2025-07-11', '14:30:00', 1, 1, GETDATE(), 'Tsouto', NULL, NULL, 5);
