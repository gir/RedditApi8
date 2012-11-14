//------------------------------------------------------------------------------
// <copyright file="AccountData.cs" company="non.io">
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
    /// AccountData class.
    /// </summary>
    [DataContract]
    public class AccountData : IData
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance has mail.
        /// Says whether the user has unread mail from messaging or comment/post
        /// replies.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has mail; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "has_mail")]
        public bool HasMail { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// The user's username.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// Unix time that the user's account was created in the time zone the
        /// server is set to.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the modhash.
        /// The user's modhash.
        /// </summary>
        /// <value>
        /// The modhash.
        /// </value>
        [DataMember(Name = "modhash")]
        public string Modhash { get; set; }

        /// <summary>
        /// Gets or sets the created UTC.
        /// Unix time that the user's account was created in UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the link karma.
        /// The user's link karma.
        /// </summary>
        /// <value>
        /// The link karma.
        /// </value>
        [DataMember(Name = "link_karma")]
        public long? LinkKarma { get; set; }

        /// <summary>
        /// Gets or sets the comment karma.
        /// The user's comment karma.
        /// </summary>
        /// <value>
        /// The comment karma.
        /// </value>
        [DataMember(Name = "comment_karma")]
        public long? CommentKarma { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is gold.
        /// Says whether the user is a reddit gold member.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is gold; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "is_gold")]
        public bool IsGold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mod.
        /// Says whether the user is a moderator.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is mod; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "is_mod")]
        public bool IsMod { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// The user's ID.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has mod mail.
        /// Says whether the user has unread moderator mail.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has mod mail; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "has_mod_mail")]
        public bool HasModMail { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// Unix time that the user's account was created in the time zone the
        /// server is set to.
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
        /// Unix time that the user's account was created in UTC.
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