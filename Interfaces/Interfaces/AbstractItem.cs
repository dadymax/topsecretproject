using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces.Abstract
{
    public interface IHaveMass
    {
        int Mass { get; }
    }

    public interface IHaveItemSize
    {
        SizeXY Size { get; }
    }

    public interface IHaveHealth
    {
        int HealthPoints { get; set; }
    }

    public struct SizeXY
    {
        public uint X;
        public uint Y;
    }

    public interface ICoordinates
    {

    }

    public interface ISlot<T> : IHaveItemSize
    {
        ItemTypes SlotSupportItem { get; }
        T ItemInside { get; set; }
    }

    public interface IItem : IHaveItemSize
    {
        ItemTypes TypeOfItem { get; }
    }

    [Flags]
    public enum ItemTypes
    {
        VehicleWeapon,
        VehicleEngine
    }

}
