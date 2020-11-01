using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Austumate.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<CampusModel> Campuses { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<CollegeModel> Colleges { get; set; }
        public DbSet<MajorModel> Majors { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<SemesterModel> Semesters { get; set; }
        public DbSet<ProfileModel> Profiles { get; set; }
        public DbSet<AvatarModel> Avatars { get; set; }
        public DbSet<CarouselModel> Carousels { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<RegistrarModel> Registrars { get; set; }
        public DbSet<AdministratorModel> Administrators { get; set; }
        public DbSet<AssignCourseModel> AssignCourses { get; set; }
        public DbSet<EnrollStudentModel> EnrollStudents { get; set; }
        public DbSet<AttendanceScoreModel> AttendanceScores { get; set; }
        public DbSet<LabScoreModel> LabScores { get; set; }
        public DbSet<HomeworkScoreModel> HomeworkScores { get; set; }
        public DbSet<FinalExamScoreModel> FinalExamScores { get; set; }
        public DbSet<ScoresheetModel> Scoresheets { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}