namespace AgendaApp.Core.Domain.Configurations
{
    public static class SqlServerConfiguration
    {
        public static string? ConnectionString =>
             @"Data Source=sistema-agenda.database.windows.net;Initial Catalog=sistemaAgendaDB;User ID=adminsistemaagenda;Password=Be15@Ju08#Ti86#Ge86*;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;";
    }
}
