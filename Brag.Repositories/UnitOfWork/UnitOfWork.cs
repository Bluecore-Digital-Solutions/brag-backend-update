
using Brag.Repositories.DataContext;
using ManagerPINs.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brag.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BragDbContext _context;
        public UnitOfWork(BragDbContext context)
        {
            _context = context;

        }
         
        

        public async Task<bool> CompleteAsync() => await _context.SaveChangesAsync() > 0;
         
    }
}
