using advisor_backend.Controllers;
using advisor_backend.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace advisor_backend.tests
{
    public class AdvisorsControllerTests
    {
        private readonly IMemoryCache _cache;

        public AdvisorsControllerTests()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        [Fact]
        public void Test_GetAdvisors_ReturnsEmptyList()
        {
            // Arrange
            var controller = new AdvisorsController(_cache);

            // Act
            var result = controller.GetAdvisors() as OkObjectResult;
            var data = result.Value as List<Advisor>;

            // Assert
            Assert.NotNull(result);
            Assert.Empty(data);
        }

        [Fact]
        public void Test_GetAdvisor_ReturnsNotFound()
        {
            // Arrange
            var controller = new AdvisorsController(_cache);
            var advisorId = 1;

            // Act
            var result = controller.GetAdvisor(advisorId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_CreateAdvisor_ReturnsNewAdvisor()
        {
           
            var controller = new AdvisorsController(_cache);
            var advisor = new Advisor
            {
                Name = "John Doe",
                SIN = "123456789",
                Address = "123 Main St",
                Phone = "555-1234"
            };

            var result = controller.CreateAdvisor(advisor) as OkObjectResult;
            var createdAdvisor = result.Value as Advisor;

            Assert.NotNull(result);
            Assert.Equal(advisor.Name, createdAdvisor.Name);
            Assert.Equal(advisor.SIN, createdAdvisor.SIN);
            Assert.Equal(advisor.Address, createdAdvisor.Address);
            Assert.Equal(advisor.Phone, createdAdvisor.Phone);
        }

        [Fact]
        public void Test_UpdateAdvisor_ReturnsUpdatedAdvisor()
        {
            
            var controller = new AdvisorsController(_cache);
            var advisorId = 1;
            var updatedAdvisor = new Advisor
            {
                Name = "Jane Smith",
                SIN = "987654321",
                Address = "456 Oak St",
                Phone = "555-5678"
            };

            var result = controller.UpdateAdvisor(advisorId, updatedAdvisor) as OkObjectResult;
            var updated = result.Value as Advisor;

           
            Assert.NotNull(result);
            Assert.Equal(updatedAdvisor.Name, updated.Name);
            Assert.Equal(updatedAdvisor.SIN, updated.SIN);
            Assert.Equal(updatedAdvisor.Address, updated.Address);
            Assert.Equal(updatedAdvisor.Phone, updated.Phone);
        }

        [Fact]
        public void Test_DeleteAdvisor_ReturnsOk()
        {
      
            var controller = new AdvisorsController(_cache);
            var advisor = new Advisor
            {
                Name = "peter",
                SIN = "123456789",
                Address = "123 Main St",
                Phone = "454545333"
            };
            controller.CreateAdvisor(advisor);
            var advisorId = advisor.Id;

           
            var result = controller.DeleteAdvisor(advisorId) as OkResult;

           
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_DeleteAdvisor_ReturnsNotFound()
        {
            
            var controller = new AdvisorsController(_cache);
            var advisorId = 1;

            
            var result = controller.DeleteAdvisor(advisorId) as NotFoundResult;

           
            Assert.NotNull(result);
        }
    }
}