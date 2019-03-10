using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ModelConventionsAndActionConstrains.Infrastrucure
{
    // NOTE: Adding additional names to the actions can be done via implementing the
    //       IControllerModelConvention which examines the controller and looks for actions
    //       with special attribute which specifies additional names for the action. Then
    //       new `ActionModel`s objects must be inserted to the target ControllerModel

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ActionNamePrefixAttribute : Attribute, IActionModelConvention
    {
        private readonly string _prefix;

        public ActionNamePrefixAttribute(string prefix)
        {
            _prefix = prefix;
        }

        public void Apply(ActionModel action)
        {
            action.ActionName = _prefix + action.ActionName;
        }
    }
}
