using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{

    [DataContract]
    [KnownType(typeof(List<Article>))]
    public partial class Article
    {
        public Article() { }
        //public Article(ArticleSet a)
        //{
        //    this.Id = a.Id;
        //    this.Abstract = a.Abstract;
        //    this.Title = a.Title;
        //    this.Category = a.Category;
        //    this.Body = a.Body;
        //    this.IsPublished = a.IsPublished;
        //    this.IsDeleted = a.IsDeleted;
        //}

        //public ArticleSet getArticleSet()
        //{
        //    return new ArticleSet{
        //         Id=this.Id,
        //         Abstract=this.Abstract,
        //         Title=this.Title,
        //         Category=this.Category,
        //         Body=this.Body,
        //         IsPublished=this.IsPublished,
        //         IsDeleted=this.IsDeleted
        //    };
        //}

        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Abstract { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public Nullable<bool> IsPublished { get; set; }
        [DataMember]
        public Nullable<bool> IsDeleted { get; set; }
    }

    
    public class Articles : List<Article>
    {
        public Articles(){ }
        //public Articles(List<ArticleSet> aset)
        //{
        //    foreach(ArticleSet a in aset)
        //    {
        //        this.Add(new Article(a));
        //    }
        //}
    }

}
