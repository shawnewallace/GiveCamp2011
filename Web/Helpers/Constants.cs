using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helpers
{
    public static class Constants
    {
        //regest
        public const string REGEX_PHONE_NUMBER = @"^\+?\(?\d+\)?(\s|\-|\.)?\d{1,3}(\s|\-|\.)?\d{4}[\s]*[\d]*$";
        public const string REGEX_ZIP_CODE = @"^\d{5}([\-]\d{4})?$";
    }
}