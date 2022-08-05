namespace TeisterMask.Common
{
    public static class EntityValidations
    {
        //Employee
        public const string UserNameRgex = @"^[A-Za-z0-9]+$";
        public const int EmployeeUserNameMinLength = 3;
        public const int EmployeeUserNameMaxLength = 40;
        public const int PhoneNumberMaxLength = 12;
        public const string EmployeePhoneRegex = @"^([0-9]{3})-([0-9]{3})-([0-9]{4})$";

        //Project
        public const int ProjectNameMinLength = 2;
        public const int ProjectNameMaxLength = 40;

        //Task
        public const int TaskNameMinLength = 2;
        public const int TaskNameMaxLength = 40;
    }
}
