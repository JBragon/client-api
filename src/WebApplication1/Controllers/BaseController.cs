using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ExecuteCreate(Func<object> func)
        {
            return Execute(func, 201, true);
        }

        protected IActionResult Execute(Func<object> func, int statusCodeSuccess = 200, bool returnObject = true)
        {
            try
            {
                var result = func();

                if (result == null ||
                      (result is IEnumerable &&
                        !((IEnumerable)result).GetEnumerator().MoveNext()))
                    return NoContent();

                if (!returnObject)
                    result = null;

                return StatusCode(statusCodeSuccess, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}: {ex.InnerException?.Message}");
            }
        }
        protected IActionResult Execute(Action func, int statusCodeSuccess = 200)
        {
            try
            {
                func();
                return StatusCode(statusCodeSuccess);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}: {ex.InnerException?.Message}");
            }
        }
    }
}
