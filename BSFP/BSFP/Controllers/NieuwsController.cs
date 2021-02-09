using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSFP.Data;
using BSFP.Models;
using BSFP.Data.UnitOfWork;
using BSFP.ViewModels;

namespace BSFP.Controllers
{
    public class NieuwsController : Controller
    {
        private readonly IUnitOfWork _uow;


        public NieuwsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Nieuws
        public async Task<IActionResult> Index()
        {
            ListNieuwsViewModel viewModel = new ListNieuwsViewModel();
            viewModel.NieuwsLijst = await _uow.NieuwsRepository.GetAll().ToListAsync();

            return View(viewModel);
        }

        // GET: Nieuws/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var nieuws = await _uow.NieuwsRepository.GetById(id);
            if (nieuws == null)
            {
                return NotFound();
            }

            return View(nieuws);
        }

        // GET: Nieuws/Create
        public IActionResult Create()
        {
            CreateNieuwsViewModel viewModel = new CreateNieuwsViewModel();
            viewModel.Nieuws = new Nieuws();
            return View(viewModel);
        }

        // POST: Nieuws/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNieuwsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Nieuws.Datum = DateTime.Now;
                _uow.NieuwsRepository.Create(viewModel.Nieuws);
                await _uow.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Nieuws/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var nieuws = await _uow.NieuwsRepository.GetById(id);
            EditNieuwsViewModel viewModel = new EditNieuwsViewModel();
            viewModel.Nieuws = nieuws;
            if (viewModel.Nieuws == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Nieuws/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditNieuwsViewModel viewModel)
        {
            if (id != viewModel.Nieuws.NieuwsID)
            {
                return NotFound();
            }
            else
            {
                var nieuws = await _uow.NieuwsRepository.GetById(id);
                viewModel.Nieuws.Datum = nieuws.Datum;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.NieuwsRepository.Update(viewModel.Nieuws);
                    await _uow.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel.Nieuws);
        }

        // GET: Nieuws/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var nieuws = await _uow.AgendaRepository.GetById(id);
            if (nieuws == null)
            {
                return NotFound();
            }

            return View(nieuws);
        }

        // POST: Nieuws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nieuws = await _uow.AgendaRepository.GetById(id);
            _uow.AgendaRepository.Delete(nieuws);
            await _uow.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
