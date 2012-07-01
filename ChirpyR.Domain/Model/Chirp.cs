using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChirpyR.Domain.Model
{
    public class Chirp
    {
        public long Id 
            { get; set; }
        public string Text 
            { get; set; }
        public List<ChirpTag> Tags 
            { get; set; }
        public List<Chirp> Replies 
            { get; set; }
        public DateTime ChirpTime 
            { get; set; }
        public string ChirpUrl 
            { get; set; }
        public ChirpyRUser ChirpBy 
            { get; set; }
        public Chirp InReplyTo 
            { get; set; }
    }
}
