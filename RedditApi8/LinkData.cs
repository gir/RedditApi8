//------------------------------------------------------------------------------
// <copyright file="LinkData.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// LinkKind class.
    /// </summary>
    [DataContract]
    public class LinkData : IData
    {
        /// <summary>
        /// Gets or sets the domain.
        /// The domain of the page that was submitted, not including the protocol.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        [DataMember(Name = "domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the banned by.
        /// </summary>
        /// <value>
        /// The banned by.
        /// </value>
        [DataMember(Name = "banned_by")]
        public string BannedBy { get; set; }

        /// <summary>
        /// Gets or sets the subreddit.
        /// The name of the subreddit this post was submitted to.
        /// </summary>
        /// <value>
        /// The subreddit.
        /// </value>
        [DataMember(Name = "subreddit")]
        public string Subreddit { get; set; }

        /// <summary>
        /// Gets or sets the self text HTML.
        /// The HTML-formatted text of the post if this was a self post. posts, it is always null.
        /// </summary>
        /// <value>
        /// The self text HTML.
        /// </value>
        [DataMember(Name = "selftext_html")]
        public string SelfTextHtml { get; set; }

        /// <summary>
        /// Gets or sets the self text.
        /// The plaintext of the post if this was a self post.
        /// </summary>
        /// <value>
        /// The self text.
        /// </value>
        [DataMember(Name = "selftext")]
        public string SelfText { get; set; }

        /// <summary>
        /// Gets or sets the likes.
        /// If the reddit_session cookie is not present in the request, the API will return null. Otherwise, this will indicate how the currently logged in user has voted the story: true for an up vote, false for a down vote, or null for no vote.
        /// </summary>
        /// <value>
        /// The likes.
        /// </value>
        [DataMember(Name = "likes")]
        public bool? Likes { get; set; }

        /// <summary>
        /// Gets or sets the link flair text.
        /// </summary>
        /// <value>
        /// The link flair text.
        /// </value>
        [DataMember(Name = "link_flair_text")]
        public string LinkFlairText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LinkData" /> is saved.
        /// If the reddit_session cookie is not present in the request, the API will return false. Otherwise, this will indicate whether the currently logged in user has saved the story.
        /// </summary>
        /// <value>
        ///   <c>true</c> if saved; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "saved")]
        public bool Saved { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// The link's id36.
        /// This is also used for short URLs (e.g. http://redd.it/6nw57) and toolbar URLs (e.g. http://www.reddit.com/tb/6nw57/).
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LinkData" /> is clicked.
        /// Seems to always be false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if clicked; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "clicked")]
        public bool Clicked { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// The username of the user who submitted the post.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        [DataMember(Name = "author")]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the num reports.
        /// </summary>
        /// <value>
        /// The num reports.
        /// </value>
        [DataMember(Name = "num_reports")]
        public long? NumReports { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// The current score (based on up votes minus down votes) of the post.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        [DataMember(Name = "score")]
        public long? Score { get; set; }

        /// <summary>
        /// Gets or sets the approved by.
        /// </summary>
        /// <value>
        /// The approved by.
        /// </value>
        [DataMember(Name = "approved_by")]
        public string ApprovedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LinkData" /> is over_18.
        /// Says whether the post has been marked NSFW.
        /// </summary>
        /// <value>
        ///   <c>true</c> if over_18; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "over_18")]
        public bool Over18 { get; set; }

        /// <summary>
        /// Gets or sets the hidden.
        /// If the reddit_session cookie is not present in the request, the API will return false. Otherwise, this will indicate whether the currently logged in user has hidden the story.
        /// </summary>
        /// <value>
        /// The hidden.
        /// </value>
        [DataMember(Name = "hidden")]
        public bool? Hidden { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// If the reddit_session cookie is present in the request and the post has been marked NSFW and the user does have the "make safe(r) for work" preference checked, the API will return "/static/nsfw2.png", which is 70px square.
        /// Otherwise, if the reddit_session cookie is not present in the request and the post has been marked NSFW, the API will return "".
        /// Otherwise, if the post is a self post, the API will return "/static/self_default2.png", which is 70px wide by 50px tall.  However, since info.json always serves information about link posts, it will never return this.
        /// Otherwise, if the post has no thumbnail, the API will return "/static/noimage.png", which is 70px wide by 50px tall.
        /// Otherwise, the API will return a full URL to a thumbnail that is 70px wide.
        /// </summary>
        /// <value>
        /// The thumbnail.
        /// </value>
        [DataMember(Name = "thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the subreddit_id.
        /// This is the ID of the subreddit this post was submitted to. This is only used internally?
        /// </summary>
        /// <value>
        /// The subreddit_id.
        /// </value>
        [DataMember(Name = "subreddit_id")]
        public string SubredditId { get; set; }

        /// <summary>
        /// Gets or sets the edited.
        /// </summary>
        /// <value>
        ///   <c>null</c> if not edited; otherwise, returns a timestamp of ticks since the epoch.
        /// </value>
        public DateTime? Edited { get; set; }

        /// <summary>
        /// Gets or sets the link flair CSS class.
        /// </summary>
        /// <value>
        /// The link flair CSS class.
        /// </value>
        [DataMember(Name = "link_flair_css_class")]
        public string LinkFlairCssClass { get; set; }

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
        /// This is the number of down votes the post has gotten, according to reddit.
        /// </summary>
        /// <value>
        /// The downs.
        /// </value>
        [DataMember(Name = "downs")]
        public long? Downs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LinkData" /> is is_self.
        /// Says whether a post is a self post.
        /// </summary>
        /// <value>
        ///   <c>true</c> if is_self; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "is_self")]
        public bool IsSelf { get; set; }

        /// <summary>
        /// Gets or sets the permalink.
        /// Holds a relative URL to the comments page for the post.
        /// </summary>
        /// <value>
        /// The permalink.
        /// </value>
        [DataMember(Name = "permalink")]
        public string Permalink { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// The FULLNAME of the post. It is also kind + _ + id.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// If the reddit_session cookie is not present in the request, this is the unix time that the post was created in UTC.
        /// Otherwise, this is the unix time that the post was created in the currently logged in user's time zone.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// The full URL of the page the post is about, including protocol.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the author flair text.
        /// </summary>
        /// <value>
        /// The author flair text.
        /// </value>
        [DataMember(Name = "author_flair_text")]
        public string AuthorFlairText { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// The title of the post, as written by the submitter of the post.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the created UTC.
        /// This is the unix time that the post was created in UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the num_comments.
        /// The number of comments the post currently has.
        /// </summary>
        /// <value>
        /// The num_comments.
        /// </value>
        [DataMember(Name = "num_comments")]
        public long? NumComments { get; set; }

        /// <summary>
        /// Gets or sets the ups.
        /// This is the number of up votes the post has gotten, according to reddit.
        /// </summary>
        /// <value>
        /// The ups.
        /// </value>
        [DataMember(Name = "ups")]
        public long? Ups { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// If the reddit_session cookie is not present in the request, this is the unix time that the post was created in UTC.
        /// Otherwise, this is the unix time that the post was created in the currently logged in user's time zone.
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
        /// This is the unix time that the post was created in UTC.
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
                if (value is bool)
                {
                    this.Edited = null;
                }

                this.Edited = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToInt64(value));
            }
        }
    }
}