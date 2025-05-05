using MilenaDmitrievaKt_42_22.Interfaces.TeachersInterfaces;
using MilenaDmitrievaKt_42_22.Filters.TeacherFilters;
using MilenaDmitrievaKt_42_22.Filters;
using Microsoft.AspNetCore.Mvc;
using MilenaDmitrievaKt_42_22.Interfaces;

namespace MilenaDmitrievaKt_42_22.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {

        private readonly ILogger<TeachersController> _logger;
        private readonly ITeacherService _teacherService;
        private readonly ICafedraService _cafedraService;
        private readonly ISubjectService _subjectService;
        private readonly ILessonsService _lessonsService;

        public TeachersController(ILogger<TeachersController> logger, ITeacherService teacherService, ICafedraService cafedraService, ISubjectService subjectService, ILessonsService lessonsService)
        {
            _logger = logger;
            _teacherService = teacherService;
            _cafedraService = cafedraService;
            _subjectService = subjectService;
            _lessonsService = lessonsService;
        }

        [HttpPost("GetSubjectsByTeachersByCafedraByHeadAsync")]
        public async Task<IActionResult> GetSubjectsByTeachersByCafedraByHeadAsync(int id, CancellationToken cancellationToken = default)
        {
            var a = await _cafedraService.GetSubjectsByTeachersByCafedraByHeadAsync(id, cancellationToken);
            return Ok(a);
        }

        [HttpPost("GetTeachersByCafedraDegreePosition")]
        public async Task<IActionResult> GetTeachersAsync(TeacherFilter filter, CancellationToken cancellationToken =default)
        {
            var teachers=await _teacherService.GetTeachersAsync(filter,cancellationToken);
            return Ok(teachers);
        }

        [HttpPost("GetCafedrasByYearNumberOfTeachers")]
        public async Task<IActionResult> GetCafedrasAsync(CafedraFilter filter, CancellationToken cancellationToken = default)
        {
            var a = await _cafedraService.GetCafedrasAsync(filter, cancellationToken);
            return Ok(a);
        }

        [HttpPost("GetSubjectsByHoursTeacherSurname")]
        public async Task<IActionResult> GetSubjectsAsync(SubjectFilter filter, CancellationToken cancellationToken = default)
        {
            var a = await _subjectService.GetSubjectsAsync(filter, cancellationToken);
            return Ok(a);
        }

        [HttpPost("GetLessonsByTeacherSurnameCafedraNameSubjectName")]
        public async Task<IActionResult> GetLessonsAsync(LessonsFilter filter, CancellationToken cancellationToken = default)
        {
            var a = await _lessonsService.GetLessonsAsync(filter, cancellationToken);
            return Ok(a);
        }

        [HttpDelete("DeleteCafedra")]
        public async Task<IActionResult> DeleteCafedraAsync(int id,CancellationToken cancellationToken = default)
        {
            var a= await _cafedraService.DeleteCafedraAsync(id,cancellationToken);
            return Ok(a);
        }
        [HttpPost("UpdateCafedra")]
        public async Task<IActionResult> UpdateCafedraAsync(CafedraUpdate newCafedra, CancellationToken cancellationToken = default)
        {
            var a = await _cafedraService.UpdateCafedraAsync(newCafedra, cancellationToken);
            return Ok(a);
        }
        [HttpPost("AddCafedra")]
        public async Task<IActionResult> AddCafedraAsync(CafedraAdd newCafedra, CancellationToken cancellationToken = default)
        {
            var a = await _cafedraService.AddCafedraAsync(newCafedra, cancellationToken);
            return Ok(a);
        }

        [HttpDelete("DeleteTeacher")]
        public async Task<IActionResult> DeleteTeacherAsync(int id, CancellationToken cancellationToken = default)
        {
            var a = await _teacherService.DeleteTeacherAsync(id, cancellationToken);
            return Ok(a);
        }
        [HttpPost("UpdateTeacher")]
        public async Task<IActionResult> UpdateTeacherAsync(TeacherUpdate t, CancellationToken cancellationToken = default)
        {
            var a = await _teacherService.UpdateTeacherAsync(t, cancellationToken);
            return Ok(a);
        }
        [HttpPost("AddTeacher")]
        public async Task<IActionResult> AddTeacherAsync(TeacherAdd t, CancellationToken cancellationToken = default)
        {
            var a = await _teacherService.AddTeacherAsync(t, cancellationToken);
            return Ok(a);
        }

        [HttpDelete("DeleteSubject")]
        public async Task<IActionResult> DeleteSubjectAsync(int id, CancellationToken cancellationToken = default)
        {
            var a = await _subjectService.DeleteSubjectAsync(id, cancellationToken);
            return Ok(a);
        }
        [HttpPost("UpdateSubject")]
        public async Task<IActionResult> UpdateSubjectAsync(SubjectUpdate t, CancellationToken cancellationToken = default)
        {
            var a = await _subjectService.UpdateSubjectAsync(t, cancellationToken);
            return Ok(a);
        }
        [HttpPost("AddSubject")]
        public async Task<IActionResult> AddSubjectAsync(SubjectAdd t, CancellationToken cancellationToken = default)
        {
            var a = await _subjectService.AddSubjectAsync(t, cancellationToken);
            return Ok(a);
        }
        [HttpPost("UpdateLessons")]
        public async Task<IActionResult> UpdateLessonsAsync(LessonsUpdate t, CancellationToken cancellationToken = default)
        {
            var a = await _lessonsService.UpdateLessonsAsync(t, cancellationToken);
            return Ok(a);
        }
        [HttpPost("AddLessons")]
        public async Task<IActionResult> AddLessonsAsync(LessonsAdd t, CancellationToken cancellationToken = default)
        {
            var a = await _lessonsService.AddLessonsAsync(t, cancellationToken);
            return Ok(a);
        }
    }
}