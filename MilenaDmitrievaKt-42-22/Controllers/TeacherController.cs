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

        public TeachersController(ILogger<TeachersController> logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        [HttpPost(Name = "GetTeachersByCafedraDegreePosition")]
        public async Task<IActionResult> GetTeachersAsync(TeacherFilter filter, CancellationToken cancellationToken =default)
        {
            var teachers=await _teacherService.GetTeachersAsync(filter,cancellationToken);
            return Ok(teachers);
        }

        [HttpPost(Name = "GetCafedrasByYearNumberOfTeachers")]
        public async Task<IActionResult> GetCafedrasAsync(CafedraFilter filter, CancellationToken cancellationToken = default)
        {
            var a = await _cafedraService.GetCafedrasAsync(filter, cancellationToken);
            return Ok(a);
        }

        [HttpPost(Name = "GetSubjectsByHoursTeacherSurname")]
        public async Task<IActionResult> GetSubjectsAsync(SubjectFilter filter, CancellationToken cancellationToken = default)
        {
            var a = await _subjectService.GetSubjectsAsync(filter, cancellationToken);
            return Ok(a);
        }

        [HttpPost(Name = "GetLessonsByTeacherSurnameCafedraNameSubjectName")]
        public async Task<IActionResult> GetSLessonsAsync(LessonsFilter filter, CancellationToken cancellationToken = default)
        {
            var a = await _lessonsService.GetLessonsAsync(filter, cancellationToken);
            return Ok(a);
        }
    }
}