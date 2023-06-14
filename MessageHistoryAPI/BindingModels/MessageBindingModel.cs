using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MessageHistoryAPI.BindingModels
{
    public class MessageBindingModel
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public required string FromUser_Id { get; set; }
        public required string ToUser_Id { get; set; }
    }
}
