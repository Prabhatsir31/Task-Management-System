//using Microsoft.Testing.Platform.Configurations;
//using NSubstitute;
//using NUnit.Framework;
//using Shouldly;
//using AutoMapper.Configuration;
//using TaskManagementSystem.Repository.Context;
//using Microsoft.Extensions.Configuration;

//namespace TaskManagementSystem.Repository.Tests
//{
//    [TestFixture]
//   public class KUrgeTruckContextFactoryTests
//    {
//        [Test]
//        public void ShouldCreateUrgeTruckContext()
//        {
//            var fakeConfiguration = Substitute.For<IConfiguration>();
//            fakeConfiguration.GetConnectionString("SQLConnection").Returns("conn");
//            var kUrgeTruckContextFactory = new KUrgeTruckContextFactory(fakeConfiguration);

//            var result = kUrgeTruckContextFactory.CreateKGASContext();

//            result.ShouldBeOfType(typeof(KUrgeTruckContext));
//        }
//    }
//}
