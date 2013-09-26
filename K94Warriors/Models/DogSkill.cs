using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogSkill
    {
        [Key]
        public int DogSkilID { get; set; }
        
        public int DogProfileID { get; set; }
        
        public int Level { get; set; }
        
        public int SkillNameId { get; set; }

        [ForeignKey("DogProfileID")]
        public virtual DogProfile DogProfile { get; set; }

        [ForeignKey("SkillNameId")]
        public virtual DogSkillName DogSkillName { get; set; }
    }
}