using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

namespace MainGateHttpServer
{
    class Program
    {
        public static HttpListener listener;
        public static string url = "http://*:21371/";
        public static string sec_key = "secret_key";

        public static void Main(string[] args)
        {
            sec_key = File.ReadAllText("/var/secret_key");
            Console.WriteLine(sec_key);

            Pi.Init<BootstrapWiringPi>();

            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }


        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                byte[] data = new byte[0];
                string[] args = req.Url.AbsolutePath.Remove(0, 1).Split('/');

                if (args.Length > 0)
                {
                    if (args.Length == 2 && args[0] == "gate")
                    {
                        if (args[1] == sec_key)
                        {
                            data = Encoding.UTF8.GetBytes("Otwieranie bramy... (System 1)");
                            new Thread(() =>
                            {
                                var virtualButton = Pi.Gpio[4];
                                virtualButton = Pi.Gpio[BcmPin.Gpio04];

                                virtualButton.PinMode = GpioPinDriveMode.Output;

                                virtualButton.Write(true);
                                Thread.Sleep(600);
                                virtualButton.Write(false);
                            }).Start();
                        }
                        else
                        {
                            data = Encoding.UTF8.GetBytes("Zły klucz dostępu... (System 1)");
                        }
                    }
                    else if (args.Length == 1 && args[0] == "info")
                    {
                        data = Encoding.UTF8.GetBytes("Ok!");
                    }
                }

                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }
    }
}