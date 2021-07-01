using System.Collections.Generic;

namespace Indexing
{
    /// <summary>
    /// кастомный компаратор для правильного пересечения списков
    /// </summary>
    public class IntersectComparer : IEqualityComparer<(string, int, int)>
    {
        public bool Equals((string, int, int) x, (string, int, int) y)
        {
            return x.Item1 == y.Item1 && x.Item2 == y.Item2;
        }

        public int GetHashCode((string, int, int) obj)
        {
            int hashPath = obj.Item1.GetHashCode();
            int hashLine = obj.Item2.GetHashCode();
            return hashPath ^ hashLine;
        }
    }
}
