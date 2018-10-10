using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Classes
{
    class GameObject
    {
        protected GameObject parent;
        protected Vector2 position, velocity;
        protected int layer;
        protected bool visible;

        public GameObject(int layer = 0)
        {
            this.layer = layer;
            position = Vector2.Zero;
            velocity = Vector2.Zero;
            visible = true;
        }
        public virtual void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
