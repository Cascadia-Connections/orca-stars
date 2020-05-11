using System;
using Xunit;
using OrcaStarsWebApplication.Repositories;
using OrcaStarsWebApplication.Services;
using OrcaStarsWebApplication.Models;
using Moq;
using XUnitTestOrcaStars.TestData;

namespace XUnitTestOrcaStars
{
    public class UnitTest1
    {
        [Fact]
        public void BusinessService_NameTest_NotNullOrEmpty()
        {
            var mockRepo = new Mock<IBusinessRepository>();

            mockRepo.Setup(r => r.GetBusiness()).Returns(BusinessData.GetBusinesses());
            mockRepo.Setup(r => r.GetCount()).Returns(BusinessData.GetCount());

            var service = new BusinessServices(mockRepo.Object);

            Assert.False(service.CheckNameNotNullOrEmpty());
        }

        [Fact]
        public void BusinessService_DescriptionTest_NotNullOrEmpty()
        {
            var mockRepo = new Mock<IBusinessRepository>();

            mockRepo.Setup(r => r.GetBusiness()).Returns(BusinessData.GetBusinesses());
            mockRepo.Setup(r => r.GetCount()).Returns(BusinessData.GetCount());

            var service = new BusinessServices(mockRepo.Object);

            Assert.False(service.CheckDescriptionNotNullOrEmpty());
        }
    }
}