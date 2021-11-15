using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class Appointment
    {
		public string PatientId { get; set; }

		public string Appointment_Data { get; set; }

		public string sDate { get; set; }

		public string eDate { get; set; }

		public string DoctorID { get; set; }

		public string PaymentType { get; set; }

		public string Amount { get; set; }

		public string AppointmentStatus { get; set; }

		public string AppointmentID { get; set; }

		public string Pateint_Email { get; set; }


        public  int AppType { get; set; }


        public string link { get; set; }

        public int  campusid { get; set; }

    }
	public class WebAppointment: Appointment
	{
		public string AppointmentType { get; set; }

		public string DrEmail { get; set; }

		public string PatientFullName { get; set; }

		public string PatientAge { get; set; }

		public string PatientGender { get; set; }

		public string OnBehalfOf { get; set; }

		public string RelationshipWithPatient { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string Address { get; set; }

		public string City { get; set; }


		public string Province { get; set; }

		public string Country { get; set; }


		public string PeriviouslyConsulted { get; set; }

		public string PeriviouslyConsultedDetails { get; set; }

		public string ReferredBy { get; set; }

		public string InitialAppointmentDateTime { get; set; }
		public string FollowupDateTime { get; set; }

	}
}