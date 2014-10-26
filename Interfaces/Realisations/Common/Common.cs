using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realisations.Common
{
    /// <summary>
    /// Slot of some size that can hold one item of some type.
    /// </summary>
    public abstract class Slot : HaveSize
    {
        public Slot(SizeXY size)
            : base(size)
        {
        }

        public Slot(SizeXY size, SlottableItemType acceptedType)
            : this(size)
        {
            AcceptedItemType = acceptedType;
        }

        private SlottableItem itemInSlot;

        public SlottableItemType AcceptedItemType { get; private set; }
        public SlottableItem ItemInSlot
        {
            get { return itemInSlot; }
            set { if (value.TypeOfItem == AcceptedItemType) itemInSlot = value; }
        }
    }

    /// <summary>
    /// Grid of some size that can hold number of items of type <c>GridspaceItem</c> in ajustable order.
    /// </summary>
    public abstract class Gridspace : HaveSize
    {
        public Gridspace(SizeXY size)
            : base(size)
        {
            grid = new bool[size.X, size.Y];
            itemsInGrid = new Dictionary<GridspaceItem, InventoryPosition>();
        }

        public Gridspace(SizeXY size, GridspaceItemType acceptedType)
            : this(size)
        {
            AcceptedItemType = acceptedType;
        }

        public GridspaceItemType AcceptedItemType { get; private set; }

        private bool[,] grid;

        Dictionary<GridspaceItem, InventoryPosition> itemsInGrid;

        /// <summary>
        /// Add item to grid at position.
        /// </summary>
        /// <param name="item"><c>GridspaceItem</c> to add.</param>
        /// <param name="position">Position to add.</param>
        /// <returns>true if adding succesfully, otherwise false.</returns>
        public bool AddItemToGrid(GridspaceItem item, InventoryPosition position)
        {
            if (item == null || position == null || position.X < 0 || position.Y < 0)
                return false;

            if (Size.X < position.X + item.Size.X || Size.Y < position.Y + item.Size.Y)
                return false;

            ValidateGridValues(position, item.Size, false);

            itemsInGrid.Add(item, position);

            SetGridValues(position, item.Size, true);

            return true;
        }

        /// <summary>
        /// Remove item from grid.
        /// </summary>
        /// <param name="item"><c>GridspaceItem</c> to remove.</param>
        /// <returns>true if item is found inside grid and succesfully removed, otherwise false.</returns>
        public bool RemoveItemFromGrid(GridspaceItem item)
        {
            if (!itemsInGrid.ContainsKey(item))
                return false;


            InventoryPosition position = itemsInGrid[item];

            itemsInGrid.Remove(item);
            SetGridValues(position, item.Size, false);

            return true;
        }

        /// <summary>
        /// Get <c>GridspaceItem</c> from grid in specified position.
        /// </summary>
        /// <param name="position">position of item.</param>
        /// <returns><c>GridspaceItem</c> if found or null if nothing found in provided position.</returns>
        public GridspaceItem GetItemFromPosition(InventoryPosition position)
        {
            if (position == null)
                return null;

            foreach (var pair in itemsInGrid)
            {
                foreach (var xypair in EnumerateGridRectangle(pair.Value, pair.Key.Size))
                    if (position.X == xypair.Item1 && position.Y == xypair.Item2)
                        return pair.Key;
            }

            return null;
        }

        /// <summary>
        /// Move item inside grid from old position to new.
        /// </summary>
        /// <param name="item">Item to move.</param>
        /// <param name="newPosition">Position where to move.</param>
        /// <returns>true if move operation succesfull, otherwise false.</returns>
        public bool MoveItemInside(GridspaceItem item, InventoryPosition newPosition)
        {
            if (newPosition == null || !itemsInGrid.ContainsKey(item))
                return false;

            var oldPosition = itemsInGrid[item];

            RemoveItemFromGrid(item);

            if (!AddItemToGrid(item, newPosition))
            {
                AddItemToGrid(item, oldPosition);
                return false;
            }

            return true;
        }


        private void SetGridValues(InventoryPosition position, SizeXY size, bool value)
        {
            foreach (var pair in EnumerateGridRectangle(position, size))
                grid[pair.Item1, pair.Item2] = value;
        }

        private bool ValidateGridValues(InventoryPosition position, SizeXY size, bool valueToValidate)
        {
            foreach (var pair in EnumerateGridRectangle(position, size))
                if (grid[pair.Item1, pair.Item2] != valueToValidate)
                    return false;

            return true;
        }

        private IEnumerable<Tuple<int, int>> EnumerateGridRectangle(InventoryPosition position, SizeXY size)
        {
            for (int i = position.X; i < size.X; i++)
                for (int j = position.Y; j < size.Y; j++)
                    yield return Tuple.Create(i, j);
        }
    }

    /// <summary>
    /// Item that must be placed in slot.
    /// </summary>
    public abstract class SlottableItem : HaveSize
    {
        public SlottableItem(SizeXY size, SlottableItemType itemType)
            : base(size)
        {
            TypeOfItem = itemType;
        }

        public SlottableItemType TypeOfItem { get; private set; }
    }

    /// <summary>
    /// Item that must be placed in grid.
    /// </summary>
    public abstract class GridspaceItem: HaveSize
    {
        public GridspaceItem(SizeXY size, GridspaceItemType itemType)
            : base(size)
        {
            TypeOfItem = itemType;
        }

        public GridspaceItemType TypeOfItem { get; private set; }
    }

    /// <summary>
    /// This item have some size that unchanged with time.
    /// </summary>
    public abstract class HaveSize
    {
        public HaveSize(SizeXY size)
        {
            Size = size;
        }

        public SizeXY Size { get; private set; }
    }

    /// <summary>
    /// Supported SlottableItem types.
    /// </summary>
    public enum SlottableItemType
    {
        VehicleWeapon,
        VehicleEngine
    }

    /// <summary>
    /// Supported GridspaceItem types.
    /// </summary>
    public enum GridspaceItemType
    {
        VehicleEquipment
    }
}
