﻿using System;
using GlobalX.AspNetCore.Hateoas.Siren.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace GlobalX.AspNetCore.Hateoas.Siren
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddSirenOptions(this IMvcBuilder builder, Action<SirenOptions> setupAction)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            AddSirenFormatterServices(builder.Services);

            builder.Services.Configure(setupAction);

            return builder;
        }

        internal static void AddSirenFormatterServices(IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor
                .Transient<IConfigureOptions<SirenOptions>, SirenOptionsSetup>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcOptionsSetup>());
        }
    }
}