using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Confab.Shared.Tests
{
    public static class DbHelper
    {
        private static readonly IConfiguration Configuration = OptionsHelper.GetConfigurationRoot();

        public static DbContextOptions<T> GetOptions<T>() where T : DbContext
            => new DbContextOptionsBuilder<T>()
                .UseNpgsql(Configuration["postgres:connectionString"])
                .EnableSensitiveDataLogging()
                .Options;
    }
}