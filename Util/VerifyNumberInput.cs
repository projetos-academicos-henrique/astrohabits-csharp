using astroHabitsCsharp.Exceptions;

namespace astroHabitsCsharp.util
{
    static public class VerifyNumber
    {
        static public void VerifyNumberRange(int valor, int min, int max)
        {
            if (valor < min || valor > max)
            {
                
                throw new InvalidNumberRangeException($"O valor deve estar entre {min} e {max}.");
            }
        }
    }
}
