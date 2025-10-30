using API.DAL.Models;
using API.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL.Repositories.ProductRepo
{
    public interface IProductRepo :IGenericRepo<Product>
    {

        Task<Product> FindProductByName(string name);
    }
}
