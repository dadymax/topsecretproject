using System;

namespace Realisations.Buildings
{
	/// <summary>
	/// One square of building. Atomic part.
	/// </summary>
	public class BuildingCell1x1 : Common.GridspaceItem
	{
		public BuildingCell1x1()
			:base(Common.GridspaceItemType.BuildingCell)
		{
			EntranceType = EntranceTypes.None;
		}
		
		public EntranceTypes EntranceType {get;set;}
	}
	
	/// <summary>
	/// One floor of building. Consists of BuildingCell1x1
	/// </summary>
	public class BuildingFloor: Common.Gridspace
	{
		public BuildingFloor(Common.SizeXY size)
			:base (size, Common.GridspaceItemType.BuildingCell)
		{
			for (int i = 0; i < size.X; i++)
				for (int j = 0; j < size.Y; j++)
			{
					itemsInGrid.Add(new BuildingCell1x1(), new Common.GridPosition() {X =i, Y = j});
					grid[i,j] = true;
			}
		}
	}
	
	public enum EntranceTypes
	{
		VehicleGround,
		VehicleAirbone,
		PersonalTransportationSystem,
		None
	}
}
