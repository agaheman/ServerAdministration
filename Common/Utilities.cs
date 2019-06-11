using System;
using System.Collections;
using System.Linq;

namespace Common.Utilities
{
    public static class Assert
    {
        public static void NotNull<T>(T obj, string name, string message = null)
            where T : class
        {
            if (obj is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        public static void NotNull<T>(T? obj, string name, string message = null)
            where T : struct
        {
            if (!obj.HasValue)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);

        }

        public static void NotEmpty<T>(T obj, string name, string message = null, T defaultValue = null)
            where T : class
        {
            if (obj == defaultValue
                || (obj is string str && string.IsNullOrWhiteSpace(str))
                || (obj is IEnumerable list && !list.Cast<object>().Any()))
            {
                throw new ArgumentException("Argument is empty : " + message, $"{name} : {typeof(T)}");
            }
        }
    }

    public class DigitalStorage
    {
        public static string ByteToHumanReadableSize(long byteCount)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = byteCount;
            var order = 0;
            while (byteCount >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }


}
