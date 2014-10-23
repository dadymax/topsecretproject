using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface Vehicle : Abstract.IHaveMass, Abstract.IHaveHealth
    {
        bool IsEngineFit(IEngine engine);
        Abstract.SizeXY EngineMountPoint { get; }
        IEngine Engine { get; set; }
        ulong FuelCapacity { get; }
        ulong FuelCurrent { get; set; }

        uint MaxVelocity { get; }

        bool IsWeaponFit(IVehicleWeapon weapon, int mountpoint);
        Dictionary<int, Abstract.SizeXY> WeaponMountSlots { get; }
        Dictionary<int, IVehicleWeapon> WeaponMounted { get; }

        uint PassagenersMax { get; }
        uint PassagenersCurrent { get; }
        void AddPassagener(Persons.IPerson person);
        List<Persons.IPerson> Passageners { get; }

        void GoToCoordinates(Abstract.ICoordinates coordinates);
        void GoToTarget(Targets.ITarget target);
        void AttackTarget(Targets.ITarget target);

        Buildings.IBuilding Home { get; set; }
        void GoHome();
        void GoToBuilding(Buildings.IBuilding building);
    }

    public interface IEngine : Abstract.IHaveMass, Abstract.IHaveItemSize
    {
        int Power { get; }
        int Thrust { get; }

        uint FuelConsumption { get; }
    }

    public interface IVehicleWeapon : Abstract.IHaveMass, Abstract.IHaveItemSize
    {
        bool HaveAmmo { get; }
        uint AmmoCount { get; }
        uint AmmoPerFire { get; }

        uint Damage { get; }
        byte Accuracy { get; }
    }
}
