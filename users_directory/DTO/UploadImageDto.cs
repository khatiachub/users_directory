namespace users_directory.DTO
{
    public class UploadImageDto
    {
        public int UserId { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
