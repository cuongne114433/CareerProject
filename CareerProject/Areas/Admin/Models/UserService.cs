using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerProject.Areas.Admin.Models
{
    public class UserService
    {
        CareerDBContext db;

        public UserService()
        {
            db = new CareerDBContext();
        }

        // Get a list of all users
        public List<tbl_User> GetListUser()
        {
            return db.tbl_User.ToList();
        }

        // Get a specific user by ID
        public tbl_User GetUser(long? id)
        {
            return db.tbl_User.Find(id);
        }

        // Insert a new user
        public bool AddUser(string name, DateTime? dob, string major, string jobCity, string profileUser, string skill, float? expected, int? experiences, string position, string email, string password)
        {
            try
            {
                tbl_User newUser = new tbl_User()
                {
                    Name = name,
                    DoB = dob,
                    Major = major,
                    JobCity = jobCity,
                    ProfileUser = profileUser,
                    Skill = skill,
                    Expected = expected,
                    Experiences = experiences,
                    Position = position,
                    Email = email,
                    PassWord = password
                };

                db.tbl_User.Add(newUser); // Add the new user to the database context
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

        // Delete a user by ID
        public bool DeleteUser(long? id)
        {
            try
            {
                tbl_User user = db.tbl_User.Find(id);
                if (user != null)
                {
                    db.tbl_User.Remove(user);
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


        // Update an existing user
        public bool UpdateUser(long? id, string name, DateTime? dob, string major, string jobCity, string profileUser, string skill, float? expected, int? experiences, string position, string email)
        {
            try
            {
                tbl_User user = db.tbl_User.Find(id);
                if (user != null)
                {
                    user.Name = name;
                    user.DoB = dob;
                    user.Major = major;
                    user.JobCity = jobCity;
                    user.ProfileUser = profileUser;
                    user.Skill = skill;
                    user.Expected = expected;
                    user.Experiences = experiences;
                    user.Position = position;
                    user.Email = email;

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
        public List<tbl_ApplyCustom> GetListAppliedJob(long?id)
        {
            List<tbl_ApplyCustom> tbl_ApplyCustoms = new List<tbl_ApplyCustom>();
            var list = db.tbl_ApplyJob.Where(x=>x.IDUser==id).ToList();
            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    tbl_ApplyCustom applyCustom = new tbl_ApplyCustom();
                    applyCustom.applyJob = item;
                    var cv = db.tbl_CV.Find(Convert.ToInt64(item.CV));
                    applyCustom.FileCV = cv.FileCV;
                    tbl_ApplyCustoms.Add(applyCustom);
                }
            }
            return tbl_ApplyCustoms;
        }

    }

}