using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class SupportNote
    {
        public enum NOTE_PROBLEM
        {
            FORM,
            MAP_DRIVE,
            ERROR,
            INSTALL_UPDATE,
            FONTS,
            REPORT_EXCEL,
            PRINT,
            MAIL_WAIT,
            STOCK,
            STATEMENT,
            SECURE,
            YEAR_END,
            PERIOD,
            ASSETS,
            TRAINING,
            TRANSFER_MKT,
            OTHER
        }

        public enum BREAK_REASON
        {
            TOILET,
            QT,
            MEET_CUST,
            TRAINING_TRAINER,
            TRAINING_ASSIST,
            CORRECT_DATA,
            OTHER
        }

        public int id { get; set; }
        public string date { get; set; }
        public string users_name { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string duration { get; set; }
        public string sernum { get; set; }
        public string contact { get; set; }
        public string problem { get; set; }
        public string remark { get; set; }
        public string is_break { get; set; }
        public string reason { get; set; }
        public string file_path { get; set; }
        public string rec_by { get; set; }
    }

    public static class SupportNoteHelper
    {
        public static string FormatNoteProblem(this SupportNote.NOTE_PROBLEM note_problem)
        {
            switch (note_problem)
            {
                case SupportNote.NOTE_PROBLEM.FORM:
                    return "{EDIT_FORM}";
                case SupportNote.NOTE_PROBLEM.MAP_DRIVE:
                    return "{MAP_DRIVE}";
                case SupportNote.NOTE_PROBLEM.ERROR:
                    return "{ERROR}";
                case SupportNote.NOTE_PROBLEM.INSTALL_UPDATE:
                    return "{INSTALL_UPDATE}";
                case SupportNote.NOTE_PROBLEM.FONTS:
                    return "{FONTS}";
                case SupportNote.NOTE_PROBLEM.REPORT_EXCEL:
                    return "{REPORT_EXCEL}";
                case SupportNote.NOTE_PROBLEM.PRINT:
                    return "{PRINT}";
                case SupportNote.NOTE_PROBLEM.MAIL_WAIT:
                    return "{MAIL_WAIT}";
                case SupportNote.NOTE_PROBLEM.STOCK:
                    return "{STOCK}";
                case SupportNote.NOTE_PROBLEM.STATEMENT:
                    return "{STATEMENT}";
                case SupportNote.NOTE_PROBLEM.SECURE:
                    return "{SECURE}";
                case SupportNote.NOTE_PROBLEM.YEAR_END:
                    return "{YEAR_END}";
                case SupportNote.NOTE_PROBLEM.PERIOD:
                    return "{PERIOD}";
                case SupportNote.NOTE_PROBLEM.ASSETS:
                    return "{ASSETS}";
                case SupportNote.NOTE_PROBLEM.TRAINING:
                    return "{TRAINING}";
                case SupportNote.NOTE_PROBLEM.TRANSFER_MKT:
                    return "{TRANSFER_MKT}";
                case SupportNote.NOTE_PROBLEM.OTHER:
                    return "{OTHER}";
                default:
                    return "{}";
            }
        }

        public static string FormatBreakReson(this SupportNote.BREAK_REASON break_reason)
        {
            switch (break_reason)
            {
                case SupportNote.BREAK_REASON.TOILET:
                    return "{TOILET}";
                case SupportNote.BREAK_REASON.QT:
                    return "{QT}";
                case SupportNote.BREAK_REASON.MEET_CUST:
                    return "{MEET_CUST}";
                case SupportNote.BREAK_REASON.TRAINING_TRAINER:
                    return "{TRAINING_TRAINER}";
                case SupportNote.BREAK_REASON.TRAINING_ASSIST:
                    return "{TRAINING_ASSIST}";
                case SupportNote.BREAK_REASON.CORRECT_DATA:
                    return "{CORRECT_DATA}";
                case SupportNote.BREAK_REASON.OTHER:
                    return "{OTHER}";
                default:
                    return "{}";
            }
        }
    }
}
