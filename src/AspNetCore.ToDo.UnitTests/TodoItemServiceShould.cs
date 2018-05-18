using System;
using AspNetCore.ToDo.Data;
using AspNetCore.ToDo.Models;
using AspNetCore.ToDo.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

using  System.Threading.Tasks;
namespace AspNetCore.ToDo.UnitTests {
    public class TodoItemServiceShould {
        [Fact]
        public async  Task AddNewItem () {
            var options = new DbContextOptionsBuilder<ApplicationDbContext> ()
                .UseInMemoryDatabase (databaseName: "Test_AddItem").Options;

            using (var inMemoryContext = new ApplicationDbContext (options)) {
                var service = new TodoItemService (inMemoryContext);
                var fakeUser = new ApplicationUser {
                    Id = "fake-000",
                    UserName = "fake@fake"
                };
                await service.AddItemAsync (new NewTodoItem { Title = "Testing?" }, fakeUser);
            }

            using (var inMemoryContext = new ApplicationDbContext (options)) {
                Assert.Equal (1, await inMemoryContext.Items.CountAsync ());
                var item = await inMemoryContext.Items.FirstAsync ();
                Assert.Equal ("Testing?", item.Title);
                Assert.False (item.IsDone);
                Assert.True (DateTimeOffset.Now.AddDays (3) - item.DueAt < TimeSpan.FromSeconds (1));
            }

        }
    }
}