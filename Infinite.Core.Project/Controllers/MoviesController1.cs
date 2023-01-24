using Infinite.Core.Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace Infinite.Core.Project.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IConfiguration _configuration; public MoviesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<MovieViewModel> movies = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Movies/GetAllMovies");
                if (result.IsSuccessStatusCode)
                {
                    movies = await result.Content.ReadAsAsync<List<MovieViewModel>>();
                }
            }
            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            MovieViewModel movie = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Movies/GetMovieById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    movie = await result.Content.ReadAsAsync<MovieViewModel>();
                }
            }
            return View(movie);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    var result = await client.PostAsJsonAsync($"Movies/CreateMovie", movie);
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(movie);
        }
    }
}