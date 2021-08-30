using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadWeather.Models
{
    //DI Database services using EF core 
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Weather> Weather { get; set; }
        public DbSet<Token.Token> Tokens { get; set; }
    }
}
