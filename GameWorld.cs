using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Tetris.Classes
{ 
    class GameWorld
    {
    enum GameState
    {
        Playing,
        GameOver
    }

    public static Random Random { get { return Random; } }
    static Random random;
    GameState gameState;
    SpriteFont font;
    TetrisGrid grid;
    

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;
        //font = TetrisProgram.ContentManager.Load<SpriteFont>("SpelFont");
    //    grid = new TetrisGrid();
    }
        public void HandleInput(GameTime gameTime, InputHelper inputHelper)
        {
        }
       
        public void Update(GameTime gameTime)
        {
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
          /*  spriteBatch.Begin();
            grid.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "Hello!", Vector2.Zero, Color.Blue);
            spriteBatch.End(); */
        }

        public void Reset()
        {
        }
    }
     
}
