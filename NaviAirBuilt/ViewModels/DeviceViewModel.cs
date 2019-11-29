using FirstFloor.ModernUI.Windows.Controls;
using LinnerToolkit.Desktop.Codec;
using Mina.Core.Future;
using Mina.Filter.Codec;
using Mina.Transport.Serial;
using NaviAirBuilt.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NaviAirBuilt.ViewModels
{
  public  class DeviceViewModel
    {
        public SerialConnector ShakeConnector { get; protected set; }
     

        public bool Initialize()
        {
            ShakeConnector = new SerialConnector();
            ShakeConnector.FilterChain.AddLast("codec", new ProtocolCodecFilter(new Codec.DefaultDemuxingProtocolCodecFactory(new DataDecoder(), new DataEncoder())));
            IConnectFuture future = ShakeConnector.Connect(new SerialEndPoint("COM5", 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One));
            future.Await();
            return true;
        }
        public void Start(string dataFileName = "")
        {
            // Stop();

            ShakeConnector.MessageReceived += ShakeConnector_MessageReceived;

        
        }

        private void ShakeConnector_MessageReceived(object sender, Mina.Core.Session.IoSessionMessageEventArgs e)
        {
            try
            {
                //if (e.Message is SensorData sensorData)
                //{
                //    if (_trainerDic.ContainsKey(sensorData.MachineID))
                //        _trainerDic[sensorData.MachineID].GetData(sensorData, ProcessController.Position);
                //}

            }
            catch (Exception ex)
            {

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ModernDialog.ShowMessage($"MessageReceived. Message: {ex.Message}, Stacktrace: {ex.StackTrace}", "错误", MessageBoxButton.OK);
                }));
            }
        }

        public void Stop()
        {
            ShakeConnector.MessageReceived -= ShakeConnector_MessageReceived;
        }

        public void Dispose()
        {
            if (ShakeConnector != null)
            {
                foreach (var session in ShakeConnector.ManagedSessions.Values)
                {
                    if (session.Connected)
                        session.Close(true);
                }
                ShakeConnector.Dispose();
                ShakeConnector = null;
            }
        }

  }
}
