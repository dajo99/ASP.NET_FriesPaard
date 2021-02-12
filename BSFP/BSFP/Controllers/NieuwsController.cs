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
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

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
                string blobstorageconnection =  _configuration.GetValue<string>("blobstorage");

                byte[] dataFiles;
                // Retrieve storage account from connection string.
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                // Create the blob client.
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                // Retrieve a reference to a container.
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("testcontainer");

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };

                if (viewModel.Nieuws.File != null)
                {
                    string systemFileName = viewModel.Nieuws.File.FileName;
                    viewModel.Nieuws.ImageName = systemFileName;
                    viewModel.Nieuws.ImagePath = "https://bsfp.blob.core.windows.net/testcontainer/" + systemFileName;
                    await cloudBlobContainer.SetPermissionsAsync(permissions);
                    await using (var target = new MemoryStream())
                    {
                        viewModel.Nieuws.File.CopyTo(target);
                        dataFiles = target.ToArray();
                    }
                    // This also does not make a service call; it only creates a local object.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                    await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
                }
                else
                {
                    viewModel.Nieuws.ImagePath = "../Images/logo_footer.png";
                }
                

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

                    string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
                    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                    CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                    string strContainerName = "testcontainer";
                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
                    var blob = cloudBlobContainer.GetBlobReference(nieuws.ImageName);
                    await blob.DeleteIfExistsAsync();

                    byte[] dataFiles;
                    BlobContainerPermissions permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };
                    string systemFileName = viewModel.Nieuws.File.FileName;
                    nieuws.ImageName = systemFileName;
                    nieuws.ImagePath = "https://bsfp.blob.core.windows.net/testcontainer/" + systemFileName;
                    await cloudBlobContainer.SetPermissionsAsync(permissions);
                    await using (var target = new MemoryStream())
                    {
                        viewModel.Nieuws.File.CopyTo(target);
                        dataFiles = target.ToArray();
                    }
                    // This also does not make a service call; it only creates a local object.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                    await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);


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
            if (nieuws.File != null)
            {
                string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                string strContainerName = "testcontainer";
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
                var blob = cloudBlobContainer.GetBlobReference(nieuws.ImageName);
                await blob.DeleteIfExistsAsync();
            }
            
            _uow.NieuwsRepository.Delete(nieuws);
            await _uow.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
