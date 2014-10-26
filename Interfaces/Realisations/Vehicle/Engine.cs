using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realisations.Vehicles.Engines
{
    public sealed class VehicleEngineItem : Common.SlottableItem
    {
        public VehicleEngineItem()
            : base(Common.SlottableItemType.VehicleEngine)
        {
            
        }

        public bool CanFly { get; private set; }

        public uint Power { get; private set; }
        public uint FuelConsumption { get; private set; }
    }

    public sealed class VehicleEngineSlot : Common.Slot
    {
        public VehicleEngineSlot()
            : base(Common.SlottableItemType.VehicleEngine)
        {

        }
    }

}
