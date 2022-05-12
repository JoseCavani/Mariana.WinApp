using Newtonsoft.Json;
using System.IO;
using SautinSoft.Document;
using Mariana.Dominio.ModuloTeste;
using System;

namespace Marina.Infra.Arquivos
{
    public class SerializadorDadosEmJsonDotnet : ISerializador
    {
        private string arquivo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Dados.json";

        public DataContext CarregarDadosDoArquivo()
        {
            if (File.Exists(arquivo) == false)
                return new DataContext();

            string arquivoJson = File.ReadAllText(arquivo);

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;

            return JsonConvert.DeserializeObject<DataContext>(arquivoJson, settings);
        }

        public void GravarDadosEmArquivo(DataContext dados)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            
            settings.Formatting = Formatting.Indented;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;

            string arquivoJson = JsonConvert.SerializeObject(dados, settings);

            File.WriteAllText(arquivo, arquivoJson);



        }
    }
}
