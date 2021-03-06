﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ModelConventionsAndActionConstrains.Infrastrucure;

namespace ModelConventionsAndActionConstrains
{
    // The application model can be customized using *model conventions*, which are classes that
    // inspect the contents of the application model and make adjustments, such as synthesizing
    // new actions or changing the way that classes are used to create controllers.
    // During the discovery process, MVC creates an instance of the
    // Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel class and populates it with
    // details  of the controllers and actions that it finds.When the discovery process is complete,
    // model conventions are applied to make any custom changes you specify.

    // There are several interfaces which can be used to customize and extend ApplicationModel:
    // - IApplicationModelConvention - is used to apply a convention to the ApplicationModel object.
    // - IControllerModelConvention - is used to apply a convention to the ControllerModel
    //                                objects in the application model
    //                                (ApplicationMode.Controllers).
    // - IActionModelConvention - is used to apply a convention to the ActionModel objects in the
    //                            application model (ApplicationMode.Controllers[i].Actions).
    // - IParameterModelConvention - is used to apply a convention to the ParameterModel objects
    //                               in the application model
    //                               (ApplicationMode.Controllers[i].Actions[i].Parameters).
    // Controller, action, and parameter model conventions can be applied as attributes,
    // which makes it easy to set the scope of the changes they apply.

    // There are several integrated basic attributes which can be used to customize ApplicationModel:
    // - ActionNameAttribute - allows the value for the ActionName property of an ActionModel to be
    //                         specified explicitly rather than derived from a method name.
    // - NonControllerAttribute - prevents a class from being used to create a ControllerModel
    //                            object.
    // - NonActionAttribute - prevents a method from being used to create an ActionModel object.

    // When MVC receives an HTTP request, it goes through a selection process to identify the action
    // method that will be used to handle it.If there are multiple actions that could handle the
    // request, then MVC needs some way to decide which one to use, and that’s where action
    // constraints are used. The purpose of action constraints is to help MVC choose between two or
    // more similar action methods when any of them could be used to handle a request. Action
    // constraints are used to tell MVC whether an action method can be used to handle a request
    // and must implement the Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraint
    // interface. Also the Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraintFactory
    // interface can be implemented if you want to create a constraint that has dependencies to
    // resolve.

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UserAgentComparer>();
            services.AddMvc();
            //services.AddMvc().AddMvcOptions(options => {
            //    options.Conventions.Add(new ActionNamePrefixAttribute("Do"));
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
