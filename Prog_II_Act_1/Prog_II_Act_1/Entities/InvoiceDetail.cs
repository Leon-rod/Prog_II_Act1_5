using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Entities
{
    public class InvoiceDetail
    {
        public int InvoiceDetailID { get; set; }
        public int InvoiceID { get; set; }
        public Article DetailArticle { get; set; }
        public int Amount { get; set; }
        
    }
}
