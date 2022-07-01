using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDbConnection.ApiModel
{
    public class TeacherApiModel
    {
        public int TeacherId { get; set; }

        [Column(TypeName = "Varchar(50)")]
        public string? TeacherName { get; set; }
        public long? TeacherMobNumber { get; set; }
    }
}
