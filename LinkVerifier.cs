﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;

namespace ConsoleAppTest
{

   
    public class LinkVerifier
    {

        public IEnumerable<AnchorTag> FindLinksWithDomainOnWebPage(string webpageUrl, string targetDomain)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(GetWebsiteHtml(webpageUrl));
            var anchorTags = htmlDocument.DocumentNode.SelectNodes("//a");

            foreach (var tag in anchorTags)
            {
                var hrefValue = tag.GetAttributeValue("href", "");
                var tempHref = hrefValue.ToUpper();
                var tempTargetDomain = targetDomain.ToUpper();
                if (tempHref.Contains(tempTargetDomain))
                {
                    var anchorTag = new AnchorTag();
                    foreach (var attribute in tag.Attributes)
                    {
                        if (attribute.Name == "href")
                        {
                            anchorTag.Attributes.Add(attribute.Name, attribute.Value);
                        }
                    }

                    anchorTag.InnerText = tag.InnerText;
                    yield return anchorTag;
                }
            }
        }

        private string GetWebsiteHtml(string webpageUrl)
        {
            WebClient webClient = new WebClient();
            byte[] buffer = webClient.DownloadData(webpageUrl);
            return Encoding.UTF8.GetString(buffer);
        }
    }

    public class AnchorTag
    {
        public Dictionary<string, string> Attributes { get; private set; }
        public string InnerText { get; set; }

        public AnchorTag()
        {
            Attributes = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("InnerText: " + InnerText);
            sb.AppendLine("Attributes:");
            foreach (var attribute in Attributes)
            {
                sb.AppendLine("\t" + attribute.Key + "=" + attribute.Value);
            
            }

            return sb.ToString();
        }

    }
}
