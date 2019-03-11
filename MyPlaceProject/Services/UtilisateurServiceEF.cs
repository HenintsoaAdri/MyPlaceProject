using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Services
{
    public class UtilisateurServiceEF
    {
        private UtilisateurServiceEF() { }
        private static UtilisateurServiceEF instance = new UtilisateurServiceEF();
        public static UtilisateurServiceEF getInstance()
        {
            return instance;
        }
        public BaseModelPagination<ApplicationUser> GetAll(int page = 1, int maxResult = 10)
        {
            using (var context = new ApplicationDbContext())
            {
                BaseModelPagination<ApplicationUser> pagination = new BaseModelPagination<ApplicationUser>(page, maxResult);
                pagination.totalResult = context.Users.Count();
                pagination.liste = context.Users.Include(i => i.Roles).OrderBy(i => i.Id).Skip(pagination.offset()).Take(maxResult).ToList();
                return pagination;
            }
        }

        public List<ApplicationUser> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.Include(i => i.Roles).ToList();
            }
        }
        public ApplicationUser Get(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                //var userrole = new ApplicationUserRole();
                //userrole.user = 
                //userrole.role = context.Roles.Where(r => r.Id == userrole.user.Roles.SingleOrDefault().RoleId).SingleOrDefault().Name;
                return context.Users.Include(i => i.Roles).Where(i => i.Id == id).FirstOrDefault();
            }
        }
        public void Save(ApplicationUser Users, String password, String role)
        {
            using (var context = new ApplicationDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                    try
                    {
                        var created = UserManager.Create(Users, password);
                        if (created.Succeeded)
                        {
                            UserManager.AddToRole(Users.Id, role);
                        }
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(ApplicationUser Users, string role)
        {
            using (var context = new ApplicationDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                    var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    try
                    {
                        var oldRoles = RoleManager.Roles.Where(r => r.Users.Any(iur => iur.UserId == Users.Id)).ToList();
                        if( oldRoles != null && oldRoles.Count > 0)
                        {
                            oldRoles.ForEach(x => {
                                var oldRoleName = x.Name;
                                if (oldRoleName != role)
                                {
                                    UserManager.RemoveFromRole(Users.Id, oldRoleName);
                                    UserManager.AddToRole(Users.Id, role);
                                }

                            });
                        }
                        else
                        {
                            UserManager.AddToRole(Users.Id, role);
                        }
                        context.Entry(Users).State = EntityState.Modified;  
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        ApplicationUser Users = context.Users.Find(id);
                        context.Users.Remove(Users);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }

        }
        public ICollection<IdentityRole> GetAllRole()
        {
            using(var context = new ApplicationDbContext())
            {
                return context.Roles.ToList();
            }
        }
        public IdentityRole GetRoleById(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Roles.Find(id);
            }
        }

        public IdentityRole GetRoleByName(string role)
        {
            using (var context = new ApplicationDbContext())
            {
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                return RoleManager.FindByName(role);
            }
        }
    }
}