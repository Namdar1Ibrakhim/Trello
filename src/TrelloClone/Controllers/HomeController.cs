using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TrelloClone.Services;
using TrelloClone.ViewModels;
using System.Linq;

namespace TrelloClone.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly BoardService _boardService;

        public HomeController(BoardService boardService)
        {
            _boardService = boardService;
        }

        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            ;

            if (!String.IsNullOrEmpty(searchString))
            {
                var modella = _boardService.ListSearchBoard(searchString);
                return View(modella);
            }
            var model = _boardService.ListBoard();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewBoard viewModel)
        {
            _boardService.AddBoard(viewModel);

            return RedirectToAction(nameof(Index));
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Delete(BoardView boardView)
        {
            try
            {
                _boardService.DeleteBoard(boardView.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}