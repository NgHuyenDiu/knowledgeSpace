using KnowledgeSpace.BackendServer.Controllers;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.BackendServer.Services;
using KnowledgeSpace.ViewModels.Contents;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KnowledgeSpace.BackendServer.UnitTest.Controllers
{
    public class CategoriesControllerTest
    {
        private ApplicationDbContext _context;
        private Mock<ICacheService> _mockCacheService;

        public CategoriesControllerTest()
        {
            _context = new InMemoryDbContextFactory().GetApplicationDbContext("CommentsControllerTest");
            _mockCacheService = new Mock<ICacheService>();
        }

        [Fact]
        public void ShouldCreateInstance_NotNull_Success()
        {
            var controller = new CategoriesController(_context, _mockCacheService.Object);
            Assert.NotNull(controller);
        }
        [Fact]
        public async Task PostCategory_ValidInput_Success()
        {
            var controller = new CategoriesController(_context, _mockCacheService.Object);
            var result = await controller.PostCategory(new CategoryCreateRequest()
            {
                Name = "PostCategory_ValidInput_Success"

            });

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task PostCategory_ValidInput_Failed()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category
                (){
                    
                    Name = "PostCategory_ValidInput_Failed",
                   
                }
            });
            await _context.SaveChangesAsync();
            var controller = new CategoriesController(_context, _mockCacheService.Object);

            var result = await controller.PostCategory(new CategoryCreateRequest()
            {
                
                Name = "PostCategory_ValidInput_Failed",
                
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetCategory_HasData_ReturnSuccess()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                    
                    Name = "GetCategory_HasData_ReturnSuccess"

                }
            });
            await _context.SaveChangesAsync();
            var controller = new CategoriesController(_context, _mockCacheService.Object);
            var result = await controller.GetCategories();
            var okResult = result as OkObjectResult;
            var UserVms = okResult.Value as IEnumerable<CategoryVm>;
            Assert.True(UserVms.Count() > 0);
        }

        [Fact]
        public async Task GetById_HasData_ReturnSuccess()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                  
                    Id= 1,
                    Name = "GetById_HasData_ReturnSuccess"
                   
                }
            });
            await _context.SaveChangesAsync();
            var controller = new CategoriesController(_context, _mockCacheService.Object);
            var result = await controller.GetById(1);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var userVm = okResult.Value as CategoryVm;
            Assert.Equal("GetById_HasData_ReturnSuccess", userVm.Name);
        }

     


        [Fact]
        public async Task PutCategory_ValidInput_Success()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                    Id = 2,
                    Name = "PutCategory_ValidInput_Success"
                }
            });
            await _context.SaveChangesAsync();
            var controller = new CategoriesController(_context, _mockCacheService.Object);
            var result = await controller.PutCategory(2, new CategoryCreateRequest()
            {
               
                Name = "PutUser_ValidInput_Success updated"
                
            });
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutCategory_ValidInput_Failed()
        {
            var controller = new CategoriesController(_context, _mockCacheService.Object);

            var result = await controller.PutCategory(3, new CategoryCreateRequest()
            {
                
                Name = "PutUser_ValidInput_Failed update"
            });
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ValidInput_Success()
        {
            _context.Categories.AddRange(new List<Category>()
            {
                new Category(){
                   Id= 4,
                    Name = "DeleteUser_ValidInput_Success"
                }
            });
            await _context.SaveChangesAsync();
            var controller = new CategoriesController(_context, _mockCacheService.Object);
            var result = await controller.DeleteCategory(4);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ValidInput_Failed()
        {
            var controller = new CategoriesController(_context, _mockCacheService.Object);
            var result = await controller.DeleteCategory(5);
            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}