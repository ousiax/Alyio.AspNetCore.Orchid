using System;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Internal
{
    internal class DefaultTypeActivator : ITypeActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultTypeActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object CreateInstance(Type instanceType, params object[] parameters)
        {
            var activated = ActivatorUtilities.CreateInstance(_serviceProvider, instanceType, parameters);

            return activated;
        }
    }
}
