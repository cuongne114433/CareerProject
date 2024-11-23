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
            return db.tbl_Job.OrderByDescending(x => x.ID).ToList();
        }

        public List<tbl_Job> getListMatchedJob(long idUser)
        {
            var user =db.tbl_User.Find(idUser);

            return db.tbl_Job.Where(x=>x.Requirement.Contains(user.Skill) || x.Requirement.Contains(user.Major) || x.Location.Contains(user.JobCity) || x.Offer >= user.Expected || x.Offer <= user.Expected).ToList();
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
                    if (name != null)
                        job.Name = name;

                    if (detail != null)
                        job.Detail = detail;

                    if (requirement != null)
                        job.Requirement = requirement;

                    if (description != null)
                        job.Description = description;

                    if (benefit != null)
                        job.Benefit = benefit;

                    if (double.TryParse(offer, out double parsedOffer))
                        job.Offer = parsedOffer;

                    if (industry != null)
                        job.Industry = industry;

                    if (creationDate != null)
                        job.CreationDate = creationDate;

                    if (limitDate != null)
                        job.LimitDate = limitDate;

                    if (total != null)
                        job.Total = total;

                    if (type != null)
                        job.Type = type;

                    if (sex != null)
                        job.Sex = sex;

                    if (location != null)
                        job.Location = location;

                    if (idCategory != null)
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