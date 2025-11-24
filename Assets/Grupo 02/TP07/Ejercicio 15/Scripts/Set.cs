using System.Collections.Generic;

// Simple wrapper so we can use Set<T>
public class Set<T> : HashSet<T>
{
    public Set() : base() { }
    public Set(IEnumerable<T> collection) : base(collection) { }
}
