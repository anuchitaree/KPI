using System;

namespace KPI.Models
{
    public static class User
    {
    
        public static string ID { get; set;}
        public static string Name { get; set; }
        public static string SectionCode { get; set; }
        public static string SectionName { get; set; }
        public static string Shift { get; set; }
        public static Roles Role { get; set; }
        public static string DayNight { get; set; }
        public static string Division { get; set; }
        public static string Plant { get; set; }
        public static string Email { get; set; }
        public static string Phone { get; set; }
        public static DateTime LogInTime { get; set; }
        public static DateTime LogOutTime { get; set; }
    }

   

    public enum Roles
    {
        zero,
        Invalid,
        General,
        Prod_Operator,
        Prod_Outline,
        Prod_LineLeader,
        Prod_TeamLeader,
        Prod_Manager,
        FacEng,
        Admin_Min,
        Admin_Full,
       
    }


}
