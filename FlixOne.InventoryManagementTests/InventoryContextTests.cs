using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlixOne.InventoryManagementTests
{
    [TestClass]
    public class InventoryContextTests
    {
        ServiceProvider Services { get; set; }

        [TestInitialize]
        public void Startup()
        {
            var services = new ServiceCollection();
            var context = new InventoryContext();
            services.AddSingleton<IInventoryContext, IInventoryContext>(p => context);
            services.AddSingleton<IInventoryWriteContext, IInventoryContext>(p => context);
            services.AddSingleton<IInventoryReadContext, IInventoryContext>(p => context);

            Services = services.BuildServiceProvider();
        }

        [TestMethod]
        public void MaintainBooks_Successful()
        {

            var tasks = new List<Task>();

            // добавление 30 книг
            foreach (var id in Enumerable.Range(1, 30))
            {
                tasks.Add(AddBook($"Book_{id}"));
            }

            Task.WaitAll(tasks.ToArray());
            tasks.Clear();

            // обновим количество книг, добавив 1, 2, 3, 4, 5...
            foreach (var quantity in Enumerable.Range(1, 10))
            {
                foreach (var id in Enumerable.Range(1, 30))
                {
                    tasks.Add(UpdateQuantity($"Book_{id}", quantity));
                }
            }

            Task.WaitAll(tasks.ToArray());
            tasks.Clear();

            // обновим количество книг, отняв 1, 2, 3, 4, 5...
            foreach (var quantity in Enumerable.Range(1, 10))
            {
                foreach (var id in Enumerable.Range(1, 30))
                {
                    tasks.Add(UpdateQuantity($"Book_{id}", -quantity));
                }
            }
            
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();

            // все количества должны быть 0
            foreach (var book in Services.GetService<IInventoryContext>().GetBooks())
            {
                Assert.AreEqual(0, book.Quantity);
            }
        }

        public Task AddBook(string book)
        {
            return Task.Run(() =>
            { 
                Assert.IsTrue(Services.GetService<IInventoryContext>().AddBook(book));
            });
        }

        public Task UpdateQuantity(string book, int quantity)
        {
            return Task.Run(() =>
            {
                Assert.IsTrue(Services.GetService<IInventoryContext>().UpdateQuantity(book, quantity));
            });
        }
    }
}
