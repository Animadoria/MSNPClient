using System;
namespace MSNPClient
{
    // Thanks, NINA! https://wiki.nina.bz/wiki/Protocols/MSNP/MSNC/Client_Capabilities

    /// <summary>
    /// The Client identification enum.
    /// </summary>
    [Flags]
    public enum ClientIdentification : uint
    {
        /// <summary>
        /// No identification.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// This means you are running a Windows Mobile device. The official client changes the little icon to a little man with a phone, and puts the status 'Phone' next to your name.
        /// </summary>
        MobileOnline = 0x01,
        /// <summary>
        /// This value is set if you are a MSN Explorer 8 user, but it is sometimes used when the client resets its capabilities
        /// </summary>
        MSN8User = 0x02,
        /// <summary>
        /// Your client can send/receive Ink (GIF format)
        /// </summary>
        RendersGif = 0x04,
        /// <summary>
        /// Your client can send/recieve Ink (ISF format)
        /// </summary>
        RendersIsf = 0x08,
        /// <summary>
        /// This option is set when you are able to participate in video conversations. In reality, it is only set when you have a webcam connected and have it set to 'shared'.
        /// </summary>
        WebCamDetected = 0x10,
        /// <summary>
        /// This value is being used with Multi-Packet Messaging.
        /// </summary>
        SupportsChunking = 0x20,
        /// <summary>
        /// This is used when the client is running on a MSN Mobile device. This is equivalent to the MOB setting in the BPR list.
        /// </summary>
        MobileEnabled = 0x40,
        /// <summary>
        /// This is used when the client is running on a MSN Direct device. This is equivalent to the WWE setting in the BPR list.
        /// </summary>
        DirectDevice = 0x80,
        /// <summary>
        /// This is used when someone signs in on the official Web-based MSN Messenger. It will show a new icon in other people's contact list.
        /// </summary>
        WebIMClient = 0x200,
        /// <summary>
        /// Internal Microsoft client and/or Microsoft Office Live client (TGWClient).
        /// </summary>
        ConnectedViaTGW = 0x800,
        /// <summary>
        /// This means you have a MSN Space.
        /// </summary>
        HasSpace = 0x1000,
        /// <summary>
        /// This means you are using Windows XP Media Center Edition.
        /// </summary>
        MCEUser = 0x2000,
        /// <summary>
        /// This means you support 'DirectIM' (creating direct connections for conversations rather than using the traditional switchboard)
        /// </summary>
        SupportsDirectIM = 0x4000,
        /// <summary>
        /// This means you support Winks receiving (If not set the official Client will warn with 'contact has an older client and is not capable of receiving Winks')
        /// </summary>
        SupportsWinks = 0x8000,
        /// <summary>
        /// Your client supports the MSN Search feature
        /// </summary>
        MSNSearch = 0x10000,
        /// <summary>
        /// The client is bot (provisioned account)
        /// </summary>
        IsBot = 0x20000,
        /// <summary>
        /// This means you support Voice Clips receiving
        /// </summary>
        SupportsVoiceIM = 0x40000,
        /// <summary>
        /// This means you support Secure Channel Communications
        /// </summary>
        SupportsSChannel = 0x80000,
        /// <summary>
        /// Supports SIP Invitations
        /// </summary>
        SupportsSipInvite = 0x100000,
        /// <summary>
        /// Supports Tunneled SIP
        /// </summary>
        SupportsTunneledSip = 0x200000,
        /// <summary>
        /// Sharing Folders
        /// </summary>
        SupportsSDrive = 0x400000,
        /// <summary>
        /// The client has OneCare
        /// </summary>
        HasOnecare = 0x1000000,
        /// <summary>
        /// Supports P2P TURN
        /// </summary>
        P2PSupportsTurn = 0x2000000,
        /// <summary>
        /// Supports P2P Bootstrap via UUN
        /// </summary>
        P2PBootstrapViaUUN = 0x4000000,
        /// <summary>
        /// Supports MSNC1 (MSN Msgr 6.0)
        /// </summary>
        MsgrVersion1 = 0x10000000,
        /// <summary>
        /// Supports MSNC2 (MSN Msgr 6.1)
        /// </summary>
        MsgrVersion2 = 0x20000000,
        /// <summary>
        /// Supports MSNC3 (MSN Msgr 6.2)
        /// </summary>
        MsgrVersion3 = 0x30000000,
        /// <summary>
        /// Supports MSNC4 (MSN Msgr 7.0)
        /// </summary>
        MsgrVersion4 = 0x40000000,
        /// <summary>
        /// Supports MSNC5 (MSN Msgr 7.5)
        /// </summary>
        MsgrVersion5 = 0x50000000,
        /// <summary>
        /// Supports MSNC6 (WL Msgr 8.0)
        /// </summary>
        MsgrVersion6 = 0x60000000,
        /// <summary>
        /// Supports MSNC7 (WL Msgr 8.1)
        /// </summary>
        MsgrVersion7 = 0x70000000,
        /// <summary>
        /// Supports MSNC8 (WL Msgr 8.5)
        /// </summary>
        MsgrVersion8 = 0x80000000,
        /// <summary>
        /// Supports MSNC9 (WL Msgr 9.0)
        /// </summary>
        MsgrVersion9 = 0x90000000,
        /// <summary>
        /// Supports MSNC10 (WL Msgr 14.0)
        /// </summary>
        MsgrVersion10 = 0xA0000000
    }
}
