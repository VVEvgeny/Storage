using System;
using System.Collections.Generic;
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

            DebugGlobal.Write("Не нашли ни 1 попадания по селекторам, опять изменились ????");
            return null;
        }
        public static IList<HtmlNode> TryGetNodeAll(this HtmlNode node, params string[] selectors)
        {
            foreach (var selector in selectors)
            {
                var n = node.QuerySelectorAll(selector);
                if (n != null && n.Count != 0)
                    return n;
            }

            DebugGlobal.Write("Не нашли ни 1 попадания по селекторам, опять изменились ????");
            return null;
        }
        public static IList<HtmlNode> TryGetNodeAll(this HtmlDocument node, params string[] selectors)
        {
            foreach (var selector in selectors)
            {
                var n = node.QuerySelectorAll(selector);
                if (n != null && n.Count != 0)
                    return n;
            }

            DebugGlobal.Write("Не нашли ни 1 попадания по селекторам, опять изменились ????");
            return null;
        }
    }
}
