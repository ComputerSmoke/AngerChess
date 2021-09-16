using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class BadSpawnException: Exception
    {
        public BadSpawnException(string message) : base(message)
        {
        }
    }
}
