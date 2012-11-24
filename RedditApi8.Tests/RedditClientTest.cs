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
        /// Tests LoginAsync.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestLoginAsync()
        {
            this.fakeHttpServer.ResponseContent = "{\"json\": {\"errors\": [], \"data\": {\"modhash\": \"927obqh4xeded22e6ec4b1994d81ac1719118d25a848f59af2\", \"cookie\": \"7380190,2012-11-14T13:23:24,45ac326604d67f49f05a3120a4a0df35066f9ddb\"}}}";
            Assert.IsTrue(await this.client.LoginAsync("user", "passwd"));
            Assert.IsTrue(this.client.IsLoggedIn);
        }

        /// <summary>
        /// Tests LoginAsync that was unsuccessful.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestUnsuccessfulLoginAsync()
        {
            this.fakeHttpServer.ResponseContent = "{\"json\": {\"errors\": [[\"WRONG_PASSWORD\", \"invalid password\", \"passwd\"]]}}";
            Assert.IsFalse(await this.client.LoginAsync("user", "passwd"));
            Assert.IsTrue(this.client.Errors.Count > 0);
            Assert.IsFalse(this.client.IsLoggedIn);
        }

        /// <summary>
        /// Tests LogoutAsync.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestLogoutAsync()
        {
            this.fakeHttpServer.ResponseContent = "{\"json\": {\"errors\": [], \"data\": {\"modhash\": \"927obqh4xeded22e6ec4b1994d81ac1719118d25a848f59af2\", \"cookie\": \"7380190,2012-11-14T13:23:24,45ac326604d67f49f05a3120a4a0df35066f9ddb\"}}}";
            await this.client.LoginAsync("user", "passwd");
            Assert.IsTrue(await this.client.LogoutAsync());
            Assert.IsFalse(this.client.IsLoggedIn);
        }

        /// <summary>
        /// Tests LogoutAsync without login.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestLogoutAsyncWithoutLogin()
        {
            Assert.IsFalse(this.client.IsLoggedIn);
            Assert.IsTrue(await this.client.LogoutAsync());
            Assert.IsFalse(this.client.IsLoggedIn);
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
        /// Tests GetPageAsync.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestGetPageAsync()
        {
            this.fakeHttpServer.ResponseContent = "{\"kind\": \"Listing\", \"data\": {\"modhash\": \"\", \"children\": [{\"kind\": \"t3\", \"data\": {\"domain\": \"imgur.com\", \"banned_by\": null, \"media_embed\": {}, \"subreddit\": \"funny\", \"selftext_html\": null, \"selftext\": \"\", \"likes\": null, \"link_flair_text\": null, \"id\": \"136ypz\", \"clicked\": false, \"title\": \"And it's gonna stay blue\", \"num_comments\": 75, \"score\": 1834, \"approved_by\": null, \"over_18\": false, \"hidden\": false, \"thumbnail\": \"\", \"subreddit_id\": \"t5_2qh33\", \"edited\": false, \"link_flair_css_class\": null, \"author_flair_css_class\": null, \"downs\": 1404, \"saved\": false, \"is_self\": false, \"permalink\": \"/r/funny/comments/136ypz/and_its_gonna_stay_blue/\", \"name\": \"t3_136ypz\", \"created\": 1352947032.0, \"url\": \"http://imgur.com/FqwYH\", \"author_flair_text\": null, \"author\": \"insadrian\", \"created_utc\": 1352918232.0, \"media\": null, \"num_reports\": null, \"ups\": 3238}}], \"after\": \"t3_136rv9\", \"before\": null}}";
            List<LinkData> links = await this.client.GetPageAsync();
            Assert.IsTrue(links.Count == 1);
        }

        /// <summary>
        /// Tests GetCommentsAsync.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestGetCommentsAsync()
        {
            this.fakeHttpServer.ResponseContent = "[{\"kind\": \"Listing\", \"data\": {\"modhash\": \"\", \"children\": [{\"kind\": \"t3\", \"data\": {\"domain\": \"imgur.com\", \"banned_by\": null, \"media_embed\": {}, \"subreddit\": \"funny\", \"selftext_html\": null, \"selftext\": \"\", \"likes\": null, \"link_flair_text\": null, \"id\": \"1355g7\", \"clicked\": false, \"title\": \"Girlfriend admitted that she masturbates about 2 times a week. She said she was embarrassed and that I probably don't masturbate that much. (SFW)\", \"num_comments\": 2072, \"score\": 2323, \"approved_by\": null, \"over_18\": false, \"hidden\": false, \"thumbnail\": \"\", \"subreddit_id\": \"t5_2qh33\", \"edited\": false, \"link_flair_css_class\": null, \"author_flair_css_class\": null, \"downs\": 27151, \"saved\": false, \"is_self\": false, \"permalink\": \"/r/funny/comments/1355g7/girlfriend_admitted_that_she_masturbates_about_2/\", \"name\": \"t3_1355g7\", \"created\": 1352869980.0, \"url\": \"http://imgur.com/wDhnq\", \"author_flair_text\": null, \"author\": \"clarke77\", \"created_utc\": 1352841180.0, \"media\": null, \"num_reports\": null, \"ups\": 29474}}], \"after\": null, \"before\": null}}, {\"kind\": \"Listing\", \"data\": {\"modhash\": \"\", \"children\": [{\"kind\": \"t1\", \"data\": {\"subreddit_id\": \"t5_2qh33\", \"banned_by\": null, \"link_id\": \"t3_1355g7\", \"likes\": null, \"replies\": {\"kind\": \"Listing\", \"data\": {\"modhash\": \"\", \"children\": [{\"kind\": \"t1\", \"data\": {\"subreddit_id\": \"t5_2qh33\", \"banned_by\": null, \"link_id\": \"t3_1355g7\", \"likes\": null, \"replies\": \"\", \"id\": \"c711pw1\", \"gilded\": 0, \"author\": \"nappysteph\", \"parent_id\": \"t1_c70zt5s\", \"approved_by\": null, \"body\": \"He is so delicious!\", \"edited\": false, \"author_flair_css_class\": null, \"downs\": 0, \"body_html\": \"&lt;div class=\\\"md\\\"&gt;&lt;p&gt;He is so delicious!&lt;/p&gt;\\n&lt;/div&gt;\", \"subreddit\": \"funny\", \"name\": \"t1_c711pw1\", \"created\": 1352891715.0, \"author_flair_text\": null, \"created_utc\": 1352862915.0, \"num_reports\": null, \"ups\": 1}}], \"after\": null, \"before\": null}}, \"id\": \"c70zt5s\", \"gilded\": 0, \"author\": \"Crushed_By_Cows\", \"parent_id\": \"t3_1355g7\", \"approved_by\": null, \"body\": \"[This is now a David Tennant thread.](http://imgur.com/VNKKG)\", \"edited\": false, \"author_flair_css_class\": null, \"downs\": 2, \"body_html\": \"&lt;div class=\\\"md\\\"&gt;&lt;p&gt;&lt;a href=\\\"http://imgur.com/VNKKG\\\"&gt;This is now a David Tennant thread.&lt;/a&gt;&lt;/p&gt;\\n&lt;/div&gt;\", \"subreddit\": \"funny\", \"name\": \"t1_c70zt5s\", \"created\": 1352884866.0, \"author_flair_text\": null, \"created_utc\": 1352856066.0, \"num_reports\": null, \"ups\": 6}}], \"after\": null, \"before\": null}}]";
            List<CommentData> comments = (await this.client.GetCommentsAsync("asdf")).Item2;
            Assert.IsTrue(comments.Count > 0);
        }

        /// <summary>
        /// Tests GetMoreCommentsAsync.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestMethod]
        public async Task TestGetMoreCommentsAsync()
        {
            this.fakeHttpServer.ResponseContent = "[{\"kind\": \"Listing\", \"data\": {\"modhash\": \"\", \"children\": [{\"kind\": \"t3\", \"data\": {\"domain\": \"imgur.com\", \"banned_by\": null, \"media_embed\": {}, \"subreddit\": \"funny\", \"selftext_html\": null, \"selftext\": \"\", \"likes\": null, \"link_flair_text\": null, \"id\": \"1355g7\", \"clicked\": false, \"title\": \"Girlfriend admitted that she masturbates about 2 times a week. She said she was embarrassed and that I probably don't masturbate that much. (SFW)\", \"num_comments\": 2072, \"score\": 2323, \"approved_by\": null, \"over_18\": false, \"hidden\": false, \"thumbnail\": \"\", \"subreddit_id\": \"t5_2qh33\", \"edited\": false, \"link_flair_css_class\": null, \"author_flair_css_class\": null, \"downs\": 27151, \"saved\": false, \"is_self\": false, \"permalink\": \"/r/funny/comments/1355g7/girlfriend_admitted_that_she_masturbates_about_2/\", \"name\": \"t3_1355g7\", \"created\": 1352869980.0, \"url\": \"http://imgur.com/wDhnq\", \"author_flair_text\": null, \"author\": \"clarke77\", \"created_utc\": 1352841180.0, \"media\": null, \"num_reports\": null, \"ups\": 29474}}], \"after\": null, \"before\": null}}, {\"kind\": \"Listing\", \"data\": {\"modhash\": \"\", \"children\": [{\"kind\": \"t1\", \"data\": {\"subreddit_id\": \"t5_2qh33\", \"banned_by\": null, \"link_id\": \"t3_1355g7\", \"likes\": null, \"replies\": {\"kind\": \"Listing\", \"data\": {\"modhash\": \"\", \"children\": [{\"kind\": \"t1\", \"data\": {\"subreddit_id\": \"t5_2qh33\", \"banned_by\": null, \"link_id\": \"t3_1355g7\", \"likes\": null, \"replies\": \"\", \"id\": \"c711pw1\", \"gilded\": 0, \"author\": \"nappysteph\", \"parent_id\": \"t1_c70zt5s\", \"approved_by\": null, \"body\": \"He is so delicious!\", \"edited\": false, \"author_flair_css_class\": null, \"downs\": 0, \"body_html\": \"&lt;div class=\\\"md\\\"&gt;&lt;p&gt;He is so delicious!&lt;/p&gt;\\n&lt;/div&gt;\", \"subreddit\": \"funny\", \"name\": \"t1_c711pw1\", \"created\": 1352891715.0, \"author_flair_text\": null, \"created_utc\": 1352862915.0, \"num_reports\": null, \"ups\": 1}}], \"after\": null, \"before\": null}}, \"id\": \"c70zt5s\", \"gilded\": 0, \"author\": \"Crushed_By_Cows\", \"parent_id\": \"t3_1355g7\", \"approved_by\": null, \"body\": \"[This is now a David Tennant thread.](http://imgur.com/VNKKG)\", \"edited\": false, \"author_flair_css_class\": null, \"downs\": 2, \"body_html\": \"&lt;div class=\\\"md\\\"&gt;&lt;p&gt;&lt;a href=\\\"http://imgur.com/VNKKG\\\"&gt;This is now a David Tennant thread.&lt;/a&gt;&lt;/p&gt;\\n&lt;/div&gt;\", \"subreddit\": \"funny\", \"name\": \"t1_c70zt5s\", \"created\": 1352884866.0, \"author_flair_text\": null, \"created_utc\": 1352856066.0, \"num_reports\": null, \"ups\": 6}}], \"after\": null, \"before\": null}}]";
            List<CommentData> comments = (await this.client.GetMoreCommentsAsync("asdf", "qwer")).Item2;
            Assert.IsTrue(comments.Count > 0);
        }
    }
}