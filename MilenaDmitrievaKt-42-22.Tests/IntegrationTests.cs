using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database;
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

        [Fact]
        public async Task GetTeachersByCafedraAsync_AAA_2Obj()
        {
            // Arrange
            var ctx = new TeachersDbContext(_dbContextOptions);
            var teacherService = new TeacherService(ctx);
            var cafedras = new List<Cafedra>
            {
                new Cafedra
                {
                    CafedraName = "ААА"
                },
                new Cafedra
                {
                    CafedraName = "АБВ"
                }
            };
            await ctx.Set<Cafedra>().AddRangeAsync(cafedras);
            var degrees = new List<Degree>
            {
                new Degree
                {
                    DegreeName = "d"
                },
                
            };
            await ctx.Set<Degree>().AddRangeAsync(degrees);
            var positions = new List<Position>
            {
                new Position
                {
                    PositionName = "f"
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
                    DegreeId=1,
                    CafedraId=1,
                },
                new Teacher
                {
                    Surname="Петров",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=1,
                    DegreeId=1,
                    CafedraId=1,
                },
                new Teacher
                {
                    Surname="Сидоров",
                    Name="Иван",
                    Patronym="Иванович",
                    PositionId=1,
                    DegreeId=1,
                    CafedraId=2,
                }

            };
            await ctx.Set<Teacher>().AddRangeAsync(teachers);

            await ctx.SaveChangesAsync();

            // Act
            var filter = new Filters.TeacherFilters.TeacherFilter
            {
                CafedraName = "ААА"
            };
            var result = await teacherService.GetTeachersByCafedraAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Length);
        }
    }
}
