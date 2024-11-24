using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerProject.Areas.Admin.Models
{
    public class CompanyService
    {
        private CareerDBContext db;

        public CompanyService()
        {
            db = new CareerDBContext();
        }

        // Get a list of all companies
        public List<tbl_Company> GetListCompany()
        {
            return db.tbl_Company.ToList();
        }

        // Get a specific company by ID
        public tbl_Company GetCompany(long? id)
        {
            return db.tbl_Company.Find(id);
        }

        // Insert a new company
        public bool AddCompany(string name, string description, string avt, string phoneNumber, string email, string location)
        {
            try
            {
                tbl_Company newCompany = new tbl_Company()
                {
                    Name = name,
                    Description = description,
                    Avt = avt,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Location = location,
                    status = "Active"
                };

                db.tbl_Company.Add(newCompany); // Add the new company to the database context
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

        // Delete a company by ID
        public bool DeleteCompany(long? id, string status)
        {
            try
            {
                tbl_Company company = db.tbl_Company.Find(id);
                if (company != null)
                {
                   company.status = status;
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

        // Update a company by ID
        public bool UpdateCompany(long? id, string name, string description, string avt, string phoneNumber, string email, string location)
        {
            try
            {
                tbl_Company company = db.tbl_Company.Find(id);
                if (company != null)
                {
                    company.Name = name;
                    company.Description = description;
                    if(avt!= null) company.Avt = avt;   
                    
                    company.PhoneNumber = phoneNumber;
                    company.Email = email;
                    company.Location = location;


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