using System;

namespace SimConnectWrapper
{
    public interface ISimConnectWrapper
    {
        void Connect();

        bool IsConnect();

        void send(Enum eventName, UInt32 value);

        void Disconnect(bool unloading = false);
    }
}