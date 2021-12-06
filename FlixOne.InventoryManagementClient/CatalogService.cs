using FlixOne.InventoryManagement.Command;
using FlixOne.InventoryManagement.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagementClient
{
    interface ICatalogService
    {
        void Run();
    }

    public class CatalogService : ICatalogService
    {
        private readonly IUserInterface userInterface;
        private readonly IInventoryCommandFactory commandFactory;
        public CatalogService(IUserInterface userInterface, IInventoryCommandFactory commandFactory)
        {
            this.userInterface = userInterface;
            this.commandFactory = commandFactory;
        }

        public void Run()
        {
            Greeting();

            var response = this.commandFactory.GetCommand("?").RunCommand();

            while (!response.shouldQuit)
            {
                // look at this mistake with the ToLower()
                var input = userInterface.ReadValue("> ").ToLower();
                var command = this.commandFactory.GetCommand(input);

                response = command.RunCommand();

                if (!response.wasSuccessful)
                {
                    userInterface.WriteMessage("Enter ? to view options.");
                }
            }
        }

        private void Greeting()
        {
            // get version and display
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            userInterface.WriteMessage("*********************************************************************************************");
            userInterface.WriteMessage("*                                                                                           *");
            userInterface.WriteMessage("*               Welcome to FlixOne Inventory Management System                              *");
            userInterface.WriteMessage($"*                                                                                v{version}   *");
            userInterface.WriteMessage("*********************************************************************************************");
            userInterface.WriteMessage("");
        }

        //public InventoryCommand GetCommand(string input)
        //{
        //    switch (input)
        //    {
        //        case "q":
        //        case "quit":
        //            return new QuitCommand(userInterface);
        //        case "a":
        //        case "addinventory":
        //            return new AddInventoryCommand(userInterface);
        //        case "g":
        //        case "getinventory":
        //            return new GetInventoryCommand(userInterface);
        //        case "u":
        //        case "updatequantity":
        //            return new UpdateQuantityCommand(userInterface);
        //        case "?":
        //            return new HelpCommand(userInterface);
        //        default:
        //            return new UnknownCommand(userInterface);
        //    }
        //}
    }
}
