using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realisations.Vehicles.Engines
{
    public sealed class VehicleEngine : Common.SlottableItem
    {
        public VehicleEngine(Common.SizeXY size)
            : base(size, Common.SlottableItemType.VehicleEngine)
        {

        }
    }

    public sealed class VehicleEngineSlot : Common.Slot
    {
        public VehicleEngineSlot(Common.SizeXY size)
            : base(size, Common.SlottableItemType.VehicleEngine)
        {

        }
    }
}
