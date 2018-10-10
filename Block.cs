using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tetris.Classes;
using Microsoft.Xna.Framework.Content;

namespace Tetris.Classes
{
    class Block
    {
        Color color;
        protected Point position;
        protected int layer;
        protected Texture2D emptyCell;
        protected bool[,] shape;

        public Block (ContentManager Content, Color color, int layer = 1)
        {
            this.color = color;
            position = new Point(3, 0);
            this.layer = layer;
            emptyCell = Content.Load<Texture2D>("block");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if(shape[x,y])
                    {
                        Vector2 drawPosition = new Vector2((position.X + x) * 30, (position.Y + y) * 30); //empty.Width 
                        spriteBatch.Draw(emptyCell, drawPosition, color);
                    }
                }
            }
        } 
    }
}
