using Microsoft.EntityFrameworkCore;
using Models;
using System.Reflection;

namespace DAL
{
    public class MailMeUpContext : DbContext
    {
        public DbSet<LogMeUp> MailMeUpUserLogs { get; set; }
        public DbSet<MailMeUpUser> MailMeUpUsers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite($"Data Source={Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "mailmeup.db")}");
    }
}