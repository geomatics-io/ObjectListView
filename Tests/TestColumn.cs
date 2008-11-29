/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/25/2008 11:06 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/25/2008 JPP  Initial Version
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestColumn
    {
        [Test]
        public void TestAspectToStringFormat()
        {
            OLVColumn column = new OLVColumn();
            column.AspectName = "BirthDate";
            column.AspectToStringFormat = "{0:dd-mm-yy}";
            Assert.AreEqual(String.Format("{0:dd-mm-yy}", this.person1.BirthDate), column.GetStringValue(this.person1));
        }

        [Test]
        public void TestAspectToStringConverter() 
        {
            OLVColumn column = new OLVColumn();
            column.AspectName = "BirthDate";
            column.AspectToStringConverter = delegate(Object x) { return "AspectToStringConverter called"; };
            Assert.AreEqual("AspectToStringConverter called", column.GetStringValue(this.person1));
        }

        [TestFixtureSetUp]
        public void Init()
        {
            this.person1 = new Person("name", "occupation", 100, DateTime.Now, 1.0, true, "  photo  ", "comments");
            this.person2 = new Person2("name", "occupation", 100, DateTime.Now, 1.0, true, "  photo  ", "comments");
        }

        protected Person person1;
        protected Person2 person2;
    }

    [TestFixture]
    public class TestAspectGetting
    {
        public void ExecuteAspect(string aspectName, object expectedResult, Person person)
        {
            OLVColumn column = new OLVColumn();
            column.AspectName = aspectName;
            Assert.AreEqual(expectedResult, column.GetValue(person));
        }

        virtual public void ExecuteAspect(string aspectName, object expectedResult)
        {
            this.ExecuteAspect(aspectName, expectedResult, this.person1);
        }

        virtual public void ExecuteAspect2(string aspectName, object expectedResult)
        {
            this.ExecuteAspect(aspectName, expectedResult, this.person2);
        }

        [Test]
        public void TestSimpleField()
        {
            this.ExecuteAspect("Comments", "comments");
        }

        [Test]
        public void TestSimpleProperty()
        {
            this.ExecuteAspect("Occupation", "occupation");
        }

        [Test]
        public void TestSimpleMethod()
        {
            this.ExecuteAspect("GetRate", 1.0);
        }

        [Test]
        public void TestChainedField()
        {
            this.ExecuteAspect("Comments.ToUpper", "COMMENTS");
        }

        [Test]
        public void TestReturningValueType()
        {
            this.ExecuteAspect("CulinaryRating.ToString.Length", 3);
        }

        [Test]
        public void TestReturningValueType2()
        {
            this.ExecuteAspect("BirthDate.Year", this.person1.BirthDate.Year);
        }

        [Test]
        public void TestChainingValueTypes()
        {
            this.ExecuteAspect("BirthDate.Year.ToString.Length", 4);
        }

        [Test]
        public void TestChainedMethod()
        {
            this.ExecuteAspect("Photo.ToString.Trim", "photo");
        }

        [Test]
        public void TestVirtualMethod()
        {
            this.ExecuteAspect2("GetRate", 2.0);
        }

        [Test]
        public void TestOverriddenProperty()
        {
            this.ExecuteAspect("CulinaryRating", 100);
            this.ExecuteAspect2("CulinaryRating", 200);
        }

        [Test]
        public void TestWrongName()
        {
            this.ExecuteAspect("Unknown", "'Unknown' is not a parameter-less method, property or field of type 'BrightIdeasSoftware.Tests.Person'");
        }

        [Test]
        public void TestChainedWrongName()
        {
            this.ExecuteAspect("Photo.Unknown", "'Unknown' is not a parameter-less method, property or field of type 'System.String'");
        }

        [TestFixtureSetUp]
        public void Init()
        {
            this.person1 = new Person("name", "occupation", 100, DateTime.Now, 1.0, true, "  photo  ", "comments");
            this.person2 = new Person2("name", "occupation", 100, DateTime.Now, 1.0, true, "  photo  ", "comments");
        }

        protected Person person1;
        protected Person2 person2;
    }

    [TestFixture]
    public class TestAspectGeneration : TestAspectGetting
    {
        public void Execute<T>(string aspectName, object expectedResult, T person) where T: class
        {
            OLVColumn column = new OLVColumn();
            column.AspectName = aspectName;

            TypedColumn<T> tcolumn = new TypedColumn<T>(column);
            Assert.IsNull(column.AspectGetter);
            tcolumn.GenerateAspectGetter();
            Assert.IsNotNull(column.AspectGetter);
            Assert.AreEqual(expectedResult, column.GetValue(person));
        }

        override public void ExecuteAspect(string aspectName, object expectedResult)
        {
            this.Execute(aspectName, expectedResult, this.person1);
        }

        override public void ExecuteAspect2(string aspectName, object expectedResult)
        {
            this.Execute(aspectName, expectedResult, this.person2);
        }

        [Test]
        public void TestPropertyReplacedByNew()
        {
            OLVColumn column = new OLVColumn();
            column.AspectName = "CulinaryRating";

            TypedColumn<Person2> tcolumn = new TypedColumn<Person2>(column);
            Assert.IsNull(column.AspectGetter);
            tcolumn.GenerateAspectGetter();
            Assert.IsNotNull(column.AspectGetter);
            Assert.AreEqual(200, column.GetValue(this.person2));
        }
    }

    [TestFixture]
    public class TestAspectSetting
    {
        public void ExecuteAspect(string aspectName, object newValue, Person person)
        {
            OLVColumn column = new OLVColumn();
            column.AspectName = aspectName;
            column.PutValue(person, newValue);
            Assert.AreEqual(newValue, column.GetValue(person));
        }

        virtual public void ExecuteAspect(string aspectName, object newValue)
        {
            this.ExecuteAspect(aspectName, newValue, this.person1);
        }

        virtual public void ExecuteAspect2(string aspectName, object newValue)
        {
            this.ExecuteAspect(aspectName, newValue, this.person2);
        }

        [Test]
        public void TestSimpleField()
        {
            this.ExecuteAspect("Comments", "NEW comments");
        }

        [Test]
        public void TestSimpleProperty()
        {
            this.ExecuteAspect2("Occupation", "NEW occupation");
        }

        [Test]
        public void TestSimpleMethod()
        {
            this.person1.SetRate(0.0);
            OLVColumn column = new OLVColumn();
            column.AspectName = "SetRate";
            column.PutValue(this.person1, 10.0);
            Assert.AreEqual(10.0, this.person1.GetRate());
        }

        [Test]
        public void TestChaining()
        {
            DateTime dt = new DateTime(1965, 8, 28);
            this.ExecuteAspect("Parent.Parent.BirthDate", dt);
            Assert.AreEqual(dt, this.person1.Parent.Parent.BirthDate);
        }

        [Test]
        public void TestChaining2()
        {
            this.person2.SetRate(0.0);
            OLVColumn column = new OLVColumn();
            column.AspectName = "Parent.Parent.SetRate";
            column.PutValue(this.person2, 10.0);
            // Person2 doubles the rate
            Assert.AreEqual(20.0, this.person2.GetRate());
        }

        [TestFixtureSetUp]
        public void Init()
        {
            this.person1 = new Person("name", "occupation", 100, DateTime.Now, 1.0, true, "  photo  ", "comments");
            this.person2 = new Person2("name", "occupation", 100, DateTime.Now, 1.0, true, "  photo  ", "comments");

        }

        protected Person person1;
        protected Person2 person2;
    }
}
