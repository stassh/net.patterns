using FlixOne.InventoryManagement.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Repository;

namespace FlixOne.InventoryManagement.Command
{
    public class UpdateQuantityCommand : NonTerminatingCommand, IParameterisedCommand
    {
        private readonly IInventoryWriteContext context;

        public UpdateQuantityCommand(IUserInterface userInterface, IInventoryWriteContext context) : base(userInterface)
        {
            this.context = context;
        }

        internal string InventoryName { get; private set; }

        private int _quantity;
        internal int Quantity { get => _quantity; private set => _quantity = value; }

        /// <summary>
        ///     UpdateQuantity requires name and an integer value
        /// </summary>
        /// <returns></returns>
        public bool GetParameters()
        {
            if (string.IsNullOrWhiteSpace(InventoryName))
                InventoryName = GetParameter("name");

            if (Quantity == 0)
                int.TryParse(GetParameter("quantity"), out _quantity);

            return !string.IsNullOrWhiteSpace(InventoryName) && Quantity != 0;
        }

        protected override bool InternalCommand()
        {
            return context.UpdateQuantity(InventoryName, Quantity);
        }
    }
}
