namespace K94Warriors.Models
{
    public class DogSkill
    {
        public int DogSkilID { get; set; }
        public int DogProfileID { get; set; }
        public int Level { get; set; }
        public int SkillNameId { get; set; }
        public virtual DogProfile DogProfile { get; set; }
        public virtual DogSkillName DogSkillName { get; set; }
    }
}