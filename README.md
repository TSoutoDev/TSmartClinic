# Tarefas API
Este projeto é uma API desenvolvida em ASP.NET Core 8 para gerenciar
tarefas e categorias.
## Tecnologias Utilizadas
- **ASP.NET Core 8**: Framework principal utilizado para construir a API.
- **Entity Framework Core**: ORM usado para interagir com o banco de
dados de forma eficiente e simplificada.
- **Dapper**: Micro ORM utilizado para consultas de desempenho mais alto
e manipulação direta de SQL.
- **AutoMapper**: Biblioteca para mapeamento automático de objetos,
usada para simplificar a conversão entre DTOs e entidades de domínio.
- **Swagger**: Ferramenta de documentação de API, integrada para
fornecer uma interface interativa para testar os endpoints da API.
## Requisitos
- .NET 8 SDK ou superior
- Banco de dados SQL Server
- Visual Studio ou Visual Studio Code
## Configuração do Projeto
1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/task-management-api.git
```
2. Navegue até o diretório do projeto:
```bash
cd task-management-api
```
3. Configure a string de conexão no arquivo `appsettings.json`:
```json
"ConnectionStrings": {
"DefaultConnection":
"Server=.;Database=TaskManagementDb;Trusted_Connection=True;"
}
```
4. Execute as migrações do Entity Framework para criar o banco de dados:
```bash
dotnet ef database update
```
5. Compile e execute o projeto:
```bash
dotnet run
```
## Documentação e Testes
A documentação da API pode ser acessada utilizando o Swagger em
`http://localhost:{porta}/swagger`. Através dessa interface, é possível
testar todos os endpoints da API de maneira interativa.
## Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull
requests.
## Licença
Este projeto está licenciado sob os termos da [MIT License](LICENSE).