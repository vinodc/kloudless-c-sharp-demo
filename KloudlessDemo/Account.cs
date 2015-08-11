using System;
using KloudlessDemo;
using System.Windows.Forms;
using System.Collections.Generic;
using RestSharp;

namespace KloudlessDemo
{
	public class Account
	{
		internal String id { get; set; }
		internal String name { get; set; }
		internal String service { get; set; }
		internal String key { get; set; }

		public Account()
		{
		}

		public T Execute<T>(RestRequest request) where T : new()
		{
			var client = Util.GetClient ();
			request.AddHeader ("Authorization", "AccountKey " + key);
			request.AddParameter("accountId", id, ParameterType.UrlSegment);

			var response = client.Execute<T>(request);

			if (response.ErrorException != null)
			{
				const string message = "Error retrieving response.  Check inner details for more info.";
				var kloudlessException = new ApplicationException(message, response.ErrorException);
				throw kloudlessException;
			}
			return response.Data;
		}

		/// <summary>
		/// An example API request that retrieves folder contents for an account.
		/// </summary>
		/// <returns>
		/// A list of FileSystem objects representing the folder contents.
		/// </returns>
		/// <param name="folderId">ID of the folder to retrieve contents of</param>
		/// <param name="page">Page number</param>
		/// <param name="pageSize">Page size</param>
		public FileSystemList RetrieveContents(
			string folderId = "root", string page = "1", int pageSize = 1000) {
			var request = new RestRequest();
			request.Resource = "accounts/{accountId}/folders/{folderId}/contents";

			request.AddParameter("folderId", folderId, ParameterType.UrlSegment);
			request.AddQueryParameter ("page", page);
			request.AddQueryParameter ("page_size", pageSize.ToString());

			return Execute<FileSystemList>(request);
		}
	}

}

