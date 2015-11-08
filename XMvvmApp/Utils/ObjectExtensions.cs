using System.Linq;

namespace XMvvmApp.Utils
{
    public static class ObjectExtensions
	{
		public static bool IsWithin<T>(this T obj, 
            params T[] values)
        {
            return values.Contains(obj);
        }
	}
}
