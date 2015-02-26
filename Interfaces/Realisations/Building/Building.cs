
using System;

namespace Realisations.Buildings
{
	/// <summary>
	/// Abstract building have some height and a floor on every height from zero to its height.
	/// On zero level there must be some entrances for grount/airbone vehicles.
	/// Also there must be personal transportation system endpoint on some floor
	/// </summary>
	public abstract class Building : Common.HaveSizeAndTag
	{
		public Building(uint height)
		{
			Height = height;
			Floors = new BuildingFloor[Height];
			for (int i = 0; i< Height; i++)
				Floors[i] = new BuildingFloor(this.Size);
		}
		
		public uint Height {get; private set;}
		
		protected BuildingFloor[] Floors {get; set;}
		
		public BuildingFloor GetFloor(uint level)
		{
			if (level > Height)
				return null;
			return Floors[level];
		}
		
	}

}
