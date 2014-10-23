using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces.Targets
{
    public interface ITarget
    {
        Abstract.ICoordinates TargetPosition { get; }
    }


}
