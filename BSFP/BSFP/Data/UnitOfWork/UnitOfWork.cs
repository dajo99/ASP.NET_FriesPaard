using BSFP.Areas.Identity.Data;
using BSFP.Data.Repository;
using BSFP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BSFPContext _context;
        private IGenericRepository<Agenda> agendaRepository;
        private IGenericRepository<Nieuws> nieuwsRepository;
        private IGenericRepository<CustomUser> userRepository;
        private IGenericRepository<Sponsor> sponsorRepository;
        private IGenericRepository<Paard> paardRepository;


        public UnitOfWork(BSFPContext context)
        {
            _context = context;
        }

        public IGenericRepository<Agenda> AgendaRepository
        {
            get
            {
                if (this.agendaRepository == null)
                {
                    this.agendaRepository = new GenericRepository<Agenda>(_context);
                }
                return agendaRepository;
            }
        }

        public IGenericRepository<Nieuws> NieuwsRepository
        {
            get
            {
                if (this.nieuwsRepository == null)
                {
                    this.nieuwsRepository = new GenericRepository<Nieuws>(_context);
                }
                return nieuwsRepository;
            }
        }

        public IGenericRepository<CustomUser> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<CustomUser>(_context);
                }
                return userRepository;
            }
        }

        public IGenericRepository<Sponsor> SponsorRepository
        {
            get
            {
                if (this.sponsorRepository == null)
                {
                    this.sponsorRepository = new GenericRepository<Sponsor>(_context);
                }
                return sponsorRepository;
            }
        }

        public IGenericRepository<Paard> PaardRepository
        {
            get
            {
                if (this.paardRepository == null)
                {
                    this.paardRepository = new GenericRepository<Paard>(_context);
                }
                return paardRepository;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
