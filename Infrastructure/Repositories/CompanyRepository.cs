using Core.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : GenericRepository<Company>
    {
        private readonly Context _context;

        public CompanyRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
