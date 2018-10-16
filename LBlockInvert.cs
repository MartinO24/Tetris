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
        List<bool[,]> shapeList;
        public LBlockInvert(ContentManager Content)
            : base(Content, Color.Magenta)
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
        public bool[,] RotatationRight2() //Same as default
        {
            shape = new bool[,]
            {
                {false, false, true, false},
                {false, false, true, false},
                {false, true, true, false},
                {false, false, false, false},
            };
            return shape;
        }
        public List<bool[,]> Rotations ()
        {
            shapeList = new List<bool[,]>();
            bool[,] r1 = RotatationLeft1();
            bool[,] r2 = RotatationLeft2();
            bool[,] r3 = RotatationRight1();
            bool[,] r4 = RotatationRight2();
            shapeList.Add(r1);
            shapeList.Add(r2);
            shapeList.Add(r3);
            shapeList.Add(r4);
            return shapeList;
        }
    }
}
