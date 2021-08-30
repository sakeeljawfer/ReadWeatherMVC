using ReadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadWeather.Token
{
    public class TokenManager 
    {
        private readonly AppDbContext context;

        public TokenManager(AppDbContext _context)
        {
            context = _context;
        }

        public bool Authenticate(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;
            var tokens = context.Tokens.ToList();
            return tokens.Any(t => token == t.TokenValue.ToString());
        }
    }
}
