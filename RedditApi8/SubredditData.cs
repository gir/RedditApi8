//------------------------------------------------------------------------------
// <copyright file="SubredditData.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// SubredditData class.
    /// </summary>
    [DataContract]
    public class SubredditData : IData
    {
        /// <summary>
        /// Gets or sets the display name.
        /// The name of the subreddit as found in the subreddit's URL, header,
        /// and sidebar.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// The FULLNAME of the subreddit.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// The title of the subreddit as found in the subreddit's &lt;title&gt;
        /// tag.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// The relative URL to the subreddit.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// This is the unix time that the subreddit was created in the
        /// currently logged in user's time zone.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the created UTC.
        /// This is the unix time that the subreddit was created in UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="SubredditData" /> is over18.
        /// Indicates whether the subreddit has been marked as NSFW.
        /// </summary>
        /// <value>
        ///   <c>true</c> if over18; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "over18")]
        public bool Over18 { get; set; }

        /// <summary>
        /// Gets or sets the subscribers.
        /// The number of subscribers the subreddit currently has.
        /// </summary>
        /// <value>
        /// The subscribers.
        /// </value>
        [DataMember(Name = "subscribers")]
        public long Subscribers { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// The subreddit's id36.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// The description of the subreddit, as written by its moderators,
        /// formatted in reddit flavored markdown. Newlines are escaped as \n.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// This is the unix time that the subreddit was created in the
        /// currently logged in user's time zone.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        [DataMember(Name = "created")]
        private long Created64
        {
            get
            {
                return this.Created.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks / 10000000;
            }

            set
            {
                this.Created = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(value);
            }
        }

        /// <summary>
        /// Gets or sets the created_utc.
        /// This is the unix time that the subreddit was created in UTC.
        /// </summary>
        /// <value>
        /// The created_utc.
        /// </value>
        [DataMember(Name = "created_utc")]
        private long CreatedUtc64
        {
            get
            {
                return this.CreatedUtc.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks / 10000000;
            }

            set
            {
                this.CreatedUtc = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(value);
            }
        }
    }
}