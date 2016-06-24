using System.IO;
using Newtonsoft.Json;
using QSI.Services.Spec;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services
{
    [QSI.Services.ServiceBehavior]
    public class DocumentService : IDocumentService
    {
        PDFGenerator _pdfGenerator = new PDFGenerator();
        public DocumentService()
        {
            if (_pdfGenerator == null)
                _pdfGenerator = new PDFGenerator();
        }

        public DocumentService( PDFGenerator pdfGenerator)
        {
            _pdfGenerator = pdfGenerator;
        }

        public DocumentResponse GenerateSearchPDF(string searchString)
        {

            DocumentResponse response = new DocumentResponse();

            var bytes = _pdfGenerator.GenerateReport(searchString);

            response.DocumentData = JsonConvert.SerializeObject(bytes);

            return response;



        }

        public void GenerateSearchPDFFile(string searchString,string name,string path)
        {
            try
            {
                DocumentResponse response = new DocumentResponse();

                var bytes = _pdfGenerator.GenerateReport(searchString);
                var serverpath = ConfigurationManager.AppSettings["WorkingFolder"].ToString();
                serverpath = serverpath +@"\"+ path;

                CreateDirectory(serverpath);
                var filepath = serverpath + @"\" + name + ".pdf";
                System.IO.File.WriteAllBytes(filepath, bytes);
                throw new WebFaultException<string>("PDF Generated Succesfully", System.Net.HttpStatusCode.OK);
            }
           catch (Exception ex)
            {
                if(ex.Message.ToString()=="OK")
                    throw new WebFaultException<string>("PDF Generated Succesfully", System.Net.HttpStatusCode.OK);
                else if(ex.Message.ToString()=="Bad Request")
                    throw new WebFaultException<string>("Bad Request", System.Net.HttpStatusCode.BadRequest);
               else
                  throw new WebFaultException<string>("PDF Gneration Failed", System.Net.HttpStatusCode.InternalServerError);
                      
            }
        }
 
        private void CreateDirectory(string path)
        {
           try 
	{	        
		  if (Directory.Exists(path)) 
            {
               
                return;
            }

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(path);
	}
	catch (Exception ex)
	{

        throw ex;
	}
        }

    }
}
