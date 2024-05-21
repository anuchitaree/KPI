using System;

namespace KPI.Parameter
{
    public static class Connection
    {

        public static String SQL_CONN_STRING = "";
        public static String SQL_CONN_STRING_WGR = "";
        public static String SQL_CONN_STRING_SRG = "";
        public static String SQL_CONN_STRING_BPK = "";

        private static string internale = "Data Source = 192.168.101.101\\SQLEXPRESS,1433; Initial Catalog = ";
        private static string externale = "Data Source = 49.229.106.33\\SQLEXPRESS, 10000; Initial Catalog = ";
        private static string login = "; User ID = Admin; Password = Admin";


        //public static String SQLLocal = @"Data Source = 192.168.1.233\SQLEXPRESS,1433; Initial Catalog = Production; User ID = Admin; Password = Admin";
        //public static String SQL4G = @"Data Source = 182.52.108.173\\SQLEXPRESS, 10000; Initial Catalog = Production; User ID = Admin; Password = Admin";
        public static void DB(string destination)
        {

            if (destination == "4G")
            {
                SQL_CONN_STRING = "Data Source = 49.229.106.33\\SQLEXPRESS, 10000; Initial Catalog = Production; User ID = Admin; Password = Admin";
                SQL_CONN_STRING_WGR = "Data Source = 49.229.106.33\\SQLEXPRESS, 10000; Initial Catalog = WGR; User ID = Admin; Password = Admin";
                SQL_CONN_STRING_SRG = "Data Source = 49.229.106.33\\SQLEXPRESS, 10000; Initial Catalog = SRG; User ID = Admin; Password = Admin";
                SQL_CONN_STRING_BPK = "Data Source = 49.229.106.33\\SQLEXPRESS, 10000; Initial Catalog = BPK; User ID = Admin; Password = Admin";
            }
            else if (destination == "Local")
            {
                SQL_CONN_STRING = @"Data Source = 192.168.101.101\SQLEXPRESS,1433; Initial Catalog = Production; User ID = Admin; Password = Admin";
                SQL_CONN_STRING_WGR = @"Data Source = 192.168.101.101\SQLEXPRESS, 1433; Initial Catalog = WGR; User ID = Admin; Password = Admin";
                SQL_CONN_STRING_SRG = @"Data Source = 192.168.101.101\SQLEXPRESS, 1433; Initial Catalog = SRG; User ID = Admin; Password = Admin";
                SQL_CONN_STRING_BPK = @"Data Source = 192.168.101.101\SQLEXPRESS, 1433; Initial Catalog = BPK; User ID = Admin; Password = Admin";
            }
        }

        public static void DBOaBoard(string connection ,string destination)
        {
            if (connection == "I")
                SQL_CONN_STRING = string.Format("{0}{1}{2}", externale, "Production", login);
            else if (connection == "E")
                SQL_CONN_STRING = string.Format("Data Source = {0}{1}{2}", destination, "\\SQLEXPRESS,1433; Initial Catalog = Production", login);
        }

        public static void ServiceConnection(string destination)
        {
            if (destination == "4G")
            {
                SQL_CONN_STRING = string.Format("{0}{1}{2}", externale, "Production", login);
                SQL_CONN_STRING_WGR = string.Format("{0}{1}{2}", externale, "WGR", login);
                SQL_CONN_STRING_SRG = string.Format("{0}{1}{2}", externale, "SRG", login);
                SQL_CONN_STRING_BPK = string.Format("{0}{1}{2}", externale, "BPK", login);
            }
            else if (destination == "Local")
            {
                SQL_CONN_STRING = string.Format("{0}{1}{2}", internale, "Production", login);
                SQL_CONN_STRING_WGR = string.Format("{0}{1}{2}", internale, "WGR", login);
                SQL_CONN_STRING_SRG = string.Format("{0}{1}{2}", internale, "SRG", login);
                SQL_CONN_STRING_BPK = string.Format("{0}{1}{2}", internale, "BPK", login);
            }

        }
    }
}
