﻿#region Includes

using System.Web.Mvc;

#endregion

namespace Daishi.FormPostClient {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}