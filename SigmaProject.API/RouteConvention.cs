using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SigmaProject.API
{
    public class RouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerName = controller.ControllerName.ToLower();
            controller.ControllerName = controllerName;
            foreach (var selector in controller.Selectors)
                selector.AttributeRouteModel.Template = selector.AttributeRouteModel.Template.ToLower();
        }
    }
}