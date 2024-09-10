using Prog_II_Act_1.Entities;
using Prog_II_Act_1.Repositories.Contracts;
using Prog_II_Act_1.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Services
{
    public class InvoiceDetailManager
    {
        private IInvoiceDetailRepository _invoiceDetailRepository;
        public InvoiceDetailManager()
        {
            this._invoiceDetailRepository = new InvoiceDetailRepositoryADO();
        }
        public List<InvoiceDetail> GetAllDetails()
        {
            return _invoiceDetailRepository.GetAll();
        }
        public List<InvoiceDetail> GetDetailsById(int id)
        {
            return _invoiceDetailRepository.GetById(id);
        }
    }
}
