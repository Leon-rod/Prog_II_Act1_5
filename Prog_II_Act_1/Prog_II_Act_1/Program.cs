using Prog_II_Act_1.Entities;
using Prog_II_Act_1.Services;

class Program
{
    public static void Main()
    {
        ArticleManager articleManager = new ArticleManager();
        Article article1 = new Article()
        {
            Name = "Water",
            Unit_Price = (decimal)125.22,
        };
        for (int i = 0; i < 4; i++)
        {
            if (articleManager.AddArticle(article1))
            {
                Console.WriteLine("Se ha añadido un producto con exito en la base de datos");
            }
        }


        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        Console.WriteLine("Lista de productos en la base de datos: ");
        List<Article> listArticle = articleManager.GetArticles();
        foreach (Article art in listArticle)
        {
            Console.WriteLine(art.Id + " - " + art.Name + ": $" + art.Unit_Price);
        };


        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        if (articleManager.DeleteArticle(listArticle[0]))
        {
            Console.WriteLine($"Se ha eliminado el articulo con el id: {listArticle[0].Id}");
        }
        else
        {
            Console.WriteLine("Ha ocurrido un error al intentar eliminar un articulo, intentelo de nuevo mas tarde");
        }
        listArticle = articleManager.GetArticles();

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        Console.WriteLine("Lista de productos en la base de datos: ");
        foreach (Article art in listArticle)
        {
            Console.WriteLine(art.Id + " - " + art.Name + ": $" + art.Unit_Price);
        };

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        Article modifiedArticle = new Article()
        {
            Id = listArticle[0].Id,
            Name = "Macaronni",
            Unit_Price = (decimal)1000.09
        };
        if (articleManager.UpdateArticle(modifiedArticle))
        {
            Console.WriteLine($"Se ha modificado el articulo con el ID: {modifiedArticle.Id}");
        }
        else
        {
            Console.WriteLine("Ha ocurrido un error al intentar modificar un articulo, intentelo de nuevo mas tarde");
        }

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        listArticle = articleManager.GetArticles();
        Console.WriteLine("Lista de productos en la base de datos: ");
        foreach (Article art in listArticle)
        {
            Console.WriteLine(art.Id + " - " + art.Name + ": $" + art.Unit_Price);
        };

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        PaymentTypeManager paymentTypeManager = new PaymentTypeManager();
        for (int i = 0; i < 2; i++)
        {
            PaymentType paymentType = new PaymentType()
            {
                ID = i + 1,
                Name = (i == 0) ? "Tarjeta de credito" : "Tarjeta de Debito"
            };
            if (paymentTypeManager.AddPaymentType(paymentType))
            {
                Console.WriteLine("Se ha añadido un nuevo tipo de pago");
            }
            else
            {
                Console.WriteLine("Error al intentar añadir un nuevo tipo de pago...");
            }
        }
        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        List<PaymentType> listPaymentTypes = paymentTypeManager.GetAllPaymentTypes();
        Console.WriteLine("Métodos de pagos disponibles:");
        foreach (PaymentType pt in listPaymentTypes)
        {
            Console.WriteLine(pt.ID + " - " + pt.Name);
        }

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        InvoiceManager invoiceManager = new InvoiceManager();
        List<Invoice> invoices = new List<Invoice>();
        for (int i = 0; i < 2; i++)
        {
            Invoice invoice = new Invoice()
            {
                Date = DateTime.Today,
                PaymenType = listPaymentTypes[0],
                Client = "LAURELIO"
            };
            for (int j = 0; j < 2; j++)
            {
                InvoiceDetail invo_det = new InvoiceDetail()
                {
                    DetailArticle = (j == 0) ? listArticle[i] : listArticle[i + 1],
                    Amount = (j == 0) ? 100 * (i + 1) : 100 * (i + 2),
                };
                invoice.InvoiceDetails.Add(invo_det);
            }
            invoices.Add(invoice);
        }
        foreach (Invoice inv in invoices)
        {
            if (invoiceManager.SaveInvoice(inv))
            {
                Console.WriteLine($"Se ha agregado con exito una factura en la base de datos.");
            }
            else
            {
                Console.WriteLine("Ha ocurrido un error al insertar una factura en la base de datos...");
            }
        }

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        List<InvoiceDetail> invoiceDetailsUpdated = new List<InvoiceDetail>();
        Invoice invoiceUpdated = new Invoice()
        {
            ID = 1,
            Date = DateTime.Today,
            PaymenType = listPaymentTypes[1],
            Client = "AR-2-B-2",
        };
        for (int i = 0; i < 2; i++)
        {
            InvoiceDetail invo_det = new InvoiceDetail()
            {
                InvoiceID = 1,
                InvoiceDetailID = i + 1,
                DetailArticle = (i == 0) ? listArticle[i] : listArticle[i + 1],
                Amount = 1000,
            };
            invoiceDetailsUpdated.Add(invo_det);
        }
        invoiceUpdated.InvoiceDetails = invoiceDetailsUpdated;

        if (invoiceManager.SaveInvoice(invoiceUpdated))
        {
            Console.WriteLine("Se ha actualizado una factura con exito");
        }
        else
        {
            Console.WriteLine("Ha ocurrido un error al intentar actualizar una factura...");
        }
        List<Invoice> allInvoices = invoiceManager.GetInvoices();

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        Console.WriteLine("Listado de facturas actuales: ");
        foreach (Invoice invoice in allInvoices)
        {
            Console.WriteLine($"ID: {invoice.ID} - {invoice.Client}");
        }

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        Invoice invoiceById = invoiceManager.GetInvoiceById((int)allInvoices[0].ID);
        Console.WriteLine($"Se ha recuperado la factura por ID: {invoiceById.ID} - {invoiceById.Client} - {invoiceById.Date}");

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        if (invoiceManager.DeleteInvoiceById((int)allInvoices[0].ID))
        {
            Console.WriteLine($"Se ha eliminado la factura con el id: {allInvoices[0].ID}");
        }
        else
        {
            Console.WriteLine("Ha ocurrido un error al intentar eliminar una factura y sus detalles");
        }

        Console.WriteLine("----------------------------");
        Console.WriteLine("");

        InvoiceDetailManager invoiceDetailManager = new InvoiceDetailManager();

        Console.WriteLine("Listado de todos los detalles de factura actuales: ");
        List<InvoiceDetail> inv_detList = invoiceDetailManager.GetAllDetails();
        foreach (InvoiceDetail inv_det in inv_detList)
        {
            Console.WriteLine($"ID FACTURA: {inv_det.InvoiceID} - ID DETALLE : {inv_det.InvoiceDetailID}");
        }


        Console.WriteLine("----------------------------");
        Console.WriteLine("");


        Console.WriteLine($"Detalles correspondiente al id de factura : {inv_detList[0].InvoiceID}");
        List<InvoiceDetail> idInv_detList = invoiceDetailManager.GetDetailsById(inv_detList[0].InvoiceID);
        foreach(InvoiceDetail inv_det_id in idInv_detList)
        {
            Console.WriteLine($"{inv_det_id.InvoiceDetailID} - Id Artículo: {inv_det_id.DetailArticle.Id} - Cantidad: {inv_det_id.Amount}");
        }
    }
}