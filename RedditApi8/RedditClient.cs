//------------------------------------------------------------------------------
// <copyright file="RedditClient.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Data.Json;

    /// <summary>
    /// RedditClient class.
    /// </summary>
    public class RedditClient
    {
        /// <summary>
        /// The time between requests in milliseconds.
        /// </summary>
        private const int TimeBetweenRequests = 2000;

        /// <summary>
        /// The cookie reddit returns at login.
        /// </summary>
        private Cookie cookie;

        /// <summary>
        /// The http client to use for all the requests.
        /// </summary>
        private HttpClient client;

        /// <summary>
        /// The http response message to use for all the responses.
        /// </summary>
        private HttpResponseMessage response;

        /// <summary>
        /// List of errors returned by the API.
        /// </summary>
        private List<string> errors;

        /// <summary>
        /// The time of next request.
        /// </summary>
        private long timeOfNextRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedditClient" /> class.
        /// </summary>
        /// <param name="userAgent">The user agent.</param>
        public RedditClient(string userAgent = "non_io_C#_RedditClient")
        {
            this.UserAgent = userAgent;
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Add("User-Agent", this.UserAgent);
            this.errors = new List<string>();
            this.IsLoggedIn = false;
            this.timeOfNextRequest = 0;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<string> Errors
        {
            get
            {
                return this.errors ?? (this.errors = new List<string>());
            }
        }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>
        /// The user agent.
        /// </value>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is logged in.
        /// </summary>
        /// <value>
        /// <c>true</c> if the user is logged in; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggedIn { get; set; }

        /// <summary>
        /// Gets or sets the modhash.
        /// </summary>
        /// <value>
        /// The modhash.
        /// </value>
        public string Modhash { get; set; }

        /// <summary>
        /// User login.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="passwd">The password.</param>
        /// <returns>Returns true if the login was successful; false otherwise.</returns>
        public async Task<bool> LoginAsync(string user, string passwd)
        {
            string loginUri = string.Format(ApiPaths.Login, user);
            HttpContent content = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("api_type", "json"),
                    new KeyValuePair<string, string>("user", user),
                    new KeyValuePair<string, string>("passwd", passwd),
                });

            this.errors.Clear();
            await this.AwaitForNextRequest();
            this.response = await this.client.PostAsync(loginUri, content);
            if (this.response.IsSuccessStatusCode)
            {
                // Parse the response into a JsonObject.
                JsonObject json = JsonObject.Parse(await this.response.Content.ReadAsStringAsync());

                // Get the errors.
                JsonArray array = json["json"].GetObject()["errors"].GetArray();
                if (array.Count > 0)
                {
                    foreach (JsonValue error in array[0].GetArray())
                    {
                        this.errors.Add(error.GetString());
                    }
                }

                // Check the errors.
                if (this.errors.Count > 0)
                {
                    return false;
                }

                // No errors so the login should be fine; set the cookie.
                this.cookie = new Cookie("reddit_session", json["json"].GetObject()["data"].GetObject()["cookie"].GetString(), "/", "reddit.com");
                this.client.DefaultRequestHeaders.Add("Cookie", this.cookie.ToString());
                this.IsLoggedIn = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// User logout.
        /// </summary>
        /// <returns>Returns true if the logout was successful; false otherwise.</returns>
        public async Task<bool> LogoutAsync()
        {
            if (this.IsLoggedIn)
            {
                string logoutUri = string.Format(ApiPaths.Logout);
                HttpContent content = new FormUrlEncodedContent(
                    new[]
                    {
                        new KeyValuePair<string, string>("api_type", "json"),
                        new KeyValuePair<string, string>("uh", this.Modhash)
                    });

                await this.AwaitForNextRequest();
                this.response = await this.client.PostAsync(logoutUri, content);
                this.cookie = null;
                this.client.DefaultRequestHeaders.Remove("Cookie");
                this.IsLoggedIn = false;
            }

            return true;
        }

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <returns>Returns AccountData.</returns>
        public async Task<AccountData> GetMeAsync()
        {
            if (this.IsLoggedIn)
            {
                return (await this.ApiGetAsync(ApiPaths.Me))[0].Data as AccountData;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the front page.
        /// </summary>
        /// <param name="additionalParams">The additional parameters.</param>
        /// <returns>
        /// Returns list of LinkData.
        /// </returns>
        public async Task<List<LinkData>> GetFrontPageAsync(Dictionary<string, string> additionalParams = null)
        {
            return await this.GetPageAsync(null, additionalParams);
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="subreddit">The subreddit.</param>
        /// <param name="additionalParams">The additional parameters.</param>
        /// <returns>
        /// Returns list of LinkData.
        /// </returns>
        public async Task<List<LinkData>> GetPageAsync(string subreddit = null, Dictionary<string, string> additionalParams = null)
        {
            // If a subreddit was specified, get that instead of the front page.
            StringBuilder uri = new StringBuilder(ApiPaths.FrontPage);
            if (!string.IsNullOrEmpty(subreddit))
            {
                uri = new StringBuilder(string.Format(ApiPaths.Subreddit, subreddit));
            }

            // Add the additional parameters.
            if (additionalParams != null && additionalParams.Count > 0)
            {
                StringBuilder query = new StringBuilder("?");
                foreach (string key in additionalParams.Keys)
                {
                    query.Append(string.Format("{0}={1}&", key, additionalParams[key]));
                }

                uri.Append(query.ToString());
            }

            // Get the links.
            return (await this.ApiGetAsync(uri.ToString()))[0].GetDataList<LinkData>();
        }

        /// <summary>
        /// Gets the comments async.
        /// </summary>
        /// <param name="id">The link id to get comments from.</param>
        /// <param name="commentId">The comment id.</param>
        /// <returns>Returns list of comments.</returns>
        public async Task<Tuple<LinkData, List<CommentData>>> GetCommentsAsync(string id, string commentId = null)
        {
            string uri;
            if (string.IsNullOrEmpty(commentId))
            {
                uri = string.Format(ApiPaths.Comments, id);
            }
            else
            {
                uri = string.Format(ApiPaths.MoreComments, id, commentId);
            }

            List<Thing> result = await this.ApiGetAsync(uri.ToString());
            if (result != null)
            {
                return Tuple.Create<LinkData, List<CommentData>>(((ListingData)result[0].Data).Children[0].Data as LinkData, result[1].GetDataList<CommentData>());
            }
            else
            {
                return Tuple.Create<LinkData, List<CommentData>>(null, null);
            }
        }

        /// <summary>
        /// API GET.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>Thing object if GET was successful; otherwise, null.</returns>
        private async Task<List<Thing>> ApiGetAsync(string uri)
        {
            await this.AwaitForNextRequest();
            this.response = await this.client.GetAsync(uri);
            if (this.response.IsSuccessStatusCode)
            {
                // Deserialize the response.
                return Thing.DeserializeList(await this.response.Content.ReadAsStreamAsync());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Awaits for when you can make next request.
        /// </summary>
        /// <returns>Returns a task.</returns>
        private async Task AwaitForNextRequest()
        {
            // Check to see if it's okay to make another request.
            while (this.timeOfNextRequest > DateTime.Now.Ticks)
            {
                await Task.Delay(100);
            }

            // Set the time of next request.
            this.timeOfNextRequest = DateTime.Now.AddMilliseconds(TimeBetweenRequests).Ticks;
        }
    }
}