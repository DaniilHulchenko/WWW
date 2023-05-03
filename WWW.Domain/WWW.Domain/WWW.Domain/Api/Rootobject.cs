using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Domain.Api
{

    public class Rootobject
    {
        public _Embedded _embedded { get; set; }
        public _Links2 _links { get; set; }
        public Page page { get; set; }
    }

    public class _Embedded
    {
        public Event[] events { get; set; }
    }

    public class Event
    {
        public string name { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public bool test { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string locale { get; set; }
        public Image[] images { get; set; }
        public Sales sales { get; set; }
        public Dates dates { get; set; }
        public string info { get; set; }
        public string pleaseNote { get; set; }
        public Ticketing ticketing { get; set; }
        public _Links _links { get; set; }
        public _Embedded1 _embedded { get; set; }
    }

    public class Sales
    {
        public Public _public { get; set; }
    }

    public class Public
    {
        public DateTime startDateTime { get; set; }
        public bool startTBD { get; set; }
        public bool startTBA { get; set; }
        public DateTime endDateTime { get; set; }
    }

    public class Dates
    {
        public Start start { get; set; }
        public string timezone { get; set; }
        public Status status { get; set; }
        public bool spanMultipleDays { get; set; }
        public End end { get; set; }
    }

    public class Start
    {
        public string localDate { get; set; }
        public string localTime { get; set; }
        public DateTime dateTime { get; set; }
        public bool dateTBD { get; set; }
        public bool dateTBA { get; set; }
        public bool timeTBA { get; set; }
        public bool noSpecificTime { get; set; }
    }

    public class Status
    {
        public string code { get; set; }
    }

    public class End
    {
        public string localDate { get; set; }
        public string localTime { get; set; }
        public DateTime dateTime { get; set; }
        public bool approximate { get; set; }
        public bool noSpecificTime { get; set; }
    }

    public class Ticketing
    {
        public Safetix safeTix { get; set; }
    }

    public class Safetix
    {
        public bool enabled { get; set; }
    }

    public class _Links
    {
        public Self self { get; set; }
        public Venue[] venues { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Venue
    {
        public string href { get; set; }
    }

    public class _Embedded1
    {
        public Venue1[] venues { get; set; }
    }

    public class Venue1
    {
        public string name { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public bool test { get; set; }
        public string url { get; set; }
        public string locale { get; set; }
        public string postalCode { get; set; }
        public string timezone { get; set; }
        public Country country { get; set; }
        public Address address { get; set; }
        public Boxofficeinfo boxOfficeInfo { get; set; }
        public Upcomingevents upcomingEvents { get; set; }
        public _Links1 _links { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string countryCode { get; set; }
    }

    public class Address
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string line3 { get; set; }
    }

    public class Boxofficeinfo
    {
        public string phoneNumberDetail { get; set; }
    }

    public class Upcomingevents
    {
        public int _total { get; set; }
        public int sportxruk_newcastlefalcons { get; set; }
        public int _filtered { get; set; }
        public int sportxrde_panther { get; set; }
        public int sportxruk_sportxr { get; set; }
    }

    public class _Links1
    {
        public Self1 self { get; set; }
    }

    public class Self1
    {
        public string href { get; set; }
    }

    public class Image
    {
        public string ratio { get; set; }
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool fallback { get; set; }
    }

    public class _Links2
    {
        public First first { get; set; }
        public Self2 self { get; set; }
        public Next next { get; set; }
        public Last last { get; set; }
    }

    public class First
    {
        public string href { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Next
    {
        public string href { get; set; }
    }

    public class Last
    {
        public string href { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }

}
