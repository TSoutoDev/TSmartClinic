namespace AgendaApp.Core.Domain.Configurations
{
    public static class SqlServerConfiguration
    {
        public static string? ConnectionString =>
             @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BdAgendaApp;Integrated Security=True;";
    }
}
