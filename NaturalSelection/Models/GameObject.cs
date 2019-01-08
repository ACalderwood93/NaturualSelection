using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSelection.Models
{
    public  class GameObject
    {

        public float PosX { get; set; }
        public float PosY { get; set; }

        public float VelX { get; set; }
        public float VelY { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }

        public float Radius { get; set; }
        public Color color { get; set; }
        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)PosX, (int)PosY, (int)Width, (int)Height);
            }
        }

        public GameObject(float x, float y, float width, float height) : this(x, y)
        {

        }

        public GameObject(float x, float y, float radius) : this(x, y)
        {
            this.Radius = radius;
        }

        public GameObject(float x, float y)
        {
            this.PosX = x;
            this.PosY = y;
            this.color = Color.Green;
        }

        public GameObject()
        {

        }
    }
}
