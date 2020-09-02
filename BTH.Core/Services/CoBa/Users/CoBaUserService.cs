using BTH.Core.Context;
using BTH.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BTH.Core.Services.CoBa.Users
{
    public class CoBaUserService : ICoBaUserService
    {
        private readonly DataContext _ctx;

        public CoBaUserService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<CoBaUser[]> GetAll()
        {
            return await _ctx.CoBaUsers.ToArrayAsync();
        }
    }
}
