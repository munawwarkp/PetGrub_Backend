namespace PetGrubBakcend.DTOs
{
    public class UserListDto
    {
        public int UserID {  get; set; }
        public string Name {  get; set; }
        public string Email { get; set; }
        public bool IsBlocked {  get; set; } = false;
    }
}
