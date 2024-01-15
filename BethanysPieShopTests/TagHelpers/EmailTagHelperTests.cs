using BethanysPieShop.TagHelpers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BethanysPieShopTests.TagHelpers
{
	public class EmailTagHelperTests
	{
		[Fact]
		public void Generates_Email_Link()
		{
			// Arrange
			EmailTagHelper emailTagHelper = new EmailTagHelper() { Address = "test@bethanyspieshop.com", Content = "Email" }; ;

			var tagHelperContext = new TagHelperContext(
				new TagHelperAttributeList(),
				new Dictionary<object, object>(), string.Empty);

			var content = A.Fake<TagHelperContent>();

			var tagHelperOutput = new TagHelperOutput("a",
				new TagHelperAttributeList(),
				(cache, encoder) => Task.FromResult(content));

			// Act
			emailTagHelper.Process(tagHelperContext, tagHelperOutput);

			//Assert
			tagHelperOutput.Content.GetContent().Should().Be("Email");
			tagHelperOutput.TagName.Should().Be("a");
			tagHelperOutput.Attributes[0].Value.Should().Be("mailto:test@bethanyspieshop.com");
		}
	}
}
