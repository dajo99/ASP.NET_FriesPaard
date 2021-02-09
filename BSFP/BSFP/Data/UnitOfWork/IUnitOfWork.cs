using BSFP.Data.Repository;
using BSFP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Agenda> AgendaRepository { get; }
        IGenericRepository<Nieuws> NieuwsRepository { get; }
        Task Save();
    }
}
