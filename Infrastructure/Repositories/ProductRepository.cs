using Core.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        private readonly Context _context;

        public ProductRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
