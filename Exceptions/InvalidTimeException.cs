using System;

namespace astroHabitsCsharp.Exceptions
{
    
    
    public class InvalidTimeException : Exception
    {
        public InvalidTimeException(string message) : base(message)
        {

        }
    }
}
