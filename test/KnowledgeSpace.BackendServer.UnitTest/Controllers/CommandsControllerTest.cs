using KnowledgeSpace.BackendServer.Controllers;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace KnowledgeSpace.BackendServer.UnitTest.Controllers
{
    public class CommandsControllerTest
    {
        private ApplicationDbContext _context;

        public CommandsControllerTest()
        {
            _context = new InMemoryDbContextFactory().GetApplicationDbContext();
        }

        [Fact]
        public void ShouldCreateInstance_NotNull_Success()
        {
            var usersController = new CommandsController(_context);
            Assert.NotNull(usersController);
        }

        [Fact]
        public async Task GetCommand_HasData_ReturnSuccess()
        {
            
            _context.Commands.AddRange(new List<Command>()
            {
                new Command(){
                    Id = "GetFunction_HasData_ReturnSuccess",
                    Name = "GetFunction_HasData_ReturnSuccess"
                }
            });
            await _context.SaveChangesAsync();
            var controller =  new CommandsController(_context);
            var result = await controller.GetCommands();
            var okResult = result as OkObjectResult;
            var UserVms = okResult.Value as IEnumerable<CommandVm>;
            Assert.True(UserVms.Count() > 0);
        }

    }
}