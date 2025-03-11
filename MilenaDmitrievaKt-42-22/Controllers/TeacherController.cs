using MilenaDmitrievaKt_42_22.Interfaces.TeachersInterfaces;
using MilenaDmitrievaKt_42_22.Filters.TeacherFilters;
using Microsoft.AspNetCore.Mvc;

namespace MilenaDmitrievaKt_42_22.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {

        private readonly ILogger<TeachersController> _logger;
        private readonly ITeacherService _teacherService;

        public TeachersController(ILogger<TeachersController> logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        [HttpPost(Name = "GetTeachersByCafedra")]
        public async Task<IActionResult> GetTeachersByCafedraAsync(TeacherCafedraFilter filter, CancellationToken cancellationToken =default)
        {
            var teachers=await _teacherService.GetTeachersByCafedraAsync(filter,cancellationToken);
            return Ok(teachers);
        }
    }
}