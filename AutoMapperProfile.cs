using AutoMapper;
using WebApiDbConnection.ApiModel;
using WebClassData.Entities;
using WebApiDbConnection;
using WebClassData;

namespace WebApiDbConnection.MappingConfiguration
{
    public class AutoMapperProfile : Profile
    {
       public AutoMapperProfile()
        {
            CreateMap<Teacher, TeacherApiModel>();
            CreateMap<Classroom, ClassroomApiModel>();
            CreateMap<TeacherApiModel, Teacher>();
            CreateMap<ClassroomApiModel, Classroom>();
        }

    }
}
