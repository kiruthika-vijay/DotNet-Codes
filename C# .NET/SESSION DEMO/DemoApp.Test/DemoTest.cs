using Microsoft.VisualStudio.TestPlatform.TestHost;
using Segue201DemoApp.entity;
using Segue201DemoApp.exception;
using Segue201DemoApp.method;
using System.Diagnostics;
namespace TestDemoApp.Test
{
    [TestFixture]
    public class Tests
    {
        AgeProcessor ageProcessor = new AgeProcessor();
        [SetUp]
        public void Setup()
        {
            Program program = new Program();
        }

        [Test]
        public void StudentAgeValidation()
        {
            List<Student> students = new List<Student>()
            {
                new Student { Id = 1, Name = "Asha", Age = 21},
                new Student { Id = 2, Name = "Birlin", Age = 17},
                new Student { Id = 3, Name = "Cathrine", Age = 32},
                new Student { Id = 4, Name = "Zubair", Age = 25},
                new Student { Id = 5, Name = "Zulina", Age = 12}
            };
            
            var studentList = students.Where(s => s.Age >= 18).ToList();
            // Assert
            Assert.AreEqual(3, studentList.Count);
            Assert.IsTrue(studentList.Any(s => s.Name == "Asha"), "Asha should be in the list.");
            Assert.IsTrue(studentList.Any(s => s.Name == "Cathrine"), "Cathrine should be in the list.");
            Assert.IsTrue(studentList.Any(s => s.Name == "Zubair"), "Zubair should be in the list.");
        }

        [Test]
        public void LambdaSumArray()
        {
            int[] array = { 3, 2, 6, 4, 8, 1, 5 };
            int sum = array.Sum(x => x);
            // Assert
            Assert.AreEqual(29, sum);
        }

        [TestCase(21, false)]
        [TestCase(-20, true)]
        [TestCase(220, true)]
        public void TestAgeProcessor(int age, bool ageNotValid)
        {
            if (ageNotValid)
            {
                var ex = Assert.Throws<InvalidAgeException>(() => ageProcessor.ValidateAge(age));
                Assert.That(ex.Message, Is.EqualTo("Invalid Age Count. Age cannot be negative or greater than 120"));
            }
            else
            {
                Assert.DoesNotThrow(() => ageProcessor.ValidateAge(age));   
            }
        }
    }
}