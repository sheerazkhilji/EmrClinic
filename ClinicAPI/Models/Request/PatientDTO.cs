using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class PatientDTO
    {

      public  List<Patient> patients = new List<Patient>();

       public List<Campus> campuses = new List<Campus>();
       public List<Doctor> doctors = new List<Doctor>();

    }

    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }

    }


    public class Campus
    {

        public int CampusID { get; set; }
        public string CampusName { get; set; }


    }

    public class Doctor
    {

        public int DoctorID { get; set; }
        public string DoctorName { get; set; }


    }


}