namespace astroHabitsCsharp.util
{
    static public class VerifyNumber
    {
        static public int VerifyNumber1To10(String input)
        {

            try
            {
                int.TryParse(input, out int parsedInput);
                if (parsedInput < 1 || parsedInput > 9)
                {
                    throw new InvalidNumberRangeException("A resposta deve ser entre 1 e 10.");
                }

                return parsedInput;
            }
            catch (InvalidNumberRangeException e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Digite um numero valido.");
                throw new Exception(e.Message);
            }

        }
    }
}
