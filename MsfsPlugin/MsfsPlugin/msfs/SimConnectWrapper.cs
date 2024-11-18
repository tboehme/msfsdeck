namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Metadata.Ecma335;
    using System.Runtime.Loader;
    using System.Text;
    using System.Threading.Tasks;

    internal class SimConnectWrapper : ISimConnectWrapper
    {
        private readonly object simWrapper;
        private static readonly Lazy<SimConnectWrapper> lazy = new Lazy<SimConnectWrapper>(() => new SimConnectWrapper());

        public static SimConnectWrapper Instance => lazy.Value;

        public SimConnectWrapper()
        {
            var context = new AssemblyLoadContext("MSFSContext");
            var assembly = context.LoadFromAssemblyPath("C:\\Users\\calib\\source\\repos\\msfsdeck\\bin\\Debug\\SimConnectWrapper.dll");
            context.LoadFromAssemblyPath("C:\\Users\\calib\\OneDrive\\Downloads\\Logi_Plugin_Tool_Win_6_0_1_20790_ccd09903f8\\LogiPluginSdkTools\\TestDLLPlugin\\bin\\Debug\\Microsoft.FlightSimulator.SimConnect.dll");
            
            Type assemblyType = assembly.GetType("SimConnectWrapper.SimConnectWrapper");

            if (assemblyType != null)
            {
                var argTypes = Array.Empty<Type>();
                ConstructorInfo cInfo = assemblyType.GetConstructor(argTypes);
                simWrapper = cInfo.Invoke(null);
            }
        }

        public void Connect() => simWrapper.InvokeMethod("Connect");
        public void Disconnect(bool unloading = false) => simWrapper.InvokeMethod("Disconnect");
        public void send(Enum eventName, uint value) => simWrapper.InvokeMethod("send", [eventName, value]);
        public void register(Enum eventName, string key) => simWrapper.InvokeMethod("register", [eventName, key]);
        public bool IsConnected() => (bool)simWrapper.InvokeMethod("IsConnected");
        public DataTransferTypes.Readers requestData() => (DataTransferTypes.Readers) simWrapper.InvokeMethod("requestData");
    }
}
