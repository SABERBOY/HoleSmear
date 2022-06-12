using System;
using System.Globalization;

public static class SignificantDigits
{
    public static decimal CeilingToSignificantFigures(decimal num, int n)
    {
        if (num == 0) return 0;

        // We are only looking for the next power of 10... 
        // The double conversion could impact in some corner cases,
        // but I'm not able to construct them...
        var d = (int)Math.Ceiling(Math.Log10((double)Math.Abs(num)));
        var power = n - d;

        // Same here, Math.Pow(10, *) is an integer number
        var magnitude = (decimal)Math.Pow(10, power);

        // I'm using the MidpointRounding.AwayFromZero . I'm not sure
        // having a MidpointRounding.ToEven would be useful (is Banker's
        // rounding used for significant figures?)
        var shifted = Math.Round(Math.Ceiling(num * magnitude), 0, MidpointRounding.AwayFromZero);
        //return num * magnitude;
        var ret = shifted / magnitude;

        return num >= 0 ? ret : -ret;
    }

    public static decimal RoundToSignificantFigures(decimal num, int n)
    {
        if (num == 0) return 0;

        // We are only looking for the next power of 10... 
        // The double conversion could impact in some corner cases,
        // but I'm not able to construct them...
        var d = (int)Math.Ceiling(Math.Log10((double)Math.Abs(num)));
        var power = n - d;

        // Same here, Math.Pow(10, *) is an integer number
        var magnitude = (decimal)Math.Pow(10, power);

        // I'm using the MidpointRounding.AwayFromZero . I'm not sure
        // having a MidpointRounding.ToEven would be useful (is Banker's
        // rounding used for significant figures?)
        var shifted = Math.Round(num * magnitude, 0, MidpointRounding.AwayFromZero);
        //return num * magnitude;
        var ret = shifted / magnitude;

        return num >= 0 ? ret : -ret;
    }
}