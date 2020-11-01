using System;
namespace MSNPClient
{
    [Flags]
    public enum ClientIdentification : uint
    {
        None = 0x00,
        // Thanks, NINA! https://wiki.nina.bz/wiki/Protocols/MSNP/MSNC/Client_Capabilities
        MobileOnline = 0x01,
        MSN8User = 0x02,
        RendersGif = 0x04,
        RendersIsf = 0x08,
        WebCamDetected = 0x10,
        SupportsChunking = 0x20,
        MobileEnabled = 0x40,
        DirectDevice = 0x80,
        WebIMClient = 0x200,
        ConnectedViaTGW = 0x800,
        HasSpace = 0x1000,
        MCEUser = 0x2000,
        SupportsDirectIM = 0x4000,
        SupportsWinks = 0x8000,
        MSNSearch = 0x10000,
        IsBot = 0x20000,
        SupportsVoiceIM = 0x40000,
        SupportsSChannel = 0x80000,
        SupportsSipInvite = 0x100000,
        SupportsTunneledSip = 0x200000,
        SupportsSDrive = 0x400000,
        HasOnecare = 0x1000000,
        P2PSupportsTurn = 0x2000000,
        P2PBootstrapViaUUN = 0x4000000,
        MsgrVersion1 = 0x10000000,
        MsgrVersion2 = 0x20000000,
        MsgrVersion3 = 0x30000000,
        MsgrVersion4 = 0x40000000,
        MsgrVersion5 = 0x50000000,
        MsgrVersion6 = 0x60000000,
        MsgrVersion7 = 0x70000000,
        MsgrVersion8 = 0x80000000,
        MsgrVersion9 = 0x90000000,
        MsgrVersion10 = 0xA0000000
    }
}
