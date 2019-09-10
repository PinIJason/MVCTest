using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC2015.Models
{
    public class ArticleMember : Article
    {
        public bool isCurrentMember { get; set; }
        public ArticleMember(Article article,bool isCurrentMember)
        {
            this.ID = article.ID;
            this.Article_Title = article.Article_Title;
            this.Article_Modify_time = article.Article_Modify_time;
            this.Article_Create_time = article.Article_Create_time;
            this.create_user_name = article.create_user_name;
            this.Article_Content = article.Article_Content;

            this.isCurrentMember = isCurrentMember;
        }
    }
}