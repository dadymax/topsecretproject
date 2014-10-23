using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces.Buildings
{
    public interface IBuilding : Targets.ITarget
    {
        Dictionary<int, ICeil> Ceils { get; }

        Dictionary<int, IEntrance> Entrances { get; }
    }

    public interface ICeil
    {

    }

    public interface IEntrance
    {

    }
    
}
