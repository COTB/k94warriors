using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K94Warriors.Enums
{
    /// <summary>
    /// Represents the possible types of users.
    /// </summary>
    public enum UserTypeEnum
    {
        /// <summary>
        /// Administrator.
        /// </summary>
        Administrator = 0,

        /// <summary>
        /// Trainer.
        /// </summary>
        Trainer,

        /// <summary>
        /// Volunteer.
        /// </summary>
        Volunteer
    }
}