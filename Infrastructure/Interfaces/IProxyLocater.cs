using Infrastructure.Proxies;
using System;

namespace Infrastructure.Interfaces
{/// <summary>
 /// Use IProxyLocater to locate all proxy you need , once you need one or two ... create instance
 /// of locate rether than create one for each object locater user lazt initialization mean create
 /// object on need as save the refrence rether then each one need proxy create instance one object
 /// handle all </summary> <createdby>Jebril mohammad</createdby>
    public interface IProxyLocater
    {
        //Lazy<ImageProxy> Image { get; set; }
       // Lazy<S3Proxy> S3 { get; set; }
       // Lazy<TranslationProxy> Translation { get; set; }
       // Lazy<ElasticDBProxy> Elastic { get; set; }
       // Lazy<PipedrivepRroxy> Pipedrive { get; set; }
        //Lazy<ThreadProxy> Thread { get; set; }
       // Lazy<SMTPProxy> SMTP { get; set; }
        Lazy<HttpClientProxy> Http { get; set; }
       // Lazy<ExcelProxy> Excel { get; set; }
      //  Lazy<SendGridProxy> SendGrid { get; set; }
    }
}