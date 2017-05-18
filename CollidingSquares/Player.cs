using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollidingSquares {
    public class Player : Sprite {
        public Circle Collider { get; set; }

        public Player(Texture2D texture, Vector2 position) : base(texture) {
            Texture = texture;
            Position = position;
            Distance = new Vector2();
            Rotation = 0f;
            Collider = new Circle(this.Origin, Texture.Width / 2);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            Collider.Center = Position - Origin;

            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float velocityX = this.Velocity.X;
            float velocityY = this.Velocity.Y;
            float positionX = this.Position.X;
            float positionY = this.Position.Y;

            float targetX = positionX;
            float targetY = positionY;

            float moveBy = (float)(this.Speed * seconds);

            //handle up/down - decelerate to 0 if no key pressed
            if (GameController.CurrentKeyboardState.IsKeyDown(Keys.W)) {
                velocityY -= moveBy;
            } else if (GameController.CurrentKeyboardState.IsKeyDown(Keys.S)) {
                velocityY += moveBy;
            } else if (velocityY > 0) {
                velocityY = (velocityY > moveBy) ? velocityY - moveBy : 0f;
            } else if (velocityY < 0) {
                velocityY = (velocityY < moveBy) ? velocityY + moveBy : 0f;
            }

            //handle left/right - decelerate to 0 if no key pressed
            if (GameController.CurrentKeyboardState.IsKeyDown(Keys.A)) {
                velocityX -= moveBy;
            } else if (GameController.CurrentKeyboardState.IsKeyDown(Keys.D)) {
                velocityX += moveBy;
            } else if (velocityX > 0) {
                velocityX = (velocityX > moveBy) ? velocityX - moveBy : 0f;
            } else if (velocityX < 0) {
                velocityX = (velocityX < moveBy) ? velocityX + moveBy : 0f;
            }

            velocityX = MathHelper.Clamp(velocityX, -100, 100);
            velocityY = MathHelper.Clamp(velocityY, -100, 100);
            targetX = MathHelper.Clamp(targetX + velocityX * seconds, 0, 600);
            targetY = MathHelper.Clamp(targetY + velocityY * seconds, 0, 600);

            Vector2 targetPosition = new Vector2(targetX, targetY);

            this.Distance = this.Position - targetPosition;
            this.Velocity = new Vector2(velocityX, velocityY);
            this.Position = new Vector2(targetX, targetY);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            Collider.Draw(spriteBatch);

            string positionString = string.Format("Position: {0}, {1}", Position.X, Position.Y);
            string directionString = string.Format("Direction: {0}, {1}", Distance.X, Distance.Y);
            string velocityString = string.Format("Velocity: {0}, {1}", Velocity.X, Velocity.Y);
            string rotationString = string.Format("Rotation (Rad): {0}", Rotation);
            string rotationDegreesString = string.Format("Rotation (Deg): {0}", Rotation * (180/Math.PI));

            spriteBatch.DrawString(GameController.MainFont, positionString, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(GameController.MainFont, directionString, new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(GameController.MainFont, velocityString, new Vector2(10, 50), Color.Black);
            spriteBatch.DrawString(GameController.MainFont, rotationString, new Vector2(10, 70), Color.Black);
            spriteBatch.DrawString(GameController.MainFont, rotationDegreesString, new Vector2(10, 100), Color.Black);
        }

        private void MoveToMouse(GameTime gameTime) {
            //get current values
            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float velocityX = this.Velocity.X;
            float velocityY = this.Velocity.Y;
            float positionX = this.Position.X;
            float positionY = this.Position.Y;

            Vector2 mousePosition = new Vector2(GameController.CurrentMouseState.X, GameController.CurrentMouseState.Y);
            Distance = this.Position - mousePosition;

            if (GameController.CurrentMouseState.LeftButton == ButtonState.Pressed) {
                //switch to Less Than (<) to push AWAY from mouse position
                int xModifier = Distance.X > 0 ? -1 : 1;
                int yModifier = Distance.Y > 0 ? -1 : 1;

                //update velocity and clamp
                velocityX += (this.Speed * seconds) * xModifier;
                velocityY += (this.Speed * seconds) * yModifier;
                velocityX = MathHelper.Clamp(velocityX, this.Speed * -10, this.Speed * 10);
                velocityY = MathHelper.Clamp(velocityY, this.Speed * -10, this.Speed * 10);

                //update position and clamp
                positionX += velocityX * seconds;
                positionY += velocityY * seconds;
                positionX = MathHelper.Clamp(positionX, 0, 1000);
                positionY = MathHelper.Clamp(positionY, 0, 1000);

                //update class variables
                this.Velocity = new Vector2(velocityX, velocityY);
                this.Position = new Vector2(positionX, positionY);
            } else {
                this.Velocity = new Vector2(0, 0);
            }
        }
    }
}
