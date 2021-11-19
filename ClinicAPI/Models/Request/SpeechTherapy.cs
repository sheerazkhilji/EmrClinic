using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class SpeechTherapy
    {

        public int StId { get; set; }

        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public string Dateofbirth { get; set; }
        public string ParentGuardian { get; set; }


        public string BillingAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }


        public string ChildLiveWithBothParents { get; set; }

        public string IfNowithWhomChildLive { get; set; }

        public string PrimaryLanguage { get; set; }

        public string SecondaryLanguage { get; set; }
        public string Pediatrician { get; set; }

        public string PediatricianPhone { get; set; }

        public string ReferringPhysician { get; set; }

        public string HowDoYouHereAboutUs { get; set; }

        public string PreviousSpeechTherapyEvaluation { get; set; }


        public string OtherTherapiesToDate { get; set; }


        public string Presentproblem { get; set; }


        public string NotedPresentProblem { get; set; }

        public string CreatedDate { get; set; }

        public string ChildReaction { get; set; }


        public string FamilyReaction { get; set; }

        public string significantChangesInLastSixMonths { get; set; }

        public string significantIfSoWhat { get; set; }

        public string ChildInfection { get; set; }


        public string EustachianTubes { get; set; }

        public string Chronic { get; set; }
        public string Allergies { get; set; }
        public string AllergiesDescribe { get; set; }
        public string SeriousOrRecurrentIllnesses { get; set; }
        public string AnyOperation { get; set; }
        public string OperationDescribe { get; set; }
        public string TraumaToHead { get; set; }
        public string AnyMedications { get; set; }
        public string VisionProblems { get; set; }
        public string VisionProblemsDescribe { get; set; }
        public string HearingDifficulties { get; set; }
        public string HearingDifficultiesDescribe { get; set; }
        public string DentalProblem { get; set; }
        public string DentalProblemDescribe { get; set; }
        public string OtherMedicalHistory { get; set; }
        public string AgeWhenChildSatUpAlone { get; set; }
        public string Crawled { get; set; }
        public string Walked { get; set; }
        public string ToitelTrained { get; set; }
        public string DressedIndependently { get; set; }
        public string TiedShoes { get; set; }
        public string LeftOrRightHanded { get; set; }
        public string SelfdiRectedActivities { get; set; }
        public string AdultDirected { get; set; }
        public string BedTime { get; set; }
        public string ChildSleepWell { get; set; }
        public string RespondTypicallyToLightSoundPeople { get;  set; }
        public string ChildPlayWithOthers { get; set; }
        public string Who { get; set; }
        public string CryAppropriately { get; set; }
        public string ChildsCurrentSchool { get; set; }

        public string Grade { get; set; }

        public string ChildPerformanceEducationally { get; set; }
        public string ReceivingSpecialServicesAtSchool { get; set; }
        public string IfYesPleaseDescribeTheSerivces { get; set; }
        public string IFSPOrIEP { get; set; }
        public string TeacherDescribePerformance { get; set; }
        public string TeacherExpressedAnyConcern { get; set; }
        public string TeacherExpressedAnyConcernExplain { get; set; }
        public string resultOfThisEvaluation { get; set; }
        public string Anythingelse { get; set; }










    }
}