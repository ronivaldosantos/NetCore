using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        //Dados necesários para cadastro de um vendedor        
        public Seller Seller { get; set; }

        public ICollection<Department> Departments { get; set; } = new List<Department>();

    }
}
