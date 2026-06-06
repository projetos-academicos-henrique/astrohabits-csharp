using System;

namespace astroHabitsCsharp.Exceptions
{
    
    public class InvalidNumberRangeException : Exception
    {
        
        public InvalidNumberRangeException(string message) : base(message)
        {
            
        }
    }
}