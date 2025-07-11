namespace PetGrubBakcend.DTOs
{
    public class PaginatedResultDto<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageNumber {  get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; } //total items

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); //readonly calculated property

    }
}
