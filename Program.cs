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
            var webpageUrl = "https://github.com/appchto";
            string targetDomain = "github.com";

            var linkFinder = new LinkFinder();

            var links = linkFinder.FindLinksToDomainOnWebPage(webpageUrl, targetDomain);
            foreach (var link in links)
                Console.WriteLine(link);

            Console.ReadLine();

        }
    }
}
