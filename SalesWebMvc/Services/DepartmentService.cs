using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }
        
        // Chamada assincrona para não bloquear app enquanto aguarda o resutado do BD ToListAsync e EntityFrameworkCore
        public async Task<List<Department>> FindAllAsync()
        {
            // Retonará os departamentos ordenados por nome.
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); 
        }
    }
}