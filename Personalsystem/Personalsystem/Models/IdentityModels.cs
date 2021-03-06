﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web;

namespace Personalsystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Salary { get; set; }
        public string CVurl { get; set; }
        public TimeSpan start { get; set; }
        public TimeSpan end { get; set; }

        public int? cId { get; set; }
        [ForeignKey("cId")]
        public virtual Company company { get; set; }
        public int? gId { get; set; }
        [ForeignKey("gId")]
        public virtual Group group { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<PrivateMessage> SentMessages { get; set; }
        [InverseProperty("Receiver")]
        public virtual ICollection<PrivateMessage> ReceivedMessages { get; set; }

        public void GetCurrentUser()
        {
            HttpContext.Current.User.Identity.GetUserId();
        }

        
        


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}