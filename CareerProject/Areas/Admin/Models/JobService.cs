using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerProject.Areas.Admin.Models
{
    public class JobService
    {
        CareerDBContext db;
        public JobService()
        {
            db = new CareerDBContext(); 
        }
        public List<tbl_Job> getListJob()
        {
            return db.tbl_Job.ToList();
        }

        public List<tbl_Job> getListMatchedJob(long idUser)
        {
            var user =db.tbl_User.Find(idUser);

            return db.tbl_Job.Where(x=>x.Requirement.Contains(user.Skill) || x.Requirement.Contains(user.Major) && x.Location.Contains(user.JobCity)).ToList();
        }

        // Get a specific job by ID
        public tbl_Job getJob(long? id)
        {
            return db.tbl_Job.Find(id);
        }

        // Insert a new job
        public bool AddJob(string name, string detail, string requirement, string description, string benefit, string offer, string industry, DateTime creationDate, DateTime limitDate, int total, string type, string sex, string location, long? idCompany, int idCategory)
        {
            try
            {
                tbl_Job newJob = new tbl_Job()
                {
                    Name = name,
                    Detail = detail,
                    Requirement = requirement,
                    Description = description,
                    Benefit = benefit,
                    Offer = Convert.ToDouble(offer),
                    Industry = industry,
                    CreationDate = creationDate,
                    LimitDate = limitDate,
                    Total = total,
                    Type = type,
                    Sex = sex,
                    Location = location,
                    IDCompany = Convert.ToInt64(idCompany),
                    IDCategory = idCategory,
                };

                db.tbl_Job.Add(newJob); // Add the new job to the database context
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

        public bool DeleteJob(long? id)
        {
            try
            {
                tbl_Job job = db.tbl_Job.Find(id);
                if (job != null)
                {
                    db.tbl_Job.Remove(job);
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


        public bool UpdateJob(long? id, string name, string detail, string requirement, string description, string benefit, string offer, string industry, DateTime creationDate, DateTime limitDate, int total, string type, string sex, string location, int idCategory)
        {
            try
            {
                tbl_Job job = db.tbl_Job.Find(id);
                if (job != null)
                {
                    job.Name = name;
                    job.Detail = detail;
                    job.Requirement = requirement;
                    job.Description = description;
                    job.Benefit = benefit;
                    job.Offer = Convert.ToDouble(offer);
                    job.Industry = industry;
                    job.CreationDate = creationDate;
                    job.LimitDate = limitDate;
                    job.Total = total;
                    job.Type = type;
                    job.Sex = sex;
                    job.Location = location;
                    job.IDCategory = idCategory;

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