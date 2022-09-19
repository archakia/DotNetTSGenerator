using System.Collections.Generic;

namespace lib.test.a.b
{
    public interface ITestGenericClass<T>
    {
        T GetT(T input);
        IEnumerable<int> data { get; set; }
    }
}
