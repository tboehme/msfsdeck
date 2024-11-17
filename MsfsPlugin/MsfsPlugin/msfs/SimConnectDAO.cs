namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Runtime.InteropServices;

    using Loupedeck.MsfsPlugin.tools;

    public class SimConnectDAO
    {
        private bool _simConnectConnected = false;

        private static readonly System.Timers.Timer timer = new System.Timers.Timer();

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

        public static void Refresh(Object source, EventArgs e)
        {

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

                    DataTransferIn.AddRequest();

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

        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            DebugTracing.Trace("Cnx opened");
            connection.SetMsfsValue(1);
            foreach (Binding binding in MsfsData.Instance.bindings.Values)
            {
                binding.MSFSChanged = true;
            }
            MsfsData.Instance.Changed();
            timer.Interval = timerInterval;
        }

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            var reader = (Readers)data.dwData[0];
            DataTransferIn.ReadMsfsValues(reader);

            DataTransferOut.SendEvents(m_oSimConnect);
            AutoTaxiInput(reader);
        }

        public void SendEvent(Enum eventName, UInt32 value)
        {
            if (_simConnectConnected)
            {
                DataTransferOut.Transmit(m_oSimConnect, eventName, value);
            }
        }

        private readonly object lockObject = new object();

        private void OnTick()
        {
            lock (lockObject)
            {
                try
                {
                    if (m_oSimConnect != null)
                    {

                        m_oSimConnect.RequestDataOnSimObjectType(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Readers, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                        m_oSimConnect.ReceiveMessage();
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

        private void AutoTaxiInput(Readers reader)
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
        }


    }
}
