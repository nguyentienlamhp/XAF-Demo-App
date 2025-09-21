using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects;
using DXApplication3.Module.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.Controllers
{
    public class CustomPageViewController : ViewController
    {
        public CustomPageViewController()
        {
            TargetObjectType = typeof(CustomPageViewModel);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            var navManager = (NavigationManager)Application.ServiceProvider.GetService(typeof(NavigationManager));
            if (navManager != null)
            {
                // Điều hướng sang Razor page
                navManager.NavigateTo("/custompage", forceLoad: true);
            }
        }
    }
}
