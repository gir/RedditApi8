//------------------------------------------------------------------------------
// <copyright file="MoreData.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// MoreData class.
    /// </summary>
    [DataContract]
    public class MoreData : IData
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [DataMember(Name = "count")]
        public long? Count { get; set; }

        /*
          I don't think this is needed and it just messes everything up anyway.
        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        [DataMember(Name = "children")]
        public List<string> Children { get; set; }
        */

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}