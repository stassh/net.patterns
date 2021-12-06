using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlixOne.InventoryManagementTests.ImplemenationFactoryTests;

namespace FlixOne.InventoryManagementTests.ImplemenationFactoryTests
{
    [TestClass]
    public class InventoryCommandServicesTests
    {
        ServiceProvider Services { get; set; }

        [TestInitialize]
        public void Startup()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<InventoryCommand, InventoryCommand.QuitCommand>();
            services.AddTransient<InventoryCommand, InventoryCommand.HelpCommand>();
            services.AddTransient<InventoryCommand, InventoryCommand.AddInventoryCommand>();
            services.AddTransient<InventoryCommand, InventoryCommand.GetInventoryCommand>();
            services.AddTransient<InventoryCommand, InventoryCommand.UpdateQuantityCommand>();
            services.AddTransient<InventoryCommand, InventoryCommand.UnknownCommand>();

            Services = services.BuildServiceProvider();
        }

        public InventoryCommand GetCommand(string input)
        {
            return Services.GetServices<InventoryCommand>().First(svc =>
                svc.IsCommandFor(input));
        }

        [TestMethod]
        public void UpdateQuantityCommand_Successful()
        {
            Assert.IsInstanceOfType(GetCommand("u"),
                typeof(InventoryCommand.UpdateQuantityCommand),
                "u should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(GetCommand("updatequantity"),
                typeof(InventoryCommand.UpdateQuantityCommand),
                "updatequantity should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(GetCommand("UpdaTEQuantity"),
                typeof(InventoryCommand.UpdateQuantityCommand),
                "UpdaTEQuantity should be UpdateQuantityCommand");
        }
    }
}