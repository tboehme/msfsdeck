using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Runtime.InteropServices;

namespace SimConnectWrapper
{
    using static DataTransferTypes;
    public class SimConnectWrapper : ISimConnectWrapper
    {
        public const Int32 WM_USER_SIMCONNECT = 0x0402;
        private SimConnect m_oSimConnect = null;
        private bool _simConnectConnected = false;
        private static readonly System.Timers.Timer timer = new System.Timers.Timer();
        private const double timerInterval = 200;
        private readonly object lockObject = new object();
        private enum DATA_REQUESTS
        {
            REQUEST_1
        }
        public enum hSimconnect : int
        {
            group1
        }
        public void Connect()
        {
            try
            {
                m_oSimConnect = new SimConnect("MSFS Plugin", new IntPtr(0), WM_USER_SIMCONNECT, null, 0);
                m_oSimConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                m_oSimConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);
                m_oSimConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);

                lock (timer)
                {
                    timer.Interval = timerInterval;
                    timer.Elapsed += Refresh;
                    timer.Enabled = true;
                }
            }
            catch (COMException ex)
            {
            }
            _simConnectConnected = true;
        }

        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            timer.Interval = timerInterval;
        }

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
        }

        private void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            SIMCONNECT_EXCEPTION eException = (SIMCONNECT_EXCEPTION)data.dwException;
        }

        private void Refresh(Object source, EventArgs e)
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
                    Disconnect();
                }
            }

        }

        public void Disconnect(bool unloading = false)
        {
            if (m_oSimConnect != null)
            {
                m_oSimConnect.Dispose();
                m_oSimConnect = null;
                _simConnectConnected = false;
            }

            if (unloading)
                return;
        }

        public bool IsConnect() => _simConnectConnected;
        public void send(Enum eventName, uint value)
        {
            m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, eventName, value, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
        }

    }
}
