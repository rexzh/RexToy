﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace RexToy.WebService
{
    public interface IStartup
    {
        void Startup(HttpApplication app);
    }
}
