using Moq;
using Xunit;
using MovieStoreMvc.Models.Domain;
using MovieStoreMvc.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MovieStoreMvc.Repositories.Implementation;

public class MovieServiceTests
{
    private DbContextOptions<DatabaseContext> GetDbContextOptions()
    {
        return new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "MovieStoreTestDb")
            .Options;
    }

    [Fact]
    public void Add_ShouldReturnTrue_WhenMovieIsAddedSuccessfully()
    {
        // Arrange
        var options = GetDbContextOptions();
        using var context = new DatabaseContext(options);
        var service = new MovieService(context);
        var movie = new Movie { Id = 1, Title = "Test Movie", Genres = new List<int> { 1, 2 } };

        // Act
        var result = service.Add(movie);

        // Assert
        Assert.True(result);
        Assert.Single(context.Movie);
        Assert.Equal(2, context.MovieGenre.Count());
    }
}
