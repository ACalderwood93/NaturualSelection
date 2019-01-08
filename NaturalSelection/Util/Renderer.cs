using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NaturalSelection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSelection.Util
{
    public static class Renderer
    {
        private static SpriteBatch _spriteBatch { get; set; }
        private static GraphicsDevice _graphicsDevice { get; set; }
        public static double Dt { get; set; }

        public static void Init(SpriteBatch sb, GraphicsDevice gd)
        {
            _spriteBatch = sb;
            _graphicsDevice = gd;
        }
        public static void Draw(GameObject obj)
        {
            if (obj.Radius > 0)
            {
                var tex = createCircleText((int)obj.Radius, obj);
                _spriteBatch.Draw(tex, new Vector2(obj.PosX, obj.PosY), Color.White);
            }
        }
        private static Texture2D createCircleText(int radius, GameObject obj)
        {
            Texture2D texture = new Texture2D(_graphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = obj.color;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }
        public static void BeginDraw()
        {
            _spriteBatch.Begin();
        }
        public static void EndDraw()
        {
            _spriteBatch.End();
        }
    }
}
