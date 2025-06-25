using Microsoft.Extensions.Options;

namespace Application.DAL
{
    public class DbContextOption : IOptions<DbContextOption>
    {
        public const string Defalut = "Defalut";
        public DbContextOption Value => this;
        public bool IsEnabled { get; set; }
        public string ConnectionName { get; set; } = Defalut;
    }
}
