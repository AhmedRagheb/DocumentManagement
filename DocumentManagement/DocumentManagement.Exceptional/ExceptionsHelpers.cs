using Newtonsoft.Json;

namespace DocumentManagement.Exceptional
{
    public static class ExceptionalHelpers
    {
        public static string Serialize(this ErrorModel error)
        {
            return JsonConvert.SerializeObject(error);
        }
    }
}
