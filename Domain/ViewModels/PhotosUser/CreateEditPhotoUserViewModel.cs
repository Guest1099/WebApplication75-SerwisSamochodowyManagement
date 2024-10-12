namespace Domain.ViewModels.PhotosUser
{
    public class CreateEditPhotoUserViewModel : PropertyBaseViewModel
    {
        public string PhotoUserId { get; set; }
        public byte[] PhotoData { get; set; }
        public string UserId { get; set; }

    }
}
