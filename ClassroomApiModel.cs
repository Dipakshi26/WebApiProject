using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDbConnection.ApiModel
{
    public class ClassroomApiModel
    {
        public int ClassRoomId { get; set; }

        [Column(TypeName = "Varchar(50)")]
        public string? ClassName { get; set; }
    }
}
