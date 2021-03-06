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
    class ZBlock : Block
    {
        public ZBlock(ContentManager Content, int layer = 1)
            : base(Content, Color.Blue, 1)
        {
            shape = new bool[,]
            {
                {false, false, false, false},
                {true, true, false, false},
                {false, true, true, false},
                {false, false, false, false},
            };
        }
    }
}
