using FlixOne.InventoryManagement.Repository;
using FlixOne.InventoryManagement.UserInterface;

namespace FlixOne.InventoryManagement.Command
{
    public interface IInventoryCommandFactory
    {
        InventoryCommand GetCommand(string input);
    }
    public class InventoryCommandFactory : IInventoryCommandFactory
    {
        private readonly IUserInterface userInterface;
        private readonly IInventoryContext context;

        public InventoryCommandFactory(IUserInterface userInterface, IInventoryContext context)
        {
            this.userInterface = userInterface;
            this.context = context;
        }

        public InventoryCommand GetCommand(string input)
        {
            switch (input.ToLower())
            {
                case "q":
                case "quit":
                    return new QuitCommand(userInterface);
                case "a":
                case "addinventory":
                    return new AddInventoryCommand(userInterface, context);
                case "g":
                case "getinventory":
                    return new GetInventoryCommand(userInterface, context);
                case "u":
                case "updatequantity":
                    return new UpdateQuantityCommand(userInterface, context);
                case "?":
                    return new HelpCommand(userInterface);
                default:
                    return new UnknownCommand(userInterface);
            }
        }
    }
}