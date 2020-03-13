using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Text;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;

namespace BuscaDados
{
    class Program
    {
        static void Main(string[] args)
        {

            string op = "";

            while (op != "exit")
            {
                Console.Clear();
                Console.WriteLine("\nBem vindo!\n Digite a Opção desejada:\n 1)Busca Cep\n 2)Busca Cnpj\n 3)Criar funcionário\n Ou 'Exit' para sair...\n ");
                op = Console.ReadLine();
                op = op.ToLower();


                switch (op)
                {
                    case "1":

                        string cep;

                        Console.WriteLine("Digite o CEP:\n");
                        cep = Console.ReadLine();
                        cep = cep.Replace("-", "");
                        try
                        {


                            if ((int)cep.Length != 8 && (int)cep.Length != 7)
                            {

                                Console.WriteLine("\nCep inválido!\nAnalise o número e tente novamente!\nAperte qualquer tecla para retornar ao menu...\n");
                                Console.ReadKey();
                            }
                            else
                            {

                                cep = cep.PadLeft(8, '0');

                                string tiposr;
                                Console.WriteLine("Blz, de qual forma você gostaria que a busca fosse feita? Digite\n1)Json\n2)XML\n3)Tanto faz...\nOu 'EXIT' para retornar ao menu inicial\n");
                                tiposr = Console.ReadLine();

                                tiposr= tiposr.ToUpper();
                                if (tiposr == "3")
                                {
                                    tiposr = "1";
                                }
                                while (tiposr!="EXIT")
                                {


                                    switch (tiposr)
                                    {

                                        case "1":
                                            Console.WriteLine("\nBlz, aguarde um momento porque você não nasceu de 7 meses...\n");

                                            var linkrequest = "https://viacep.com.br/ws/" + cep + "/json/";
                                            var requisicaoWeb = WebRequest.CreateHttp(linkrequest);
                                            requisicaoWeb.Method = "GET";
                                            requisicaoWeb.UserAgent = "RequisicaoWebDemo";


                                            using (var resposta = requisicaoWeb.GetResponse())
                                            {
                                                using (var streamDados = resposta.GetResponseStream())
                                                {

                                                    StreamReader reader = new StreamReader(streamDados);
                                                    object objResponse = reader.ReadToEnd();
                                                    var post = JsonConvert.DeserializeObject<Post>(objResponse.ToString());

                                                    if (post.erro == false)
                                                    {

                                                        string caminhos = @"D:\API\Cep\json";
                                                        StreamWriter caminho = null;
                                                        //salvando 
                                                        using (caminho = new StreamWriter(@"D:\API\Cep" + "\\" + cep + ".txt"))
                                                        {

                                                            caminho.WriteLine(objResponse.ToString());

                                                        }



                                                        Console.WriteLine("\n\n Aquivo Salvo em:\n" + caminhos + "\n\n");
                                                        //tratando o resultado json com a classe post 

                                                        Console.WriteLine("\n=======Reusltado=======\n\n" + "  Cep:" + post.cep + "\n" + "  Rua:" + post.logradouro + "  Bairro:" + post.bairro + "\n" + "  Cidade:" + post.localidade + "  Estado:" + post.uf);
                                                        Console.ReadKey();
                                                         tiposr = "EXIT";
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nCep inválido!\nAnalise o número e tente novamente!\nAperte qualquer tecla para retornar ao menu...\n");
                                                        Console.ReadKey();
                                                        tiposr = "EXIT";
                                                    }



                                                }
                                            }

                                            break;

                                        case "2":
                                            Console.WriteLine("\nBlz, aguarde um momento porque você não nasceu de 7 meses...\n");

                                            var linkrequestx = "https://viacep.com.br/ws/" + cep + "/xml/";
                                            var requisicaoWebx = WebRequest.CreateHttp(linkrequestx);
                                            requisicaoWebx.Method = "GET";
                                            requisicaoWebx.UserAgent = "RequisicaoWebDemo";


                                            using (var resposta = requisicaoWebx.GetResponse())
                                            {
                                                using (var streamDados = resposta.GetResponseStream())
                                                {

                                                    StreamReader reader = new StreamReader(streamDados);
                                                    object objResponse = reader.ReadToEnd();
                                                    //var post = JsonConvert.DeserializeObject<Post>(objResponse.ToString());

                                                    string caminhos = @"D:\API\Cep\xml";
                                                    StreamWriter caminho = null;
                                                    string cm = @"D:\API\Cep\xml" + "\\" + cep + ".txt";
                                                    //salvando 
                                                    using (caminho = new StreamWriter(@"D:\API\Cep\xml" + "\\" + cep + ".xml"))
                                                    {

                                                        caminho.WriteLine(objResponse.ToString());

                                                    }
                                                    Post post = new Post();

                                                    XDocument documentoXML = XDocument.Load(linkrequestx);

                                                    Console.WriteLine("\n\n Aquivo Salvo em:\n" + caminhos + "\n\n");


                                                    post.logradouro = documentoXML.Descendants().Elements("logradouro").First().Value;
                                                    post.cep = documentoXML.Descendants().Elements("cep").First().Value;
                                                    post.bairro = documentoXML.Descendants().Elements("bairro").First().Value;
                                                    post.localidade = documentoXML.Descendants().Elements("localidade").First().Value;
                                                    post.uf = documentoXML.Descendants().Elements("uf").First().Value;

                                                    Console.WriteLine("\n=======Reusltado=======\n\n" + "  Cep:" + post.cep + "\n" + "  Rua:" + post.logradouro + "  Bairro:" + post.bairro + "\n" + "  Cidade:" + post.localidade + "  Estado:" + post.uf);
                                                    Console.ReadKey();
                                                    tiposr = "EXIT";

                                                }
                                            }


                                            break;

                                        case "EXIT":

                                            break;
                                        default:
                                            Console.WriteLine("\nOpção inválida!\nSelecione uma opção válida ou 'EXIT' para retornar ao menu...\n");
                                            tiposr=Console.ReadLine();
                                            break;

                                    }



                                }


                            }



                        }
                        catch (Exception ex)

                        {
                            Console.WriteLine("\nErro tente novamente mais tarde, ou analise se seu número de CEP está correto...\nAperte qualquer tecla para retornar ao menu...\n");
                            Console.ReadKey();
                        }


                        break;
                    case "2":

                        string cnpj;
                        Console.WriteLine("Digite o CNPJ:\n");
                        cnpj = Console.ReadLine();
                        cnpj = cnpj.Replace("-", "").Replace("/", "").Replace(".", "");
                        try
                        {
                            if ((int)cnpj.Length != 14)
                            {

                                Console.WriteLine("\n CNPJ inválido!\nAnalise o número e tente novamente!\nAperte Enter para retornar ao menu...\n");
                                Console.ReadKey();
                            }
                            else
                            {

                                var linkrequest = "https://www.receitaws.com.br/v1/cnpj/" + cnpj;

                                var requisicaoweb = WebRequest.CreateHttp(linkrequest);

                                requisicaoweb.Method = "GET";

                                requisicaoweb.UserAgent = "Teste";


                                using (var resposta = requisicaoweb.GetResponse())
                                {
                                    using (var streamDados = resposta.GetResponseStream())
                                    {

                                        StreamReader reader = new StreamReader(streamDados);
                                        object objResponse = reader.ReadToEnd();
                                        var post = JsonConvert.DeserializeObject<BuscaCnpj>(objResponse.ToString());
                                        if (post.status != "ERROR")
                                        {


                                            //salvando o bagui
                                            StreamWriter caminho = null;
                                            string caminhos = @"D:\API\Cnpj";
                                            using (caminho = new StreamWriter(@"D:\API\Cnpj" + "\\" + cnpj + ".txt"))
                                            {

                                                caminho.WriteLine(objResponse.ToString());

                                            }
                                            Console.WriteLine("\n\n Aquivo Salvo em:\n" + caminhos + "\n\n");

                                            Console.WriteLine("\n=======Reusltado=======\n" + " Nome:" + post.nome + "  Email:" + post.email + "\n" +
                                                " Telefone:" + post.telefone + "  Capital Social:" + post.capital_social + "\n" + "  Cidade:" + post.municipio + "  Estado:" + post.uf);
                                            Console.ReadLine();

                                        }
                                        else
                                        {

                                            Console.WriteLine("\n CNPJ inválido!\nAnalise o número e tente novamente!\nAperte Enter para retornar ao menu...\n");
                                            Console.ReadKey();
                                        }
                                    }
                                }




                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nErro tente novamente mais tarde!\nAperte Enter para retornar ao menu...\n");
                            Console.ReadLine();
                        }
                        break;
                    case "3":

                        var link = "http://dummy.restapiexample.com/api/v1/create";
                        employee employee = new employee();
                        Console.WriteLine("====// Dados para realizar cadastro de funcionários //====:\n");

                        Console.WriteLine("Digite o Nome do funcionário:\n");
                        employee.name = Console.ReadLine();

                        Console.WriteLine("Agora a idade:\n");
                        employee.age = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("E o salário:\n");
                        employee.salary = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("\nObrigado!\n\n Agora aguarde um momento...");

                        try
                        {
                            var post = JsonConvert.SerializeObject(employee);
                            var dados = Encoding.UTF8.GetBytes(post.ToString());
                            var requisicaoWeb = WebRequest.CreateHttp(link);


                            requisicaoWeb.Method = "POST";
                            requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                            requisicaoWeb.ContentLength = dados.Length;
                            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

                            using (var stream = requisicaoWeb.GetRequestStream())
                            {
                                stream.Write(dados, 0, dados.Length);

                            }

                            using (var resposta = requisicaoWeb.GetResponse())
                            {
                                using (var streamDados = resposta.GetResponseStream())
                                {
                                    StreamReader reader = new StreamReader(streamDados);
                                    object objResponse = reader.ReadToEnd();
                                    var posta = JsonConvert.DeserializeObject<employee>(objResponse.ToString());
                                    var datapost = JsonConvert.SerializeObject(posta.data);
                                    var empregado = JsonConvert.DeserializeObject<employee>(datapost);


                                    string caminhos = @"D:\API\func";
                                    StreamWriter caminho = null;
                                    using (caminho = new StreamWriter(@"D:\API\func" + "\\" + empregado.id + ".txt"))
                                    {

                                        caminho.WriteLine(objResponse.ToString());

                                    }

                                    Console.WriteLine("\n\n Aquivo Salvo em:\n" + caminhos + "\n\n");

                                    Console.WriteLine("\n=======Reusltado=======\n" + "Funcionário criado com sucesso!\n Segue abaxio os dados refente a criação:\n" + "Nome: " + empregado.name + " ID: " + empregado.id);
                                    Console.WriteLine("Caso queira conferir no site acesse:  http://dummy.restapiexample.com/api/v1/employee/" + empregado.id + "\n");
                                }
                            }
                            Console.ReadKey();



                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nErro tente novamente mais tarde!\nAperte qualquer tecla para retornar ao menu...\n");
                            Console.ReadKey();
                        }

                        break;
                    case "exit":

                        break;
                    default:
                        Console.WriteLine("\nOpção inválida!\nAperte qualquer tecla para retornar ao menu...\n");
                        Console.ReadKey();
                        break;

                }

            }


        }
        static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\-]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}