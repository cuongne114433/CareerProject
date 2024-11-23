using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerProject.Areas.Admin.Models
{
    public class NewsService
    {
        CareerDBContext db;

        // Constructor
        public NewsService()
        {
            db = new CareerDBContext();
        }

        // Get all news
        public List<tlb_news> GetAllNews()
        {
            return db.tlb_news.OrderByDescending(x => x.IDnews).ToList();
        }

        // Get a specific news item by ID
        public tlb_news GetNewsById(long? id)
        {
            return db.tlb_news.Find(id);
        }

        // Add a new news item
        public bool AddNews(string title, DateTime creationDate, string content, string image)
        {
            try
            {
                tlb_news newNews = new tlb_news()
                {
                    Title = title,
                    CreationDate = creationDate,
                    Content = content,
                    Image = image
                };

                db.tlb_news.Add(newNews); // Add the news to the database context
                db.SaveChanges(); // Save the changes to the database
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Update an existing news item
        public bool UpdateNews(long? id, string title, DateTime? creationDate, string content, string image)
        {
            try
            {
                tlb_news news = db.tlb_news.Find(id);
                if (news != null)
                {
                    if (title != null)
                        news.Title = title;

                    if (creationDate.HasValue)
                        news.CreationDate = creationDate.Value;

                    if (content != null)
                        news.Content = content;

                    if (image != null)
                        news.Image = image;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Delete a news item by ID
        public bool DeleteNews(long? id)
        {
            try
            {
                tlb_news news = db.tlb_news.Find(id);
                if (news != null)
                {
                    db.tlb_news.Remove(news);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}