using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Entities
{
    public class Invoice
    {
        public int? ID { get; set; }
        public DateTime Date { get; set; }
        public PaymentType PaymenType { get; set; }
        public String Client { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }
}
