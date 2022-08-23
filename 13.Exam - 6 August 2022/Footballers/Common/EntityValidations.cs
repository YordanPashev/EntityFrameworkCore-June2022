namespace Footballers.Common
{
    public static class EntityValidations
    {
        //Footballer
        public const int FootballerNameMinLength = 2;
        public const int FootballerNameMaxLength = 40;

        //Team
        public const int TeamNameMinLength = 3;
        public const int TeamNameMaxLength = 40;
        public const string TeamNameRegex = @"[A-Za-z\d\s\.\-]+";
        public const int NationalityMinLength = 2;
        public const int NationalityMaxLength = 40;
        public const int MinThropiesCount = 1;
        public const int MaxThropiesCount = int.MaxValue;

        //Coach
        public const int CoachNameMinLength = 2;
        public const int CoachNameMaxLength = 40;
      

    }
}
