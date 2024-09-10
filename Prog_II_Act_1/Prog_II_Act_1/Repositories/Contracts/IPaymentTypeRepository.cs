using Prog_II_Act_1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Repositories.Contracts
{
    internal interface IPaymentTypeRepository
    {
        List<PaymentType> GetAll();
        bool Add(PaymentType paymentType);
    }
}
