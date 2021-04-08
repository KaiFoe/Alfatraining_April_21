using System;
namespace Kommunikation
{
    public class Models
    {
        public Models()
        {
        }
    }

    static class PhoneNumber
    {
        public static string iPhone { get; } = "(49) 160 97501952";
        public static string mobile { get; } = "(49) 172 55432123";
    }

    static class Name
    {
        public static string familienname { get; } = "Foerstermann";
        public static string vorname { get; } = "Kai";
    }

    static class Adresse
    {
        public static string strasse { get; } = "Schmaler Weg 6";
        public static string stadt { get; } = "Hermannsburg";
        public static string plz { get; } = "29320";
    }
}
