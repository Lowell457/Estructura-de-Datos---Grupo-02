using System.Collections.Generic;

public class Set<T> : HashSet<T>
{
    public Set() : base() { }
    public Set(IEnumerable<T> collection) : base(collection) { }
}
