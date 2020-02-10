using System;

namespace Bsoft.Common
{
    public class MeasurementUnit
    {
        public MeasurementUnit(int unitID, string unitName, double sqFeetPerUnit)
        {
            UnitID = unitID;
            UnitName = unitName;
            SQFeetPerUnit = sqFeetPerUnit;
        }

        public int UnitID = 0;
        public string UnitName = string.Empty;
        public double SQFeetPerUnit = 0;
    }

    public static class LengthConvert
    {
        public static decimal InchToMeter(decimal Inch)
        {
            decimal Meter = 0;
            Meter = (decimal)(0.304803319025215 / 12) * Inch;
            return Math.Round(Meter, 4);
        }

        public static decimal InchToMeter(string Inch)
        {
            return InchToMeter(Convert.ToDecimal(Inch));
        }

        public static decimal InchToFeet(decimal Inch)
        {
            decimal Feet = 0;
            Feet = Inch / 12;
            return Math.Round(Feet, 4);
        }

        public static decimal InchToFeet(string Inch)
        {
            return InchToFeet(Convert.ToDecimal(Inch));
        }

        public static decimal MeterToInch(decimal Meter)
        {
            decimal Inch = 0;
            Inch = (decimal)(1 / 0.304803319025215) * 12 * Meter;
            return Math.Round(Inch, 4); ;
        }

        public static decimal MeterToInch(string Meter)
        {
            return MeterToInch(Convert.ToDecimal(Meter));
        }

        public static decimal MeterToFeet(decimal Meter)
        {
            decimal Feet = 0;
            Feet = (decimal)(1 / 0.304803319025215) * Meter;
            return Math.Round(Feet, 4);
        }

        public static decimal MeterToFeet(string Meter)
        {
            return MeterToFeet(Convert.ToDecimal(Meter));
        }

        public static decimal FeetToInch(decimal Feet)
        {
            decimal Inch = 0;
            Inch = 12 * Feet;
            return Math.Round(Inch, 4);
        }

        public static decimal FeetToInch(string Feet)
        {
            return FeetToInch(Convert.ToDecimal(Feet));
        }

        public static decimal FeetToMeter(decimal Feet)
        {
            decimal Meter;
            Meter = (decimal)(0.304803319025215) * Feet;
            return Math.Round(Meter, 4);
        }

        public static decimal FeetToMeter(string Feet)
        {
            return FeetToMeter(Convert.ToDecimal(Feet));
        }
    }
}