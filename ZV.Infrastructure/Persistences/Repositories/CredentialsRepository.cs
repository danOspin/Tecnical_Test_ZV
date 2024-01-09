using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Domain.Entities;
using ZV.Infrastructure.Persistences.Contexts;
using ZV.Infrastructure.Persistences.Interfaces;

namespace ZV.Infrastructure.Persistences.Repositories
{
    public class CredentialsRepository : GenericRepository<TCredential>, ICredentialsRepository
    {
        public CredentialsRepository(DataBaseContext context) : base(context)
        {
        }

        //private readonly DataBaseContext _context;

        /*public CredentialsRepository(DataBaseContext context)
        {
            _context = context;
        }*/

        public async Task<bool> RegisterUserCredential(TCredential credentials)
        {
            //cifrar contraseña aqui? 
            await base._context.AddAsync(credentials);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> EditCredentials(TCredential credential)
        {
            throw new NotImplementedException();
        }
    }
}
