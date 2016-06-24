using System.Collections.Generic;

namespace AspNetX.Initialization
{
    public interface IExtensionProvider<T>
        where T : class
    {
        IEnumerable<T> Instances { get; }
    }
}
