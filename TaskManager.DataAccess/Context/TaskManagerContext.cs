using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TaskManager.DataAccess
{
    public class TaskManagerContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public TaskManagerContext() : base("name=TaskMangerDbConString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaskManagerContext,
                                      TaskManager.DataAccess.Migrations.Configuration>("TaskMangerDbConString"));

        }

    }
}
