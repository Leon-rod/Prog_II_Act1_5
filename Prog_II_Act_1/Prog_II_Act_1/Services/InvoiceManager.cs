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
    public class InvoiceManager
    {
        private IInvoiceRepository _invoiceRepository;
        public InvoiceManager()
        {
            this._invoiceRepository = new InvoiceRepositoryADO();
        }
        public bool SaveInvoice (Invoice invoice)
        {
            return this._invoiceRepository.Save(invoice);
        }
        public List<Invoice> GetInvoices()
        {
            return this._invoiceRepository.GetAll();
        }
        public Invoice GetInvoiceById (int id)
        {
            return this._invoiceRepository.GetById(id);
        }
        public bool DeleteInvoiceById(int id)
        {
            return this._invoiceRepository.Delete(id);
        }
    }
}
