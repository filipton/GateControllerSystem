using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace GateServer4
{
	class Program
	{
		public static string sec_key = "secret_key";
		static HttpListener _listener;

		public static bool MakeGateAction = false;

		static void Main(string[] args)
		{
			try
			{
				sec_key = File.ReadAllText("secret_key");
			}
			catch 
			{ 
				sec_key = "test"; 
			}

			StartServer();
		}

		static void StartServer()
		{
			_listener = new HttpListener();
			try
			{
				_listener.Prefixes.Add("http://*:21374/");
				_listener.Start();
			}
			catch
			{
				_listener = new HttpListener();
				_listener.Prefixes.Add("http://localhost:21374/");
				_listener.Start();
			}
			_listener.BeginGetContext(OnContext, null);

			Console.ReadLine();
		}

		private static void OnContext(IAsyncResult ar)
		{
			var ctx = _listener.EndGetContext(ar);
			_listener.BeginGetContext(OnContext, null);

			Console.Out.WriteLineAsync($"[{DateTime.Now:HH:mm:ss.fff}] Handling request {ctx.Request.RawUrl}");

			string[] args = HttpUtility.UrlDecode(ctx.Request.RawUrl).Remove(0, 1).Split('/');

			byte[] buf = Encoding.UTF8.GetBytes(GetPageContent(args));

			ctx.Response.ContentType = "text/html";
			ctx.Response.OutputStream.Write(buf, 0, buf.Length);
			ctx.Response.OutputStream.Close();
		}

		static string GetPageContent(string[] args)
		{
			if(args.Length == 1 && args[0] == "info")
			{
				return "Ok!";
			}
			else if(args.Length == 1 && args[0] == "get")
			{
				if (MakeGateAction)
				{
					MakeGateAction = false;
					return "true";
				}

				return "false";
			}
			else if (args.Length == 2 && args[0] == "gate")
			{
				if(args[1] == sec_key)
				{
					if (!MakeGateAction)
					{
						MakeGateAction = true;
						return "Otwieranie bramy... (System 4)";
					}
					else
					{
						return "Poczekaj na zakończenie poprzedniego zapytania... (System 4)";
					}
				}
				else
				{
					return "Zły klucz dostępu... (System 4)";
				}
			}

			return "";
		}
	}
}
