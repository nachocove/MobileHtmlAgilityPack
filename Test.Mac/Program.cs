using System;
using System.IO;
using HtmlAgilityPack;

namespace Test.Mac
{
    class MainClass
    {
        private static void Walk (HtmlNode node)
        {
            switch (node.NodeType) {
            case HtmlNodeType.Text:
                if (!String.IsNullOrWhiteSpace (node.InnerText)) {
                    Console.WriteLine ("<{0}> {1}", node.ParentNode.Name, node.InnerText);
                }
                break;
            default:
                foreach (var subnode in node.ChildNodes) {
                    Walk (subnode);
                }
                break;
            }
        }

        private static bool ExtractText (string path)
        {
            if (!File.Exists (path)) {
                Console.WriteLine ("ERROR: {0} does not exist.", path);
                return false;
            }
            Console.WriteLine ("[{0}]", path);

            try {
                // Load the HTML file
                var doc = new HtmlDocument ();
                using (var stream = new FileStream (path, FileMode.Open, FileAccess.Read)) {
                    using (var reader = new StreamReader (stream)) {
                        doc.Load (reader);
                    }
                }
                Walk (doc.DocumentNode); // extract all texts
                Console.WriteLine ("");
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public static void Main (string[] args)
        {
            int numProcessed = 0, numSucceeded = 0;
            foreach (var path in args) {
                numProcessed += 1;
                if (ExtractText (path)) {
                    numSucceeded += 1;
                }
            }
            Console.WriteLine ("{0} processed. {1} succeeded", numProcessed, numSucceeded);
            if (numProcessed != numSucceeded) {
                Environment.Exit (1);
            }
        }
    }
}
