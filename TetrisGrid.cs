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
        Block block;
        Color color = Color.White;
        Vector2 position;
        Vector2 blockPosition;
        //LBlockInvert lBlockInvert;
        bool[,] grid;
        bool[,] shape;
        
        public int Width { get { return 10; } }

        public int Height { get { return 20; } }

        public TetrisGrid(ContentManager Content)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            emptyCell = Content.Load<Texture2D>("block");
            //här skapas Gridden 10*20
            grid = new bool[Width, Height];
            //Clear();
        }
        public void GridBlock () //Should take in a shape from block -- How do I make it so I can use block here?
        {
            shape = new bool[,]
            {
                {false, false, true, false},
                {false, false, true, false},
                {false, true, true, false},
                {false, false, false, false},
            };
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    grid[y + 3, x] = shape[x, y]; //Can I get it to print "false" "outside" grid? To manage collision
                }                               //grid [y + block.position.Y, x + block.position.X]
            }
        }
        public void Update(GameTime gameTime)
        {
            GridBlock();
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Vector2 position = new Vector2(0 + x * emptyCell.Width, 0 + y * emptyCell.Height);
                    if (grid[x, y] == false)
                    {
                        spriteBatch.Draw(emptyCell, position, Color.White);
                    }
                    else if (grid[x, y])
                    {
                        blockPosition = new Vector2((0 + x) * emptyCell.Width,(0 + y) * emptyCell.Height);
                        spriteBatch.Draw(emptyCell, blockPosition, Color.Green);
                    }
                }
            }
        }
     /*   public void Clear()
        {
            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 10; y++)
                    grid[x, y] = 0;
        } */
        public bool InsideGrid(Point position)
        {
            return position.X >= 0 && position.X < Width && position.Y < Height && position.Y >= 0;
        }
        public bool ShapeInsideGrid (bool[,]shape)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (grid[y, x] = shape[x, y])
                        return true; //Can I get it to print "false" "outside" grid? To manage collision
                }                               //grid [y + block.position.Y, x + block.position.X]
            }
            return false;
        }
    }
}

