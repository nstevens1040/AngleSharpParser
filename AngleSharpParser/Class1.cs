namespace Angle
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
    // 
    public class Sharp : IDisposable
    {
        public Sharp()
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
        ~Sharp() => Dispose(false);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                context.Dispose();
            }
            _disposed = true;
        }
        public IBrowsingContext context;
        public IDocument GetDomDocument(string HtmlString)
        {
            Task<IDocument> doc = Task.Factory.StartNew(async () =>
            {
                context = BrowsingContext.New(Configuration.Default);
                IDocument d = await context.OpenAsync(req => req.Content(HtmlString));
                return d;
            }).Result;
            return doc.Result;
        }
    }
    public class SharpDom
    {
        public static IDocument ParseFromString(string html_string)
        {
            IDocument document = null;
            using(Sharp object_ = new Sharp())
            {
                document = object_.GetDomDocument(html_string);
            }
            return document;
        }
    }
}