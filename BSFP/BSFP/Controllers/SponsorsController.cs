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
using Microsoft.Extensions.Configuration;
using BSFP.ViewModels;
using BSFP.Blobstorage;

namespace BSFP.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        public SponsorsController(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        // GET: Sponsor
        public async Task<IActionResult> Index()
        {
            ListSponsorViewModel viewModel = new ListSponsorViewModel();
            viewModel.SponsorLijst = await _uow.SponsorRepository.GetAll().ToListAsync();

            return View(viewModel);
        }


        // GET: Sponsor/Create
        public IActionResult Create()
        {
            CreateSponsorViewModel viewModel = new CreateSponsorViewModel();
            viewModel.Sponsor = new Sponsor();
            return View(viewModel);
        }

        // POST: Sponsor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSponsorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                BlobCRUD blob = await BlobCRUD.CreateBlobFile("sponsors", viewModel.Sponsor.File, _configuration);

                viewModel.Sponsor.ImageName = blob.ImageName;
                viewModel.Sponsor.ImagePath = blob.ImagePath;
                _uow.SponsorRepository.Create(viewModel.Sponsor);
                await _uow.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Sponsor/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var sponsor = await _uow.SponsorRepository.GetById(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // GET: Sponsor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sponsor = await _uow.SponsorRepository.GetById(id);
            EditSponsorViewModel viewModel = new EditSponsorViewModel();
            viewModel.Sponsor = sponsor;
            if (viewModel.Sponsor == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Sponsor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditSponsorViewModel viewModel)
        {
            if (id != viewModel.Sponsor.SponsorID)
            {
                return NotFound();
            }

            Sponsor sponsor = await _uow.SponsorRepository.GetById(id);
            if (ModelState.IsValid)
            {

                try
                {
                    sponsor.Titel = viewModel.Sponsor.Titel;
                    sponsor.WebsiteLink = viewModel.Sponsor.WebsiteLink;
                    sponsor.Omschrijving = viewModel.Sponsor.Omschrijving;

                    if (viewModel.Sponsor.File != null)
                    {
                        BlobCRUD blob = await BlobCRUD.EditBlobFile(sponsor.ImageName, viewModel.Sponsor.File, "sponsors", _configuration);
                        sponsor.ImageName = blob.ImageName;
                        sponsor.ImagePath = blob.ImagePath;
                    }

                    _uow.SponsorRepository.Update(sponsor);
                    await _uow.Save();


                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }

                return RedirectToAction(nameof(Index));



            }
            return View(sponsor);

        }

        // POST: Sponsor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sponsor = await _uow.SponsorRepository.GetById(id);
            if (sponsor.ImageName != null)
            {
                await BlobCRUD.DeleteBlobFile("sponsors", sponsor.ImageName, _configuration);
            }

            _uow.SponsorRepository.Delete(sponsor);
            await _uow.Save();
            return RedirectToAction(nameof(Index));
        }



    }
}
