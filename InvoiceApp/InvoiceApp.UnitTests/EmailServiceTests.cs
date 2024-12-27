using InvoiceApp.WebApi.Models;
using InvoiceApp.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InvoiceApp.UnitTests
{
    

    public class EmailServiceTests
    {
        [Fact]
        public void GenerateInvoiceEmail_Should_Return_Email()
        {
            var invoiceId = Guid.NewGuid();

            var dueDate = DateTime.Now.AddDays(30);


            var invoice = new Invoice
            {
                Id = invoiceId,
                ContactId = Guid.NewGuid(),
                InvoiceNumber = "INV_001",
                InvoiceDate = DateTime.Now,
                Amount = 100,
                DueDate = dueDate,
                Status = WebApi.InvoiceStatus.Overdue,
                
                InvoiceItems = new List<InvoiceItem>
                { new InvoiceItem
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test",
                        Description = "Test Description",
                        Amount = 50,
                        UnitPrice = 50,
                        Quantity = 1,
                        InvoiceId = invoiceId
                    }
                },

                Contact = new Contact
                {
                    Id= Guid.NewGuid(),
                    Address = "Test address",
                    City = "Krk",
                    Company = "Test company",
                    Country = "Test country",
                    Email = "bartosz.rzemek@gmail.com",
                    FirstName = "Bartosz",
                    LastName = "Rzemek",
                    Notes = "None",
                    Phone = "922377282",
                    State = "State",
                    Zip = "31111"
                }
            };

            var (to, subject, body) = new EmailService().GenerateInvoiceEmail(invoice);

            Assert.Equal(invoice.Contact.Email, to);
            Assert.Equal($"Invoice INV_001 for Bartosz Rzemek", subject);
            Assert.Equal($"""
            Dear Bartosz Rzemek,
            
            Thank you for your business. Here are your invoice details:
            Invoice Number: INV_001
            Invoice Date: {DateTime.Now.ToShortDateString()}
            Invoice Amount: 100,00 zł
            Invoice Items:
            Test Description - 1 x 50,00 zł
            
            Please pay by {dueDate.ToShortDateString()}. Thank you!
            
            Regards,
            InvoiceApp
            """, body);
        }
    }
}
