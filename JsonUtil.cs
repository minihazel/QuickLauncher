using System.Web.Script.Serialization;

namespace QuickLauncher
{
    public static class JsonUtil
    {
        public static JavaScriptSerializer initSerializer()
        {
            return new JavaScriptSerializer {
                MaxJsonLength = int.MaxValue
            };
        }
    }
}
