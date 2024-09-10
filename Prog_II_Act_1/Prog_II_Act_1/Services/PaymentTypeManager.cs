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
    public class PaymentTypeManager
    {
        private IPaymentTypeRepository _paymentTypeManager;
        public PaymentTypeManager()
        {
            this._paymentTypeManager = new PaymentTypeRepositoryADO();
        }
        public List<PaymentType> GetAllPaymentTypes()
        {
            return _paymentTypeManager.GetAll();
        }
        public bool AddPaymentType(PaymentType paymentType)
        {
            return _paymentTypeManager.Add(paymentType);
        }
    }
}
