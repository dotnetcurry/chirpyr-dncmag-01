using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChirpyR.Domain.Model
{
    public class ChirpyRUser
    {
        public long Id 
            { get; set; }
        public string UserId 
            { get; set; }
        public string FullName 
            { get; set; }
        public string Email 
            { get; set; }
        public string Gravataar 
            { get; set; }
        /// <summary>
        /// Password is not loaded from 
        /// the database. It is for
        /// new account registration ONLY
        /// </summary>
        public string Password 
            { get; set; }
        /// <summary>
        /// Used in the ViewModel only
        /// </summary>
        public string OldPassword 
            { get; set; }
        public List<ChirpyRUser> FollowerIds 
            { get; set; }
        public List<ChirpyRUser> FollowingIds 
            { get; set; }
    }
}
