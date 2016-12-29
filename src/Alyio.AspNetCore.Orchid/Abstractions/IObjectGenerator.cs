using System;

namespace Orchid.Abstractions
{
    /// <summary>
    /// This class will create an object of a given type and populate it with sample data.
    /// </summary>
    public interface IObjectGenerator
    {
        /// <summary>
        /// Generates an object for a given type. The type needs to be public, have a public default constructor and settable public properties/fields. Currently it supports the following types:
        /// Simple types: <see cref="int"/>, <see cref="string"/>, <see cref="Enum"/>, <see cref="DateTime"/>, <see cref="Uri"/>, etc.
        /// Complex types: POCO types.
        /// Nullables: <see cref="Nullable{T}"/>.
        /// Arrays: arrays of simple types or complex types.
        /// Key value pairs: <see cref="KeyValuePair{TKey,TValue}"/>
        /// Tuples: <see cref="Tuple{T1}"/>, <see cref="Tuple{T1,T2}"/>, etc
        /// Dictionaries: <see cref="IDictionary{TKey,TValue}"/> or anything deriving from <see cref="IDictionary{TKey,TValue}"/>.
        /// Collections: <see cref="IList{T}"/>, <see cref="IEnumerable{T}"/>, <see cref="ICollection{T}"/>, <see cref="IList"/>, <see cref="IEnumerable"/>, <see cref="ICollection"/> or anything deriving from <see cref="ICollection{T}"/> or <see cref="IList"/>.
        /// Queryables: <see cref="IQueryable"/>, <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>An object of the given type.</returns>
        object GenerateObject(Type type);
    }
}
