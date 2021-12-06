using FlixOne.InventoryManagement.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement.Command
{
    public class HelpCommand : NonTerminatingCommand
    {
        public HelpCommand(IUserInterface userInterface) : base(userInterface)
        {
        }

        protected override bool InternalCommand()
        {
            userInterface.WriteMessage("USAGE:");
            userInterface.WriteMessage("\taddinventory (a)");
            userInterface.WriteMessage("\tgetinventory (g)");
            userInterface.WriteMessage("\tupdatequantity (u)");
            userInterface.WriteMessage("\tquit (q)");
            userInterface.WriteMessage("\t?");
            userInterface.WriteMessage("Examples:");
            userInterface.WriteMessage("\tNew Inventory");
            userInterface.WriteMessage("\t> addinventory");
            userInterface.WriteMessage("\tEnter name:The Meaning of Life");
            userInterface.WriteMessage("");
            userInterface.WriteMessage("\tGet Inventory");
            userInterface.WriteMessage("\t> getinventory");
            userInterface.WriteMessage("\tThe Meaning of Life        Quantity:10");
            userInterface.WriteMessage("\tThe Life of a Ninja        Quantity:2");
            userInterface.WriteMessage("");
            userInterface.WriteMessage("\tUpdate Quantity (Increase)");
            userInterface.WriteMessage("\t> updatequantity");
            userInterface.WriteMessage("\tEnter name:The Meaning of Life");
            userInterface.WriteMessage("\t11");
            userInterface.WriteMessage("\t11 added to quantity");
            userInterface.WriteMessage("");
            userInterface.WriteMessage("\tUpdate Quantity (Decrease)");
            userInterface.WriteMessage("\t> updatequantity");
            userInterface.WriteMessage("\tEnter name:The Life of a Ninja");
            userInterface.WriteMessage("\t-3");
            userInterface.WriteMessage("\t3 removed from quantity");
            userInterface.WriteMessage("");

            return true;
        }
    }
}
