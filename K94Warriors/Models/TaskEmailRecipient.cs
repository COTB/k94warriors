using System.ComponentModel.DataAnnotations;
using K94Warriors.Enums;

namespace K94Warriors.Models
{
    public class TaskEmailRecipient
    {
        [Key]
        public int TaskEmailRecipientID { get; set; }

        [Required]
        public TaskTypeEnum TaskType { get; set; }

        public string EmailAddress { get; set; }
    }
}