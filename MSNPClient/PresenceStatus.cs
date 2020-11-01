using System;
using System.Collections.Generic;

namespace MSNPClient
{
    public enum PresenceStatus
    {
        Online,
        Busy,
        Idle,
        BeRightBack,
        Away,
        OnThePhone,
        OutToLunch
    }

    public static class Presence
    {
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
