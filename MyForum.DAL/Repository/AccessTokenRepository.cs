using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Repository
{
    public class AccessTokenRepository : BaseRepository<AccessToken>, IAccessTokenRepository
    {

        public AccessTokenRepository(ModelDbContext dbctx) : base(dbctx)
        {
        }

        public async Task<bool> DeleteByUserId(int id)
        {
            var a = await dbctx.AccessTokens.FirstOrDefaultAsync(a => a.Id == id);
            if (a == null)
            {
                return false;
            }
            else
            {
                dbctx.AccessTokens.Remove(a);
                return await dbctx.SaveChangesAsync() > 0;
            }
        }

        public async Task<AccessToken> FindByToken(string token)
        {
            return await dbctx.AccessTokens.FirstOrDefaultAsync(a => a.Token == token);
        }

        public async Task<AccessToken> FindByUserId(int userId)
        {
            return await dbctx.AccessTokens.FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<bool> Save(AccessToken entity)
        {
            await dbctx.AccessTokens.AddAsync(entity);
            return await dbctx.SaveChangesAsync() > 0;
        }
    }
}
