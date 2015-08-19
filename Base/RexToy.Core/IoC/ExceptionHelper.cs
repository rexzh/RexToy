using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.IoC
{
    static class ExceptionHelper
    {
        public static void ThrowInfoNotComplete(string componentId)
        {
            throw new ManifestException(string.Format("Component type and Service type are not set for component {0}.", componentId));
        }

        public static void ThrowBuildUpError(Exception inner, Stages stage)
        {
            throw new ObjectBuilderException(string.Format("Exception occur on build up stage [{0}]", stage), inner);
        }

        public static void ThrowTearDownError(Exception inner, Stages stage)
        {
            throw new ObjectBuilderException(string.Format("Exception occur on tear down stage [{0}]", stage), inner);
        }

        public static void ThrowPolicyInitNullError(Type policyType, string item)
        {
            throw new ManifestException(string.Format("[{0}] initialize error: [{1}] is null.", policyType, item));
        }

        public static void ThrowDuplicateComponentId(string id)
        {
            throw new ManifestException(string.Format("Multiple component have id [{0}].", id));
        }

        public static void ThrowIdNotFound(string id)
        {
            throw new ObjectBuilderException(string.Format("Component information of id [{0}] not found.", id));
        }

        public static void ThrowServiceNotFound(Type serviceType)
        {
            throw new ObjectBuilderException(string.Format("Component information of service type [{0}] not found.", serviceType));
        }

        public static void ThrowServiceMultiFound(Type serviceType)
        {
            throw new ObjectBuilderException(string.Format("Component information of service type [{0}] is not unique, use id instead.", serviceType));
        }

        public static void ThrowNoValidConstructor(Type type)
        {
            throw new ObjectBuilderException(string.Format("No valid constructor found on type [{0}].", type.Name));
        }

        public static void ThrowConstructorNotReady(Type type)
        {
            throw new ObjectBuilderException(string.Format("No constructor is ready to invoke on type [{0}].", type.Name));
        }

        public static void ThrowNoValidMethod(Type type, string method)
        {
            throw new ObjectBuilderException(string.Format("No valid method [{0}] found on type [{1}].", method, type.Name));
        }

        public static void ThrowMethodNotReady(Type type, string method)
        {
            throw new ObjectBuilderException(string.Format("No method [{0}] is ready to invoke on type [{1}].", method, type.Name));
        }
    }
}
