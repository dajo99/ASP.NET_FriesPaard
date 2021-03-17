using BSFP.Areas.Identity.Data;
using BSFP.Blobstorage;
using BSFP.Data.UnitOfWork;
using BSFP.Models;
using BSFP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Controllers
{
    public class PaardController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly UserManager<CustomUser> _userManager;

        public PaardController(IUnitOfWork uow, IConfiguration configuration, UserManager<CustomUser> userManager)
        {
            _uow = uow;
            _configuration = configuration;
            _userManager = userManager;
        }

        // GET: Paard
        public async Task<IActionResult> Index()
        {
            ListPaardViewModel viewModel = new ListPaardViewModel();
            viewModel.PaardenLijst = await _uow.PaardRepository.GetAll().ToListAsync();

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> EigenAdvertenties()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            ListPaardViewModel viewModel = new ListPaardViewModel();
            viewModel.PaardenLijst = await _uow.PaardRepository.GetAll().Where(x => x.CustomUserID == userid).ToListAsync();

            return View(viewModel);
        }

        // GET: Paard/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var paard = await _uow.PaardRepository.GetById(id);
            paard.CustomUser = await _userManager.FindByIdAsync(paard.CustomUserID);
            if (paard == null)
            {
                return NotFound();
            }

            return View(paard);
        }

        // GET: Paard/Create
        [Authorize]
        public IActionResult Create()
        {
            CreatePaardViewModel viewModel = new CreatePaardViewModel();
            viewModel.Paard = new Paard();
            return View(viewModel);
        }

        // POST: Paard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePaardViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userid = _userManager.GetUserId(HttpContext.User);
                viewModel.Paard.CustomUserID = userid;

                BlobCRUD blob1 = await BlobCRUD.CreateBlobFile("marktplaats", viewModel.Paard.Image1, _configuration);

                viewModel.Paard.ImageName1 = blob1.ImageName;
                viewModel.Paard.ImagePath1 = blob1.ImagePath;

                BlobCRUD blob2 = await BlobCRUD.CreateBlobFile("marktplaats", viewModel.Paard.Image2, _configuration);

                viewModel.Paard.ImageName2 = blob2.ImageName;
                viewModel.Paard.ImagePath2 = blob2.ImagePath;

                BlobCRUD blob3 = await BlobCRUD.CreateBlobFile("marktplaats", viewModel.Paard.Image3, _configuration);

                viewModel.Paard.ImageName3 = blob3.ImageName;
                viewModel.Paard.ImagePath3 = blob3.ImagePath;

                BlobCRUD blob4 = await BlobCRUD.CreateBlobFile("marktplaats", viewModel.Paard.Image4, _configuration);

                viewModel.Paard.ImageName4 = blob4.ImageName;
                viewModel.Paard.ImagePath4 = blob4.ImagePath;

                _uow.PaardRepository.Create(viewModel.Paard);
                await _uow.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Paard/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var paard = await _uow.PaardRepository.GetById(id);
            EditPaardViewModel viewModel = new EditPaardViewModel();
            viewModel.Paard = paard;
            if (viewModel.Paard == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Paard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditPaardViewModel viewModel)
        {
            if (id != viewModel.Paard.PaardID)
            {
                return NotFound();
            }

            Paard paard = await _uow.PaardRepository.GetById(id);
            if (ModelState.IsValid)
            {

                try
                {

                    paard.Paardnaam = viewModel.Paard.Paardnaam;
                    paard.Geslacht = viewModel.Paard.Geslacht;
                    paard.Leeftijd = viewModel.Paard.Leeftijd;
                    paard.Levensnummer = viewModel.Paard.Levensnummer;
                    paard.Gebruiksdiscipline = viewModel.Paard.Gebruiksdiscipline;
                    paard.Stokmaat = viewModel.Paard.Stokmaat;
                    paard.Prijs = viewModel.Paard.Prijs;
                    paard.LocatiePaard = viewModel.Paard.LocatiePaard;
                    paard.Informatie = viewModel.Paard.Informatie;

                    if (viewModel.Paard.Image1 != null)
                    {
                        BlobCRUD blob1 = await BlobCRUD.EditBlobFile(paard.ImageName1, viewModel.Paard.Image1, "marktplaats", _configuration);
                        paard.ImageName1 = blob1.ImageName;
                        paard.ImageName2 = blob1.ImagePath;
                    }

                    if (viewModel.Paard.Image2 != null)
                    {
                        BlobCRUD blob2 = await BlobCRUD.EditBlobFile(paard.ImageName2, viewModel.Paard.Image2, "marktplaats", _configuration);
                        paard.ImageName2 = blob2.ImageName;
                        paard.ImageName2 = blob2.ImagePath;
                    }

                    if (viewModel.Paard.Image3 != null)
                    {
                        BlobCRUD blob3 = await BlobCRUD.EditBlobFile(paard.ImageName3, viewModel.Paard.Image3, "marktplaats", _configuration);
                        paard.ImageName3 = blob3.ImageName;
                        paard.ImageName3 = blob3.ImagePath;
                    }

                    if (viewModel.Paard.Image4 != null)
                    {
                        BlobCRUD blob4 = await BlobCRUD.EditBlobFile(paard.ImageName4, viewModel.Paard.Image4, "marktplaats", _configuration);
                        paard.ImageName4 = blob4.ImageName;
                        paard.ImageName4 = blob4.ImagePath;
                    }


                    _uow.PaardRepository.Update(paard);
                    await _uow.Save();

                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }

                return RedirectToAction(nameof(Index));



            }
            return View(paard);

        }

        // GET: Paard/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var paard = await _uow.PaardRepository.GetById(id);
            if (paard == null)
            {
                return NotFound();
            }

            return View(paard);
        }

        // POST: Paard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paard = await _uow.PaardRepository.GetById(id);
            if (paard.ImageName1 != null)
            {
                await BlobCRUD.DeleteBlobFile("marktplaats", paard.ImageName1, _configuration);
            }

            if (paard.ImageName2 != null)
            {
                await BlobCRUD.DeleteBlobFile("marktplaats", paard.ImageName2, _configuration);
            }
            if (paard.ImageName3 != null)
            {
                await BlobCRUD.DeleteBlobFile("marktplaats", paard.ImageName3, _configuration);
            }
            if (paard.ImageName4 != null)
            {
                await BlobCRUD.DeleteBlobFile("marktplaats", paard.ImageName4, _configuration);
            }
            _uow.PaardRepository.Delete(paard);
            await _uow.Save();
            return RedirectToAction(nameof(Index));
        }


    }
}


