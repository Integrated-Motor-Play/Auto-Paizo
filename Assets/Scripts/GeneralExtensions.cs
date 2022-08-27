using System;

public static class GeneralExtensions {
    public static T Next<T>(this T src) where T : struct {
        if (!typeof(T).IsEnum)
            throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

        var Arr = (T[])Enum.GetValues(src.GetType());

        var j = (Array.IndexOf<T>(Arr, src) + 1) % Arr.Length; // <- Modulo % Arr.Length added

        return Arr[j];
    }
}