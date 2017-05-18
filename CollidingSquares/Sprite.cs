using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollidingSquares {
    public class Sprite {
        public Texture2D Texture { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Distance { get; protected set; }
        public Vector2 Velocity { get; protected set; }

        public float Rotation { get; protected set; }
        public float Scale { get; protected set; }
        public float Speed { get; protected set; }

        public Vector2 Origin => new Vector2(Texture.Width / 2, Texture.Height / 2);

        public Sprite(Texture2D texture) {
            Texture = texture;
            Speed = 100f;
            Scale = 1f;
        }

        public virtual void Update(GameTime gameTime) {
            if (Distance != Vector2.Zero) {
                Distance.Normalize();
            }

            Rotation = (float)Math.Atan2(Distance.Y, Distance.X);
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0);
        }
    }
}
