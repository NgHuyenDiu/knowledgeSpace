using KnowledgeSpace.BackendServer.Controllers;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.BackendServer.Services;
using KnowledgeSpace.ViewModels.Contents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace KnowledgeSpace.BackendServer.UnitTest.Controllers
{
    public class AttachmentsControllerTest
    {
        private ApplicationDbContext _context;
        private Mock<ISequenceService> _mockSequenceService;
        private Mock<IStorageService> _mockStorageService;
        private Mock<ILogger<AttachmentsController>> _mockLoggerService;
        private Mock<IEmailSender> _mockEmailSender;
        private Mock<IViewRenderService> _mockViewRenderService;
        private Mock<ICacheService> _mockCacheService;

        public AttachmentsControllerTest()
        {
            _context = new InMemoryDbContextFactory().GetApplicationDbContext("AttachmentsControllerTest");
            _mockSequenceService = new Mock<ISequenceService>();
            _mockStorageService = new Mock<IStorageService>();
            _mockLoggerService = new Mock<ILogger<AttachmentsController>>();
            _mockEmailSender = new Mock<IEmailSender>();
            _mockViewRenderService = new Mock<IViewRenderService>();
            _mockCacheService = new Mock<ICacheService>();
        }

        [Fact]
        public void ShouldCreateInstance_NotNull_Success()
        {
            var controller = new AttachmentsController(_context, _mockSequenceService.Object, _mockStorageService.Object,
                _mockLoggerService.Object, _mockEmailSender.Object, _mockViewRenderService.Object, _mockCacheService.Object);
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task DeleteAttachment_ValidInput_Success()
        {
            _mockSequenceService.Setup(x => x.GetKnowledgeBaseNewId()).ReturnsAsync(1);
            var controller = new AttachmentsController(_context, _mockSequenceService.Object, _mockStorageService.Object,
                           _mockLoggerService.Object, _mockEmailSender.Object, _mockViewRenderService.Object, _mockCacheService.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var result = await controller.PostKnowledgeBase(new KnowledgeBaseCreateRequest()
            {
                Title = "test",
                Id = 1,
                Problem = "test",
                Note = "test"
            });

            _context.Attachments.AddRange(new List<Attachment>()
            {
                new Attachment(){
                    Id = 1,
                    FileName = "test",
                    FilePath ="./test.jpg",
                    FileType =".jpg",
                    FileSize=9572,
                    KnowledgeBaseId= 1
                  
                }
            });
            await _context.SaveChangesAsync();
            
            var delete_result = await controller.DeleteAttachment(1);
            Assert.IsType<OkResult>(delete_result);
        }
    }
}
