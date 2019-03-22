using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.DataAccess
{
    public class Task
    {
        public Task()
        {
            TasksUnderParents = new HashSet<Task>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Task_ID")]
        public int TaskId { get; set; }
        [MaxLength(20)]
        [Required]
        public string TaskName { get; set; }
        [Column("Start_Date")]
        public DateTime StartDate { get; set; }
        [Column("End_Date")]
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }

        [Column("Parent_ID")]

        public int? ParentTaskId { get; set; }

        [ForeignKey("ParentTaskId")]
        [InverseProperty("TasksUnderParents")]
        public virtual Task ParentTask { get; set; }

        [InverseProperty("ParentTask")]
        public virtual ICollection<Task> TasksUnderParents { get; set; }
    }

}

