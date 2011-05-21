using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Text;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class HelperExtensions
    {
        /// <summary>
        /// This HTML helper will create a Dropdownlist.
        /// </summary>
        /// <param name="helper">HTMLHelper</param>
        /// <param name="dataSource">dataSource to create the Dropdownlist.</param>
        /// <param name="dataValueProperty">Property name , which is used to populate the value field for Dropdownlist</param>
        /// <param name="dataTextProperty">Property name , which is used to populate the Text field for Dropdownlist</param>
        /// <param name="selectedValue">Property name , which is used to identify the selected item for Dropdownlist</param>
        /// <param name="htmlAttributes">Additional htmlAttributes.</param>
        /// <returns></returns>
        public static IHtmlString DataBoundDropDownList(this HtmlHelper helper, object dataSource, string dataValueProperty, string dataTextProperty, string selectedValue, object htmlAttributes)
        {
            var selectTag = CreateDropDownlist(dataSource, dataValueProperty, dataTextProperty, selectedValue);
            selectTag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(selectTag.ToString());
        }

        public static IHtmlString DataBoundDropDownList(this HtmlHelper helper, object dataSource, string dataValueProperty, string dataTextProperty, string selectedValue)
        {
            var selectTag = CreateDropDownlist(dataSource, dataValueProperty, dataTextProperty, selectedValue);
            return MvcHtmlString.Create(selectTag.ToString());
        }

        private static TagBuilder CreateDropDownlist(object dataSource, string dataValueProperty, string dataTextProperty, string selectedValue)
        {
            #region "Create the DropdownList"
            var selectBuilder = new TagBuilder("select");
            var optionStringBuilder = new StringBuilder();
            #endregion

            #region "Create optionlist to populate dropdownlist"
            var ddlSource = dataSource as IEnumerable;
            if (ddlSource == null)
            {
                throw new Exception("dataSource must implement IEnumerable Interface.");
            }
            IEnumerator item = ddlSource.GetEnumerator();
            while (item.MoveNext())
            {
                string strText = DataBinder.GetPropertyValue(item.Current, dataTextProperty).ToString();
                string strVal = DataBinder.GetPropertyValue(item.Current, dataValueProperty).ToString();
                var optionBuilder = new TagBuilder("option");
                optionBuilder.MergeAttribute("value", strVal);
                optionBuilder.InnerHtml = strText;
                if (strVal == selectedValue)
                {
                    optionBuilder.MergeAttribute("selected", "selected");
                }
                optionStringBuilder.Append(optionBuilder.ToString());
            }
            selectBuilder.InnerHtml = optionStringBuilder.ToString();
            #endregion

            return (selectBuilder);
        }

        //public static IHtmlString DatePicker(this HtmlHelper html, string name, object date)
        //{
        //    var inputTag = new TagBuilder("input");
        //    inputTag.MergeAttribute("id", name);
        //    inputTag.MergeAttribute("name", name);
        //    inputTag.MergeAttribute("type", "text");
        //    //inputTag.AddCssClass("jqueryDatePicker");

        //    if (date != null)
        //    {
        //        string dateValue = String.Empty;
        //        if ((date is DateTime? || date is DateTime) && (DateTime)date != DateTime.MinValue)
        //        {
        //            dateValue = ((DateTime)date).ToShortDateString();
        //        }
        //        else if (date is string)
        //        {
        //            dateValue = (string)date;
        //        }

        //        inputTag.MergeAttribute("value", dateValue);
        //    }

        //    var scriptTag = new TagBuilder("script");
        //    scriptTag.Attributes.Add("type", "text/javascript");
        //    var jsonSerializer = new JavaScriptSerializer();
        //    scriptTag.InnerHtml = "$(document).ready(function() { $(\"#" + name + "\").datepicker(); });";

        //    return MvcHtmlString.Create(scriptTag.ToString() + inputTag.ToString());
        //}

        public static IHtmlString ReadOrEditFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool enabled) where TModel : class
        {
            
            return enabled ? MvcHtmlString.Create(htmlHelper.EditorFor(expression).ToString() + htmlHelper.ValidationMessageFor(expression).ToString()) : htmlHelper.DisplayFor(expression);
        }

        public static IHtmlString DatePicker(this HtmlHelper html, string name)
        {
            var inputTag = new TagBuilder("input");
            inputTag.MergeAttribute("id", name);
            inputTag.MergeAttribute("name", name);
            inputTag.MergeAttribute("type", "text");
            var scriptTag = new TagBuilder("script");
            scriptTag.Attributes.Add("type", "text/javascript");
            var jsonSerializer = new JavaScriptSerializer();
            scriptTag.InnerHtml = "$(document).ready(function() { $(\"#" + name + "\").datepicker(); });";

            return MvcHtmlString.Create(scriptTag.ToString() + inputTag.ToString());
        }

        public static IHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) where TModel : class
        {
            var inputName = ExpressionHelper.GetExpressionText(expression);
            return htmlHelper.DatePicker(inputName);
        }
        //public static IHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) where TModel : class
        //{
        //    Func<TModel, TProperty> methodCall = expression.Compile();
        //    TProperty value = methodCall(htmlHelper.ViewData.Model);
        //    var inputName = ExpressionHelper.GetExpressionText(expression);

        //    return htmlHelper.DatePicker(inputName, value);
        //}
    }
}