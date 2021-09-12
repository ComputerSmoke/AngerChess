using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class InvalidMoveException : Exception
    {
        public InvalidMoveException(string message): base(message)
        {
        }
    }
}
