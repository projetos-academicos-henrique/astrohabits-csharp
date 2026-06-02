
using astroHabitsCsharp.util;

namespace astroHabitsCsharp.model
{
    abstract public class Entry
    {
        
        private string createdDate;

        protected Entry()
        {
            GenerateTimeStamp();
        }

        private void GenerateTimeStamp()
        {
            createdDate = Timestamp.GetTimestamp(new DateTime());
        }

    }
}