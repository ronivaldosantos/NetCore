using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext contex)
        {
            _context = contex;
        }

        //Operação para retornar todos os vendedores cadastrados

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        
        }



    }
}
