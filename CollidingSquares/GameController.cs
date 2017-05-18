using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollidingSquares {
    public static class GameController {
        public static GraphicsDevice GraphicsDevice { get; private set; }
        //Inputs
        public static MouseState CurrentMouseState { get; private set; }
        public static MouseState LastMouseState { get; private set; }

        public static KeyboardState CurrentKeyboardState { get; private set; }
        public static KeyboardState LastKeyboardState { get; private set; }

        public static GamePadState CurrentGamePadState { get; set; }
        public static GamePadState LastGamePadState { get; private set; }

        //Drawables
        public static SpriteFont MainFont { get; private set; }
        public static Texture2D SquareTexture { get; private set; }

        public static void Initialize(GraphicsDevice graphicsDevice) {
            GraphicsDevice = graphicsDevice;
        }

        public static void LoadContent(ContentManager content) {
            MainFont = content.Load<SpriteFont>("mainfont");
            SquareTexture = content.Load<Texture2D>("square");
        }

        public static void Update() {
            LastMouseState = CurrentMouseState;
            LastKeyboardState = CurrentKeyboardState;
            LastGamePadState = CurrentGamePadState;

            CurrentMouseState = Mouse.GetState();
            CurrentKeyboardState = Keyboard.GetState();
            CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public static Texture2D CreateCircle(int radius) {
            int outerRadius = (int)(radius * 2) + 2;
            Texture2D texture = new Texture2D(GraphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            //color the entire texture transparent
            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.Transparent;
            }

            float angleStep = 1f / radius;

            for (float angle = 0; angle < Math.PI * 2; angle += angleStep) {
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }
    }
}
