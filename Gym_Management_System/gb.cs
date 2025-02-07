using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Interface
{
    internal class gb
    {
        //login form
        public static string mun;
        public static string mpass;
        //register

        public static string mfname;
        public static string mlname;
        public static string mdob;
        public static string mgender;
        public static string memail;
        public static string musername;
        public static string mpassword;
        public static string membershiptype;
        public static int duration;
        //member woirkout and dietplan
        public static int workoutid;
        public static int dietplanid;
        public static int noofmealz;
        public static int mchoosen;

        //login and registration form
        public static string T_un;
        public static string T_pass;
        public static string T_fname;
        public static string T_lname;
        public static string T_gender;
        public static string T_email;
        public static string T_dob;
        public static string T_qualification;
        public static string T_specialityarea;
        public static string T_experience;
        public static string T_status;
        public static string T_gym;
        public static int T_gymid;

        //trainer workout and dietplan
        public static int Tworkoutid;
        public static int Tdietplanid;
        public static int Tnoofmealz;

        // dietplan

        public static int check;


        public static string a_un;//admin user name
        public static string a_pass;
        public static string g_un; //onwer user name
        public static string g_pass;
        public static string g_fname;
        public static string g_lname;
        public static string g_email;
        public static string g_dob;
        public static string g_gym_name; //gym name
        public static string g_gender;
        public static int g_id;
        public static int CurrentGymId { get; set; }

    }
}
