using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess.Engines.Human
{
    class Human : Engine
    {
        public Human()
        {
            modelName = "HH";
            build();
        }
    }
}
