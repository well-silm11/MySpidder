using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscaDados
{
    public class Post
    {

        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string uf { get; set; }
        public string localidade { get; set; }
        public string cep { get; set; }
        public string complemento { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public string unidade { get; set; }


        public bool erro { get; set; }
    }

    public class BuscaCnpj
    {
        public string nome { get; set; }
        public string uf { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }

        public string municipio { get; set; }
        public decimal capital_social { get; set; }

        public string status { get; set; }
    }

    public class employee
    {
        public string status { get; set; }
        public int id { get; set; }
        public Dictionary<string, string> data { get; set; }

        public string name { get; set; }
        public int age { get; set; }

        public decimal salary { get; set; }

    }
}
