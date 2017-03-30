using System.IO.Ports;

namespace Core
{
    public static class Port
    {
        public static void Write(int portId, byte[] data)
        {
            string portName = $"COM{portId}";
            using (var port = new SerialPort(portName, 9600))
            {
                port.WriteTimeout = 5000;
                port.ReadTimeout = 5000;

                if (port.IsOpen)
                {
                    port.Close();
                }
                port.Open();
                port.Write(data, 0, data.Length);
                var result = port.ReadByte();
                port.Close();
            }
        }
    }
}
