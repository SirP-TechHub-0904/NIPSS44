﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44
{
    public static class AddPageRoute
    {

        public static RazorPagesOptions AddPageRouteOption(this RazorPagesOptions options, string pageName, string route)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrEmpty(pageName))
            {
                throw new ArgumentException(nameof(pageName));
            }

            if (string.IsNullOrEmpty(route))
            {
                throw new ArgumentException(nameof(route));
            }

            options.Conventions.AddPageRouteModelConvention(pageName, model =>
            {
               

                foreach (var selector in model.Selectors)
                {
                    selector.AttributeRouteModel.SuppressLinkGeneration = true;
                }

                model.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = route,
                    }
                });
            });

            return options;
        }
    }
}
