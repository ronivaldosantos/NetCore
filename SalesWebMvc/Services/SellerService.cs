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

        //Acão para cadastrar novo vendedor
        public void Insert(Seller obj)
        {
            //Cadastra novo vendedor
            _context.Add(obj);

            //Confirma a atualização no BD
            _context.SaveChanges();
        }        
    }
}
