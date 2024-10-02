using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerProject.Areas.Admin.Models
{
    public class ApplyService
    {
        CareerDBContext db;

        public ApplyService()
        {
            db = new CareerDBContext();
        }

        // Get list of all job applications
        public List<tbl_ApplyCustom> GetListApplyJob()
        {
            List<tbl_ApplyCustom> tbl_ApplyCustoms = new List<tbl_ApplyCustom>();
            var list = db.tbl_ApplyJob.ToList();
            if (list != null && list.Count()>0) {
                foreach (var item in list) { 
                    tbl_ApplyCustom applyCustom = new tbl_ApplyCustom();
                    applyCustom.applyJob = item;
                    var cv = db.tbl_CV.Find(Convert.ToInt64(item.CV));
                    applyCustom.FileCV = cv.FileCV;
                    tbl_ApplyCustoms.Add(applyCustom);
                }
            }
            return tbl_ApplyCustoms;
        }

        // Get a specific job application by ID
        public tbl_ApplyJob GetApplyJob(long? id)
        {
            return db.tbl_ApplyJob.Find(id);
        }

        // Insert a new job application
        public bool AddApplyJob(long idUser, long idJob, DateTime appliedDate, string name, string mail, string cv, string coverLetter, string applyStatus)
        {
            try
            {
                tbl_ApplyJob newApplyJob = new tbl_ApplyJob()
                {
                    IDUser = idUser,
                    IDJob = idJob,
                    AppliedDate = appliedDate,
                    Name = name,
                    Mail = mail,
                    CV = cv,
                    CoverLetter = coverLetter,
                    ApplyStatus = applyStatus,
                };

                db.tbl_ApplyJob.Add(newApplyJob); // Add the new application to the database context
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

        // Delete a job application
        public bool DeleteApplyJob(long? id)
        {
            try
            {
                tbl_ApplyJob applyJob = db.tbl_ApplyJob.Find(id);
                if (applyJob != null)
                {
                    db.tbl_ApplyJob.Remove(applyJob);
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
        public bool ProcessCV(long? id, string status)
        {
            try
            {
                tbl_ApplyJob applyJob = db.tbl_ApplyJob.Find(id);
                if (applyJob != null)
                {
                    applyJob.ApplyStatus = status;  
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

        // Update a job application
        public bool UpdateApplyJob(long? id, long idUser, long idJob, DateTime appliedDate, string name, string mail, string cv, string coverLetter, string applyStatus)
        {
            try
            {
                tbl_ApplyJob applyJob = db.tbl_ApplyJob.Find(id);
                if (applyJob != null)
                {
                    applyJob.IDUser = idUser;
                    applyJob.IDJob = idJob;
                    applyJob.AppliedDate = appliedDate;
                    applyJob.Name = name;
                    applyJob.Mail = mail;
                    applyJob.CV = cv;
                    applyJob.CoverLetter = coverLetter;
                    applyJob.ApplyStatus = applyStatus;

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