using Microsoft.AspNetCore.Mvc;
using Santander.DeveloperTestAPI.Model.ViewModel;
using Santander.DeveloperTestAPI.Services;

namespace Santander.DeveloperTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly INewsGetterService _newsGetterService;

        public HackerNewsController(INewsGetterService newsGetterService)
        {
            _newsGetterService = newsGetterService;
        }

        [HttpGet(Name = "GetHackerNews")]
        [Produces(typeof(IEnumerable<NewsItemViewModel>))]
        public async Task<ActionResult<IEnumerable<NewsItemViewModel>>> Get(int howMany)
        {
            throw new Exception("dupa");
            var result = await _newsGetterService.GetItems(howMany);
            return Ok(result.Select(x=>new NewsItemViewModel(x)).OrderByDescending(y=>y.Score));
        }
    }
}
