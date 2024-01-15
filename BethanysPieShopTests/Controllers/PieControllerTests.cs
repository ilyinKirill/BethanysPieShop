using BethanysPieShop.Controllers;
using BethanysPieShop.ViewModels;
using BethanysPieShopTests.Fakes;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopTests.Controllers
{
	public class PieControllerTests
	{
		[Fact]
		public void List_EmptyCategory_ReturnsAllPies()
		{
			//Arrange
			var fakePieRepository = RepositoryFakes.GetFakePieRepository();
			var fakeCategoryRepository = RepositoryFakes.GetFakeCategoryRepository();

			var pieController = new PieController(fakePieRepository, fakeCategoryRepository);

			//Act
			var result = pieController.List("");

			//Assert
			result.Should().BeOfType<ViewResult>();
			var viewModel = ((ViewResult)result).ViewData.Model;
			viewModel.Should().BeAssignableTo<ListViewModel>();
			((ListViewModel)viewModel).Pies.Count().Should().Be(10);
		}
	}
}
