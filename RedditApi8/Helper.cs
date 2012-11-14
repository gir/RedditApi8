//------------------------------------------------------------------------------
// <copyright file="Helper.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Helper class.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Converts the string to a stream.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns>Returns a stream.</returns>
        public static Stream ToStream(this string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Converts an IData object to a more specific implementation.
        /// </summary>
        /// <typeparam name="T">The generic type to convert to.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>Returns T.</returns>
        public static T ToData<T>(this IData data)
        {
            TypeInfo toTypeInfo = typeof(T).GetTypeInfo();
            TypeInfo fromType = data.GetType().GetTypeInfo();
            T toData = (T)Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo propertyInfo in toTypeInfo.DeclaredProperties)
            {
                // Check to see if the property is Private.
                // Unfortunately WinRT didn't implement a way to get this, do it
                // by hand.
                if (!propertyInfo.Name.Equals("EditedObject")
                    && !propertyInfo.Name.Equals("Created64")
                    && !propertyInfo.Name.Equals("CreatedUtc64"))
                {
                    propertyInfo.SetValue(toData, fromType.GetDeclaredProperty(propertyInfo.Name).GetValue(data));
                }
            }

            return toData;
        }
    }
}