using Microsoft.Extensions.Options;

namespace SigmaProject.API.Options
{
    public class DatabaseOptionSetup : IConfigureOptions<DatabaseOption>
    {
        private const string ConfigurationSectionName = "DatabaseOption";
        private readonly IConfiguration _configuration;

        public DatabaseOptionSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseOption option)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            option.ConnectionString = connectionString;

            _configuration.GetSection(ConfigurationSectionName).Bind(option);
        }
    }
}