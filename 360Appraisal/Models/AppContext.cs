using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _360Appraisal.Models
{
    public interface IApplicationUserManager
    {
        Task<User> FindAsync(string code, string password);
        Task SignInAsync(User user, bool isPersistent);
        void SignOut();
    }

    public interface IUserService
    {
        Task<User> Authenticate(string code, string password);
        Task<User> GetUserByCode(string code);
    }

    public class User : IUser<string>
    {
        public string companyCode { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public Department department { get; set; }
        public Position position { get; set; }
        public string payroll { get; set; }
        public string division { get; set; }
        public string grade { get; set; }
        public string jobCode { get; set; }
        public string Id { get { return code; } set { code = value; } }
        public string UserName { get { return name; } set { name = value; } }        
    }

    public class UserService : IUserService
    {
        public async Task<User> Authenticate(string code, string password)
        {
            return await Service.Get(code, password);
        }

        public async Task<User> GetUserByCode(string code)
        {
            return await Service.Get(code);
        }
    }

    public class UserStore<T> : IUserStore<T, string> where T : User
    {
        private readonly IUserService _userService;
        public UserStore(IUserService userService)
        {
            _userService = userService;
        }       

        public Task CreateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByIdAsync(string userId)
        {
            return (T)await _userService.GetUserByCode(userId);
        }

        public Task<T> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }   

    public class ApplicationUserManager : UserManager<User, string>, IApplicationUserManager
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationManager _authenticationManager;

        public ApplicationUserManager(IUserService userService, IAuthenticationManager authenticationManager)
            : base(new UserStore<User>(userService))
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
        }       

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserService(), context.Authentication);

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override async Task<User> FindAsync(string code, string password)
        {
            return await _userService.Authenticate(code, password);
        }

        public async Task SignInAsync(User user, bool isPersistent = false)
        {
            SignOut();
            var identity = await base.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        public void SignOut()
        {
            _authenticationManager.SignOut();
        }       
    }

    public class AppContext : DbContext
    {
        public AppContext() : base("DefaultConnection") { }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public static AppContext Create()
        {
            return new AppContext();
        }

        private void BeforeSave()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Base)
                {
                    var entity = (Base)entry.Entity;

                    if (entry.State == EntityState.Added)
                    {
                        if (String.IsNullOrWhiteSpace(entity.Key))
                        {
                            entity.Key = Guid.NewGuid().ToString();
                        }

                        entity.CreatedAt = DateTime.Now;
                        entity.UpdatedAt = DateTime.Now;
                        entity.ActiveFlag = true;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = DateTime.Now;
                    }
                }
            }
        }
        public override int SaveChanges()
        {
            BeforeSave();
            return base.SaveChanges();
        }
        public async override Task<int> SaveChangesAsync()
        {
            BeforeSave();
            return await base.SaveChangesAsync();
        }
    }

    public class AppContextInitializer : DropCreateDatabaseAlways<AppContext>
    {
        public AppContextInitializer(AppContext context)
        {

        }

        protected override void Seed(AppContext context)
        {
            var Sections = new List<Section>
            {
                new Section{ Description="Section 1"},
                new Section{ Description="Section 2"},
            };

            var Topics = new List<Topic>
            {
                new Topic { Description="Topic 1", Section = Sections[0]},
                new Topic { Description="Topic 2", Section = Sections[0]},
                new Topic { Description="Topic 3", Section = Sections[1]},
                new Topic { Description="Topic 4", Section = Sections[1]}
            };

            var Questions = new List<Question>
            {
                new Question { Text="This is Question 1", Topic = Topics[0]},
                new Question { Text="This is Question 2", Topic = Topics[0]},
                new Question { Text="This is Question 3", Topic = Topics[1]},
                new Question { Text="This is Question 4", Topic = Topics[1]},
                new Question { Text="This is Question 5", Topic = Topics[2]},
                new Question { Text="This is Question 6", Topic = Topics[2]},
                new Question { Text="This is Question 7", Topic = Topics[3]},
                new Question { Text="This is Question 8", Topic = Topics[3]},
                new Question { Text="This is Question 9", Topic = Topics[0]},
                new Question { Text="This is Question 10", Topic = Topics[0]},
                new Question { Text="This is Question 11", Topic = Topics[1]},
                new Question { Text="This is Question 12", Topic = Topics[1]},
                new Question { Text="This is Question 13", Topic = Topics[2]},
                new Question { Text="This is Question 14", Topic = Topics[2]},
                new Question { Text="This is Question 15", Topic = Topics[3]},
                new Question { Text="This is Question 16", Topic = Topics[3]}
            };

            Sections.ForEach(x =>
            {
                context.Sections.Add(x);
            });

            Topics.ForEach(x =>
            {
                context.Topics.Add(x);
            });

            Questions.ForEach(x =>
            {
                context.Questions.Add(x);
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }

    public static class Service
    {
        public static async Task<string> GetResponse(string Url)
        {
            var Request = WebRequest.Create(Url);
            var Response = await Task.Factory.FromAsync<WebResponse>(Request.BeginGetResponse, Request.EndGetResponse, null);

            using (StreamReader Stream = new StreamReader(Response.GetResponseStream()))
            {
                return await Stream.ReadToEndAsync();
            }
        }

        public static async Task<List<User>> List()
        {
            return JsonConvert.DeserializeObject<ResponseList<User>>(await GetResponse("http://maqx.iprings.com/employee/Default.ashx")).List;
        }

        public static async Task<User> Get(string Code)
        {
            return JsonConvert.DeserializeObject<ResponseValue<User>>(await GetResponse("http://maqx.iprings.com/employee/Default.ashx?type=employee&value=" + Code)).Value;
        }

        public static async Task<User> Get(string Code, string Password)
        {
            return JsonConvert.DeserializeObject<ResponseValue<User>>(await GetResponse("http://maqx.iprings.com/employee/Default.ashx?type=auth&code=" + Code + "&password=" + Password)).Value;
        }
    }
}