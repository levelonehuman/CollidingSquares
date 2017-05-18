using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollidingSquares {
    public class Circle {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }
        public Texture2D Texture { get; private set; } //debug

        public Circle(Vector2 center, float radius) {
            Center = center;
            Radius = radius;
            Texture = GameController.CreateCircle((int)radius);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Center, Color.Green);
        }

        public bool Contains(Vector2 point) {
            return (point - Center).Length() <= Radius;
        }

        public bool Intersects(Circle other) {
            return (other.Center - Center).Length() < (other.Radius - Radius);
        }
    }
}
