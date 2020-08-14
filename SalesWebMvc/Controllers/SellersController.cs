using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //colocar dependências 
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        //Injeção de dependência no construtor
        public SellersController(SellerService sellerService,DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            //Retorna para view uma lista dos vendedores.
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        [HttpPost] //Anotacion indicando o tipo do verbo
        [ValidateAntiForgeryToken] // Evitar ataques de hackers que aproveitam requisão aberta.
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller); //Chama o método para gravar no BD
            return RedirectToAction(nameof(Index)); //Redireciona para recarregar a página.
        }
        
        // Delete GET - Retorna os dados para confirmar se será deletado
        public IActionResult Delete(int? id) // ?-> Significa opcional
        {
            if (id == null) // Não foi passado Id
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) // Id passado é inválido
            {
                return NotFound();
            }

            return View(obj);
        }

        //Delete Post - Realiza a deleção no banco
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}