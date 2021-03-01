using BSFP.Data.UnitOfWork;
using BSFP.Models;
using BSFP.ViewModels;
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

        public PaardController(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        // GET: Paard
        public async Task<IActionResult> Index()
        {
            ListPaardViewModel viewModel = new ListPaardViewModel();
            viewModel.PaardenLijst = await _uow.PaardRepository.GetAll().ToListAsync();

            return View(viewModel);
        }

        // GET: Paard/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var paard = await _uow.PaardRepository.GetById(id);
            if (paard == null)
            {
                return NotFound();
            }

            return View(paard);
        }

        // GET: Paard/Create
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
                string blobstorageconnection = _configuration.GetValue<string>("blobstorage");

                byte[] dataFiles;
                // Retrieve storage account from connection string.
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                // Create the blob client.
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                // Retrieve a reference to a container.
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("marktplaats");

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };

                if (viewModel.Paard.Image1 != null)
                {
                    string systemFileName = viewModel.Paard.Image1.FileName;
                    viewModel.Paard.ImageName1 = systemFileName;
                    viewModel.Paard.ImagePath1 = "https://bsfp.blob.core.windows.net/marktplaats/" + systemFileName;
                    await cloudBlobContainer.SetPermissionsAsync(permissions);
                    await using (var target = new MemoryStream())
                    {
                        viewModel.Paard.Image1.CopyTo(target);
                        dataFiles = target.ToArray();
                    }
                    // This also does not make a service call; it only creates a local object.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                    await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
                }
                else
                {
                    viewModel.Paard.ImagePath1 = "../Images/logo_footer.png";
                }

                if (viewModel.Paard.Image2 != null)
                {
                    string systemFileName = viewModel.Paard.Image2.FileName;
                    viewModel.Paard.ImageName2 = systemFileName;
                    viewModel.Paard.ImagePath2 = "https://bsfp.blob.core.windows.net/marktplaats/" + systemFileName;
                    await cloudBlobContainer.SetPermissionsAsync(permissions);
                    await using (var target = new MemoryStream())
                    {
                        viewModel.Paard.Image2.CopyTo(target);
                        dataFiles = target.ToArray();
                    }
                    // This also does not make a service call; it only creates a local object.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                    await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
                }
                else
                {
                    viewModel.Paard.ImagePath2 = "../Images/logo_footer.png";
                }

                if (viewModel.Paard.Image3 != null)
                {
                    string systemFileName = viewModel.Paard.Image3.FileName;
                    viewModel.Paard.ImageName3 = systemFileName;
                    viewModel.Paard.ImagePath3 = "https://bsfp.blob.core.windows.net/marktplaats/" + systemFileName;
                    await cloudBlobContainer.SetPermissionsAsync(permissions);
                    await using (var target = new MemoryStream())
                    {
                        viewModel.Paard.Image3.CopyTo(target);
                        dataFiles = target.ToArray();
                    }
                    // This also does not make a service call; it only creates a local object.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                    await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
                }
                else
                {
                    viewModel.Paard.ImagePath4 = "../Images/logo_footer.png";
                }

                if (viewModel.Paard.Image4 != null)
                {
                    string systemFileName = viewModel.Paard.Image4.FileName;
                    viewModel.Paard.ImageName4 = systemFileName;
                    viewModel.Paard.ImagePath4 = "https://bsfp.blob.core.windows.net/testcontainer/" + systemFileName;
                    await cloudBlobContainer.SetPermissionsAsync(permissions);
                    await using (var target = new MemoryStream())
                    {
                        viewModel.Paard.Image4.CopyTo(target);
                        dataFiles = target.ToArray();
                    }
                    // This also does not make a service call; it only creates a local object.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                    await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
                }
                else
                {
                    viewModel.Paard.ImagePath4 = "../Images/logo_footer.png";
                }
                _uow.PaardRepository.Create(viewModel.Paard);
                await _uow.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Paard/Edit/5
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
                    /*
                    paard.Titel = viewModel.Nieuws.Titel;
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
                    */

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
            var nieuws = await _uow.NieuwsRepository.GetById(id);
            string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = "testcontainer";
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
            var blob = cloudBlobContainer.GetBlobReference(nieuws.ImageName);
            await blob.DeleteIfExistsAsync();
            _uow.NieuwsRepository.Delete(nieuws);
            await _uow.Save();
            return RedirectToAction(nameof(Index));
        }


    }
}


