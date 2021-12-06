using System;
using System.Collections.Generic;
using FlixOne.InventoryManagement.Command;
using FlixOne.InventoryManagement.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlixOne.InventoryManagementTests
{
    [TestClass]
    public class InventoryCommandFunctionTests
    {
        ServiceProvider Services { get; set; }

        [TestInitialize]
        public void Startup()
        {
            var expectedInterface = new Helpers.TestUserInterface(
                new List<Tuple<string, string>>(),
                new List<string>(),
                new List<string>()
            );
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IInventoryContext, InventoryContext>();
            services.AddTransient<Func<string,
                InventoryCommand>>(InventoryCommand.GetInventoryCommand);
            Services = services.BuildServiceProvider();
        }

        [TestMethod]
        public void QuitCommand_Successful()
        {
            Assert.IsInstanceOfType(Services.GetService<Func<string,
                    InventoryCommand>>().Invoke("q"),
                typeof(QuitCommand),
                "q should be QuitCommand");
            Assert.IsInstanceOfType(Services.GetService<Func<string,
                    InventoryCommand>>().Invoke("quit"),
                typeof(QuitCommand),
                "quit should be QuitCommand");
        }
    }
}