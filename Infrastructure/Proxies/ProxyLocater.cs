using Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Proxies
{
    public class ProxyLocater : IProxyLocater
    {
        public ProxyLocater(IMemoryCache memoryCache)
        {
            //Pipedrive = new Lazy<PipedrivepRroxy>(() => new PipedrivepRroxy(memoryCache));
            // Elastic = new Lazy<ElasticDBProxy>(() => new ElasticDBProxy(memoryCache));
          //  Thread = new Lazy<ThreadProxy>(() => new ThreadProxy());
            //  SMTP = new Lazy<SMTPProxy>(() => new SMTPProxy(memoryCache));
            Http = new Lazy<HttpClientProxy>(() => new HttpClientProxy());
          // Excel = new Lazy<ExcelProxy>(() => new ExcelProxy());
            // SendGrid = new Lazy<SendGridProxy>(() => new SendGridProxy(memoryCache));
            // S3 = new Lazy<S3Proxy>(() => new S3Proxy(memoryCache));
           // Image = new Lazy<ImageProxy>(() => new ImageProxy());
            //  Translation = new Lazy<TranslationProxy>(() => new TranslationProxy());
        }


        //public Lazy<ImageProxy> Image { get; set; }
        //   public Lazy<S3Proxy> S3 { get; set; }
        //   public Lazy<TranslationProxy> Translation { get; set; }
        public Lazy<HttpClientProxy> Http { get; set; }
        //  public Lazy<ElasticDBProxy> Elastic { get; set; }
        //  public Lazy<PipedrivepRroxy> Pipedrive { get; set; }
        //  public Lazy<SMTPProxy> SMTP { get; set; }
      //  public Lazy<ThreadProxy> Thread { get; set; }
        //public Lazy<ExcelProxy> Excel { get; set; }
        //   public Lazy<SendGridProxy> SendGrid { get; set; }
    }
}
