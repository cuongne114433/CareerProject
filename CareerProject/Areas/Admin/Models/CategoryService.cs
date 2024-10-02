using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerProject.Areas.Admin.Models
{
    public class CategoryService
    {
        CareerDBContext db;
        public CategoryService()
        {
            db = new CareerDBContext();
        }

        // Get all categories
        public List<tbl_Category> getListCategory()
        {
            return db.tbl_Category.ToList();
        }

        // Get a specific category by ID
        public tbl_Category getCategory(int id)
        {
            return db.tbl_Category.Find(id);
        }

        // Add a new category
        public bool AddCategory(string name, string image)
        {
            try
            {
                tbl_Category newCategory = new tbl_Category()
                {
                    Name = name,
                    image = image
                };

                db.tbl_Category.Add(newCategory);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log exception if necessary
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Delete a category by ID
        public bool DeleteCategory(int id)
        {
            try
            {
                tbl_Category category = db.tbl_Category.Find(id);
                if (category != null)
                {
                    db.tbl_Category.Remove(category);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Update an existing category
        public bool UpdateCategory(int id, string name, string image)
        {
            try
            {
                tbl_Category category = db.tbl_Category.Find(id);
                if (category != null)
                {
                    category.Name = name;
                    if(image != null)
                    {
                        category.image = image;
                    }

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}