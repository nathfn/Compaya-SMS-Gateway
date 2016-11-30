namespace CompayaSmsGateway.Config
{
    internal static class Endpoints
    {
        public static string SendSmsUrl = "https://api.cpsms.dk/v2/send";
        public static string SendGroupSmsUrl = "https://api.cpsms.dk/v2/sendgroup";
        public static string SmsCreditValueUrl = "https://api.cpsms.dk/v2/creditvalue";
        public static string SmsDeleteUrl = "https://api.cpsms.dk/v2/deletesms";

        public static string GroupCreate = "https://api.cpsms.dk/v2/addgroup";
        public static string GroupList = "https://api.cpsms.dk/v2/listgroups";
        public static string GroupUpdate = "https://api.cpsms.dk/v2/updategroup";
        public static string GroupDelete = "https://api.cpsms.dk/v2/deletegroup";

        public static string ContactCreate = "https://api.cpsms.dk/v2/addcontact";
        public static string ContactList = "https://api.cpsms.dk/v2/listcontacts/";
        public static string ContactUpdate  = "https://api.cpsms.dk/v2/updatecontact";
        public static string ContactDelete = "https://api.cpsms.dk/v2/deletecontact";

        public static string LogList = "https://api.cpsms.dk/v2/getlog";
    }
}
