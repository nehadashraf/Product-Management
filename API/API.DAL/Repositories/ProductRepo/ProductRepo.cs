using API.DAL.Context;
using API.DAL.Models;
using API.DAL.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL.Repositories.ProductRepo
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly AppDbContext _dbContext;
        public ProductRepo(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> FindProductByName(string name)
        {
           return await _dbContext.Products
                .FirstOrDefaultAsync(p=>p.Name.ToLower()==name.ToLower());
        }
    }
}
