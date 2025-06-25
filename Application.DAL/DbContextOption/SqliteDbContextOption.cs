using System.IO;

namespace Application.DAL
{
    public class SqliteDbContextOption : DbContextOption
    {
        public string? DBFileName { get; set; }

        public new SqliteDbContextOption Value => this;

        public string ConnectionString => 
            $"Data Source={Path.Combine(Environment.CurrentDirectory, DBFileName)};";
    }
}
