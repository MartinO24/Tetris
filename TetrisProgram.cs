using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Tetris.Classes;

namespace Tetris
{
    class TetrisProgram : Game
    {
        TetrisGrid tetGrid;
        Block block;
        ZBlock zBlock;
        SquBlock squBlock;
        TBlock tBlock;
        IBlock iBlock;
        LBlock lBlock;
        LBlockInvert lBlockInvert;
        ZBlockInvert zBlockInvert;
        //Gjort detta utifrån JewJam2 
        //Klasser från JewJam3 "gameObject.class" etc. 
        //Layers ska användas på gridden. 
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected Matrix spriteScale;
        protected static Point screen;
        protected Point windowSize;
        protected static Random random;
        protected Texture2D emptyCell;
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
            //ContentManager = Content;
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
            spriteScale = Matrix.CreateScale(inputHelper.Scale.X +1f, inputHelper.Scale.Y +1f, 1);
        }

        protected override void LoadContent()
        {
            screen = new Point(1440, 1080);
            windowSize = new Point(1024, 768);
            FullScreen = false;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tetGrid = new TetrisGrid(Content);
           // block = new Block(Content);
            zBlock = new ZBlock(Content);
            squBlock = new SquBlock(Content);
            tBlock = new TBlock(Content);
            lBlock = new LBlock(Content);
            lBlockInvert = new LBlockInvert(Content);
            zBlockInvert = new ZBlockInvert(Content);
        }
        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Denhär metoden skriver ut GRIDDEN
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScale);
            tetGrid.Draw(gameTime, spriteBatch);
            lBlock.Draw(gameTime, spriteBatch);
            spriteBatch.End();
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
