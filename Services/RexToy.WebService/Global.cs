using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Reflection;

namespace RexToy.WebService
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var root = this.Server.MapPath("/");
            string[] fileList = Directory.GetFiles(Path.Combine(root, "bin"), "*.dll");
            foreach (var file in fileList)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    var startupAttr = assembly.GetSingleAttribute<StartupAttribute>();
                    if (startupAttr != null)
                    {
                        var startup = Activator.CreateInstance(startupAttr.StartType) as IStartup;
                        startup.Startup(this);
                        return;
                    }
                }
                catch (BadImageFormatException ex)
                {
                    //Note: Not valid .NET dll
                }
            }
        }
    }
}
