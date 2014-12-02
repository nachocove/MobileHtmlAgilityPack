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

        private static void ExtractText (string path)
        {
            if (!File.Exists (path)) {
                Console.WriteLine ("ERROR: {0} does not exist.", path);
                return;
            }
            Console.WriteLine ("[{0}]", path);

            // Load the HTML file
            var doc = new HtmlDocument ();
            using (var stream = new FileStream (path, FileMode.Open, FileAccess.Read)) {
                using (var reader = new StreamReader (stream)) {
                    doc.Load (reader);
                }
            }

            // Extract all texts
            Walk (doc.DocumentNode);
            Console.WriteLine ("");
        }

        public static void Main (string[] args)
        {
            foreach (var path in args) {
                ExtractText (path);
            }
        }
    }
}
