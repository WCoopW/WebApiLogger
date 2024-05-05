using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using WebApiRis.Model;

namespace WebApiRis.DB
{
	public class AppDbContext : DbContext
	{
		public DbSet<AirUser> AirUsers { get; set; } = null!;
		public DbSet<RSAKey> Keys { get; set; } = null!;
		public DbSet<UserExceptions> UserExceptions { get; set; }
		public DbSet<AirUserExceptions> AirUserExceptions { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{ }
	}
}
