using Microsoft.Xna.Framework;
using NaturalSelection.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalSelection.Util;

namespace NaturalSelection.Models
{
    class Blob : GameObject
    {
        public enum state { idle, searching, moving, dead };

        public state CurrentState { get; set; }
        public bool Alive { get; set; }
        public float Size { get; set; }
        public float Speed { get; set; }
        public float Sense { get; set; }
        public float Energy { get; set; }
        public bool HasEaten { get; set; }
        public Blob(float x, float y, float radius) : base(x, y, radius)
        {
            this.color = Color.Green;
        }
        private Vector2 _MoveTarget { get; set; }
        private double _TimeSearching { get; set; }
        public Blob()
        {
            this.color = Color.Green;
            Alive = true;
            CurrentState = state.searching;
            Size = 1;
            this.Radius = 15 * Size;
            Sense = 200;
            Speed = 50;
            Energy = 1000;

            bool willMutate = Util.Util.rand.Next(0, 4) == 1;

            if (willMutate)
            {
                _Mutate();
            }
        }

        public void Update(List<Food> food)
        {
            switch (CurrentState)
            {
                case state.idle:
                    break;
                case state.moving:
                    MoveTo(_GetClosestFood(food));
                    break;
                case state.searching:
                    Search(food);
                    break;
                case state.dead:
                    break;
            }


            this.PosX += VelX;
            this.PosY += VelY;

           // this.Energy -= (float)((Speed + Size) * Renderer.Dt);

            if (Energy <= 0)
                Alive = false;
        }
        public void Search(List<Food> food)
        {
            var closestFood = _GetClosestFood(food);

            if (closestFood != null)
            {
                _MoveTarget = new Vector2(closestFood.PosX, closestFood.PosY);
                _TimeSearching = 0;
                this.CurrentState = state.moving;
            }
            else if (_TimeSearching > 10 || _MoveTarget == new Vector2(0, 0)) // they can go 2 seconds without finding food then they will search somewhere else
            {


                var x = Util.Util.rand.Next(0, 1600);
                var y = Util.Util.rand.Next(0, 900);
                _TimeSearching = 0;
                _MoveTarget = new Vector2(x, y);

            }

            MoveTo(_MoveTarget);
            _TimeSearching += 1 * Renderer.Dt;

        }
        private Food _GetClosestFood(List<Food> food)
        {
            var result = food.OrderBy(f => Util.Util.CalculateDistance(this, f)).Where(f => !f.Eaten).FirstOrDefault();

            if (result == null)
                return null;

            var distance = Util.Util.CalculateDistance(this, result);
            if (Sense * 2 >= distance)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        public void MoveTo(GameObject obj)
        {

            if (obj == null)
            {
                this.CurrentState = state.searching;
                return;
            }

            if (obj is Food)
            {
                var f = obj as Food;

                if (f.Eaten)
                {
                    this.CurrentState = state.searching;
                }
            }

            var dir = new Vector2(obj.PosX, obj.PosY) - new Vector2(PosX, PosY);
            dir.Normalize();

            VelX = (float)(dir.X * Speed * Renderer.Dt);
            VelY = (float)(dir.Y * Speed * Renderer.Dt);
        }
        public void MoveTo(Vector2 target)
        {
            var dir = target - new Vector2(PosX, PosY);
            dir.Normalize();

            VelX = (float)(dir.X * Speed * Renderer.Dt);
            VelY = (float)(dir.Y * Speed * Renderer.Dt);
        }
        public void Stop()
        {
            VelX = 0;
            VelY = 0;
            _MoveTarget = new Vector2(0, 0);
        }
        public void Reset()
        {
            this.CurrentState = state.searching;
            this.color = Color.Green;
        }
        private void _Mutate()
        {
            var type = Util.Util.rand.Next(1, 4);

            switch (type)
            {
                case 1: // speed
                    this.color = Color.Red;
                    this.Speed = 75;
                    break;
                case 2: // size
                    this.color = Color.Blue;
                    this.Size = 5;
                    this.Speed = 30;
                    this.Radius = 15 * Size;
                    break;
                case 3: // sense
                    this.color = Color.Yellow;
                    this.Sense = 400;
                    break;
            }
        }
    }
}
