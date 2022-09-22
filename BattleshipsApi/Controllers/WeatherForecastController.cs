using BattleshipsApi.Handlers;
using BattleshipsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipsApi.Controllers;

 [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayersHandler _playersHandler;

        public PlayerController(PlayersHandler playersHandler)
        {
            _playersHandler = playersHandler;
        }

        // GET: PlayerController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers()
        {
            return await _playersHandler.GetAllPlayers();
        }













        //// GET: PlayerController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PlayerController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PlayerController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PlayerController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PlayerController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PlayerController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PlayerController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }