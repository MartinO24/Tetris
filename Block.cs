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
        protected Color[,] shape;
        Random rnd;

        public List<List<Color[,]>> gameBlocks;
        public int currentRotation = 0;
        private int currentBlock;

        public Block (ContentManager Content, Color color)
        {
            this.color = color;
            position = new Point(3, 0);
            emptyCell = Content.Load<Texture2D>("block");
            tetrisGrid = new TetrisGrid(Content);
            rnd = new Random();
            currentBlock = RandomBlock();
            //TetrisGameBlocks(Content);
        }
        public Color[,] Transpose(Color[,] shape) //https://stackoverflow.com/questions/29483660/how-to-transpose-matrix
        {
            int width = shape.GetLength(0);
            int height = shape.GetLength(1);

            Color[,] result = new Color[height, width];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[y, x] = shape[x, y];
                }
            }
            return result;
        }
        public List<List<Color[,]>>  TetrisGameBlocks (Color[,] shape)
        {
            Color white = Color.White;
            Color magenta = Color.Magenta;
            Color blue = Color.Blue;
            gameBlocks = new List<List<Color[,]>>();
            List<Color[,]> lBlockShapes = new List<Color[,]>();

            List<Color[,]> lBlockInvertShapes = new List<Color[,]>();
            Color[,] lBlockInvertshape1 = new Color[,]
            {
                {white, white, magenta, white},
                {white, white, magenta, white},
                {white, magenta, magenta, white},
                {white, white, white, white},
            };
            Color[,] lBlockInvertshape2 = new Color[,]
            {
                {magenta, white, white, white},
                {magenta, magenta, magenta, white},
                {white, white, white, white},
                {white, white, white, white},
            };
            Color[,] lBlockInvertshape3 = new Color[,]
            {
                {white, magenta, magenta, white},
                {white, magenta, white, white},
                {white, magenta, white, white},
                {white, white, white, white},
            };
            Color[,] lBlockInvertshape4 = new Color[,]
            {
                {white, magenta, magenta, magenta},
                {white, white, white, magenta},
                {white, white, white, white},
                {white, white, white, white},
            };
          /*  lBlockInvertshape1 = Transpose(lBlockInvertshape1);
            lBlockInvertshape2 = Transpose(lBlockInvertshape2);
            lBlockInvertshape3 = Transpose(lBlockInvertshape3);
            lBlockInvertshape4 = Transpose(lBlockInvertshape4); */
            lBlockInvertShapes.Add(lBlockInvertshape1);
            lBlockInvertShapes.Add(lBlockInvertshape2);
            lBlockInvertShapes.Add(lBlockInvertshape3);
            lBlockInvertShapes.Add(lBlockInvertshape4);
            List<Color[,]> zBlockShapes = new List<Color[,]>();
            List<Color[,]> ZBlockInvertShapes = new List<Color[,]>();
            List<Color[,]> squBlock = new List<Color[,]>();

            List<Color[,]> iBlockShapes = new List<Color[,]>();
            Color[,] iBlockshape1 = new Color[,]
            {
                {white, blue, white, white},
                {white, blue, white, white},
                {white, blue, white, white},
                {white, blue, white, white},
            };
            Color[,] iBlockshape2 = new Color[,]
            {
                {white, white, white, white},
                {blue, blue, blue, blue},
                {white, white, white, white},
                {white, white, white, white},
            };
            Color[,] iBlockshape3 = new Color[,]
            {
                {white, blue, white, white},
                {white, blue, white, white},
                {white, blue, white, white},
                {white, blue, white, white},
            };
            Color[,] iBlockshape4 = new Color[,]
            {
                {white, white, white, white},
                {blue, blue, blue, blue},
                {white, white, white, white},
                {white, white, white, white},
            };
          //  iBlockshape1 = Transpose(iBlockshape1); //For some reason the blocks were inverted so we used transpose method to correct this by swapping the X and Y values.
          //  iBlockshape2 = Transpose(iBlockshape2);
          //  iBlockshape2 = Transpose(iBlockshape3);
          //  iBlockshape2 = Transpose(iBlockshape4);
            iBlockShapes.Add(iBlockshape1);
            iBlockShapes.Add(iBlockshape2);
            iBlockShapes.Add(iBlockshape3);
            iBlockShapes.Add(iBlockshape4);
            List<Color[,]> tBlockShapes = new List<Color[,]>();
            
            gameBlocks.Add(lBlockInvertShapes);
            gameBlocks.Add(iBlockShapes);
            return gameBlocks;

            /* tBlock = new TBlock(Content);
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
             activeBlock = RandomBlock(); */
        }
        public Color[,] SpawnNewBlock(Color[,] shape)
        {
            gameBlocks = TetrisGameBlocks(shape);
            shape = gameBlocks[currentBlock][0];
            return shape;
        }
        public int RandomBlock()
        {
            int rndBlock = 0; 
            //rndBlock = rnd.Next(2);
            return rndBlock;
        }
        public void Rotate(int direction) 
        {
            currentRotation += direction;
            currentRotation = currentRotation % 4;
            if (currentRotation < 0)
                currentRotation = 3;
            shape = gameBlocks[currentBlock][currentRotation]; //iBlock rotationen är lite fucked

            /*  if (activeBlock == 1)
              {
                  shapeList = lBlockInvert.Rotations();
                  currentRotation += direction;
                  currentRotation = currentRotation % 4;
                  if (currentRotation < 0)
                      currentRotation = 3;
                  shape = shapeList[currentRotation];
              } */
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
            Color[,] shape = gameBlocks [currentBlock][currentRotation];
            if (tetrisGrid.InsideGrid(position)) //&& tetrisGrid.ShapeInsideGrid(shape, position))
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

            //Rotate blocks
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && !previousKeyboardState.IsKeyDown(Keys.Q))
                Rotate(1);
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W))
                Rotate(-1);
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
                    shape = gameBlocks[currentBlock][currentRotation];
                    if(shape[x,y] != Color.White)
                    {
                        Vector2 drawPosition = new Vector2((position.X + x) * emptyCell.Width, (position.Y + y) * emptyCell.Height);
                        spriteBatch.Draw(emptyCell, drawPosition, color);
                    }
                }
            }
        } 
    }
}
