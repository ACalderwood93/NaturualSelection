using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSelection.Models
{
    public class Food : GameObject
    {
        public bool Eaten { get; set; }
        public int Energy { get; set; }
        public Food(float x, float y, float radius) : base(x, y, radius)
        {
            this.color = Color.Yellow;
        }

        public Food()
        {



            this.Radius = 5;
            this.color = Color.Yellow;
            Energy = 200;
            //this.color = new Color(Energy,Energy,Energy);

            Eaten = false;
        }
    }
}
