﻿using System;
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
        public List<Serial_list> serial_list { get; set; }
    }
}
