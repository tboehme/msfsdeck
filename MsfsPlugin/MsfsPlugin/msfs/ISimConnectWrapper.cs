

namespace Loupedeck.MsfsPlugin.msfs
{
    using static Loupedeck.MsfsPlugin.msfs.DataTransferTypes;

    public interface ISimConnectWrapper
    {
        void Connect();

        bool IsConnected();

        void send(Enum eventName, UInt32 value);

        void register(Enum eventName, string key);

        Readers requestData();

        void Disconnect(bool unloading = false);
    }
}