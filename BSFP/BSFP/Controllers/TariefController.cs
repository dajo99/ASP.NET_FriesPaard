using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSFP.Data;
using BSFP.Models;
using BSFP.ViewModels;
using BSFP.Data.UnitOfWork;

namespace BSFP.Controllers
{
    public class TariefController : Controller
    {
        private readonly IUnitOfWork _uow;

        public TariefController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Tariefs
        public async Task<IActionResult> Index()
        {
            ListTariefViewModel viewModel = new ListTariefViewModel();
            viewModel.TariefLijst = await _uow.TariefRepository.GetAll().Where(x => x.IsTeruggave == false).ToListAsync();
            viewModel.TeruggaveLijst = await _uow.TariefRepository.GetAll().Where(x => x.IsTeruggave == true).ToListAsync();

            return View(viewModel);
        }

        // GET: Tariefs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tarief = await _uow.TariefRepository.GetById(id);
            if (tarief == null)
            {
                return NotFound();
            }

            return View(tarief);
        }

        // GET: Tariefs/Create
        public IActionResult Create()
        {
            CreateTariefViewModel viewModel = new CreateTariefViewModel();
            viewModel.Tarief = new Tarief();
            return View(viewModel);
        }

        // POST: Tariefs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTariefViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _uow.TariefRepository.Create(viewModel.Tarief);
                await _uow.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Tariefs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tarief = await _uow.TariefRepository.GetById(id);
            EditTariefViewModel viewModel = new EditTariefViewModel();
            viewModel.Tarief = tarief;
            if (viewModel.Tarief == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Tariefs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTariefViewModel viewModel)
        {
            if (id != viewModel.Tarief.TariefID)
            {
                return NotFound();
            }

            Tarief tarief = await _uow.TariefRepository.GetById(id);
            if (ModelState.IsValid)
            {

                try
                {
                    tarief.Omschrijving_nl = viewModel.Tarief.Omschrijving_nl;
                    tarief.Omschrijving_fr = viewModel.Tarief.Omschrijving_fr;
                    tarief.Prijs = viewModel.Tarief.Prijs;
                    tarief.IsTeruggave = viewModel.Tarief.IsTeruggave;

                    _uow.TariefRepository.Update(tarief);
                    await _uow.Save();

                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }

                return RedirectToAction(nameof(Index));

            }
            return View(tarief);
        }

        // GET: Tariefs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tarief = await _uow.TariefRepository.GetById(id);
            if (tarief == null)
            {
                return NotFound();
            }

            return View(tarief);
        }

        // POST: Tariefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)

        {
            var tarief = await _uow.TariefRepository.GetById(id);
            _uow.TariefRepository.Delete(tarief);
            await _uow.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
