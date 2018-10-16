using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Tetris.Classes;

namespace Tetris
{
    class TetrisProgram : Game
    {
        /* TO DO
         * Collisions
         * Score
         * Control with just arrows
         * Music
         * All rotations in all block-classes
         * Currentblock - för ett block så ett skapas
         * 
        */

        public enum GameState
        {
            WelcomeScreen,
            StartScreen,
            PlayScreen,
            GameOverScreen
        }
        GameState CurrentState = GameState.WelcomeScreen;

        //Ska till inputhelper
        KeyboardState currentKeyboardState, previousKeyboardState;

        GameWorld gameWorld;
        TetrisGrid tetrisGrid;
        Block block;
        public Block activeBlock;

        public bool gameRun { get; set; }

        Random rnd;
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected Matrix spriteScale;
        protected static Point screen;
        protected Point windowSize;
        protected static Random random;
        protected int[,] grid;
        protected InputHelper inputHelper;

        // public static ContentManager ContentManager { get; private set;  }
        public static Point ScreenSize { get; private set; }

        [STAThread]
        static void Main()
        {
            TetrisProgram game = new TetrisProgram();
            game.Run();
        }
        public TetrisProgram()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            random = new Random();
            inputHelper = new InputHelper();
        }
        public bool FullScreen
        {
            get { return graphics.IsFullScreen; }
            set
            {
                ApplyResolutionSettings(value);
            }
        }
        public void ApplyResolutionSettings(bool fullScreen = false)
        {
            if (!fullScreen)
            {
                graphics.PreferredBackBufferWidth = windowSize.X;
                graphics.PreferredBackBufferHeight = windowSize.Y;
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
            }
            else
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }

            float targetAspectRatio = (float)screen.X / (float)screen.Y;
            int width = graphics.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio);
            if (height > graphics.PreferredBackBufferHeight)
            {
                height = graphics.PreferredBackBufferHeight;
                width = (int)(height * targetAspectRatio);
            }

            Viewport viewport = new Viewport();
            viewport.X = (graphics.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (graphics.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            GraphicsDevice.Viewport = viewport;

            inputHelper.Scale = new Vector2((float)GraphicsDevice.Viewport.Width / screen.X,
                                            (float)GraphicsDevice.Viewport.Height / screen.Y);
            inputHelper.Offset = new Vector2(viewport.X, viewport.Y);
            spriteScale = Matrix.CreateScale(inputHelper.Scale.X +2/3f, inputHelper.Scale.Y +2/3f, 1);
        }
        protected override void LoadContent()
        {
            screen = new Point(1340, 1180);
            windowSize = new Point(924, 868);
            FullScreen = false;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tetrisGrid = new TetrisGrid(Content);
            gameWorld = new GameWorld(Content);
            block = new Block(Content, Color.White);
            rnd = new Random();
            //activeBlock = new Block(Content, Color.White);
            //shape = new bool[,];
        }
        protected override void Update(GameTime gameTime) //----------Find a way to get it back to default
        {
            switch (CurrentState)
            {
                case GameState.WelcomeScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        CurrentState = GameState.StartScreen;
                    block = block.SpawnNewBlock(Content, block); //Move to block?
                    block.TetrisGameBlocks(Content);             //Move to block?
                    break;
            }
            switch (CurrentState)
            {
                case GameState.StartScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.X))
                        CurrentState = GameState.PlayScreen;
                    //activeBlock.TetrisGameBlocks(Content);
                    break;
            }
          //  if(GameState.PlayScreen == CurrentState)
          //      activeBlock.Falling(gameTime);

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();   //-----------Allt här ska väl till inputhelper?

            //Exit game
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           // lBlockInvert.Falling(gameTime);

            inputHelper.Update(gameTime);
            tetrisGrid.Update(gameTime);
            block.Update(gameTime);
            
        }
        protected override void Draw(GameTime gameTime)
        {
            switch (CurrentState)
            {
                case GameState.WelcomeScreen:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScale);
                    gameWorld.Draw(gameTime, spriteBatch);
                    //tetrisGrid.Draw(gameTime, spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.StartScreen:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScale);
                    gameWorld.Draw(gameTime, spriteBatch);
                    tetrisGrid.Draw(gameTime, spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.PlayScreen:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScale);
                    gameWorld.Draw(gameTime, spriteBatch);
                    tetrisGrid.Draw(gameTime, spriteBatch);
                    block.Draw(gameTime, spriteBatch);
                    spriteBatch.End();
                    break;
            }
        }
        protected void MoveRowsDown()
        {
            //Funkar men har ingen knapp för att testas
            int x, y;
            for (y = 18; y >= 0; y--)
            {
                for (x = 0; x < 10; x++)
                {
                    grid[x, y + 1] = grid[x, y];
                }
            }
            for (x = 0; x < 10; x++)
            {
                grid[x, 0] = random.Next(3);
            }
        }
        public static Point Screen
        { get
            {
                return screen;
            }
            set { screen = value; }        
        }
    }
}
