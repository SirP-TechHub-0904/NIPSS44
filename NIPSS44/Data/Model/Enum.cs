using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NIPSS44.Data.Model
{
    public enum EventType
    {
       
        [Description("NONE")]
        NONE = 0,
        [Description("Timetable")]
        Timetable = 2,
        [Description("Event")]
        Event = 3,
       
    }
    public enum OptionType
    {

        [Description("NONE")]
        NONE = 0,
        [Description("ShortNote")]
        ShortNote = 2,
        [Description("LongNote")]
        LongNote = 3,
        [Description("YesNo")]
        YesNo = 4,
        [Description("Option")]
        Option = 5,

    }
    public enum PostFileType
    {

        [Description("IMG")]
        IMG = 0,
        [Description("VIDEO")]
        VIDEO = 2,
       
    }
    public enum DocType
    {

        [Description("PDF")]
        PDF = 0,
        [Description("DOC")]
        DOC = 2,
        [Description("PowerPoint")]
        PowerPoint = 3,

    }

    public enum ProfileUpdateLevel
    {

        [Description("ONE")]
        ONE = 0,
        [Description("TWO")]
        TWO = 2,
        [Description("THREE")]
        THREE = 3,

    }

    public enum ContentStatus
    {

        [Description("NONE")]
        NONE = 0,
        [Description("IsBold")]
        IsBold = 2,
        [Description("IsRed")]
        IsRed = 3,

    }

    public enum ContentType
    {

        [Description("NONE")]
        NONE = 0,
        [Description("IsSingle")]
        IsSingle = 2,
       
    }

    public enum NotificationStatus
    {
        [Description("NotDefind")]
        NotDefind = 0,
        [Description("Sent")]
        Sent = 1,

        [Description("NotSent")]
        NotSent = 2,


    }
    public enum NotificationType
    {
        [Description("NotDefind")]
        NotDefind = 0,
        [Description("SMS")]
        SMS = 1,

        [Description("Email")]
        Email = 2


    }
}