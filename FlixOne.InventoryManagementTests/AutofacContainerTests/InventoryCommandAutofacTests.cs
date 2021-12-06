using Autofac;
using FlixOne.InventoryManagementTests.ImplemenationFactoryTests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlixOne.InventoryManagementTests.AutofacContainerTests
{
    [TestClass]
    public class InventoryCommandAutofacTests
    {
        private IContainer Container { get; set; }

        [TestInitialize]
        public void Startup()
        {
            IServiceCollection services = new ServiceCollection();
            var builder = new ContainerBuilder();

            // команды
            builder.RegisterType<InventoryCommand.QuitCommand>().Named<InventoryCommand>("q");
            builder.RegisterType<InventoryCommand.QuitCommand>().Named<InventoryCommand>("quit");
            builder.RegisterType<InventoryCommand.UpdateQuantityCommand>().Named<InventoryCommand>("u");
            builder.RegisterType<InventoryCommand.UpdateQuantityCommand>().Named<InventoryCommand>
                ("updatequantity");
            builder.RegisterType<InventoryCommand.HelpCommand>().Named<InventoryCommand>("?");
            builder.RegisterType<InventoryCommand.AddInventoryCommand>().Named<InventoryCommand>("a");
            builder.RegisterType<InventoryCommand.AddInventoryCommand>().Named<InventoryCommand>
                ("addinventory");
            builder.RegisterType<InventoryCommand.GetInventoryCommand>().Named<InventoryCommand>("g");
            builder.RegisterType<InventoryCommand.GetInventoryCommand>().Named<InventoryCommand>
                ("getinventory");
            builder.RegisterType<InventoryCommand.UnknownCommand>().As<InventoryCommand>();

            Container = builder.Build();
        }

        public InventoryCommand GetCommand(string input)
        {
            return Container.ResolveOptionalNamed<InventoryCommand>(input.ToLower()) ??
                Container.Resolve<InventoryCommand>();
        }

        [TestMethod]
        public void QuitCommand_Successful()
        {
            Assert.IsInstanceOfType(GetCommand("q"), typeof(InventoryCommand.QuitCommand),
                "q should be QuitCommand");
            Assert.IsInstanceOfType(GetCommand("quit"), typeof(InventoryCommand.QuitCommand),
                "quit should be QuitCommand");
        }

        [TestMethod]
        public void HelpCommand_Successful()
        {
            Assert.IsInstanceOfType(GetCommand("?"), typeof(InventoryCommand.HelpCommand), "? should be HelpCommand");
        }

        [TestMethod]
        public void UnknownCommand_Successful()
        {
            Assert.IsInstanceOfType(GetCommand("add"), typeof(InventoryCommand.UnknownCommand), "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(GetCommand("addinventry"), typeof(InventoryCommand.UnknownCommand), "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(GetCommand("h"), typeof(InventoryCommand.UnknownCommand), "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(GetCommand("help"), typeof(InventoryCommand.UnknownCommand), "unmatched command should be UnknownCommand");
        }

        [TestMethod]
        public void AddInventoryCommand_Successful()
        {
            Assert.IsInstanceOfType(GetCommand("a"), typeof(InventoryCommand.AddInventoryCommand), "a should be AddInventoryCommand");
            Assert.IsInstanceOfType(GetCommand("addinventory"), typeof(InventoryCommand.AddInventoryCommand), "addinventory should be AddInventoryCommand");
        }

        [TestMethod]
        public void GetInventoryCommand_Successful()
        {
            Assert.IsInstanceOfType(GetCommand("g"), typeof(InventoryCommand.GetInventoryCommand), "g should be GetInventoryCommand");
            Assert.IsInstanceOfType(GetCommand("getinventory"), typeof(InventoryCommand.GetInventoryCommand), "getinventory should be GetInventoryCommand");
        }

        [TestMethod]
        public void UpdateQuantityCommand_Successful()
        {
            Assert.IsInstanceOfType(GetCommand("u"), typeof(InventoryCommand.UpdateQuantityCommand), "u should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(GetCommand("updatequantity"), typeof(InventoryCommand.UpdateQuantityCommand), "updatequantity should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(GetCommand("UpdaTEQuantity"), typeof(InventoryCommand.UpdateQuantityCommand), "UpdaTEQuantity should be UpdateQuantityCommand");
        }
    }
}