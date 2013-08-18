using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class DogSkillName
    {
        public DogSkillName()
        {
            this.DogSkills = new List<DogSkill>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DogSkill> DogSkills { get; set; }
    }
}
