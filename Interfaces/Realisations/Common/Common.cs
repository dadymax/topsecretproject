using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realisations.Common
{
    /// <summary>
    /// Slot of some size that can hold one item of some type.
    /// </summary>
    public abstract class Slot : HaveSizeAndTag
    {
        public Slot(SlottableItemType acceptedType)
        {
            AcceptedItemType = acceptedType;
        }

        private SlottableItem itemInSlot;

        public SlottableItemType AcceptedItemType { get; protected set; }
        public SlottableItem ItemInSlot
        {
            get { return itemInSlot; }
            set { if (value.TypeOfItem == AcceptedItemType) itemInSlot = value; }
        }
    }

    /// <summary>
    /// Grid of some size that can hold number of items of type <c>GridspaceItem</c> in ajustable order.
    /// </summary>
    public class Gridspace : HaveSizeAndTag
    {
        public Gridspace(SizeXY size, GridspaceItemType acceptedType)
        {
            Size = size;
            grid = new bool[size.X, size.Y];
            itemsInGrid = new Dictionary<GridspaceItem, GridPosition>();
            AcceptedItemType = acceptedType;
        }

        public GridspaceItemType AcceptedItemType { get; protected set; }

        /// <summary>
        /// Two dimension table of booleans that represent occupied and free cells
        /// </summary>
        protected bool[,] grid;

        protected Dictionary<GridspaceItem, GridPosition> itemsInGrid;
        
        public Dictionary<GridspaceItem, GridPosition> GetItemsInGrid
        {
        	get {return new Dictionary<GridspaceItem, GridPosition>(itemsInGrid);}
        }

        /// <summary>
        /// Add item to grid at position.
        /// </summary>
        /// <param name="item"><c>GridspaceItem</c> to add.</param>
        /// <param name="position">Position to add.</param>
        /// <returns>true if adding succesfully, otherwise false.</returns>
        public bool AddItemToGrid(GridspaceItem item, GridPosition position)
        {
            if (item == null || position == null || position.X < 0 || position.Y < 0)
                return false;
            
            if (item.TypeOfItem != this.AcceptedItemType)
            	return false;

            if (Size.X < position.X + item.Size.X || Size.Y < position.Y + item.Size.Y)
                return false;

            if (!ValidateGridValues(position, item.Size, false))
            	return false;

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

            GridPosition position = itemsInGrid[item];

            itemsInGrid.Remove(item);
            SetGridValues(position, item.Size, false);

            return true;
        }

        /// <summary>
        /// Get <c>GridspaceItem</c> from grid in specified position.
        /// </summary>
        /// <param name="position">position of item.</param>
        /// <returns><c>GridspaceItem</c> if found or null if nothing found in provided position.</returns>
        public GridspaceItem GetItemFromPosition(GridPosition position)
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
        public bool MoveItemInside(GridspaceItem item, GridPosition newPosition)
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

        /// <summary>
        /// Sets selected cells of grid to value. 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        private void SetGridValues(GridPosition position, SizeXY size, bool value)
        {
            foreach (var pair in EnumerateGridRectangle(position, size))
                grid[pair.Item1, pair.Item2] = value;
        }

        /// <summary>
        /// Checks that on this position item with given size present or abscent.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="valueToValidate"></param>
        /// <returns></returns>
        private bool ValidateGridValues(GridPosition position, SizeXY size, bool valueToValidate)
        {
            foreach (var pair in EnumerateGridRectangle(position, size))
                if (grid[pair.Item1, pair.Item2] != valueToValidate)
                    return false;

            return true;
        }

        private IEnumerable<Tuple<int, int>> EnumerateGridRectangle(GridPosition position, SizeXY size)
        {
            for (int i = position.X; i < size.X; i++)
                for (int j = position.Y; j < size.Y; j++)
                    yield return Tuple.Create(i, j);
        }
    }

    /// <summary>
    /// Item that must be placed in slot.
    /// </summary>
    public abstract class SlottableItem : HaveMassAndSize
    {
        public SlottableItem(SlottableItemType itemType)
        {
            TypeOfItem = itemType;
        }

        public SlottableItemType TypeOfItem { get; protected set; }
    }

    /// <summary>
    /// Item that must be placed in grid.
    /// </summary>
    public abstract class GridspaceItem: HaveMassAndSize
    {
        public GridspaceItem(GridspaceItemType itemType)
        {
            TypeOfItem = itemType;
        }

        public GridspaceItemType TypeOfItem { get; protected set; }
    }

    public abstract class HaveMassAndSize : HaveSizeAndTag, IHaveName, IHaveMass
    {
        public string Name { get; set; }

        public uint Mass { get; set; }
    }

    /// <summary>
    /// This item have some size that unchanged with time.
    /// </summary>
    public abstract class HaveSizeAndTag: IHaveTag
    {
        public SizeXY Size { get; protected set; }

        public string Tag { get; set; }
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
        VehicleEquipment,
        BuildingCell
    }

    public abstract class HaveHeapthPoints
    {
        public uint HeapthPoints { get; protected set; }
    }

    public interface IHaveName
    {
        string Name { get; set; }
    }

    public interface IHaveMass
    {
        uint Mass { get; set; }
    }

    public interface IHaveTag
    {
        string Tag { get; set; }
    }
    
    public interface IHaveID
    {
    	ulong Id {get;set;}
    }


}
