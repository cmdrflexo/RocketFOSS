using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RFOSSCore
{
    public static class ConvertTimeCode
    {
        public static string ToString(double tc)
        {
            long timecode = Convert.ToInt64(tc);

            int minuteLength = 60; //length of a minute in seconds
            int hourLength = 3600; //length of an hour in seconds
            int dayLength = 86400; //length of a day in seconds
            int yearLength = 31536000; //length of a year in seconds

            int seconds;
            int minutes;
            int hours;
            int days;
            int years;

            years =(int)(timecode / yearLength);
            days = (int)(timecode / dayLength) % (yearLength / dayLength);
            hours = (int)(timecode / hourLength) % (dayLength / hourLength);
            minutes = (int)(timecode / minuteLength) % minuteLength;
            seconds = (int)(timecode % 60);

            return "Y: " + years + " D: " + days + " H: " + hours + " M: " + minutes + " S: " + seconds;
        }
    }
}