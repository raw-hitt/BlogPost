using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class Seeding
    {
        public Seeding()
        {

        }

        public static async Task SeedData(AppdbContext db)
        {
            await InsertDefaultUserAsync(db);
        }

        private static async Task InsertDefaultUserAsync(AppdbContext db)
        {
            try
            {
                if (await db.Users.FirstOrDefaultAsync() == null)
                {
                    await db.Users.AddAsync(new Users { UserName = "admin@blog.com", Password = "admin@blog.com" });
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                ExceptionLogs exl = new ExceptionLogs(ex.Message);
            }
        }

    }
}
