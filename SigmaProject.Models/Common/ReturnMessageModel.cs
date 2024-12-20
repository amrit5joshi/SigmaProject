namespace SigmaProject.Models.Common
{
    public class ReturnMessageModel
    {
        public string ReturnMessage { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        public static string SaveSuccess(string entity)
        {
            return $"{entity} Saved Successfully";
        }

        public static string DeleteSuccess(string entity)
        {
            return $"{entity} Deleted Successfully";
        }

        public static string UpdateSuccess(string entity)
        {
            return $"{entity} Updated Successfully";
        }
    }
}