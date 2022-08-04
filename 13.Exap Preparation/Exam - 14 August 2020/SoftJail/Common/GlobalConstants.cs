using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.Common
{
    public static class GlobalConstants
    {
        //Prisoner
        public const int PrisonerFullNameMinLength = 3;
        public const int PrisonerFullNameMaxLength = 20;
        public const int PrisonerMinAge = 18;
        public const int PrisonerMaxAge = 65;
        public const double BailMinAmount = 0;
        public const double BailMaxAmount = double.MaxValue;

        //Officer
        public const int OfficerFullNameMinLength = 3;
        public const int OfficerFullNameMaxLength = 30;
        public const double SalaryMinAmount = 0;
        public const double SalaryMaxAmount = double.MaxValue;

        //Department
        public const int DepartmenNameMinLength = 3;
        public const int DepartmenNameMaxLength = 25;

        //Cell
        public const int CellNumberMin = 1;
        public const int CellNumberMax = 1000;
    }
}
