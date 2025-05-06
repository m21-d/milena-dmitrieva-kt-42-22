using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Interfaces;
using MilenaDmitrievaKt_42_22.Interfaces.TeachersInterfaces;
using MilenaDmitrievaKt_42_22.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
                    Surname="Иванов123",
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
            var lessons = new List<Lessons>
            {
                new Lessons
                {
                    SubjectId=1,
                    TeacherId=7,
                    Hours=10
                },
                new Lessons
                {
                    SubjectId=1,
                    TeacherId=3,
                    Hours=8
                },
                new Lessons
                {
                    SubjectId=2,
                    TeacherId=7,
                    Hours=16
                },
                new Lessons
                {
                    SubjectId=2,
                    TeacherId=4,
                    Hours=6
                },
                new Lessons
                {
                    SubjectId=2,
                    TeacherId=3,
                    Hours=12
                },
                new Lessons
                {
                    SubjectId=2,
                    TeacherId=3,
                    Hours=10
                },
                new Lessons
                {
                    SubjectId=6,
                    TeacherId=3,
                    Hours=10
                },
                new Lessons
                {
                    SubjectId=2,
                    TeacherId=8,
                    Hours=10
                },
                new Lessons
                {
                    SubjectId=5,
                    TeacherId=9,
                    Hours=10
                },
                new Lessons
                {
                    SubjectId=3,
                    TeacherId=5,
                    Hours=10
                },
            };
            await ctx.Set<Lessons>().AddRangeAsync(lessons);
            await ctx.SaveChangesAsync();
            var heads = new List<int>
            {
                7,1,2
            };
            for (int i = 0; i < 3; i++)
            {
                ctx.Set<Cafedra>().Find(i + 1)!.HeadId = heads[i];
            }
            await ctx.SaveChangesAsync();

            return ctx;
        }
        /////////////////////////////////////////////////////////
        [Fact]
        public async Task GetSubjectsByTeachersByCafedraByHead_7_5()
        {
            // Arrange
            var ctx = await Arrange();
            var cafedraService = new CafedraService(ctx);
            // Act
            var id = 7;
            var result = await cafedraService.GetSubjectsByTeachersByCafedraByHeadAsync(id, CancellationToken.None);

            // Assert
            Assert.Equal(5, result.Length);
        }
        //////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////
        [Fact]
        public async Task GetSubjectsByHours_12_20_1()
        {
            // Arrange
            var ctx = await Arrange();
            var subjectService = new SubjectService(ctx);
            // Act
            var filter = new Filters.SubjectFilter
            {
                HoursMin = 12,
                HoursMax = 20,
            };
            var result = await subjectService.GetSubjectsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(1, result.Length);
        }


        [Fact]
        public async Task GetSubjectsByTeacherSurname_Иванов123_3()
        {
            // Arrange
            var ctx = await Arrange();
            var subjectService = new SubjectService(ctx);
            // Act
            var filter = new Filters.SubjectFilter
            {
                TeacherSurname = "Иванов123"
            };
            var result = await subjectService.GetSubjectsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(3, result.Length);
        }


        [Fact]
        public async Task GetSubjectsByHoursTeacherSurname_10_60_Иванов_2()
        {
            // Arrange
            var ctx = await Arrange();
            var subjectService = new SubjectService(ctx);
            // Act
            var filter = new Filters.SubjectFilter
            {
                HoursMin = 10,
                HoursMax = 60,
                TeacherSurname = "Иванов"
            };
            var result = await subjectService.GetSubjectsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Length);
        }
        ////////////////////////////////////////////
        [Fact]
        public async Task GetLessonsByTeacherSurname_Иванов123_4()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            // Act
            var filter = new Filters.LessonsFilter
            {
                TeacherSurname = "Иванов123",
            };
            var result = await lessonsService.GetLessonsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(4, result.Length);
        }


        [Fact]
        public async Task GetLessonsByCafedraName_qwe_10()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            // Act
            var filter = new Filters.LessonsFilter
            {
                CafedraName = "qwe",
            };
            var result = await lessonsService.GetLessonsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(10, result.Length);
        }
        [Fact]
        public async Task GetLessonsByCafedraName_d_0()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            // Act
            var filter = new Filters.LessonsFilter
            {
                CafedraName = "d",
            };
            var result = await lessonsService.GetLessonsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.Length);
        }

        [Fact]
        public async Task GetLessonsBySubjectName_hgjk_1()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            // Act
            var filter = new Filters.LessonsFilter
            {
                SubjectName = "hgjk"
            };
            var result = await lessonsService.GetLessonsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(1, result.Length);
        }

        [Fact]
        public async Task GetLessonsByTeacherSurnameCafedraNameSubjectName_Иванов_qwe_sadf_2()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            // Act
            var filter = new Filters.LessonsFilter
            {
                TeacherSurname = "Иванов",
                CafedraName = "qwe",
                SubjectName = "sadf"
            };
            var result = await lessonsService.GetLessonsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Length);
        }
        ////////////////////////////////////////////////////////////////

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
        public async Task UpdateTeacher_true()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);
            int before = ctx.Set<Teacher>().Count();
            // Act
            var filter = new Filters.TeacherUpdate
            {
                Surname = "a",
                DegreeId = 5,
                CafedraId = 2,
                TeacherId = 1,
            };
            var result = await teacherService.UpdateTeacherAsync(filter, CancellationToken.None);

            // Assert
            int after = ctx.Set<Teacher>().Count();
            var t = ctx.Set<Teacher>().Find(1);
            bool f = t.CafedraId == 2 && t.DegreeId == 5 && t.Surname == "a";
            Assert.True(((before - after) == 0) && f);
        }
        [Fact]
        public async Task AddTeacher_true()
        {
            // Arrange
            var ctx = await Arrange();
            var teacherService = new TeacherService(ctx);
            int before = ctx.Set<Teacher>().Count();
            // Act
            var filter = new Filters.TeacherAdd
            {
                Surname = "a",
                Name = "b",
                Patronym = "c",
                DegreeId = 5,
                PositionId = 1,
                CafedraId = 2,
            };
            var result = await teacherService.AddTeacherAsync(filter, CancellationToken.None);

            // Assert
            int after = ctx.Set<Teacher>().Count();
            Assert.True(((before - after) == -1));
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
        public async Task UpdateSubject_true()
        {
            // Arrange
            var ctx = await Arrange();
            var subjectService = new SubjectService(ctx);
            int before = ctx.Set<Subject>().Count();
            // Act
            var filter = new Filters.SubjectUpdate
            {
                SubjectId = 3,
                SubjectName = "p123",
            };
            var result = await subjectService.UpdateSubjectAsync(filter, CancellationToken.None);

            // Assert
            int after = ctx.Set<Subject>().Count();
            var t = ctx.Set<Subject>().Find(3);
            bool f = t.SubjectName == "p123";
            Assert.True(((before - after) == 0) && f);
        }
        [Fact]
        public async Task AddSubject_true()
        {
            // Arrange
            var ctx = await Arrange();
            var subjectService = new SubjectService(ctx);
            int before = ctx.Set<Subject>().Count();
            // Act
            var filter = new Filters.SubjectAdd
            {
                SubjectName = "asdf"
            };
            var result = await subjectService.AddSubjectAsync(filter, CancellationToken.None);

            // Assert
            int after = ctx.Set<Subject>().Count();
            Assert.True(((before - after) == -1));
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
        public async Task UpdateCafedra_true()
        {
            // Arrange
            var ctx = await Arrange();
            var cafedraService = new CafedraService(ctx);
            int before = ctx.Set<Cafedra>().Count();
            // Act
            var filter = new Filters.CafedraUpdate
            {
                CafedraId = 2,
                CafedraName = "g567",
                HeadId = 4,
            };
            var result = await cafedraService.UpdateCafedraAsync(filter, CancellationToken.None);

            // Assert
            int after = ctx.Set<Cafedra>().Count();
            var t = ctx.Set<Cafedra>().Find(2);
            bool f = t.CafedraName == "g567" && t.HeadId == 4;
            Assert.True(((before - after) == 0) && f);
        }
        [Fact]
        public async Task AddCafedra_true()
        {
            // Arrange
            var ctx = await Arrange();
            var cafedraService = new CafedraService(ctx);
            int before = ctx.Set<Cafedra>().Count();
            // Act
            var filter = new Filters.CafedraAdd
            {
                CafedraName = "asd",
                Year = 2002
            };
            var result = await cafedraService.AddCafedraAsync(filter, CancellationToken.None);

            // Assert
            int after = ctx.Set<Cafedra>().Count();
            Assert.True(((before - after) == -1));
        }
        //////////////////////////////////////////////////////////
        //Lessons
        [Fact]
        public async Task UpdateLessons_true()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            int before = ctx.Set<Lessons>().Count();
            // Act
            var filter = new Filters.LessonsUpdate
            {
                Hours = 120,
                TeacherId = 8,
                SubjectId = 4,
                LessonsId = 2,
            };
            var result = await lessonsService.UpdateLessonsAsync(filter, CancellationToken.None);

            // Assert
            int after = ctx.Set<Lessons>().Count();
            var t = ctx.Set<Lessons>().Find(2);
            bool f = t.TeacherId == 8 && t.Hours == 120 && t.SubjectId == 4;
            Assert.True(((before - after) == 0) && f);
        }
        [Fact]
        public async Task AddLessons_true()
        {
            // Arrange
            var ctx = await Arrange();
            var lessonsService = new LessonsService(ctx);
            int before = ctx.Set<Lessons>().Count();
            // Act
            var filter = new Filters.LessonsAdd
            {
                SubjectId = 3,
                TeacherId = 2,
                Hours = 32,
            };
            var result = await lessonsService.AddLessonsAsync(filter, CancellationToken.None);

            // Assert
            int after = ctx.Set<Lessons>().Count();
            Assert.True(((before - after) == -1));
        }
    }
}
