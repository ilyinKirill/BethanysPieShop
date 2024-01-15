﻿using BethanysPieShop.Models.Repositories;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPieRepository _pieRepository;

		public HomeController(IPieRepository pieRepository)
		{
			_pieRepository = pieRepository;
		}

		public IActionResult Index()
		{
			var piesOfTheWeek = _pieRepository.PiesOfTheWeek;
			var indexViewModel = new IndexViewModel(piesOfTheWeek);
			return View(indexViewModel);
		}
	}
}
