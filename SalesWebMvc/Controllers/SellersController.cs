﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //colocar dependências 
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        //Injeção de dependência no construtor
        public SellersController(SellerService sellerService, DepartmentService departmentService)
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
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) // Id passado é inválido
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
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

        //Criar método para exibir detalhes do vendedor
        public IActionResult Details(int? id) //Recebe um Id opcional
        {
            if (id == null) // Não foi passado Id
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) // Id passado é inválido
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        //Criar método para editar os dados
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) // Id passado é inválido
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            //Carregar lista de departamentos
            List<Department> departments = _departmentService.FindAll();
            //Carrega vendedores para edição
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        //Delete Post - Realiza a Atualização no BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            //Se o Id passado pela URL for diferente do Id do vendedor retorna exception
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // Pega Id interno da transação
            };

            return View(viewModel);
        }

    }
}