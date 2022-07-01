using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDbConnection.ApiModel;
using WebClassData;
using WebClassData.Entities;

namespace WebApiDbConnection.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherAndClassroomController : ControllerBase
      
    {
        private readonly IMapper _mapper;


        private readonly DemoDbContext _context;

            public TeacherAndClassroomController(DemoDbContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            [HttpPost]
            public ActionResult PostTeacherNClassRoom(TeacherClassroomApiModel teacherAndClassroomApiModel)
            {
            if (_context.Teachers == null)
            {
                return Problem("Entity ser 'EmployeeDbContext.Teacher' is null");
            }

            
            var obj = _mapper.Map<Teacher>(teacherAndClassroomApiModel.teacherApiModel);
            var classRoomList = _mapper.Map<List<Classroom>>(teacherAndClassroomApiModel.classroomApiModel);

            obj.ClassRoomList = classRoomList;
            _context.Teachers.Add(obj);
            _context.SaveChanges();
            return Ok();

        }
        [HttpGet]

        public async Task<TeacherClassroomApiModel> GetTeacherAndClassroom(int TeacherId)
        {

            TeacherClassroomApiModel teacherClassroomApiModel = new();

            TeacherApiModel teacherApiModel = new();
            List<ClassroomApiModel> classroomApiModelsList = new List<ClassroomApiModel>();

            var teacher = await _context.Teachers.FindAsync(TeacherId);
            if (teacher != null)
            {
                var classroomList = _context.Classrooms.Where(c => c.teacher.TeacherId == TeacherId).ToList();

                teacherApiModel = _mapper.Map<TeacherApiModel>(teacher);
                classroomApiModelsList = _mapper.Map<List<ClassroomApiModel>>(classroomList);

                teacherClassroomApiModel.teacherApiModel = teacherApiModel;
                teacherClassroomApiModel.classroomApiModel = classroomApiModelsList;

                return teacherClassroomApiModel;
            }
            else
            {
                return teacherClassroomApiModel;
            }}

            [HttpGet]

            public ActionResult GetAllTeachers()
            {

                if (_context.Teachers == null)
                {
                    return Ok(NotFound());
                }
                var allTeachers = _context.Teachers.Include(cl => cl.ClassRoomList).ToList();

                return Ok(allTeachers);
            }

            [HttpPut]

            public ActionResult PutTeacherAndClassrooms(int id, TeacherClassroomApiModel teacherClassroomApiModel)
            {
                var teacherObj = _context.Teachers.Find(id);
                if (teacherObj != null)
                {

                    var teacher = _mapper.Map<Teacher>(teacherClassroomApiModel.teacherApiModel);
                    var classroom = _mapper.Map<List<Classroom>>(teacherClassroomApiModel.classroomApiModel);
                    var updateTeacher = _context.Teachers.Where(t => t.TeacherId == id).Include(c => c.ClassRoomList).FirstOrDefault();


                    updateTeacher.TeacherName = teacher.TeacherName;
                    updateTeacher.TeacherMobNumber = teacher.TeacherMobNumber;

                    updateTeacher.ClassRoomList = classroom;

                    _context.Update(updateTeacher);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            [HttpPatch]

            public ActionResult PatchTeacherAndClassrooms(int id, int mobile)
            {
                var teacherObj = _context.Teachers.Where(t => t.TeacherId == id).FirstOrDefault();
                if (teacherObj != null)
                {
                    var update = _context.Teachers.Where(t => t.TeacherId == id).FirstOrDefault();


                    update.TeacherMobNumber = mobile;

                    _context.Update(update);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            [HttpDelete]
            public ActionResult DeleteTeacheAndClassroom(int id)
            {
                var teacher = _context.Teachers.Where(t => t.TeacherId == id).FirstOrDefault();
                if (teacher != null)
                {
                    _context.Teachers.Remove(teacher);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }


