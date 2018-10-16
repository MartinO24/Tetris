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

        KeyboardState currentKeyboardState, previousKeyboardState;

        TetrisGrid tetrisGrid;
        Color color;
        public Point position;
        protected Texture2D emptyCell;
        protected bool[,] shape;
        Random rnd;
        ZBlock zBlock;
        TBlock tBlock;
        SquBlock squBlock;
        LBlockInvert lBlockInvert;
        LBlock lBlock;
        ZBlockInvert zBlockInvert;
        int activeBlock;
        IBlock iblock;
        public List<Block> gameBlocks;
        public List<bool[,]> shapeList;
        public int currentRotation = 0;

        public Block (ContentManager Content, Color color)
        {
            this.color = color;
            position = new Point(3, 0);
            emptyCell = Content.Load<Texture2D>("block");
            tetrisGrid = new TetrisGrid(Content);
            rnd = new Random();
        }
        public List<Block> TetrisGameBlocks (ContentManager Content)
        {
            gameBlocks = new List<Block>();
            lBlock = new LBlock(Content);
            tBlock = new TBlock(Content);
            zBlock = new ZBlock(Content);
            squBlock = new SquBlock(Content);
            iblock = new IBlock(Content);
            lBlockInvert = new LBlockInvert(Content);
            zBlockInvert = new ZBlockInvert(Content);
            //gameBlocks.Add(lBlock);
            //gameBlocks.Add(tBlock);
            //gameBlocks.Add(zBlock);
            //gameBlocks.Add(squBlock);
            gameBlocks.Add(iblock);
            gameBlocks.Add(lBlockInvert);
            //gameBlocks.Add(zBlockInvert);
            activeBlock = RandomBlock();
            return gameBlocks;
        }
        public Block SpawnNewBlock(ContentManager Content, Block block)
        {
            gameBlocks = TetrisGameBlocks(Content);
            block = gameBlocks[activeBlock];
            return block;
        }
        public int RandomBlock()
        {
            int rndBlock = 1; 
            // int rndBlock = rnd.Next(2);
            return rndBlock;
        }
        public void TurnLeft() //Should depend on what block we got from SpawnedNewBlock
        {
            bool moveAllowed = true;
            if (activeBlock == 1)
            {
                shapeList = lBlockInvert.Rotations();                 
                shape = shapeList[currentRotation];
                currentRotation++;
                if (currentRotation > 3)
                    currentRotation = 0;
            }
        }
        public void TurnRight()
        {
            if (activeBlock == 1)
            {
                shapeList = lBlockInvert.Rotations();
                shape = shapeList[currentRotation];
                currentRotation--;
                if (currentRotation < 0)
                    currentRotation = 3;
            }
        }
        public void TurnRight2()
        {
            //lBlockInvert = new LBlockInvert(Content);
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
                position.X--;
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

        public void Update (GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Q) && !previousKeyboardState.IsKeyDown(Keys.Q))
                TurnLeft();
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W))
                TurnRight();
            //Move block sideways
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A))
                MoveLeft();
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D))
                MoveRight();
            //Move block down
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S))
                MoveDown();
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
