using Newtonsoft.Json;
using System.IO;
using SautinSoft.Document;
using Mariana.Dominio.ModuloTeste;

namespace Marina.Infra.Arquivos
{
    public class SerializadorDadosEmJsonDotnet : ISerializador
    {
        private const string arquivo = @"D:\visual studio files\FilesJunk\dados.json";

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

            DocumentCore dc = new DocumentCore();

            foreach (var item in dados.Teste)
            {
                dc.Content.End.Insert(item.ToString());
            }
          
            string filePath = @"D:\visual studio files\FilesJunk\dados.pdf";

            dc.Save(filePath, new PdfSaveOptions()
            {
                Compliance = PdfCompliance.PDF_A1a,
                PreserveFormFields = true
            });



        }
    }
}
