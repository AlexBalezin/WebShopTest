﻿using Moq;
using WebShop.Models;
using Xunit;
using System.Linq;
using WebShop.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using WebShop.Infrastructure;
using WebShop.Models.ViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace WebShopTests
{
    public class PageLinkTagHelperTests
    {
        [Fact]
        public void Can_Generate_Page_Links()
        {
            // Arrange
            var urlHelper = new Mock<IUrlHelper>();

            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
            .Returns("Test/Page1")
            .Returns("Test/Page2")
            .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();

            urlHelperFactory.Setup(f =>
                f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            PageLinkTagHelper helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            TagHelperContext ctx = new TagHelperContext( new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();

            TagHelperOutput output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object));

            // Act
            helper.Process(ctx, output);

            // Assert
            Assert.Equal(@"<a href=""Test/Page1"">1</a>" + @"<a href=""Test/Page2"">2</a>" + @"<a href=""Test/Page3"">3</a>",  output.Content.GetContent());
        }

    }
}
