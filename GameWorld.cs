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
    public GameWorld(ContentManager Content)
    {
        random = new Random();
        gameState = GameState.Playing;
        font = Content.Load<SpriteFont>("SpelFont");
        grid = new TetrisGrid(Content);
    }
        public void HandleInput(GameTime gameTime, InputHelper inputHelper)
        {
        }
       
        public void Update(GameTime gameTime)
        {
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 scorePosition = new Vector2(330, 0);
            Vector2 levelPosition = new Vector2(330, 30);
            Vector2 nextBlockPosition = new Vector2(330, 60);

            spriteBatch.DrawString(font, "Score: ", scorePosition, Color.Blue);
            spriteBatch.DrawString(font, "Level: ", levelPosition, Color.Blue);
            spriteBatch.DrawString(font, "Next block: ", nextBlockPosition, Color.Blue);
        }
        public void Reset()
        {
        }
    }
     
}
