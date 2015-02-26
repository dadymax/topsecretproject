using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realisations.Common
{
    /// <summary>
    /// 3 dimensions coordinates, where Z axis is named Level.
    /// </summary>
    public class Coordinates : GridPosition
    {
        public int Level { get; set; }
    }

    /// <summary>
    /// Position of something somewere. Commonly position of item in inventory grid.
    /// </summary>
    public class GridPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    /// <summary>
    /// Size of something. Struct because size is a constant for item.
    /// </summary>
    public struct SizeXY
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
