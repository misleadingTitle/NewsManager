using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Shared;

namespace NewsManager
{
    public class NewsRESTService : INewsRESTService
    {
        public List<Article> GetArticlesList()
        {
            var a = WebOperationContext.Current.IncomingRequest;
            List<Article> result= new List<Article>();
            using (SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from ArticleSet", connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Article
                        {
                            Id = reader[0].ToString(),
                            Title = reader[1].ToString(),
                            Category = reader[2].ToString(),
                            Abstract = reader[3].ToString(),
                            Body = reader[4].ToString(),
                            IsPublished = (bool)reader[5],
                            IsDeleted = (bool)reader[6]
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return result;
        }


        public Article GetArticleById(string Id)
        {
            Article result = new Article();
            using (SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from ArticleSet where Id='"+Id+"'", connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result= new Article()
                        {
                            Id = reader[0].ToString(),
                            Title = reader[1].ToString(),
                            Category = reader[2].ToString(),
                            Abstract = reader[3].ToString(),
                            Body = reader[4].ToString(),
                            IsPublished = (bool)reader[5],
                            IsDeleted = (bool)reader[6]
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return result;
        }

        public string InsertArticle(Article article, string Id)
        {
            using (SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    string.Format(@"INSERT INTO [dbo].[ArticleSet]
           ([Id]
           ,[Title]
           ,[Category]
           ,[Abstract]
           ,[Body]
           ,[IsPublished]
           ,[IsDeleted])
     VALUES
           ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",article.Id,article.Title,article.Category,article.Abstract,article.Body,article.IsPublished.ToString(),false.ToString())
                                         , connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return article.Id;
            }
        }

        public string UpdateArticle(Article article, string Id)
        {
            using (SqlConnection connection =
new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    string.Format(@"UPDATE [dbo].[ArticleSet]
   SET [Title] = '{1}'
      ,[Category] = '{2}'
      ,[Abstract] = '{3}'
      ,[Body] = '{4}'
      ,[IsPublished] = '{5}'
      ,[IsDeleted] = '{6}'
 WHERE Id='{0}'", article.Id, article.Title, article.Category, article.Abstract, article.Body, article.IsPublished.ToString(), false.ToString())
                                         , connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                return article.Id;
            }
        }

        public string DeleteArticle(string Id)
        {
             using (SqlConnection connection =
new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    string.Format(@"DELETE FROM [dbo].[ArticleSet] WHERE Id='{0}'", Id)
                                         , connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                return Id;
            }
        }
    }
}
