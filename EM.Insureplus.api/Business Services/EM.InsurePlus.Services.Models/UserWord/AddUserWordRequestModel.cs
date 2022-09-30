namespace EM.InsurePlus.Services.Models
{
    public class AddUserWordRequestModel
    {
        public int UserId { get; set; }
        public WordKeysModel Word { get; set; }

        public AddUserWordRequestModel()
        {
            Word = new();
        }
    }
}