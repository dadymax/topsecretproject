using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realisations.Vehicles.Weapons
{
    /// <summary>
    /// Abstract vehicle weapon is slottable and has some properties.
    /// </summary>
    public sealed class VehicleWeapon : Common.SlottableItem
    {
        public VehicleWeapon(Common.SizeXY size)
            : base(size, Common.SlottableItemType.VehicleWeapon)
        {

        }

        public string Name { get; private set; }

        public int Damage { get; private set; }

        public int Accuracy { get; private set; }

        public int MaxDistance { get; private set; }

        public VehicleWeaponAmmoTypes AmmoType { get; private set; }
        public uint AmmoCapacity { get; private set; }
        public uint AmmoLeft { get; private set; }
        public uint AmmoForOneShot { get; private set; }

        public delegate void PlayFireDelegate(uint rounds);
        public PlayFireDelegate Playfire { get; set; }

        public void Fire()
        {
            if (AmmoLeft == 0)
                return;

            uint ammoForShot = AmmoForOneShot;

            if (AmmoForOneShot >= AmmoLeft)
            {
                ammoForShot = AmmoLeft;
                AmmoLeft = 0;
            }
            else
                AmmoLeft -= AmmoForOneShot;

            if (Playfire != null)
                Playfire(ammoForShot);
        }

        public bool Reload(ref uint TotalAmmoOfThisType)
        {
            if (TotalAmmoOfThisType == 0)
                return false;
            uint ammoNeeded = AmmoCapacity - AmmoLeft;
            if (TotalAmmoOfThisType >= ammoNeeded)
                TotalAmmoOfThisType -= ammoNeeded;
            else
            {
                ammoNeeded = TotalAmmoOfThisType;
                TotalAmmoOfThisType = 0;
            }
            AmmoLeft += ammoNeeded;
            return true;
        }

    }

    /// <summary>
    /// Slot that can hold vehicle weapon.
    /// </summary>
    public sealed class VehicleWeaponSlot : Common.Slot
    {
        public VehicleWeaponSlot(Common.SizeXY size)
            : base(size, Common.SlottableItemType.VehicleWeapon)
        {

        }
    }

    public enum VehicleWeaponAmmoTypes
    {
        Bullet50cal
    }
}
