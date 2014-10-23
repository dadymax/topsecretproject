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

}
