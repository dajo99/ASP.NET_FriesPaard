using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
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
    public class AgendaController : Controller
    {
        private readonly IUnitOfWork _uow;

        public AgendaController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Agenda
        public async Task<IActionResult> Index()
        {
            ListAgendaViewModel viewModel = new ListAgendaViewModel();
            viewModel.AgendaLijst = await _uow.AgendaRepository.GetAll().ToListAsync();
            
            return View(viewModel);
        }
        public async Task<IActionResult> Search(ListAgendaViewModel viewModel)
        {
            IQueryable<Agenda> queryableAgenda = _uow.AgendaRepository.GetAll().OrderBy(x => x.Datum).AsQueryable();

            if (!string.IsNullOrEmpty(viewModel.SearchName))
            {
                if (CultureInfo.CurrentCulture.Name == "nl")
                {
                    queryableAgenda = queryableAgenda.Where(k => k.Titel_nl.Contains(viewModel.SearchName));
                }
                if (CultureInfo.CurrentCulture.Name == "fr")
                {
                    queryableAgenda = queryableAgenda.Where(k => k.Titel_fr.Contains(viewModel.SearchName));
                }

            }

            if (!string.IsNullOrEmpty(viewModel.SearchPlace))
            {
                queryableAgenda = queryableAgenda.Where(k => k.Locatie.Contains(viewModel.SearchPlace));
            }

            if (!string.IsNullOrEmpty(viewModel.SearchDate.ToString()))
            {
                queryableAgenda = queryableAgenda.Where(k => k.Datum == viewModel.SearchDate);
            }



            viewModel.AgendaLijst = await queryableAgenda.ToListAsync();

            return View("Index", viewModel);
        }

        // GET: Agenda/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var agenda = await _uow.AgendaRepository.GetById(id);
            if (agenda == null)
            {
                return NotFound();
            }

            return View(agenda);
        }

        // GET: Agenda/Create
        public IActionResult Create()
        {
            CreateAgendaViewModel viewModel = new CreateAgendaViewModel();
            viewModel.Agenda = new Agenda();
            return View(viewModel);
        }

        // POST: Agenda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAgendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _uow.AgendaRepository.Create(viewModel.Agenda);
                await _uow.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Agenda/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var agenda = await _uow.AgendaRepository.GetById(id);
            if (agenda == null)
            {
                return NotFound();
            }
            return View(agenda);
        }

        // POST: Agenda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgendaID,Titel_nl,Titel_fr,Omschrijving_nl,Omschrijving_fr,Datum,Starttijd,Eindtijd,Locatie")] Agenda agenda)
        {
            if (id != agenda.AgendaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.AgendaRepository.Update(agenda);
                    await _uow.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(agenda);
        }

        // GET: Agenda/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var agenda = await _uow.AgendaRepository.GetById(id);
            if (agenda == null)
            {
                return NotFound();
            }

            return View(agenda);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agenda = await _uow.AgendaRepository.GetById(id);
            _uow.AgendaRepository.Delete(agenda);
            await _uow.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
