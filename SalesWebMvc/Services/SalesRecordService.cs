﻿using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecords select obj;
            if (minDate.HasValue)
            {
                // Componto a consulta
                result = result.Where(x => x.Date >= minDate.Value);

            }
            if (maxDate.HasValue)
            {
                // Componto a consulta
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            //Retornando a consulta
            return await result
                    .Include(x => x.Seller)
                    .Include(x => x.Seller.Department)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync();
        }
        
        // O retorno é IGrouping pois o resultado da consulta é agrupado por vendas.
        public async Task<List< IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecords select obj;
            if (minDate.HasValue)
            {
                // Componto a consulta
                result = result.Where(x => x.Date >= minDate.Value);

            }
            if (maxDate.HasValue)
            {
                // Componto a consulta
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            //Retornando a consulta
            return await result
                    .Include(x => x.Seller)
                    .Include(x => x.Seller.Department)
                    .OrderByDescending(x => x.Date)
                    .GroupBy(x => x.Seller.Department)
                    .ToListAsync();
        }
    }
}
