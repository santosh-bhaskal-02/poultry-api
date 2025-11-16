namespace MyProject.Utilities
{
    public static class DateTimeHelper
    {
        public static DateTime NormalizeToUtc(DateTime date)
        {
            return date.Kind switch
            {
                DateTimeKind.Utc => date,
                DateTimeKind.Local => date.ToUniversalTime(),
                _ => DateTime.SpecifyKind(date, DateTimeKind.Utc)
            };
        }
    }
}
