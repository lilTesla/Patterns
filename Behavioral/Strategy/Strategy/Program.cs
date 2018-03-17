using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>();
            list.Sort(new ComparerDelegate<int>((x, y) => x - y));//using without factory

            list.Sort(ComparerFactory.Create<int>((x, y) => x - y));//using with factory
        }
        public class ComparerDelegate<T> : IComparer<T>//without factory
        {
            readonly Comparison<T> _compare;
            public ComparerDelegate(Comparison<T> del)
            {
                _compare = del;
            }
            public int Compare(T x, T y)
            {
                return _compare(x, y);
            }
        }
        public class ComparerFactory//with factory
        {
            public static IComparer<T> Create<T>(Comparison<T> del)
            {
                return new DelegateComparer<T>(del);
            }
            private class DelegateComparer<T> : IComparer<T>
            {
                private readonly Comparison<T> _compare;
                public DelegateComparer(Comparison<T> compare)
                {
                    _compare = compare;
                }

                public int Compare(T x, T y)
                {
                    return _compare(x, y);
                }
            }
        }
    }
}
