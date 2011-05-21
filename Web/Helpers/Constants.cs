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
        public const string REGEX_EMAIL_ADDRESS = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})";
    }
}