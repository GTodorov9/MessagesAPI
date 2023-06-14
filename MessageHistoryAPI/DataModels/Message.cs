using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageHistoryAPI.DataModels
{
    [Table("messages")]
    public class Message
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  Id { get; set; }
        [Column("content")]
        public required string Content { get; set; }
        [Column("timeStamp")]
        public DateTime TimeStamp { get; set; }
        [Column("fromUser_Id")]
        public required string FromUser_Id { get; set; }
        [Column("toUser_Id")]
        public required string ToUser_Id { get; set; }
    }
}
