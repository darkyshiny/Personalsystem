﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class Repo
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        static Random rnd = new Random();

        public void GetRandom(int first, int last)
        {
            last++;
            rnd.Next(first, last);
        }

        public void SetUserRoleToSuperAdmin(string userid)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            UserManager.AddToRole(userid, "Super Admin");
        }

        public List<ApplicationUser> FindUser(string name)
        {
            var query = db.user.Where(u => u.UserName.Equals(name));
            return query.ToList();
        }


        public List<ApplicationUser> FindPersonalsBySearchDepId(string uname)
        
        {
            var query = db.user.Where(u => u.UserName == uname);
            return query.ToList();
        }

        internal void SerUserRoleToAdmin(string userid, int companyid)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            UserManager.AddToRole(userid, "Admin");
            db.user.Find(userid).cId = companyid;
            db.SaveChanges();
            
        }
//Begin, Written by Ali 
        public List<ApplicationUser> FindPersonalsBySearchUserName(string USERNAME) 
        {
            var query = db.user.Where(u => u.UserName  == USERNAME);
            return query.ToList();
        }

        public List<ApplicationUser> FindPersonalsBySearchId(string USERID)
        {
            var query = db.user.Where(u => u.Id == USERID);
            return query.ToList();
        }

        public List<ApplicationUser> FindUserHandleEmploymentById(string usrid)       
        {
            var query = db.user.Where(u => u.Id == usrid);
            return query.ToList();
        }

//End

    }
}
