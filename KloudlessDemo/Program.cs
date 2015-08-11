using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace KloudlessDemo
{
	class MainClass
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// Note that we declare this thread to be a single-threaded apartment
		/// since we are using a web browser control in a console app.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		[STAThread]
		public static void Main (string[] args)
		{

            // This initial code is to obtain input from the user so that
            // we can determine an app to use and service to authenticate.
            String appId = Util.GetAppId(allowDefault: !Util.SetBaseUrl());
            String chosenService = Util.GetService();
			
            // Launch a browser to perform authentication.
            WebBrowser wb = Authentication.open(appId, services: new List<String> {chosenService});
            while (!wb.IsDisposed)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            // After authentication, perform any API requests.
			performAPIRequests();
			return;
		}

		/// <summary>
		/// Small example performing an API request and printing the names of
		/// the contents of the root folder.
		/// </summary>
		private static void performAPIRequests()
		{
			if (Authentication.accounts.Count == 0) {
				Console.WriteLine ("No accounts connected. Press Enter to exit.");
                Console.ReadLine();
				return;
			}

			FileSystemList fsList = Authentication.accounts.First().Value.RetrieveContents ();

			Console.WriteLine ("\nFolder contents:");
			foreach (FileSystem fs in fsList) {
				Console.WriteLine ("\t" + fs.name);
			}

			Console.WriteLine ("\nPress Enter to exit.");
			Console.ReadLine();
		}
	}
}
