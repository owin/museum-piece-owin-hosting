namespace Owin.Types
{
    public static class OwinConstants
    {
        #region 3.2.1 Request Data of OWIN v1.0.0

        // Defined by section 3.2.1 Request Data of OWIN v1.0.0
        // http://owin.org/spec/owin-1.0.0.html

        public const string RequestScheme = "owin.RequestScheme";
        public const string RequestMethod = "owin.RequestMethod";
        public const string RequestPathBase = "owin.RequestPathBase";
        public const string RequestPath = "owin.RequestPath";
        public const string RequestQueryString = "owin.RequestQueryString";
        public const string RequestProtocol = "owin.RequestProtocol";
        public const string RequestHeaders = "owin.RequestHeaders";
        public const string RequestBody = "owin.RequestBody";

        #endregion

        #region 3.2.2 Response Data of OWIN v1.0.0

        // Defined by section 3.2.2 Response Data of OWIN v1.0.0
        // http://owin.org/spec/owin-1.0.0.html

        public const string ResponseStatusCode = "owin.ResponseStatusCode";
        public const string ResponseReasonPhrase = "owin.ResponseReasonPhrase";
        public const string ResponseProtocol = "owin.ResponseProtocol";
        public const string ResponseHeaders = "owin.ResponseHeaders";
        public const string ResponseBody = "owin.ResponseBody";

        #endregion

        #region 3.2.3 Other Data of OWIN v1.0.0

        // Defined by section 3.2.3 Other Data of OWIN v1.0.0
        // http://owin.org/spec/owin-1.0.0.html

        public const string CallCancelled = "owin.CallCancelled";
        public const string OwinVersion = "owin.Version";

        #endregion
    }
}