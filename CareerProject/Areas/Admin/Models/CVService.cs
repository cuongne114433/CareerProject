using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerProject.Areas.Admin.Models
{
    public class CVService
    {
        CareerDBContext db;

        public CVService()
        {
            db = new CareerDBContext();
        }

        public bool AddCV(string fileName, DateTime creationDate, long idUser)
        {
            try
            {
                tbl_CV cv = new tbl_CV()
                {
                    CreationDate = creationDate,
                    FileCV = fileName,
                    IDUser = idUser
                };
                db.tbl_CV.Add(cv);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<tbl_CV> GetAllCV(long idUser)
        {
            return db.tbl_CV.Where(x=>x.IDUser == idUser).ToList();
        }

        public List<tbl_ApplyJob> JobApplied(long idUser)
        {
            return db.tbl_ApplyJob.Where(x => x.IDUser == idUser).ToList();
        }

    }
}