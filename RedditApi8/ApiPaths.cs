//------------------------------------------------------------------------------
// <copyright file="ApiPaths.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ApiPaths class.
    /// </summary>
    public static class ApiPaths
    {
        /// <summary>
        /// Login path.
        /// </summary>
        public const string Login = SecureDomain + "api/login/{0}";

        /// <summary>
        /// Me path.
        /// </summary>
        public const string Me = Domain + "api/me.json";

        /// <summary>
        /// Subreddit path.
        /// </summary>
        public const string Subreddit = Domain + "r/{0}/.json";

        /// <summary>
        /// Front page path.
        /// </summary>
        public const string FrontPage = Domain + ".json";

        /// <summary>
        /// Reddit domain.
        /// </summary>
        private const string Domain = "http://www.reddit.com/";

        /// <summary>
        /// Secure Reddit domain.
        /// </summary>
        private const string SecureDomain = "https://ssl.reddit.com/";
    }
}