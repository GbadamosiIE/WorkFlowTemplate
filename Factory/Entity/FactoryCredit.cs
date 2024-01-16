using System.Security.Cryptography.X509Certificates;

namespace Factory.Entity
{
    public static class FactoryCredit 
    {
        public static object CreateCredit(string type)
        {
            switch (type)
            {
                case "Personal":
                    return new PersonalCredit();
                case "Vehicle":
                    return new VehicleCredit();
                case "Home":
                    return new HomeCredit();
                default:
                    return new PersonalCredit();
            }
        }
    }

    public class HomeCredit
    {
        public const string Name = "Home";
    }

    public class VehicleCredit
    {
        public const string Name = "Vehicle";
    }

    public class PersonalCredit
    {
        public const string Name = "Personal";
    }
}