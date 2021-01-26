using System;
using HtmlAgilityPack;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Parsers
{
    public static class ParserExt
    {
        public static HtmlNode TryGetNode(this HtmlNode node, params string[] selectors)
        {
            foreach (var selector in selectors)
            {
                var n = node.QuerySelector(selector);
                if (n != null)
                    return n;
            }
            return null;
        }
    }
}
