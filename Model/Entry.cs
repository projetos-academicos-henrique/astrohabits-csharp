using astroHabitsCsharp.util;

namespace astroHabitsCsharp.model
{
    
    
    
    public abstract partial class Entry
    {
        
        private string createdDate;

        
        public string CreatedDate { get { return createdDate; } set { createdDate = value; } }

        
        protected Entry()
        {
            GenerateTimeStamp();
        }

        private void GenerateTimeStamp()
        {
            
            createdDate = Timestamp.GetTimestamp(DateTime.Now); 
        }

        
        
        public abstract string GetDetails();
    }
}