using System;
using System.Collections.Generic;
using FlixOne.InventoryManagement.Command;
using FlixOne.InventoryManagement.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlixOne.InventoryManagementTests
{
    [TestClass]
    public class InventoryCommandFactoryTests
    {
        public InventoryCommandFactory Factory { get; set; }
        ServiceProvider Services { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            var expectedInterface = new Helpers.TestUserInterface(
                new List<Tuple<string, string>>(),
                new List<string>(),
                new List<string>()
            );
            
            var context = new InventoryContext();
            Factory = new InventoryCommandFactory(expectedInterface, context);
        }

        [TestMethod]
        public void QuitCommand_Successful()
        {
            Assert.IsInstanceOfType(Factory.GetCommand("q"), typeof(QuitCommand),
                "q should be QuitCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("quit"), typeof(QuitCommand),
                "quit should be QuitCommand");
        }

        [TestMethod]
        public void HelpCommand_Successful()
        {
            Assert.IsInstanceOfType(Factory.GetCommand("?"), typeof(HelpCommand),
                "h should be HelpCommand");
        }


        [TestMethod]
        public void UnknownCommand_Successful()
        {
            Assert.IsInstanceOfType(Factory.GetCommand("add"),
                typeof(UnknownCommand),
                "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("addinventry"),
                typeof(UnknownCommand),
                "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("h"), typeof(UnknownCommand),
                "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("help"), typeof(UnknownCommand),
                "unmatched command should be UnknownCommand");
        }

        [TestMethod]
        public void UpdateQuantityCommand_Successful()
        {
            Assert.IsInstanceOfType(Factory.GetCommand("u"),
                typeof(UpdateQuantityCommand),
                "u should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("updatequantity"),
                typeof(UpdateQuantityCommand),
                "updatequantity should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("UpdaTEQuantity"),
                typeof(UpdateQuantityCommand),
                "UpdaTEQuantity should be UpdateQuantityCommand");
        }
    }
}
