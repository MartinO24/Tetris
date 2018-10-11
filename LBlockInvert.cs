using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Tetris.Classes
{
    class LBlockInvert : Block
    {
        public LBlockInvert(ContentManager Content, int layer = 1)
            : base(Content, Color.Magenta, 1)
        {
            shape = new bool[,]
            {
                {false, false, true, false},
                {false, false, true, false},
                {false, true, true, false},
                {false, false, false, false},
            };
        }
        public bool[,] RotatationLeft1()
        {
            shape = new bool[,]
            {
                {true, false, false, false},
                {true, true, true, false},
                {false, false, false, false},
                {false, false, false, false},
            };
            return shape;
        }
        public bool[,] RotatationLeft2()
        {
            shape = new bool[,]
            {
                {false, true, true, false},
                {false, true, false, false},
                {false, true, false, false},
                {false, false, false, false},
            };
            return shape;
        }
        public bool[,] RotatationRight1()
        {
            shape = new bool[,]
            {
                {false, true, true, true},
                {false, false, false, true},
                {false, false, false, false},
                {false, false, false, false},
            };
            return shape;
        }
        public bool[,] RotatationRight2()
        {
            shape = new bool[,]
            {
                {false, true, true, false},
                {false, true, false, false},
                {false, true, false, false},
                {false, false, false, false},
            };
            return shape;
        }
    }
}
