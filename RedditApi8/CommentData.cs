//------------------------------------------------------------------------------
// <copyright file="CommentData.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// CommentData class.
    /// </summary>
    [DataContract]
    public class CommentData : IData
    {
        /// <summary>
        /// Gets or sets the subreddit id.
        /// </summary>
        /// <value>
        /// The subreddit id.
        /// </value>
        [DataMember(Name = "subreddit_id")]
        public string SubredditId { get; set; }

        /// <summary>
        /// Gets or sets the banned by.
        /// </summary>
        /// <value>
        /// The banned by.
        /// </value>
        [DataMember(Name = "banned_by")]
        public string BannedBy { get; set; }

        /// <summary>
        /// Gets or sets the link id.
        /// </summary>
        /// <value>
        /// The link id.
        /// </value>
        [DataMember(Name = "link_id")]
        public string LinkId { get; set; }

        /// <summary>
        /// Gets or sets the likes.
        /// </summary>
        /// <value>
        /// The likes.
        /// </value>
        [DataMember(Name = "likes")]
        public string Likes { get; set; }

        /// <summary>
        /// Gets or sets the replies.
        /// </summary>
        /// <value>
        /// The replies.
        /// </value>
        [DataMember(Name = "replies")]
        public List<Thing> Replies { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CommentData" /> is gilded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if gilded; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "gilded")]
        public bool Gilded { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        [DataMember(Name = "author")]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        /// <value>
        /// The parent id.
        /// </value>
        [DataMember(Name = "parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the approved by.
        /// </summary>
        /// <value>
        /// The approved by.
        /// </value>
        [DataMember(Name = "approved_by")]
        public string ApprovedBy { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        [DataMember(Name = "body")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the edited.
        /// </summary>
        /// <value>
        ///   <c>null</c> if not edited; otherwise, returns a timestamp of ticks since the epoch.
        /// </value>
        public DateTime? Edited { get; set; }

        /// <summary>
        /// Gets or sets the author flair CSS class.
        /// </summary>
        /// <value>
        /// The author flair CSS class.
        /// </value>
        [DataMember(Name = "author_flair_css_class")]
        public string AuthorFlairCssClass { get; set; }

        /// <summary>
        /// Gets or sets the downs.
        /// </summary>
        /// <value>
        /// The downs.
        /// </value>
        [DataMember(Name = "downs")]
        public long Downs { get; set; }

        /// <summary>
        /// Gets or sets the body HTML.
        /// </summary>
        /// <value>
        /// The body HTML.
        /// </value>
        [DataMember(Name = "body_html")]
        public string BodyHtml { get; set; }

        /// <summary>
        /// Gets or sets the subreddit.
        /// </summary>
        /// <value>
        /// The subreddit.
        /// </value>
        [DataMember(Name = "subreddit")]
        public string Subreddit { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the author flair text.
        /// </summary>
        /// <value>
        /// The author flair text.
        /// </value>
        [DataMember(Name = "author_flair_text")]
        public string AuthorFlairText { get; set; }

        /// <summary>
        /// Gets or sets the created UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the num reports.
        /// </summary>
        /// <value>
        /// The num reports.
        /// </value>
        [DataMember(Name = "num_reports")]
        public long? NumReports { get; set; }

        /// <summary>
        /// Gets or sets the ups.
        /// </summary>
        /// <value>
        /// The ups.
        /// </value>
        [DataMember(Name = "ups")]
        public long Ups { get; set; }

        /// <summary>
        /// Gets or sets the created.
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

        /// <summary>
        /// Gets or sets the edited object.
        /// </summary>
        /// <value>
        /// The edited object.
        /// </value>
        [DataMember(Name = "edited")]
        private object EditedObject
        {
            get
            {
                if (this.Edited == null)
                {
                    return null;
                }

                return ((DateTime)this.Edited).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks / 10000000;
            }

            set
            {
                if (value.GetType() == typeof(bool))
                {
                    this.Edited = null;
                }

                this.Edited = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToInt64(value));
            }
        }
    }
}