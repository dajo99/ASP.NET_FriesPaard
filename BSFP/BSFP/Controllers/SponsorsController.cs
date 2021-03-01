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
                Nieuws nieuws = await BlobCRUD.CreateBlobFile("sponsors", viewModel.Sponsor.File, _configuration);

                viewModel.Sponsor.ImageName = nieuws.ImageName;
                viewModel.Sponsor.ImagePath = nieuws.ImagePath;
                _uow.SponsorRepository.Create(viewModel.Sponsor);
                await _uow.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }



    }
}
