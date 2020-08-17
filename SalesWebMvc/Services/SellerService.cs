using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Responsável pela função include(Join)
using SalesWebMvc.Services.Exceptions;

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
        // Chamada assincrona para não bloquear app enquanto aguarda o resutado do BD ToListAsync e EntityFrameworkCore
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //Acão para cadastrar novo vendedor
        public async Task InsertAsync(Seller obj)
        {
            //Provisoriamente para não ocorrer erro de ForeignKey está cadastrando o primeiro departamento
            //para todos os vendedores novos cadastrados.
            // obj.Department = _context.Department.First();

            //Cadastra novo vendedor
            _context.Add(obj);

            //Confirma a atualização no BD

            await _context.SaveChangesAsync();
        }

        // Retornar um vendedor específico utilizando Linq
        // Com utilização do "Include" traz o departamento do vendedor.
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // Remover vendedor selecionado
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales.");
            }
        }

        // Método Update
        public async Task UpdateAsync(Seller obj)
        {
            // Lança exceptio se não existir no banco de dados um Id igual a Id do obj recebido
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id Not Found");
            }

            try
            {
                //Realiza atualização 
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) // Caso ocorra concorrencia durante atualização no DB
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
