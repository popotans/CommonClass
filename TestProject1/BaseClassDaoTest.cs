using CommonClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using Helper;

namespace TestProject1
{


    /// <summary>
    ///这是 BaseClassDaoTest 的测试类，旨在
    ///包含所有 BaseClassDaoTest 单元测试
    ///</summary>
    [TestClass()]
    public class BaseClassDaoTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Insert 的测试
        ///</summary>
        // TODO: 确保 UrlToTest 特性指定一个指向 ASP.NET 页的 URL(例如，
        // http://.../Default.aspx)。这对于在 Web 服务器上执行单元测试是必需的，
        //无论要测试页、Web 服务还是 WCF 服务都是如此。
        [TestMethod()]
        // [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\Codelib\\Git\\CommonClass\\CommonClass", "/")]
        public void InsertTest()
        {
            string connstr = "E:\\Codelib\\Git\\CommonClass\\CommonClass\\App_Data\\class.mdb";
            BaseClass target = new BaseClass(connstr);
            //ClassInfo c1i = new ClassInfo()
            //{
            //    P1 = 2,
            //    P2 = 7,
            //    Title = "电脑科技",
            //    OrderIdx = 1,
            //    SiteID = 2
            //};
            //int expected = 3; // TODO: 初始化为适当的值
            //int actual;
            //actual = target.InsertRoot(c1i);
            //Console.WriteLine(actual);
            //Assert.IsTrue(actual > 4);

            // get

            List<ClassInfo> list = target.GetRoot(0);
            foreach (ClassInfo ci in list)
            {
                Console.WriteLine(ci.Title + " " + ci.IDx);

                List<ClassInfo> licist = target.GetByP1(ci.IDx);
                foreach (ClassInfo cii in licist)
                {
                    Console.WriteLine("     " + cii.Title + " " + cii.IDx);

                    List<ClassInfo> licist3 = target.GetByP2(cii.IDx);
                    foreach (ClassInfo c in licist3)
                    {
                        Console.WriteLine("          " + c.Title + " " + c.IDx);
                    }

                }

            }

            //getby ids
            //List<ClassInfo> list = target.GetByIds(new int[] { 1, 2, 6, 5, 9 });
            //foreach (ClassInfo ci in list)
            //{
            //    Console.WriteLine(ci.Title + " " + ci.IDx);
            //}
            // update 
            //ClassInfo ci11 = target.Get(1);
            //Console.WriteLine(ci11.Title + " " + ci11.IDx);
            //ci11.Title = "关于我们";
            //target.Update(ci11);
            //Console.WriteLine("update suc");
        }

        [TestMethod()]
        public void TestDb()
        {
            string connstr = "server=localhost;User Id=root;password=niejunhua;charset=utf8";//Database=nq
            MySqlClass msc = new MySqlClass(connstr);
            msc.InitDatabase();
        }


        [TestMethod()]
        // [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\Codelib\\Git\\CommonClass\\CommonClass", "/")]
        public void TestMysqlInsert()
        {
            string connstr = "server=localhost;User Id=root;password=niejunhua;Database=nq;charset=utf8";//Database=nq

            IBaseClass db = new MySqlClass(connstr);
            db.InsertP1P2(new ClassInfo
            {
                P1 = 5,
                P2 = 1,
                Title = "澳门",
                OrderIdx = 1,
                SiteID = 0,
            });
        }

        string str = "server=localhost;User Id=root;password=niejunhua;Database=nq";//Database=nq
        [TestMethod]
        public void TestList()
        {
            IBaseClass db = new MySqlClass(str);
            var ci = db.GetByP1(1);
            //  Console.WriteLine(ci[0].IDx + "  " + ci[0].Title);
            Console.WriteLine(ci.Count);
        }

        [TestMethod]
        public void InsertArticle()
        {
            MySqlArticle msa = new MySqlArticle(str);
            int id = msa.Insert(new Article()
            {
                Desc = "desc",
                Kwd = "kwd",
                AuthorID = 0,
                InDate = DateTime.Now,
                CP2 = 1,
                CP1 = 5,
                CID = 8,
                Click = 0,
                Content = Guid.NewGuid().ToString() + "this is the article content ",
                Icon = "",
                Title = Guid.NewGuid().ToString(),
                Url = ""
            });
            Console.WriteLine("idx is :" + id
                );
        }

        [TestMethod]
        public void TestartInfo()
        {
            MySqlArticle msa = new MySqlArticle(str);
            Article art = msa.Get(17);
            Console.WriteLine(art.Title);
        }

        [TestMethod]
        public void TestartList()
        {
            MySqlArticle msa = new MySqlArticle(str);
            List<Article> list = new List<Article>();
            Article ap = new Article();
            ap.CID = 1;
            PageModel pm = msa.GetPageModelList(1, 2, ap);
            list = pm.List as List<Article>;
            foreach (Article art in list)
            {
                Console.WriteLine("idx:" + art.IDx + ", title=" + art.Title);
            }
            Console.WriteLine("TotalRecord:" + pm.TotalRecord);
            Console.WriteLine("TotalPage:" + pm.TotalPage);
            Console.WriteLine("CurrentPage:" + pm.Page);
        }

        [TestMethod]
        public void TestUpdateArtInfo()
        {
            MySqlArticle msa = new MySqlArticle(str);
            Article art = msa.Get(17);
            Console.WriteLine(art.Title);
            art.Title = "njhtitile update ";
            msa.Update(art);
            art = msa.Get(17);
            Console.WriteLine(art.Title);
        }

        [TestMethod]
        public void TestDeleteArtInfo()
        {
            MySqlArticle msa = new MySqlArticle(str);
            msa.Delete(1);
        }


    }
}
