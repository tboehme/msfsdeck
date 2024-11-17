namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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
            var assembly = context.LoadFromAssemblyPath("SimConnectWrapper.dll");
            context.LoadFromAssemblyPath("Microsoft.FlightSimulator.SimConnect.dll");

            Type assemblyType = assembly.GetType("SimConnectWrapper.SimConnectWrapper");

            if (assemblyType != null)
            {
                var argTypes = Array.Empty<Type>();

                ConstructorInfo cInfo = assemblyType.GetConstructor(argTypes);

                simWrapper = cInfo.Invoke(null);

                PluginLog.Info($"Connected :  {(bool)simWrapper.InvokeMethod("IsConnect")}");
                simWrapper.InvokeMethod("Connect");
                PluginLog.Info($"Connected :  {(bool)simWrapper.InvokeMethod("IsConnect")}");
            }
        }

        public void Connect() => throw new NotImplementedException();
        public void Disconnect(bool unloading = false) => throw new NotImplementedException();
        public bool IsConnect() => throw new NotImplementedException();
        public void send(Enum eventName, uint value) => throw new NotImplementedException();
        public void write(Enum eventName, uint value) => throw new NotImplementedException();
        public void register(Enum eventName, string key) => throw new NotImplementedException();
    }
}
