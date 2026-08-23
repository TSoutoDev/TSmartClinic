namespace TSmartClinic.Core.Domain.Configurations
{
    public static class PostgresConfiguration
    {
        public static string? ConnectionString =>
           @"Host=localhost;
              Port=5432;
              Database=tsmartclinic;
              Username=postgres;
              Password=Julia310308@;
              Pooling=true;";
    }
}
