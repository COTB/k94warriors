using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogSkill
    {
        [Key]
        public int DogSkillID { get; set; }

        public int DogProfileID { get; set; }
        public virtual DogProfile DogProfile { get; set; }

        public int SkillNameID { get; set; }
        public virtual DogSkill DogSkillName { get; set; }

        public string Level { get; set; }
    }
}
