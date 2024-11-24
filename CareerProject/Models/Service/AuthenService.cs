using CareerProject.Models.DTO;
using System;
using System.Linq;

namespace CareerProject.Models.Service
{
    public class AuthenService
    {
        CareerDBContext db;
        public AuthenService()
        {
            db = new CareerDBContext();
        }

        public bool signUpCompany(string name, string decription, string Avt, string phoneNumber, string location, string email, string passWord)
        {
            var isExistedAccount = db.tbl_Company.Where(x => x.Email.Trim().ToLower() == email.ToLower()).ToList();
            if (isExistedAccount != null && isExistedAccount.Count() > 0)
            {
                return false;
            }
            try
            {
                tbl_Company company = new tbl_Company();
                company.Name = name;
                company.Avt = Avt;
                company.PhoneNumber = phoneNumber;
                company.Location = location;
                company.Email = email;
                company.PassWord = passWord;
                company.Description = decription;
                company.CreatedDate = DateTime.Now; 
                db.tbl_Company.Add(company);
                db.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public bool signUpUser(string name, DateTime dob, string major, string jobCity, string profileUser, string skill, float expectedSalary, int experiences, string position, string email, string password)
        {
            var isExistedAccount = db.tbl_User.Where(x=>x.Email.Trim().ToLower() == email.ToLower()).ToList();
            if(isExistedAccount != null && isExistedAccount.Count() > 0)
            {
                return false;
            }
            try
            {
                tbl_User user = new tbl_User();

                user.Name = name;
                user.DoB = dob;
                user.Major = major;
                user.JobCity = jobCity;
                user.ProfileUser = profileUser;
                user.Skill = skill;
                user.Expected = expectedSalary;
                user.Experiences = experiences;
                user.Position = position;
                user.Email = email;
                user.PassWord = password;

                db.tbl_User.Add(user);

                db.SaveChanges();

                return true; 
            }
            catch
            {
                return false;
            }
        }

        public tbl_User signInUser(string mail, string password) {
            var data = db.tbl_User.Where(x=> x.Email.Trim().Equals(mail) && x.PassWord.Trim().Equals(password)).ToArray();
            if (data != null && data.Length > 0)
            {
                return data.First();
            }
            else
            {
                return null;
            }
        }

        public tbl_Company signInCompany(string mail, string password)
        {
            var data = db.tbl_Company.Where(x => x.Email.Trim().Equals(mail) && x.PassWord.Trim().Equals(password)).ToArray();
            if (data != null && data.Length > 0)
            {
                return data.First();
            }
            else
            {
                return null;
            }
        }
    }
}