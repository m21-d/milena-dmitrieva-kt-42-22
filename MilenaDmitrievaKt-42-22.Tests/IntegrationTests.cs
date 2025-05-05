using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Interfaces;
using MilenaDmitrievaKt_42_22.Interfaces.TeachersInterfaces;
using MilenaDmitrievaKt_42_22.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MilenaDmitrievaKt_42_22.Tests
{

    public class IntegrationTests
    {
        public readonly DbContextOptions<TeachersDbContext> _dbContextOptions;

        public IntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<TeachersDbContext>().UseInMemoryDatabase(databaseName: "teachers_db_new1").Options;
        }
        private async Task<TeachersDbContext> Arrange()
        {

            var ctx = new TeachersDbContext(_dbContextOptions);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var cafedras = new List<Cafedra>
            {
                new Cafedra
                {
                    CafedraName = "qwe",
                    Year=2000,
                },
                new Cafedra
                {
                    CafedraName = "d",
                    Year=2007,
                },
                new Cafedra
                {
                    CafedraName = "asd",
                    Year=1955,
                }
            };
            await ctx.Set<Cafedra>().AddRangeAsync(cafedras);
            var degrees = new List<Degree>
            {
                new Degree
                {
                    DegreeName = "y"
                },
                new Degree
                {
                    DegreeName = "d"
                },
                new Degree
                {
                    DegreeName = "e"
                },
            };
            await ctx.Set<Degree>().AddRangeAsync(degrees);
            var positions = new List<Position>
            {
                new Position
                {
                    PositionName = "f"
                },
                new Position
                {
                    PositionName = "ert"
                },
                new Position
                {
                    PositionName = "d"
                },

            };
            await ctx.Set<Position>().AddRangeAsync(positions);
            var teachers = new List<Teacher>
            {
                new Teacher
                {
                    Surname="Иванов",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=1,
                    DegreeId=2,
                    CafedraId=3,
                },
                new Teacher
                {
                    Surname="Петров",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=2,
                    DegreeId=3,
                    CafedraId=2,
                },

                new Teacher
                {
                    Surname="Сидоров",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=1,
                    DegreeId=2,
                    CafedraId=1,
                },
                new Teacher
                {
                    Surname="Иванов",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=3,
                    DegreeId=1,
                    CafedraId=1,
                },
                new Teacher
                {
                    Surname="Петров",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=2,
                    DegreeId=2,
                    CafedraId=1,
                },

                new Teacher
                {
                    Surname="Сидоров",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=1,
                    DegreeId=2,
                    CafedraId=1,
                },
                new Teacher
                {
                    Surname="Иванов",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=1,
                    DegreeId=3,
                    CafedraId=1,
                },
                new Teacher
                {
                    Surname="Петров",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=2,
                    DegreeId=2,
                    CafedraId=1,
                },

                new Teacher
                {
                    Surname="Сидоров",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=1,
                    DegreeId=2,
                    CafedraId=1,
                },
            };
            await ctx.Set<Teacher>().AddRangeAsync(teachers);
            var subjects = new List<Subject>
            {
                new Subject
                {
                    SubjectName="as"
                },
                new Subject
                {
                    SubjectName="sadf"
                },
                new Subject
                {
                    SubjectName="hgjk"
                },
                new Subject
                {
                    SubjectName="ad"
                },
                new Subject
                {
                    SubjectName="qwerty"
                },
                new Subject
                {
                    SubjectName="cxv"
                },
            };
            await ctx.Set<Subject>().AddRangeAsync(subjects);
            await ctx.SaveChangesAsync();
            return ctx;
        }
        /////////////////////////////////////////////////////////
        [Fact]
        public async Task GetTeachersByCafedraAsync_qwe_7Obj()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);
            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "qwe"
            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(7, result.Length);
        }


        [Fact]
        public async Task GetTeachersByPositionAsync_ert_3Obj()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                PositionName = "ert"
            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public async Task GetTeachersByDegreeAsync_e_2Obj()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                DegreeName = "e"
            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public async Task GetTeachersByCafedraDegreePositionAsync_d_e_f_0Obj()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "d",
                DegreeName = "e",
                PositionName = "f",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
        [Fact]
        public async Task GetTeachersByCafedraDegreePositionAsync_qwe_d_ert_2Obj()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "qwe",
                DegreeName = "d",
                PositionName = "ert",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Length);
        }
        ///////////////////////////////////////////////////////////////

        [Fact]
        public async Task GetCafedrasByYear_2007_1()
        {
            // Arrange
            var ctx = await Arrange();
            var cafedraService = new CafedraService(ctx);
            // Act
            var filter = new Filters.CafedraFilter
            {
                Year = 2007
            };
            var result = await cafedraService.GetCafedrasAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(1, result.Length);
        }


        [Fact]
        public async Task GetCafedrasByNumberOfTeachers_7_1()
        {
            // Arrange
            var ctx = await Arrange();
            var cafedraService = new CafedraService(ctx);
            // Act
            var filter = new Filters.CafedraFilter
            {
                NumberOfTeachers = 7
            };
            var result = await cafedraService.GetCafedrasAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(1, result.Length);
        }

        [Fact]
        public async Task GetCafedrasByYearNumberOfTeachers_2000_5_0()
        {
            // Arrange
            var ctx = await Arrange();
            var cafedraService = new CafedraService(ctx);
            // Act
            var filter = new Filters.CafedraFilter
            {
                Year = 2000,
                NumberOfTeachers = 5
            };
            var result = await cafedraService.GetCafedrasAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
        /////////////////////////////////////////////TODO: ниже прописать данные для тестов
        [Fact]
        public async Task GetSubjectsByHours_()
        {
            // Arrange
            var ctx = await Arrange();
            var subjectService = new SubjectService(ctx);
            // Act
            var filter = new Filters.SubjectFilter
            {
                HoursMin = 10,
                HoursMax = 20,
            };
            var result = await subjectService.GetSubjectsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(7, result.Length);
        }


        [Fact]
        public async Task GetSubjectsByTeacherSurname_()
        {
            // Arrange
            var ctx = await Arrange();
            var subjectService = new SubjectService(ctx);
            // Act
            var filter = new Filters.SubjectFilter
            {
                TeacherSurname = "a"
            };
            var result = await subjectService.GetSubjectsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(7, result.Length);
        }


        [Fact]
        public async Task GetSubjectsByHoursTeacherSurname_()
        {
            // Arrange
            var ctx = await Arrange();
            var subjectService = new SubjectService(ctx);
            // Act
            var filter = new Filters.SubjectFilter
            {
                HoursMin = 10,
                HoursMax = 20,
                TeacherSurname = "a"
            };
            var result = await subjectService.GetSubjectsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(7, result.Length);
        }
        ////////////////////////////////////////////
        [Fact]
        public async Task GetLessonsByTeacherSurname_()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            // Act
            var filter = new Filters.LessonsFilter
            {
                TeacherSurname = "a",
            };
            var result = await lessonsService.GetLessonsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(7, result.Length);
        }


        [Fact]
        public async Task GetLessonsByCafedraName_()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            // Act
            var filter = new Filters.LessonsFilter
            {
                CafedraName = "a",
            };
            var result = await lessonsService.GetLessonsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task GetLessonsBySubjectName_()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            // Act
            var filter = new Filters.LessonsFilter
            {
                SubjectName = "a"
            };
            var result = await lessonsService.GetLessonsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task GetLessonsByTeacherSurnameCafedraNameSubjectName_()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            // Act
            var filter = new Filters.LessonsFilter
            {
                TeacherSurname = "a",
                CafedraName = "a",
                SubjectName = "a"
            };
            var result = await lessonsService.GetLessonsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(7, result.Length);
        }
        ////////////////////////////////////////////////////////////////
        //TODO тесты по запросам на изменение кроме удаления:
        //Teacher
        [Fact]
        public async Task DeleteTeacher_3_True()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);
            int before = ctx.Set<Teacher>().Count();
            // Act
            int id = 3;
            await teacherService.DeleteTeacherAsync(id, CancellationToken.None);

            // Assert
            int after = ctx.Set<Teacher>().Count();
            var f = ctx.Set<Teacher>().Where(e => e.TeacherId == id).Count();
            Assert.True(((before - after) == 1) && (f == 0));
        }
        [Fact]
        public async Task UpdateTeacher_()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "d",
                DegreeName = "e",
                PositionName = "f",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
        [Fact]
        public async Task AddTeacher_()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "d",
                DegreeName = "e",
                PositionName = "f",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
        /////////////////////////////////////////////////
        //Subject
        [Fact]
        public async Task DeleteSubject_2_True()
        {
            // Arrange
            var ctx = await Arrange();
            var subjectService = new SubjectService(ctx);
            int before = ctx.Set<Subject>().Count();
            // Act
            int id = 2;
            await subjectService.DeleteSubjectAsync(id, CancellationToken.None);

            // Assert
            int after = ctx.Set<Subject>().Count();
            var f = ctx.Set<Subject>().Where(e => e.SubjectId == id).Count();
            Assert.True(((before - after) == 1) && (f == 0));
        }
        [Fact]
        public async Task UpdateSubject_()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "d",
                DegreeName = "e",
                PositionName = "f",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
        [Fact]
        public async Task AddSubject_()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "d",
                DegreeName = "e",
                PositionName = "f",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
        ////////////////////////////////////////////////////////////
        //Cafedra
        [Fact]
        public async Task DeleteCafedra_1_True()
        {
            // Arrange
            var ctx = await Arrange();
            var cafedraService = new CafedraService(ctx);

            int before = ctx.Set<Cafedra>().Count();
            // Act
            int id = 1;
            await cafedraService.DeleteCafedraAsync(id, CancellationToken.None);

            // Assert
            int after = ctx.Set<Cafedra>().Count();
            var f = ctx.Set<Cafedra>().Where(e => e.CafedraId == id).Count();
            Assert.True(((before - after) == 1) && (f == 0));
        }
        [Fact]
        public async Task UpdateCafedra_()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "d",
                DegreeName = "e",
                PositionName = "f",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
        [Fact]
        public async Task AddCafedra_()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "d",
                DegreeName = "e",
                PositionName = "f",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
        //////////////////////////////////////////////////////////
        //Lessons
        [Fact]
        public async Task UpdateLessons_()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "d",
                DegreeName = "e",
                PositionName = "f",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
        [Fact]
        public async Task AddLessons_()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "d",
                DegreeName = "e",
                PositionName = "f",

            };
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }
    }
}
