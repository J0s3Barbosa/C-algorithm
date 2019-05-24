using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ConsoleAppTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var HostURI = "https://github.com/appchto";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(HostURI);
            request.Method = "GET";
            String test = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                test = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }
            Console.WriteLine(test);

            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();

        }
    }
}
