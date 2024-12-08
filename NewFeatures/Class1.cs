using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NewFeatures
{
    public static class Class1
    {
        internal static bool TryGetSpan<TSource>(this IEnumerable<TSource> source, out ReadOnlySpan<TSource> span)
        {
            // Use `GetType() == typeof(...)` rather than `is` to avoid cast helpers.  This is measurably cheaper
            // but does mean we could end up missing some rare cases where we could get a span but don't (e.g. a uint[]
            // masquerading as an int[]).  That's an acceptable tradeoff.  The Unsafe usage is only after we've
            // validated the exact type; this could be changed to a cast in the future if the JIT starts to recognize it.
            // We only pay the comparison/branching costs here for super common types we expect to be used frequently
            // with LINQ methods.

            bool result = true;
            if (source.GetType() == typeof(TSource[]))
            {
                span = Unsafe.As<TSource[]>(source);
            }
            else if (source.GetType() == typeof(List<TSource>))
            {   
                span = CollectionsMarshal.AsSpan(Unsafe.As<List<TSource>>(source));
            }
            else
            {
                span = default;
                result = false;
            }

            return result;
        }
    }
}
