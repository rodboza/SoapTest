using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pressione uma tecla para iniciar....");
            Console.ReadKey();
            Task t = teste();
            while (t.Status != TaskStatus.Faulted && t.Status != TaskStatus.RanToCompletion)
                Console.Write(".");
            Console.ReadKey();
        }

        /*
        static async Task Main(string[] args)
        {
            Console.WriteLine("Pressione uma tecla para iniciar....");
            Console.ReadKey();

            HttpClient clienteHttp = new HttpClient();

            HttpRequestMessage pergunta = new HttpRequestMessage(HttpMethod.Post, @"http://localhost:2218/Service1.svc");

            HttpResponseMessage respota = await clienteHttp.SendAsync(pergunta);

            Console.WriteLine(respota);
            Console.ReadKey();
        }
        */

        static async Task teste()
        {
            try
            {

                HttpClient clienteHttp = new HttpClient();
                HttpRequestMessage pergunta = new HttpRequestMessage(HttpMethod.Post, @"http://localhost:2218/Service1.svc");
                pergunta.Headers.Add("SOAPAction", @"http://tempuri.org/IService1/SomarDoisInteiros");
                
                String corpo = 
@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope 
		xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
		xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
		xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" >
<soap:Body>
	<SomarDoisInteiros xmlns=""http://tempuri.org/"">
      <a>555</a>
      <b>222</b>
    </SomarDoisInteiros>
</soap:Body>
</soap:Envelope>";


                pergunta.Content = new StringContent(corpo, Encoding.UTF8, @"text/xml");
                Console.WriteLine("////");
                Console.WriteLine(pergunta);
                Console.WriteLine("////");
                Console.WriteLine(corpo);
                Console.WriteLine("////");
                HttpResponseMessage respota = await clienteHttp.SendAsync(pergunta);

                Console.WriteLine("");
                Console.WriteLine("////");
                Console.WriteLine(respota);
                Console.WriteLine("////");
                string textoResposta = await respota.Content.ReadAsStringAsync();
                Console.WriteLine(textoResposta);
                Console.WriteLine("////");

            }
            catch (Exception e)
            {

                Console.WriteLine("");
                Console.WriteLine("ERRO");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
