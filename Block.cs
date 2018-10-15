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
        float elapsedGameTime = 0.0f;

        TetrisGrid tetrisGrid;

        Color color;
        protected Point position;
        protected Texture2D emptyCell;
        protected bool[,] shape;
        Random rnd;
        ZBlock zBlock;
        TBlock tBlock;
        SquBlock squBlock;
        LBlockInvert lBlockInvert;
        LBlock lBlock;
        Block activeblock;
        CurrentBlock currentblock;
        IBlock iblock;

        public Block (ContentManager Content, Color color)
        {
            this.color = color;
            position = new Point(3, 0);
            emptyCell = Content.Load<Texture2D>("block");
            tetrisGrid = new TetrisGrid(Content);
            rnd = new Random();
        }      
        public Block SpawnNewBlock(ContentManager Content)
        {
            LBlockInvert block = new LBlockInvert(Content);
            return block;
            /*
            int rndBlock = rnd.Next(0,2);
            if (rndBlock == 0)
            {
                block = new ZBlock(Content);
                return block;
            }
            else if (rndBlock == 1)
            {
                block = new SquBlock(Content);
                return block;
            }
            else
            {
                block = new TBlock(Content);
                return block;
            }   */         
        }
        public void TurnLeft(ContentManager Content, Block block)
        {
            lBlockInvert = new LBlockInvert(Content);
            shape = lBlockInvert.RotatationLeft1();
        }
        public void TurnLeft2(ContentManager Content, Block block)
        {
            //lBlockInvert = new LBlockInvert(Content);
            shape = lBlockInvert.RotatationLeft2();
        }
        public void TurnRight(ContentManager Content, Block block)
        {
            lBlockInvert = new LBlockInvert(Content);
            shape = lBlockInvert.RotatationRight1();
        }
        public void TurnRight2(ContentManager Content, Block block)
        {
            lBlockInvert = new LBlockInvert(Content);
            shape = lBlockInvert.RotatationRight2();
        }
        public void MoveLeft()
        {
            if (tetrisGrid.InsideGrid(position))
                position.X--;
            else
                position.X++;
        }
        public void MoveRight()  //Kan ett block vara på denna positionen innanför gridden
        {
            if (tetrisGrid.InsideGrid(position))
                position.X++;
            else
                position.X = 1;
        }
        public void MoveDown()
        {
            if (tetrisGrid.InsideGrid(position))
                position.Y++;
            else
                position.Y--;
        }
        public void Falling(GameTime gameTime)
        {
            elapsedGameTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedGameTime >= 1000.0f)
            {
                position.Y++;
                elapsedGameTime = 0;
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if(shape[x,y])
                    {
                        Vector2 drawPosition = new Vector2((position.X + y) * emptyCell.Width, (position.Y + x) * emptyCell.Height);
                        spriteBatch.Draw(emptyCell, drawPosition, color);
                    }
                }
            }
        } 
    }
}
