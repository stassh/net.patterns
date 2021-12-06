using FlixOne.InventoryManagement.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Repository;

namespace FlixOne.InventoryManagement.Command
{
    public class GetInventoryCommand : NonTerminatingCommand
    {
        private readonly IInventoryReadContext context;
        public GetInventoryCommand(IUserInterface userInterface, IInventoryReadContext context) : base(userInterface)
        {
            this.context = context;
        }

        protected override bool InternalCommand()
        {
            foreach (var book in context.GetBooks())
            {
                userInterface.WriteMessage($"{book.Name,-30}\tQuantity:{book.Quantity}");
            }
            return true;
        }
    }
}
