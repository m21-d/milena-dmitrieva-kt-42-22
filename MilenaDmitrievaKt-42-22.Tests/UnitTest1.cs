
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void IsValidDegreeName0()
        {
            
            var testDegree = new Degree { DegreeName="ך. ע. ם."};

            var result = testDegree.IsValidDegree();

            Assert.True(result);
        }

        [Fact]
        public void IsValidDegreeName10()
        {

            var testDegree = new Degree { DegreeName = " ךך. ע. ם." };

            var result = testDegree.IsValidDegree();

            Assert.False(result);
        }
        [Fact]
        public void IsValidDegreeName1()
        {

            var testDegree = new Degree { DegreeName = "fgjh" };

            var result = testDegree.IsValidDegree();

            Assert.False(result);
        }
        [Fact]
        public void IsValidDegreeName2()
        {

            var testDegree = new Degree { DegreeName = "ך. ע.   ם." };

            var result = testDegree.IsValidDegree();

            Assert.False(result);
        }
        [Fact]
        public void IsValidDegreeName3()
        {

            var testDegree = new Degree { DegreeName = "ך. ׂ. ם." };

            var result = testDegree.IsValidDegree();

            Assert.False(result);
        }

        [Fact]
        public void IsValidDegreeName4()
        {

            var testDegree = new Degree { DegreeName = "ך. t. ם." };

            var result = testDegree.IsValidDegree();

            Assert.False(result);
        }

        [Fact]
        public void IsValidDegreeName5()
        {

            var testDegree = new Degree { DegreeName = "ך. עוץ. ם." };

            var result = testDegree.IsValidDegree();

            Assert.False(result);
        }
        ////////////////////////////////////////////////////
        [Fact]
        public void IsValidName_True()
        {
            var testName = "ְבגדהוזח";
            var testTeacher = new Teacher();

            var result=testTeacher.IsValidName(testName);

            Assert.True(result);
        }
        [Fact]
        public void IsValidName_NotLetters_False()
        {
            var testName = "ְבג4דה";
            var testTeacher = new Teacher();

            var result = testTeacher.IsValidName(testName);

            Assert.False(result);
        }
        [Fact]
        public void IsValidName_Lowercase_False()
        {
            var testName = "אבגדה";
            var testTeacher = new Teacher();

            var result = testTeacher.IsValidName(testName);

            Assert.False(result);
        }
    }
}