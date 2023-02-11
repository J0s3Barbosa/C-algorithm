using HtmlAgilityPack;

namespace ConsoleAppTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var lnkVerifier = new LinkVerifier();
            string targetElement = "div.clan__donation";
            string targetElementTabWar = "div.clanParticipants__row";

            start:
            Console.WriteLine("Insert Clan's link");

            var warlink = Console.ReadLine();

            while (string.IsNullOrEmpty(warlink) || string.IsNullOrWhiteSpace(warlink))
            {
                Console.WriteLine("Insert Clan's link");
                warlink = Console.ReadLine();
            }


            if (!warlink.StartsWith("https") && warlink != "l")
            {
                var clanId = warlink;
                warlink = string.Format("https://statsroyale.com/clan/{0}/war", clanId);
            }
            else if (warlink.StartsWith("https") && !warlink.EndsWith("war"))
            {
                var addWar = "war";
                warlink = string.Format("{0}/{1}", warlink, addWar );
            }
            else if (warlink == "l")
            {
                Console.WriteLine("Insert a list of Clan's link split by comma");
                warlink = Console.ReadLine();

                var lstOfclashLinks = warlink.Split(',');
                foreach (var item in lstOfclashLinks)
                {
                   var itemclashUrl = item.Replace("war", "");

                    GetNumberOfPlayersAndDonations(lnkVerifier, targetElement, itemclashUrl.Trim());

                    GetWarNumberOfPlayersAndDonations(lnkVerifier, targetElementTabWar, item.Trim());
                }
                goto start;
            }

            string? clashUrl = warlink.Replace("war", "");

            GetNumberOfPlayersAndDonations(lnkVerifier, targetElement, clashUrl);

            GetWarNumberOfPlayersAndDonations(lnkVerifier, targetElementTabWar, warlink);


            /*
            ExtractLinks(lstOfLinks, webpageUrl, targetDomain);
            FormatLinkToShow(lstOfLinks);
            */
            goto start;

        }

        private static void GetWarNumberOfPlayersAndDonations(LinkVerifier lnkVerifier, string targetElement, string clashUrl)
        {
            List<HtmlAgilityPack.HtmlNode> numberOfPlayers = new();
            var lstWarData = new List<WarData>();
            var zeroWins = 0;
            var greatWins = 0;

            var data = lnkVerifier.FindElementsOnWebPage(clashUrl, targetElement);

            CountingPlayers(numberOfPlayers, data);

            FillObjWithWarData(lstWarData, ref zeroWins, ref greatWins, data);

            Console.WriteLine(NumberOfPlayersInWar(lstWarData));

            var resultzeroWins = string.Format("Number of Zero Wins - {0}", zeroWins);

            var resultgreatWins = string.Format("Number of Wins - {0}", greatWins);

            Console.WriteLine(resultzeroWins);

            Console.WriteLine(resultgreatWins);
        }

        private static void FillObjWithWarData(List<WarData> lstWarData, ref int zeroWins, ref int greatWins, IEnumerable<HtmlNode> data)
        {
            var warData = new WarData();
            var count = 0;
            foreach (var item in data)
            {
                switch (count)
                {
                    case 0:
                        warData.Rank = item.InnerText;
                        break;
                    case 1:
                        warData.Name = item.InnerText;
                        break;
                    case 2:

                        warData.Battles = item.InnerText;
                        break;
                    case 3:
                        warData.Wins = int.Parse(item.InnerText);
                        if (warData.Wins == 0)
                        {
                            zeroWins++;
                        }
                        else
                        {
                            greatWins++;
                        }

                        break;

                }
                if (count == 4)
                {
                    warData.ClanCards = int.Parse(item.InnerText);
                    lstWarData.Add(warData);
                    count = 0;
                }
                else
                {
                    count++;
                }

            }
        }

        private static string NumberOfPlayersInWar(List<WarData> lstWarData)
        {
            var totalOfplayers = lstWarData.Count;
            var resultT = string.Format("Number of players in War - {0}", totalOfplayers);
            return resultT;
        }

        private static string FormatMessageToShow(string message, object formated)
        {
            return string.Format(message + " - {0}", formated);
        }

        private static void GetNumberOfPlayersAndDonations(LinkVerifier lnkVerifier, string targetElement, string clashUrl)
        {
            var numberOfPlayers = new List<HtmlNode>();
            var zeroDonations = 0;
            var greatDonations = 0;

            var data = lnkVerifier.FindElementsOnWebPage(clashUrl, targetElement);

            CountingPlayers(numberOfPlayers, data);

            foreach (var item in data)
            {
                var NumberOfDonations = int.Parse(item.InnerText);
                var resultD = string.Format("Donations - {0}", NumberOfDonations);

                if (NumberOfDonations == 0)
                {
                    zeroDonations++;
                }
                else if (NumberOfDonations >= 400)
                {
                    greatDonations++;
                }

                //Console.WriteLine(resultD);
            }
            var totalOfplayers = numberOfPlayers.Count;
            var resultT = string.Format("Number of players - {0}", totalOfplayers);
            Console.WriteLine(resultT);

            var resultZ = string.Format("Number of Zero Donations - {0}", zeroDonations);
            var resultGreatDonations = string.Format("Number of Donations Over 400 - {0}", greatDonations);
            Console.WriteLine(resultZ);
            Console.WriteLine(resultGreatDonations);
        }

        private static void CountingPlayers(List<HtmlNode> listOfZeroDonations, IEnumerable<HtmlNode> data)
        {
            foreach (var countingItem in data)
            {
                listOfZeroDonations.Add(countingItem);
            }
        }

        private static void ExtractLinks(List<Link> lstOfLinks, string webpageUrl, string targetDomain)
        {
            var lnkVerifier = new LinkVerifier();
            var links = lnkVerifier.FindLinksWithDomainOnWebPage(webpageUrl, targetDomain);
            foreach (var link in links)
            {
                AddLinksToObjct(lstOfLinks, link);
            }
        }

        private static void FormatLinkToShow(List<Link> lstOfLinks)
        {
            Console.WriteLine("************lstOfLinks***********");
            foreach (var link in lstOfLinks)
            {
                var desc = link.description;
                var li = link.link;
                if (string.IsNullOrEmpty(desc))
                {
                    desc = li.Split('/')[2];
                }
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
