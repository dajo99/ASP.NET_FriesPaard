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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using BSFP.Blobstorage;

namespace BSFP.Controllers
{
    public class NieuwsController : Controller
    {
        private readonly IUnitOfWork _uow;

        private readonly IConfiguration _configuration;
        public NieuwsController(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        // GET: Nieuws
        public async Task<IActionResult> Index()
        {
            ListNieuwsViewModel viewModel = new ListNieuwsViewModel();
            viewModel.NieuwsLijst = await _uow.NieuwsRepository.GetAll().OrderByDescending(x=>x.Datum).ToListAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> Search(ListNieuwsViewModel viewModel)
        {
            IQueryable<Nieuws> queryableNieuws = _uow.NieuwsRepository.GetAll().OrderByDescending(x => x.Datum).AsQueryable();

            if (!string.IsNullOrEmpty(viewModel.Search))
            {
                queryableNieuws = queryableNieuws.Where(k => k.Intro.Contains(viewModel.Search));
            }

            viewModel.NieuwsLijst = await queryableNieuws.ToListAsync();

            return View("Index", viewModel);
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
                BlobCRUD blob = await BlobCRUD.CreateBlobFile("nieuws",viewModel.Nieuws.File, _configuration);

                viewModel.Nieuws.ImageName = blob.ImageName;
                viewModel.Nieuws.ImagePath = blob.ImagePath;
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

            Nieuws nieuws = await _uow.NieuwsRepository.GetById(id);
            if (ModelState.IsValid)
            {
                
                try
                {
                    nieuws.Titel = viewModel.Nieuws.Titel;
                    nieuws.Intro = viewModel.Nieuws.Intro;
                    nieuws.Omschrijving = viewModel.Nieuws.Omschrijving;

                    if (viewModel.Nieuws.File != null)
                    {
                        BlobCRUD blob = await BlobCRUD.EditBlobFile(nieuws.ImageName, viewModel.Nieuws.File, "nieuws",_configuration);
                        nieuws.ImageName = blob.ImageName;
                        nieuws.ImagePath = blob.ImagePath;
                    }
                    
                    _uow.NieuwsRepository.Update(nieuws);
                    await _uow.Save();


                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }

                return RedirectToAction(nameof(Index));

                

            }
            return View(nieuws);

        }

       



        // GET: Nieuws/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var nieuws = await _uow.NieuwsRepository.GetById(id);
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
            var nieuws = await _uow.NieuwsRepository.GetById(id);
            if (nieuws.ImageName != null)
            {
                await BlobCRUD.DeleteBlobFile("nieuws", nieuws.ImageName, _configuration);
            }
            
            _uow.NieuwsRepository.Delete(nieuws);
            await _uow.Save();
            return RedirectToAction(nameof(Index));
        }


    }
}
