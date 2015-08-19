using System;

namespace RexToy.IoC
{
    public enum Stages
    {
        PreCreation,//Note:Lifecycle manager etc.
        Creation,//Note:Create policy, constructor injection hint, return proxy instance instead of real instance etc.
        Initialization,//Note:Setter injection hint etc.
        PostInitialization//Note:Cutomized action etc.
    }
}