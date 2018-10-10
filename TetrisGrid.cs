using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Tetris.Classes;


namespace Tetris.Classes
{
    class TetrisGrid
    {
        protected int layer;
        protected Matrix spriteScale;
        Texture2D emptyCell;
        
        Vector2 position;

        int[,] grid;


        public int Width { get { return 10; } }

        public int Height { get { return 20; } }
        

        public TetrisGrid(ContentManager Content, int layer = 0)
        {
            this.layer = layer;
            // Create a new SpriteBatch, which can be used to draw textures.
            emptyCell = Content.Load<Texture2D>("block");
            //här skapas Gridden 10*20
            grid = new int[10, 20];
            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 10; y++)
                    grid[x, y] = 0;
            Clear();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    Vector2 position = new Vector2(0 + x * 30, 0 + y * 30);
                    if (grid[x, y] == 0)
                    {
                        spriteBatch.Draw(emptyCell, position, Color.White);
                    }
                }
            }
        }
        
        public void Clear()
        {
        }
    }
}
