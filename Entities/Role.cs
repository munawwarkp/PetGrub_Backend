namespace PetGrubBakcend.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name {  get; set; }

        //each role have many user

        public ICollection<User> Users { get; set; }
    }
}
