using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //colocar a dependência 
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            //Retorna para view uma lista dos vendedores.
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //Anotacion indicando o tipo do verbo
        [ValidateAntiForgeryToken] // Evitar ataques de hackers que aproveitam requisão aberta.
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller); //Chama o método para gravar no BD
            return RedirectToAction(nameof(Index)); //Redireciona para recarregar a página.
        }
    }
}