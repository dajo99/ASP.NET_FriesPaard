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


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
