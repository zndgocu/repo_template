using System.Reflection;

namespace EntityContext.Helper
{
    public static class EntityFinder
    {
        public static List<Type> GetClassTypes(){
            return Assembly.GetExecutingAssembly().GetTypes().ToList();
        }

        public static Type? FindType(string typeName){
            return GetClassTypes().Where(x => x.Name == typeName).FirstOrDefault();
        }
    }
}