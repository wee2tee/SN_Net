using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SN_Net.DataModels;

namespace SN_Net.MiscClass
{
    /// <summary>
    /// This class is for retrieve processing result from Server
    /// e.g. Some data return from Server when users login is containing 
    ///     - result (success | failed)
    ///     - message [optional] (a mesage from server to describe about failed cause)
    ///     - users (JSON string data about that users on success | blank JSON string on failed))
    ///     
    /// So, should declare all the data model as List<model-class> and naming it like the value key that return from Server
    /// </summary>
    public class ServerResult
    {
        public const int SERVER_CREATE_RESULT_FAILED = 0;
        public const int SERVER_CREATE_RESULT_FAILED_EXIST = 1;
        public const int SERVER_READ_RESULT_FAILED = 2;
        public const int SERVER_UPDATE_RESULT_FAILED = 3;
        public const int SERVER_UPDATE_RESULT_FAILED_EXIST = 4;
        public const int SERVER_DELETE_RESULT_FAILED = 5;
        public const int SERVER_RESULT_SUCCESS = 99;

        public int result { get; set; }
        public string message { get; set; }
        //public List<int> serial_id_list { get; set; }
        public List<Users> users { get; set; }
        public List<MacAllowed> macallowed { get; set; }
        public List<Dealer> dealer { get; set; }
        public List<Problem> problem { get; set; }
        public List<Serial> serial { get; set; }
        public List<Istab> istab { get; set; }
        public List<Istab> busityp { get; set; }
        public List<Istab> area { get; set; }
        public List<Istab> howknown { get; set; }
        public List<Istab> verext { get; set; }
        public List<Istab> problem_code { get; set; }
        public List<SupportNote> support_note { get; set; } // for retrieve support note data from server
        public List<SupportNoteComment> support_note_comment { get; set; } // for retrieve support note comment data from server
        public List<Serial_list> serial_list { get; set; } // for inquiry window
        public List<MACloud> macloud_list { get; set; } // for inquiry window
        public List<Dealer_list> dealer_list { get; set; } // for inquiry window
        public List<RegisterData> register_data { get; set; }
        public List<D_msg> d_msg { get; set; } // for retrieve d_msg (F8 in dealer window)
        public List<EventCalendar> event_calendar { get; set; } // for retrieve event_calendar (display in calendar)
        public List<TrainingCalendar> training_calendar { get; set; } // for retrieve training_calendar (display in calendar)
        public List<NoteCalendar> note_calendar { get; set; } // for retrieve note_calendar (display in calendar)
        public List<SpyLog> spy_log { get; set; } // for retrieve spy_log (Search history)
        public List<SerialPassword> serial_password { get; set; } // for retrieve password data for service to customer (display in SnWindow, SupportNoteWindow)
        public List<Ma> ma { get; set; } // for retrieve ma data to show in SnWindow
        public List<CloudSrv> cloudsrv { get; set; } // for retrieve cloudsrv data to show in SnWindow
        public PrintPageSetup print_page_setup { get; set; } // for retrieve print page setup of each form
    }
}
