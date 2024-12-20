using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SigmaProject.API
{
    public class RouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerName = controller.ControllerName.ToLower();
            controller.ControllerName = controllerName;
            // Update the selector to use the lowercase name
            foreach (var selector in controller.Selectors)
            {
                selector.AttributeRouteModel.Template = selector.AttributeRouteModel.Template.ToLower();
            }
        }

      
    }
}
