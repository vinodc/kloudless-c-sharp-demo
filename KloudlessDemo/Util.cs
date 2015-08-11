using System;
using System.Collections.Generic;
using RestSharp;

namespace KloudlessDemo
{
	public class Util
	{
		public static string BASE_URL = "https://api.kloudless.com";
		public static string VERSION = "v0";
        public static List<String> SERVICES = new List<String> {
			"skydrive", "box", "gdrive", "sharepoint", "onedrivebiz"
		};

		public static string GetPath(String path="")
		{
			return BASE_URL + '/' + VERSION + '/' + path.Trim();
		}

		public static string GetAuthPath(String appId,
			List<String> services = null)
		{
			String url = BASE_URL + "/services/?";
			url += "retrieve_account_key=true&admin=";

			url += "&app_id=" + appId;

			if (services != null && services.Count > 0) {
				url += "&services=" + String.Join (",", services);
			}

			return url;
		}

		public static RestClient GetClient()
		{
			var client = new RestClient ();
			client.BaseUrl = new Uri(Util.GetPath());
			return client;
        }

        #region User Input

        // This region does not have to do with API requests and just handles user input.

        /// <summary>
        /// Prompts the user to enter the location of the API server
        /// </summary>
        /// <returns>true if a custom base URL was entered, false if the default was used.</returns>
        public static bool SetBaseUrl()
        {
            Console.Write("Please enter the URL of the Kloudless API server, " +
                "or press Enter to use https://api.kloudless.com: ");
            String baseUrl = Console.ReadLine().Trim();
            if (baseUrl.Length > 0)
            {
                Util.BASE_URL = baseUrl;
                return true;
            }
            return false;
        }

        public static string GetAppId(bool allowDefault = true)
        {
            String appId = "", defaultText = "";

            if (allowDefault)
                defaultText = ", or press Enter to use a Kloudless demo app";

            while (appId == "") {
                Console.Write("Please enter the Kloudless App ID" + defaultText + ": ");
                appId = Console.ReadLine().Trim();
                if (appId.Length == 0 && allowDefault)
                    appId = "iCZ_ICMy43H0NSoz0QbLvmyjzCHf2frAOPaBfWVgh9_vrFIM";
            }
            return appId;
        }

        public static string GetService()
        {
            String chosenService = null;
            Console.WriteLine("Services available to connect:");
            foreach (String service in SERVICES)
                Console.WriteLine("\t * " + service);
            while (!SERVICES.Contains(chosenService))
            {
                Console.Write("Please enter the name of the service to authenticate: ");
                chosenService = Console.ReadLine();
            }
            return chosenService;
        }
        #endregion
    }
}

