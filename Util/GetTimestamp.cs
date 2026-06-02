namespace astroHabitsCsharp.util
{
    static public class Timestamp{
        static public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
