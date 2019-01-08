using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSelection.Models
{
    class Factory<T> where T : GameObject, new()
    {

        public Factory()
        {

        }

        public List<T> CreateRandom(int amount)
        {
            
            

            var result = new List<T>();
            for (int i = 0; i < amount; i++)
            {
                var randX = Util.Util.rand.Next(0, 1600);
                var randY = Util.Util.rand.Next(0, 900);
                T obj = new T();
                obj.PosX = randX;
                obj.PosY = randY;

                result.Add(obj);



            }

            return result;

        }
        public List<T> CreateRandom(int amount,Rectangle area)
        {



            var result = new List<T>();
            for (int i = 0; i < amount; i++)
            {
                var randX = Util.Util.rand.Next(area.Left, area.Right);
                var randY = Util.Util.rand.Next(area.Top, area.Bottom);
                T obj = new T();
                obj.PosX = randX;
                obj.PosY = randY;

                result.Add(obj);



            }

            return result;
        }
    }
}
