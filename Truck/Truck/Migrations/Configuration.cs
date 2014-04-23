using Truck.Models;
using System.Data.Entity.Migrations;
namespace Truck.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Truck.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Truck.Models.FoodTruckContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Truck.Models.FoodTruckContext";
        }

        protected override void Seed(Truck.Models.FoodTruckContext context)
        {
            this.AddUserAndRoles();
        }


        bool AddUserAndRoles()
        {
            bool success = false;

            var idManager = new IdentityManager();
            success = idManager.CreateRole("Admin");
            if (!success == true) return success;

            success = idManager.CreateRole("CanEdit");
            if (!success == true) return success;

            success = idManager.CreateRole("User");
            if (!success) return success;


            var newUser = new ApplicationUser()
            {
                UserName = "User",
                FirstName = "user",
                LastName = "user",
                Email = "user@admin.com"
            };

            // Be careful here - you  will need to use a password which will 
            // be valid under the password rules for the application, 
            // or the process will abort:
            success = idManager.CreateUser(newUser, "Password1");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "CanEdit");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "User");
            if (!success) return success;

            return success;
        }
    }
}
