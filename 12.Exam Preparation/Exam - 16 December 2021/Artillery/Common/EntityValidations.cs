namespace Artillery.Common
{
    public static class EntityValidations
    {
        //Country
        public const int CountryNameMinLength = 4;
        public const int CountryNameMaxLength = 60;
        public const int MinArmySize = 50_000;
        public const int MaxArmySize = 10_000_000;

        //Manufacturer
        public const int ManufacturerNameMinLength = 4;
        public const int ManufacturerNameMaxLength = 40;
        public const int FoundedTextMinLength = 10;
        public const int FoundedTextMaxLength = 100;

        //Shell
        public const int MinShellWeight = 2;
        public const int MaxShellWeight = 1_680;
        public const int MinCaliberLength = 4;
        public const int MaxCaliberLength = 30;

        //Gun
        public const int MinGunWeight = 100;
        public const int MaxGunWeight = 1_350_000;
        public const double MinBarrelLength = 2;
        public const double MaxBarrelLength = 35;
        public const int GunMinRange = 1;
        public const int GunMaxRange = 100_000;
    }
}
