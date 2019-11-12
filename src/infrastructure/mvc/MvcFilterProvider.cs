using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace infrastructure.mvc
{
    public class MvcFilterProvider : IFilterProvider
    {
        public int Order => -1000;

        public void OnProvidersExecuted(FilterProviderContext context)
        {
        }

        public void OnProvidersExecuting(FilterProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (context.ActionContext.ActionDescriptor.FilterDescriptors != null)
            {
                for (int i = 0; i < context.Results.Count; i++)
                {
                    ProvideFilter(context, context.Results[i]);
                }
            }
        }
        public virtual void ProvideFilter(FilterProviderContext context, FilterItem filterItem)
        {
            if (filterItem.Descriptor.Filter is AuthorizeFilter && filterItem.Filter == null)
            {
                AuthorizeFilter val = filterItem.Descriptor.Filter as AuthorizeFilter;
                IEnumerable<IAuthorizeData> enumerable = val.AuthorizeData;
                if (enumerable == null && (object)val.Policy != null)
                {
                    List<IAuthorizeData> list = new List<IAuthorizeData>();
                    string text = val.Policy.AuthenticationSchemes.FirstOrDefault();
                    if (!string.IsNullOrEmpty(text))
                    {
                        List<IAuthorizeData> list2 = list;
                        MvcAuthorizeAttribute mvcAuthorizeAttribute = new MvcAuthorizeAttribute();
                        mvcAuthorizeAttribute.AuthenticationSchemes = text;
                        list2.Add(mvcAuthorizeAttribute);
                    }
                    enumerable = list;
                }
                filterItem.Filter = new MvcAuthorizeFilter(enumerable).CreateInstance(context.ActionContext.HttpContext.RequestServices);
                filterItem.IsReusable = true;
            }
            if ((object)filterItem.Filter != null)
            {
                AuthorizeFilter val2;
                MvcAuthorizeFilter mvcAuthorizeFilter = default(MvcAuthorizeFilter);
                if ((val2 = (filterItem.Filter as AuthorizeFilter)) != null && (mvcAuthorizeFilter = (filterItem.Descriptor.Filter as MvcAuthorizeFilter)) != null)
                {
                    filterItem.Filter = mvcAuthorizeFilter.CreateInstance(context.ActionContext.HttpContext.RequestServices);
                }
            }
            else
            {
                IFilterMetadata val3 = filterItem.Descriptor.Filter;
                IFilterFactory val4;
                if ((val4 = (val3 as IFilterFactory)) == null)
                {
                    filterItem.Filter = val3;
                    filterItem.IsReusable = true;
                }
                else
                {
                    IServiceProvider requestServices = context.ActionContext.HttpContext.RequestServices;
                    filterItem.Filter = val4.CreateInstance(requestServices);
                    filterItem.IsReusable = val4.IsReusable;
                    if ((object)filterItem.Filter == null)
                    {
                        throw new InvalidOperationException("The 'CreateInstance' method of type '" + typeof(IFilterFactory).Name + "' cannot return a null value.");
                    }
                    ApplyFilterToContainer((object)filterItem.Filter, val4);
                }
            }
        }
        private void ApplyFilterToContainer(object actualFilter, IFilterMetadata filterMetadata)
        {
            Debug.Assert(actualFilter != null, "actualFilter should not be null");
            Debug.Assert(filterMetadata != null, "filterMetadata should not be null");
            IFilterContainer val;
            if ((val = (actualFilter as IFilterContainer)) != null)
            {
                val.FilterDefinition = filterMetadata;
            }
        }
    
    }
}