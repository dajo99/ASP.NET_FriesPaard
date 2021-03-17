using BSFP.Areas.Identity.Data;
using BSFP.Data.UnitOfWork;
using BSFP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MemberController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<CustomUser> _userManager;

        public MemberController(IUnitOfWork uow, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _uow = uow;
            _userManager = userManager;
            _roleManager = roleManager;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ListMemberViewModel viewModel = new ListMemberViewModel();
            viewModel.Members = new List<CustomUser>();
            viewModel.Members = await _uow.UserRepository.GetAll().ToListAsync();
           
            return View(viewModel);
        }

        public async Task<IActionResult> Search(ListMemberViewModel viewModel)
        {
            IQueryable<CustomUser> queryableKlanten = _uow.UserRepository.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(viewModel.AchternaamSearch))
            {
                queryableKlanten = queryableKlanten.Where(k => k.Achternaam.StartsWith(viewModel.AchternaamSearch));
            }
            if (!string.IsNullOrEmpty(viewModel.VoornaamSearch))
            {
                queryableKlanten = queryableKlanten.Where(k => k.Voornaam.StartsWith(viewModel.VoornaamSearch));
            }

            viewModel.Members = await queryableKlanten.ToListAsync();

            return View("Index", viewModel);
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomUser member = await _userManager.FindByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            EditMemberViewModel editModel = new EditMemberViewModel();
            editModel.Member = user;
            editModel.Claims = userClaims.Select(c => c.Value).ToList();
            editModel.Roles = userRoles;

            if (editModel.Member == null)
            {
                return NotFound();
            }
            return View(editModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditMemberViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Member.Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Voornaam = model.Member.Voornaam;
                user.Achternaam = model.Member.Achternaam;
                user.UserName = model.Member.Voornaam;
                user.Email = model.Member.Email;
                user.PhoneNumber = model.Member.PhoneNumber;
                user.Lidnummer = model.Member.Lidnummer;
                var result = await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }

        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomUser member = await _userManager.FindByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            CustomUser user = await _userManager.FindByIdAsync(id);

            /*
            List<Order> orders = await _uow.OrderRepository.GetAll().ToListAsync();
            List<Tijdslot> tijdslots = await _uow.TijdslotRepository.GetAll().ToListAsync();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    if (order.CustomUserID == id)
                    {
                        _uow.OrderRepository.Delete(order);
                        await _uow.Save();
                    }

                }

            }
            if (tijdslots != null)
            {
                foreach (var tijdslot in tijdslots)
                {
                    if (tijdslot.CustomUserID == id)
                    {
                        _uow.TijdslotRepository.Delete(tijdslot);
                        await _uow.Save();
                    }

                }

            }*/

            _uow.UserRepository.Delete(user);
            await _uow.Save();

            return RedirectToAction(nameof(Index));
        }


    }
}
