using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogSkillName
    {
        public DogSkillName()
        {
            DogSkills = new List<DogSkill>();
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<DogSkill> DogSkills { get; set; }
    }
}