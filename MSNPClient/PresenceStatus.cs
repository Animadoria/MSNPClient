using System;
using System.Collections.Generic;

namespace MSNPClient
{
    /// <summary>
    /// The presence status enum.
    /// </summary>
    public enum PresenceStatus
    {
        /// <summary>
        /// Online
        /// </summary>
        Online,
        /// <summary>
        /// Busy
        /// </summary>
        Busy,
        /// <summary>
        /// Idle
        /// </summary>
        Idle,
        /// <summary>
        /// Be Right Back
        /// </summary>
        BeRightBack,
        /// <summary>
        /// Away
        /// </summary>
        Away,
        /// <summary>
        /// OnThePhone
        /// </summary>
        OnThePhone,
        /// <summary>
        /// OutToLunch
        /// </summary>
        OutToLunch
    }

    /// <summary>
    /// This class is only used to translate the <c>PresenceStatus</c> enum to its command representation.
    /// </summary>
    public static class Presence
    {
        /// <summary>
        /// A dictionary containing the correspondent presence codes.
        /// </summary>
        public static Dictionary<PresenceStatus, string> PresenceCodes = new Dictionary<PresenceStatus, string>
        {
            { PresenceStatus.Online, "NLN" },
            { PresenceStatus.Busy, "BSY" },
            { PresenceStatus.Idle, "IDL" },
            { PresenceStatus.BeRightBack, "BRB" },
            { PresenceStatus.Away, "AWY" },
            { PresenceStatus.OnThePhone, "PHN" },
            { PresenceStatus.OutToLunch, "LUN" }
        };
    }
}
