using Microsoft.Xna.Framework;
using NaturalSelection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSelection.Util
{
    public static class Util
    {
        public static Random rand { get; set; }
        public static double CalculateDistance(Vector2 source, Vector2 target)
        {

            var difX = target.X - source.X;
            var difY = target.Y - source.Y;
            return Math.Sqrt(Math.Abs(difX) + Math.Abs(difY));
        }
        public static double CalculateDistance(GameObject source, GameObject target)
        {
            var difX = Math.Pow(target.PosX - source.PosX, 2);
            var difY = Math.Pow(target.PosY - source.PosY, 2);
            return Math.Sqrt(difX + difY);
        }
        public static void Init()
        {
            rand = new Random(DateTime.Now.Millisecond);
        }

    }
}
