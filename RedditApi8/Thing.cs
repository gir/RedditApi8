//------------------------------------------------------------------------------
// <copyright file="Thing.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Thing class.
    /// </summary>
    [DataContract]
    public class Thing
    {
        /// <summary>
        /// Thing kinds.
        /// </summary>
        public enum ThingKind
        {
            /// <summary>
            /// Listing kind.
            /// </summary>
            Listing,

            /// <summary>
            /// Comment kind.
            /// </summary>
            t1,

            /// <summary>
            /// Account kind.
            /// </summary>
            t2,

            /// <summary>
            /// Link kind.
            /// </summary>
            t3,

            /// <summary>
            /// Message kind.
            /// </summary>
            t4,

            /// <summary>
            /// Subreddit kind.
            /// </summary>
            t5,

            /// <summary>
            /// More kind.
            /// </summary>
            more,
        }

        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        /// <value>
        /// The kind.
        /// </value>
        public ThingKind Kind { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// This will contain the more specific type of data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IData Data { get; set; }

        /// <summary>
        /// Gets or sets the kind of the internal.
        /// </summary>
        /// <value>
        /// The kind of the internal.
        /// </value>
        [DataMember(Name = "kind")]
        private string InternalKind
        {
            get
            {
                return this.Kind.ToString();
            }

            set
            {
                switch ((ThingKind)Enum.Parse(typeof(ThingKind), value))
                {
                    case ThingKind.Listing:
                        this.Kind = ThingKind.Listing;
                        break;
                    case ThingKind.t1:
                        this.Kind = ThingKind.t1;
                        break;
                    case ThingKind.t2:
                        this.Kind = ThingKind.t2;
                        break;
                    case ThingKind.t3:
                        this.Kind = ThingKind.t3;
                        break;
                    case ThingKind.t4:
                        this.Kind = ThingKind.t4;
                        break;
                    case ThingKind.t5:
                        this.Kind = ThingKind.t5;
                        break;
                    case ThingKind.more:
                        this.Kind = ThingKind.more;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the internal data.
        /// </summary>
        /// <value>
        /// The internal data.
        /// </value>
        [DataMember(Name = "data")]
        private Data InternalData { get; set; }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>Returns a list of deserialized Things.</returns>
        public static List<Thing> DeserializeList(Stream stream)
        {
            // Some times (like in the case of comments) the json comes back as
            // an array of Things. The first thing is the parent Thing, then the
            // rest is what you actually wanted.
            try
            {
                string s = (new StreamReader(stream)).ReadToEnd();
                stream.Position = 0;
                if (s[0] == '[')
                {
                    // Comments with no replies comes back as a string, get rid of it.
                    s = s.Replace("\"replies\": \"\",", string.Empty);

                    // Sometimes children is just a list of ids, get rid of it.
                    s = Regex.Replace(s, @"""children"": \["".+?""\],", string.Empty);
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Thing>));
                    return (List<Thing>)serializer.ReadObject(s.ToStream());
                }
                else
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Thing));
                    List<Thing> result = new List<Thing>();
                    result.Add((Thing)serializer.ReadObject(stream));
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the data list.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <returns>
        /// Returns a list of data.
        /// </returns>
        public List<T> GetDataList<T>() where T : class, IData
        {
            List<T> list = new List<T>();
            foreach (Thing child in ((ListingData)this.Data).Children)
            {
                if (child.Data is T)
                {
                    list.Add((T)child.Data);
                }
            }

            return list;
        }

        /// <summary>
        /// Called when [deserialized].
        /// </summary>
        /// <param name="context">The context.</param>
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            if (this.Kind == null)
            {
                return;
            }

            switch (this.Kind)
            {
                case ThingKind.Listing:
                    this.Data = this.InternalData.ToData<ListingData>();
                    break;
                case ThingKind.t1:
                    this.Data = this.InternalData.ToData<CommentData>();
                    break;
                case ThingKind.t2:
                    this.Data = this.InternalData.ToData<AccountData>();
                    break;
                case ThingKind.t3:
                    this.Data = this.InternalData.ToData<LinkData>();
                    break;
                case ThingKind.t4:
                    this.Data = this.InternalData.ToData<MessageData>();
                    break;
                case ThingKind.t5:
                    this.Data = this.InternalData.ToData<SubredditData>();
                    break;
                case ThingKind.more:
                    this.Data = this.InternalData.ToData<MoreData>();
                    break;
            }
        }
    }
}