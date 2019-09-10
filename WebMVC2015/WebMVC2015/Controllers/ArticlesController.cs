using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMVC2015.Models;

namespace WebMVC2015.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private ArticleEntities db = new ArticleEntities();

        // GET: Articles
        
        public ActionResult Index()
        {
            List<ArticleMember> articleMembers = new List<ArticleMember>();
            List<Article> articles = new List<Article>();
               articles = db.Article.ToList();
            String userName = User.Identity.Name;
            for (int i = 0; i < articles.Count; i++)
            {
                bool isCurrentMember = false;
                if (userName == articles[i].create_user_name)
                {
                    isCurrentMember = true;
                }
                articleMembers.Add(new ArticleMember(articles[i],isCurrentMember) );
            }
            string dataStr = JsonConvert.SerializeObject(articleMembers, Formatting.None);
            // store it in viewdata/ viewbag
            ViewBag.Data = new HtmlString(dataStr);
            return View();
            //return View(db.Articles.ToList());
        }
        public JsonResult Get_data()
        {
            return Json(db.Article.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Articles/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Article article = db.Article.Find(id);
        //    if (article == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(article);
        //}

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Article_Title,Article_Content,Create_user_name,Article_Create_time,Article_Modify_time")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.create_user_name = User.Identity.Name;
                article.Article_Create_time = DateTime.Now;
                article.Article_Modify_time = DateTime.Now;
                db.Article.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Article_Title,Article_Content,Create_user_name,Article_Create_time,Article_Modify_time")] Article article)
        {
            if (ModelState.IsValid)
            {
                
                if (article.create_user_name != User.Identity.Name)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                article.Article_Modify_time = DateTime.Now;
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Article.Find(id);
            if (article.create_user_name != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Article.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
