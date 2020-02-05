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
            Task t = TesteSomarDoisInteiros(3333, 3333);
            while (t.Status != TaskStatus.Faulted && t.Status != TaskStatus.RanToCompletion)
                Console.Write(".");
            Console.ReadKey();
        }

        static async Task TesteSomarDoisInteiros(int valorA, int valorB )
        {
            try
            {

                HttpClient clienteHttp = new HttpClient();
                HttpRequestMessage pergunta = new HttpRequestMessage(HttpMethod.Post, @"http://localhost:2218/Service1.svc");
                pergunta.Headers.Add("SOAPAction", @"http://tempuri.org/IService1/SomarDoisInteiros");

                StringBuilder corpo = new StringBuilder();
                corpo.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                corpo.AppendLine(@"<soap:Envelope ");
                corpo.AppendLine(@"       xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" ");
                corpo.AppendLine(@"		xmlns:xsd=""http://www.w3.org/2001/XMLSchema""");
                corpo.AppendLine(@"      xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" >");
                corpo.AppendLine(@"    <soap:Body>");
                corpo.AppendLine(@"        <SomarDoisInteiros xmlns=""http://tempuri.org/"">");
                corpo.AppendLine(@"            <a>" + valorA.ToString() + "</a>");
                corpo.AppendLine(@"            <b>" + valorB.ToString() + "</b>");
                corpo.AppendLine(@"        </SomarDoisInteiros>");
                corpo.AppendLine(@"    </soap:Body>");
                corpo.AppendLine(@"</soap:Envelope>");


                pergunta.Content = new StringContent(corpo.ToString(), Encoding.UTF8, @"text/xml");
                Console.WriteLine("////");
                Console.WriteLine(pergunta);
                Console.WriteLine("////");
                Console.WriteLine(corpo.ToString());
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
