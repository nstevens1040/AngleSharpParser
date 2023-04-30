namespace AngleSharp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp;
    using AngleSharp.Dom;
    using System.Reflection;
    using AngleSharpParser.Properties;
    // test github fake change
    public class Parser
    {
        public Parser()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
        }
        public IDocument GetDomDocument(string HtmlString)
        {
            Task<IDocument> doc = Task.Factory.StartNew(async () =>
            {
                IBrowsingContext context = BrowsingContext.New(Configuration.Default);
                IDocument d = await context.OpenAsync(req => req.Content(HtmlString));
                return d;
            }).Result;
            return doc.Result;
        }
    }
}