//------------------------------------------------------------------------------
// <copyright file="RedditClientTest.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    /// <summary>
    /// RedditClientTest class.
    /// </summary>
    [TestClass]
    public class RedditClientTest
    {
        /// <summary>
        /// The client
        /// </summary>
        private RedditClient client;

        /// <summary>
        /// The fake HTTP server
        /// </summary>
        private FakeHttpServer fakeHttpServer;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestInitialize]
        public async Task Initialize()
        {
            this.client = new RedditClient();
            this.fakeHttpServer = new FakeHttpServer();
            await this.fakeHttpServer.Initialize();
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            this.client = null;
            this.fakeHttpServer.Cleanup();
        }

        /// <summary>
        /// Tests successful login.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestSuccessfulLogin()
        {
            this.fakeHttpServer.ResponseContent = "{\"json\": {\"errors\": [], \"data\": {\"modhash\": \"927obqh4xeded22e6ec4b1994d81ac1719118d25a848f59af2\", \"cookie\": \"7380190,2012-11-14T13:23:24,45ac326604d67f49f05a3120a4a0df35066f9ddb\"}}}";
            Assert.IsTrue(await this.client.LoginAsync("user", "passwd"));
        }

        /// <summary>
        /// Tests unsuccessful login.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestUnsuccessfulLogin()
        {
            this.fakeHttpServer.ResponseContent = "{\"json\": {\"errors\": [[\"WRONG_PASSWORD\", \"invalid password\", \"passwd\"]]}}";
            Assert.IsFalse(await this.client.LoginAsync("user", "passwd"));
            Assert.IsTrue(this.client.Errors.Count > 0);
        }

        /// <summary>
        /// Tests GetMeAsync.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestGetMeAsync()
        {
            // First sign in.
            this.fakeHttpServer.ResponseContent = "{\"json\": {\"errors\": [], \"data\": {\"modhash\": \"927obqh4xeded22e6ec4b1994d81ac1719118d25a848f59af2\", \"cookie\": \"7380190,2012-11-14T13:23:24,45ac326604d67f49f05a3120a4a0df35066f9ddb\"}}}";
            Assert.IsTrue(await this.client.LoginAsync("user", "passwd"));

            // Now try to get me.
            this.fakeHttpServer.ResponseContent = "{\"kind\": \"t2\", \"data\": {\"has_mail\": false, \"name\": \"username\", \"created\": 1213716360.0, \"modhash\": \"f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0\", \"created_utc\": 1213716360.0, \"link_karma\": 5000, \"comment_karma\": 10000, \"is_gold\": false, \"is_mod\": true, \"id\": \"0ffff\", \"has_mod_mail\": false}}";
            Assert.IsTrue(((AccountData)(await this.client.GetMeAsync())).Name.Equals("username"));
        }

        /// <summary>
        /// Tests GetMeAsync without logging in.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestGetMeAsyncWithoutLoggingIn()
        {
            this.fakeHttpServer.ResponseContent = "{\"kind\": \"t2\", \"data\": {\"has_mail\": false, \"name\": \"username\", \"created\": 1213716360.0, \"modhash\": \"f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0\", \"created_utc\": 1213716360.0, \"link_karma\": 5000, \"comment_karma\": 10000, \"is_gold\": false, \"is_mod\": true, \"id\": \"0ffff\", \"has_mod_mail\": false}}";
            Assert.IsTrue((await this.client.GetMeAsync()) == null);
        }

        /// <summary>
        /// Tests GetFrontPageAsync.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestGetFrontPageAsync()
        {
            this.fakeHttpServer.ResponseContent = "{\"kind\": \"Listing\", \"data\": {\"modhash\": \"\", \"children\": [{\"kind\": \"t3\", \"data\": {\"domain\": \"imgur.com\", \"banned_by\": null, \"media_embed\": {}, \"subreddit\": \"funny\", \"selftext_html\": null, \"selftext\": \"\", \"likes\": null, \"link_flair_text\": null, \"id\": \"136ypz\", \"clicked\": false, \"title\": \"And it's gonna stay blue\", \"num_comments\": 75, \"score\": 1834, \"approved_by\": null, \"over_18\": false, \"hidden\": false, \"thumbnail\": \"\", \"subreddit_id\": \"t5_2qh33\", \"edited\": false, \"link_flair_css_class\": null, \"author_flair_css_class\": null, \"downs\": 1404, \"saved\": false, \"is_self\": false, \"permalink\": \"/r/funny/comments/136ypz/and_its_gonna_stay_blue/\", \"name\": \"t3_136ypz\", \"created\": 1352947032.0, \"url\": \"http://imgur.com/FqwYH\", \"author_flair_text\": null, \"author\": \"insadrian\", \"created_utc\": 1352918232.0, \"media\": null, \"num_reports\": null, \"ups\": 3238}}], \"after\": \"t3_136rv9\", \"before\": null}}";
            List<LinkData> links = await this.client.GetFrontPageAsync();
            Assert.IsTrue(links.Count == 1);
        }
    }
}