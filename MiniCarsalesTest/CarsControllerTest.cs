using MiniCarsales.DataStore;
using MiniCarsales.Controllers;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using MiniCarsales.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MiniCarsalesTest
{
    public class CarsControllerTest
    {
        private ApplicationDbContext InitializeDbContext(string testName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: testName)
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               .Options;
            var context = new ApplicationDbContext(options);
            context.Car.Add(new Car
            {
                Type = nameof(Car),
                Make = "Mazda",
                Model = "6",
                Engine = "Diesel",
                Doors = 4,
                Wheels = 4,
                BodyType = "Sedan"
            });
            context.Car.Add(new Car
            {
                Type = nameof(Car),
                Make = "Toyota",
                Model = "RAV4",
                Engine = "Petrol",
                Doors = 4,
                Wheels = 4,
                BodyType = "SUV"
            });
            context.Car.Add(new Car
            {
                Type = nameof(Car),
                Make = "Audi",
                Model = "A5",
                Engine = "Petrol",
                Doors = 4,
                Wheels = 4,
                BodyType = "Hatch"
            });
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task TestListCars()
        {
            // Arrange
            var context = InitializeDbContext(nameof(TestListCars));
            var controller = new CarsController(context);

            // Act
            var response = await controller.ListCars();
            var cars = response.Value.ToList();

            context.Dispose();

            // Assert
            Assert.Equal(3, cars.Count);
            Assert.All(cars, car => Assert.Equal(nameof(Car), car.Type));
        }

        [Fact]
        public async Task TestGetCar_NotFound()
        {
            // Arrange
            var context = InitializeDbContext(nameof(TestGetCar_NotFound));
            var controller = new CarsController(context);
            var carId = 7;

            // Act            
            var response = await controller.GetCar(carId);

            context.Dispose();

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task TestGetCar_CorrectItem()
        {
            // Arrange
            var context = InitializeDbContext(nameof(TestGetCar_CorrectItem));
            var controller = new CarsController(context);
            var carId = 2;

            // Act
            var response = await controller.GetCar(carId);

            context.Dispose();

            // Assert
            Assert.IsType<OkObjectResult>(response);
            var car = (response as OkObjectResult).Value;
            Assert.IsType<Car>(car);
            Assert.Equal(carId, (car as Car).Id);
        }

        [Fact]
        public async Task TestCreateCar_ItemInvalid()
        {
            // Arrange
            var context = InitializeDbContext(nameof(TestCreateCar_ItemInvalid));
            var controller = new CarsController(context);
            var newCar = new Car
            {
                Type = typeof(Car).ToString(),
                Model = "X-Trail",
                Engine = "Petrol",
                Doors = 4,
                Wheels = 4,
                BodyType = "SUV"
            };
            controller.ModelState.AddModelError("Make", "Required");

            // Act
            var response = await controller.CreateCar(newCar);

            context.Dispose();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task TestCreateCar_ItemCreated()
        {
            // Arrange
            var context = InitializeDbContext(nameof(TestCreateCar_ItemCreated));
            var controller = new CarsController(context);
            var newCar = new Car
            {
                Type = typeof(Car).ToString(),
                Make = "Nissan",
                Model = "X-Trail",
                Engine = "Petrol",
                Doors = 4,
                Wheels = 4,
                BodyType = "SUV"
            };

            // Act            
            var response = await controller.CreateCar(newCar);

            context.Dispose();

            // Assert
            Assert.IsType<CreatedAtActionResult>(response);
            var car = (response as CreatedAtActionResult).Value;
            Assert.IsType<Car>(car);
            Assert.Equal(newCar.Make, (car as Car).Make);
        }

        [Fact]
        public async Task TestUpdateCar_NotFound()
        {
            // Arrange
            var context = InitializeDbContext(nameof(TestUpdateCar_NotFound));
            var controller = new CarsController(context);
            var notFoundCar = new Car
            {
                Id = 7,
                Type = typeof(Car).ToString(),
                Make = "Nissan",
                Model = "X-Trail",
                Engine = "Petrol",
                Doors = 4,
                Wheels = 4,
                BodyType = "SUV"
            };

            // Act            
            var response = await controller.UpdateCar(notFoundCar.Id, notFoundCar);

            context.Dispose();

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task TestUpdateCar_ItemUpdated()
        {
            // Arrange
            var context = InitializeDbContext(nameof(TestUpdateCar_ItemUpdated));
            var controller = new CarsController(context);           
            var carToUpdate = new Car
            {
                Id = 3,
                Type = typeof(Car).ToString(),
                Make = "Audi",
                Model = "A5",
                Engine = "Petrol",
                Doors = 4,
                Wheels = 4,
                BodyType = "Sedan"
            };

            // Act            
            var response = await controller.UpdateCar(carToUpdate.Id, carToUpdate);           

            // Assert
            Assert.IsType<NoContentResult>(response);
            Assert.Equal("Sedan", context.Car.Find(carToUpdate.Id).BodyType);

            context.Dispose();
        }

        [Fact]
        public async Task TestDeleteCar_NotFound()
        {
            // Arrange
            var context = InitializeDbContext(nameof(TestDeleteCar_NotFound));
            var controller = new CarsController(context);
            var notFoundCarId = 7;

            // Act            
            var response = await controller.DeleteCar(notFoundCarId);

            context.Dispose();

            // Assert
            Assert.IsType<NotFoundResult>(response);            
        }

        [Fact]
        public async Task TestDeleteCar_ItemDeleted()
        {
            // Arrange
            var context = InitializeDbContext(nameof(TestDeleteCar_ItemDeleted));
            var controller = new CarsController(context);
            var carId = 3;

            // Act            
            var response = await controller.DeleteCar(carId);            

            // Assert
            Assert.IsType<OkObjectResult>(response);
            var car = (response as OkObjectResult).Value;
            Assert.IsType<Car>(car);
            Assert.Equal(carId, (car as Car).Id);
            Assert.Equal(2, context.Car.Count());

            context.Dispose();
        }
    }
}
