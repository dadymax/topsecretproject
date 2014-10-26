using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realisations.Vehicles.Equipment
{
    public sealed class VehicleEquipment : Common.GridspaceItem
    {
        public VehicleEquipment(Common.SizeXY size)
            : base(size, Common.GridspaceItemType.VehicleEquipment)
        {

        }
        //some effect
        //

        //

    }

    public sealed class VehicleEquipmentGridspace : Common.Gridspace
    {
        public VehicleEquipmentGridspace(Common.SizeXY size)
            : base(size, Common.GridspaceItemType.VehicleEquipment)
        {

        }
    }



}
