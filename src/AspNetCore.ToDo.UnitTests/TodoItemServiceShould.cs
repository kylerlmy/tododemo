using System;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AspNetCore.ToDo.UnitTests {
    public class TodoItemServiceShould {
        [Fact]
        public void Task AddNewItem () {
            var options = new DbContextOptionsBuilder<ApplicationDbContext> ()
                .UseInMomeryDatabase (databaseName: "Test_AddItem").Options;

            using (var inMemoryContext = new ApplicationDbContext (options)) {
                var service = new TodoItemService (inMemoryContext);
                var fakeUser = new ApplicationUser {
                    Id = "fake-000",
                    UserName = "fake@fake"
                };
                await service.AddItemAsync (new NewTodoItem { Tile = "Tesing?" }, fakeUser);
            }

            using (var inMemoryContext = new ApplicationDbContext (options)) {
                Assert.Equal (1, await inMemoryContext.Items.CountAsync ());
                var item = await inMemoryContext.Items.FirstAsync ();
                Assert.Equal ("Testing?", item.Title);
                Assert.Equal (false, item.IsDone);
                Assert.True (DateTimeOffset.Now.AddDays (3) - item.DueAt < TimeSpan.FromSeconds (1));
            }

        }
    }
}