using System;
using System.Collections.Generic;

namespace ConsoleAppTest
{

    public  class Link
    {
        public string description { get; set; }
        public string link { get; set; }

    }

    internal class Program
    {


        private static void Main(string[] args)
        {
            List<Link> lstOfLinks = new List<Link>();
            var webpageUrl = "https://github.com/appchto";
            string targetDomain = "github.com";

            ExtractLinks(lstOfLinks, webpageUrl, targetDomain);
            FormatLinkToShow(lstOfLinks);
            Console.ReadLine();

        }

        private static void ExtractLinks(List<Link> lstOfLinks, string webpageUrl, string targetDomain)
        {
            var linkFinder = new LinkFinder();
            var links = linkFinder.FindLinksToDomainOnWebPage(webpageUrl, targetDomain);
            foreach (var link in links)
            {
                AddLinksToObjct(lstOfLinks, link);
                Console.WriteLine(link);
            }
        }

        private static void FormatLinkToShow(List<Link> lstOfLinks)
        {
            Console.WriteLine("************lstOfLinks***********");
            foreach (var link in lstOfLinks)
            {
                var desc = link.description;
                var li = link.link;
                var show = string.Format("{0} - {1}", desc, li);
                Console.WriteLine(show);
            }
        }

        private static void AddLinksToObjct(List<Link> lstOfLinks, AnchorTag link)
        {
            var l = new Link();
            l.description = link.InnerText.Trim();
            foreach (var lin in link.Attributes.Values)
            {
                l.link = lin;
            }
            lstOfLinks.Add(l);
        }

    }
}
