using NaturalSelection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSelection.Util
{
    public static class CollisionDetector
    {

        public static bool Collides(GameObject source, GameObject target)
        {
            var distance = Util.CalculateDistance(source, target);
            return distance < source.Radius + target.Radius;
        }
    }
}
