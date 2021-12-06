using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Command;
using FlixOne.InventoryManagement.Models;

namespace FlixOne.InventoryManagementTests
{
    /// <summary>
    /// GetInventory Command Tests
    /// </summary>
    /// <remarks>
    /// A get inventory command ("g", "getinventory") is available
    ///     returns all the books and their quantities in the database
    /// </remarks>
    [TestClass]
    public class GetInventoryCommandTests
    {
        [TestMethod]
        public void GetInventoryCommand_Successful()
        {
            var expectedInterface = new Helpers.TestUserInterface(
                new List<Tuple<string, string>>(),
                new List<string>
                {
                    "Gremlins                      \tQuantity:7",
                    "Willowsong                    \tQuantity:3",
                },
                new List<string>()
            );

            var context = new TestInventoryContext(new Dictionary<string, Book>
            {
                { "Gremlins", new Book { Id = 1, Name = "Gremlins", Quantity = 7 } },
                { "Willowsong", new Book { Id = 2, Name = "Willowsong", Quantity = 3 }
                },
            });

            // создание экземпляра команды
            var command = new GetInventoryCommand(expectedInterface, context);
            var result = command.RunCommand();
            Assert.IsFalse(result.shouldQuit, "GetInventory is not a terminating command.");

            Assert.AreEqual(0, context.GetAddedBooks().Length, "GetInventory should Not have added any books.");
            Assert.AreEqual(0, context.GetUpdatedBooks().Length, "GetInventory should not have updated any books.");
        }
    }
}
