using System;

namespace AsyncNet.Start.Logging
{
    public class GeneralLogger : IGeneralLogger
    {
        public void Info(string message)
        {
            Write(message);
        }

        private void Write(string message)
        {
            System.Console.WriteLine(string.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss.fff"), message));
        }
    }
}
