using Dreamer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamer.Services
{
    public interface IUserService
    {
        public Task<Users> LoginAsync(Users user);
        public Task<Users> RegisterUserAsync(Users user);
        public Task<Users> GetUserByAccessTokenAsync(string accessToken);
        public Task<Users> RefreshTokenAsync(RefreshRequests refreshRequest);
    }
}
