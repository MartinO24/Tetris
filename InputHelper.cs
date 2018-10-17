using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetris.Classes
{
    class InputHelper

    {
        protected Vector2 scale, offset;
        KeyboardState currentKeyboardState, previousKeyboardState;

        public InputHelper()
        {
            scale = Vector2.One;
            offset = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        public bool KeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
        }

        public bool KeyDown(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k);
        }
    }
}
