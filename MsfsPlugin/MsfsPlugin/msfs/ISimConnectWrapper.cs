

namespace Loupedeck.MsfsPlugin.msfs
{
    public interface ISimConnectWrapper
    {
        void Connect();

        bool IsConnect();

        void send(Enum eventName, UInt32 value);

        void write(Enum eventName, UInt32 value);

        void register(Enum eventName, string key);

        void Disconnect(bool unloading = false);
    }
}