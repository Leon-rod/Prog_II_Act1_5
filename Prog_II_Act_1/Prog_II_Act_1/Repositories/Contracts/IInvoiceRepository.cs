using Prog_II_Act_1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Repositories.Contracts
{
    internal interface IInvoiceRepository
    {
        List<Invoice> GetAll();
        Invoice GetById(int id);
        bool Delete(int id);
        bool Save(Invoice invoice);
    }
}
