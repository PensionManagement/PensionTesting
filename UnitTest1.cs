using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PensionorDetailAPI.Models;
using PensionorDetailAPI.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PensionTesting
{
    public class Tests
    {
        List<PensionerDetail> pension = new List<PensionerDetail>();
        IQueryable<PensionerDetail> pensiondata;
        Mock<DbSet<PensionerDetail>> mockSet;
        Mock<PensionManagementDBContext> contextmock;


        [SetUp]
        public void Setup()
        {
            pension = new List<PensionerDetail>()
            {
                new PensionerDetail{AadhaarNo="123456789012",Name="SriNisha"},
                new PensionerDetail{AadhaarNo="123456789987",Name="AkPro"},

            };
            pensiondata = pension.AsQueryable();
            mockSet = new Mock<DbSet<PensionerDetail>>();
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.Provider).Returns(pensiondata.Provider);
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.Expression).Returns(pensiondata.Expression);
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.ElementType).Returns(pensiondata.ElementType);
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.GetEnumerator()).Returns(pensiondata.GetEnumerator());
            var p = new DbContextOptions<PensionManagementDBContext>();
            contextmock = new Mock<PensionManagementDBContext>(p);
            contextmock.Setup(x => x.PensionerDetails).Returns(mockSet.Object);
        }

        [Test]
        public void GetName()
        {

            var searchrepo = new PensionerDetailRepo(contextmock.Object);
            var list = searchrepo.Getaadhar("123456789012");
            string name = list.Name;
            Assert.AreEqual("SriNisha", name);
        }
    }
}