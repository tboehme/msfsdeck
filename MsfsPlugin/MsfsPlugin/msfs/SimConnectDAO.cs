namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Runtime.InteropServices;

    using Loupedeck.MsfsPlugin.tools;
    using static Loupedeck.MsfsPlugin.msfs.DataTransferTypes;

    public class SimConnectDAO
    {
        private bool _simConnectConnected = false;

        private static readonly System.Timers.Timer timer = new System.Timers.Timer();

        private static readonly Lazy<SimConnectDAO> lazy = new Lazy<SimConnectDAO>(() => new SimConnectDAO());
        public static SimConnectDAO Instance => lazy.Value;


        private const double timerInterval = 200;
        private enum DATA_REQUESTS
        {
            REQUEST_1
        }

        private readonly Binding connection;
        private readonly Binding autoTaxi;

        private SimConnectDAO()
        {
            connection = MsfsData.Instance.Register(BindingKeys.CONNECTION);
            autoTaxi = MsfsData.Instance.Register(BindingKeys.AUTO_TAXI);
        }

        private void Refresh(Object source, EventArgs e)
        {
            lock (lockObject)
            {
                try
                {
                    if (SimConnectWrapper.Instance.IsConnected())
                    {
                        connection.SetMsfsValue(1);
                        //DataTransferIn.ReadMsfsValues(SimConnectWrapper.Instance.requestData());
                        DataTransferOut.SendEvents(SimConnectWrapper.Instance);
                        MsfsData.Instance.Changed();
                    }
                    else
                    {
                        timer.Enabled = false;
                    }
                }
                catch (COMException exception)
                {
                    DebugTracing.Trace(exception);
                    Disconnect();
                }
            }
        }
        public void Connect()
        {
            if (connection.MsfsValue == 0)
            {
                DebugTracing.Trace("Trying cnx");
                connection.SetMsfsValue(2);
                foreach (Binding binding in MsfsData.Instance.bindings.Values)
                {
                    binding.MSFSChanged = true;
                }
                MsfsData.Instance.Changed();
                try
                {
                    SimConnectWrapper.Instance.Connect();

                    DataTransferOut.setPlugin(MsfsData.Instance.plugin);
                    DataTransferOut.initEvents();


                    lock (timer)
                    {
                        timer.Interval = timerInterval;
                        timer.Elapsed += Refresh;
                        timer.Enabled = true;
                    }
                }
                catch (COMException ex)
                {
                    DebugTracing.Trace(ex);
                    connection.SetMsfsValue(0);
                    foreach (Binding binding in MsfsData.Instance.bindings.Values)
                    {
                        binding.MSFSChanged = true;
                    }
                    MsfsData.Instance.Changed();
                }
                _simConnectConnected = true;
            }
        }
        public bool IsSimConnectConnected() => _simConnectConnected;
        
        public void Disconnect(bool unloading = false)
        {
            DebugTracing.Trace($"Disconnecting - unloading={unloading}");
            SimConnectWrapper.Instance.Disconnect();
            _simConnectConnected = false;
            

            if (unloading)
                return;

            connection.SetMsfsValue(0);
            foreach (Binding binding in MsfsData.Instance.bindings.Values)
            {
                binding.MSFSChanged = true;
            }
            MsfsData.Instance.Changed();
        }

        private readonly object lockObject = new object();

/*        private void AutoTaxiInput(Readers reader)
        {
            if (reader.onGround == 1)
            {
                if (autoTaxi.ControllerValue >= 2)
                {
                    if (reader.groundSpeed > 19)
                    {
                        autoTaxi.SetMsfsValue(3);
                        DataTransferOut.Transmit(m_oSimConnect, EVENTS.BRAKES, 1);
                    }
                    else
                    {
                        autoTaxi.SetMsfsValue(2);
                    }
                }
                else
                {
                    autoTaxi.SetMsfsValue(1);
                }
            }
            else
            {
                autoTaxi.SetMsfsValue(0);
            }
        }*/


    }
}
