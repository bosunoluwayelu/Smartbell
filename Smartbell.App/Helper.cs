using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Smartbell.App
{
    public class Helper
    {
        public static string RenderRazorViewToString(Controller controller, string viewName, object? model = null)
        {
            // set ViewData model
            controller.ViewData.Model = model;
            using (var stringWriter = new StringWriter())
            {
                IViewEngine? viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewEngineResult = viewEngine.FindView(controller.ControllerContext, viewName, false);
                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewEngineResult.View,
                    controller.ViewData,
                    controller.TempData,
                    stringWriter,
                    new HtmlHelperOptions());
                viewEngineResult.View.RenderAsync(viewContext);

                return stringWriter.GetStringBuilder().ToString();
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class NoDirectAccessAttrubute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                //base.OnActionExecuting(context);
                if (context.HttpContext.Request.GetTypedHeaders().Referer == null ||
                    context.HttpContext.Request.GetTypedHeaders().Host.ToString() != context.HttpContext.Request.GetTypedHeaders().Referer.Host.ToString())
                {
                    context.HttpContext.Response.Redirect("/");
                }
            }
        }
    }
}
