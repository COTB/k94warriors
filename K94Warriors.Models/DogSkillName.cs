using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogSkillName
    {
        [Key]
        public int DogSkillNameID { get; set; }

        public string SkillName { get; set; }

        public virtual ICollection<DogSkill> DogSkills { get; set; }
    }
}
