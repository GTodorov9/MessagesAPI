using System.ComponentModel.DataAnnotations;

namespace MessageHistoryAPI.BindingModels
{
    public class SaveMessagesBindingModel
    {
        [Required]
        public List<MessageBindingModel> Messages { get; set; } = new();
    }
}
