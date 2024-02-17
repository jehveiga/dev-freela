namespace DevFreela.Core.Entities
{
    public class UserSkill(int idUser, int idSkill) : BaseEntity
    {
        public int IdUser { get; private set; } = idUser;
        public int IdSkill { get; private set; } = idSkill;

        public Skill? Skill { get; private set; }
    }
}
