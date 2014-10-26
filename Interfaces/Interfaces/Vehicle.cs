using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces.Vehicles
{
    public interface Vehicle : Abstract.IHaveMass, Abstract.IHaveHealth
    {
        bool IsEngineFit(IEngine engine);
        Abstract.SizeXY EngineMountPoint { get; }
        IEngine Engine { get; set; }
        ulong FuelCapacity { get; }
        ulong FuelCurrent { get; set; }

        uint MaxVelocity { get; }

        Dictionary<int, Abstract.ISlot<IVehicleWeapon>> Weapons { get; }

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

    //public class EngineSlot : Abstract.ISlot<IEngine>
    //{

    //}

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

        int MaxFireDistance { get; }
        int FireRate { get; }


    }
}
