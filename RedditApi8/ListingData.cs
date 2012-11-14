//------------------------------------------------------------------------------
// <copyright file="ListingData.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Data.Json;

    /// <summary>
    /// ListingKind class.
    /// </summary>
    [DataContract]
    public class ListingData : IData
    {
        /// <summary>
        /// Gets or sets the modhash.
        /// The page's modhash needed for voting and other API calls.
        /// </summary>
        /// <value>
        /// The modhash.
        /// </value>
        [DataMember(Name = "modhash")]
        public string Modhash { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// An array that holds an object for each post.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        [DataMember(Name = "children")]
        public List<Thing> Children { get; set; }

        /// <summary>
        /// Gets or sets the after.
        /// If not null, it is the FULLNAME of the post previous to the first
        /// one in children if what the API returns is paginated.
        /// </summary>
        /// <value>
        /// The after.
        /// </value>
        [DataMember(Name = "after")]
        public string After { get; set; }

        /// <summary>
        /// Gets or sets the before.
        /// If not null, it is the FULLNAME (see glossary on the API page) of the
        /// post after the last one in children if what the API returns is paginated.
        /// </summary>
        /// <value>
        /// The before.
        /// </value>
        [DataMember(Name = "before")]
        public string Before { get; set; }
    }
}