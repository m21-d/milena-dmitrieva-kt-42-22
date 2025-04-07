
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Tests
{
    public class UnitTest1
    {
        
        [Fact]
        public void IsValidName_True()
        {
            var testName = "��������";
            var testTeacher = new Teacher();

            var result=testTeacher.IsValidName(testName);

            Assert.True(result);
        }
        [Fact]
        public void IsValidName_NotLetters_False()
        {
            var testName = "���4��";
            var testTeacher = new Teacher();

            var result = testTeacher.IsValidName(testName);

            Assert.False(result);
        }
        [Fact]
        public void IsValidName_Lowercase_False()
        {
            var testName = "�����";
            var testTeacher = new Teacher();

            var result = testTeacher.IsValidName(testName);

            Assert.False(result);
        }
    }
}