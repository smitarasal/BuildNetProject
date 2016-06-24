using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services
{
   public  class PDFGenerator
    {
       public virtual byte[] GenerateReport(string searchResult)
        {
     
            Document document = new Document(PageSize.A4, 30f, 0f, 10f, 10f);
            Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = null;


                document.Open();

               

                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.3f, 0.7f });

                //Separater Line
                color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                DrawLine(writer, 20f, document.Top - 69f, document.PageSize.Width - 25f, document.Top - 69f, color);
                DrawLine(writer, 20f, document.Top - 70f, document.PageSize.Width - 25f, document.Top - 70f, color);
                document.Add(table);

                table = new PdfPTable(2);
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(new float[] { 0.3f, 1f });
                table.SpacingBefore = 20f;

                //Document heading
                cell = PhraseCell(new Phrase("Search Result", FontFactory.GetFont("Arial", 12, Font.UNDERLINE, Color.BLACK)), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                table.AddCell(cell);
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 30f;
                table.AddCell(cell);
                document.Add(table);




                //Horizontal Table
                //one attribute
                //string jsonArrayAsString = "[{\"alias\":{\"NAME\":\"Name\",\"RDPREFIX\":\"RD Prefix\",\"ADDR_NUMBE\":\"Address Number\",\"RDNAME\":\"RD Name\",\"RDSUFFIX\":\"RD SUFFIX\",\"CITY\":\"CITY\",\"ZIP\":\"ZIP\",\"PHONE\":\"PHONE\",\"CONTACT\":\"CONTACT\",\"XY_SOURCE\":\"XY SOURCE\",\"XY_ISSUES\":\"XY ISSUES\",\"ATT_SOURCE\":\"ATT SOURCE\",\"ATT_ISSUES\":\"ATT ISSUES\",\"ADDNAME\":\"ADD NAME\"},\"attributes\":[{\"NAME\":\"OWENSBORO-DAVIESS COUNTY REGIONAL AIRPORT\",\"RDPREFIX\":\" \",\"ADDR_NUMBE\":\"2200\",\"RDNAME\":\"AIRPORT\",\"RDSUFFIX\":\"RD\",\"CITY\":\"OWENSBORO\",\"ZIP\":\"42301\",\"PHONE\":\"270-685-4179\",\"CONTACT\":\"BILL SMITH\",\"XY_SOURCE\":\"FSA FILES AND KYTC ROAD CENTERLINES\",\"XY_ISSUES\":\" \",\"ATT_SOURCE\":\"TIM BRADSHAW\",\"ATT_ISSUES\":\" \",\"ADDNAME\":\"GRADD\"}]}]";

                //two attributes
                //string jsonArrayAsString = "[{\"alias\":{\"NAME\":\"Name\",\"RDPREFIX\":\"RD Prefix\",\"ADDR_NUMBE\":\"Address Number\",\"RDNAME\":\"RD Name\",\"RDSUFFIX\":\"RD SUFFIX\",\"CITY\":\"CITY\",\"ZIP\":\"ZIP\",\"PHONE\":\"PHONE\",\"CONTACT\":\"CONTACT\",\"XY_SOURCE\":\"XY SOURCE\",\"XY_ISSUES\":\"XY ISSUES\",\"ATT_SOURCE\":\"ATT SOURCE\",\"ATT_ISSUES\":\"ATT ISSUES\",\"ADDNAME\":\"ADD NAME\"},\"attributes\":[{\"NAME\":\"OWENSBORO-DAVIESS COUNTY REGIONAL AIRPORT\",\"RDPREFIX\":\" \",\"ADDR_NUMBE\":\"2200\",\"RDNAME\":\"AIRPORT\",\"RDSUFFIX\":\"RD\",\"CITY\":\"OWENSBORO\",\"ZIP\":\"42301\",\"PHONE\":\"270-685-4179\",\"CONTACT\":\"BILL SMITH\",\"XY_SOURCE\":\"FSA FILES AND KYTC ROAD CENTERLINES\",\"XY_ISSUES\":\" \",\"ATT_SOURCE\":\"TIM BRADSHAW\",\"ATT_ISSUES\":\" \",\"ADDNAME\":\"GRADD\"},{\"NAME\":\"ABC\",\"RDPREFIX\":\" \",\"ADDR_NUMBE\":\"2200\",\"RDNAME\":\"AIRPORT\",\"RDSUFFIX\":\"RD\",\"CITY\":\"OWENSBORO\",\"ZIP\":\"42301\",\"PHONE\":\"270-685-4179\",\"CONTACT\":\"BILL SMITH\",\"XY_SOURCE\":\"FSA FILES AND KYTC ROAD CENTERLINES\",\"XY_ISSUES\":\" \",\"ATT_SOURCE\":\"TIM BRADSHAW\",\"ATT_ISSUES\":\" \",\"ADDNAME\":\"GRADD\"}]}]";

                //>14 columns
                //string jsonArrayAsString = "[{\"alias\":{\"NAME\":\"Name\",\"ExtraCol\":\"ExtraCol\",\"RDPREFIX\":\"RD Prefix\",\"ADDR_NUMBE\":\"Address Number\",\"RDNAME\":\"RD Name\",\"RDSUFFIX\":\"RD SUFFIX\",\"CITY\":\"CITY\",\"ZIP\":\"ZIP\",\"PHONE\":\"PHONE\",\"CONTACT\":\"CONTACT\",\"XY_SOURCE\":\"XY SOURCE\",\"XY_ISSUES\":\"XY ISSUES\",\"ATT_SOURCE\":\"ATT SOURCE\",\"ATT_ISSUES\":\"ATT ISSUES\",\"ADDNAME\":\"ADD NAME\"},\"attributes\":[{\"NAME\":\"OWENSBORO-DAVIESS COUNTY REGIONAL AIRPORT\",\"ExtraCol\":\"ExtraCol\",\"RDPREFIX\":\" \",\"ADDR_NUMBE\":\"2200\",\"RDNAME\":\"AIRPORT\",\"RDSUFFIX\":\"RD\",\"CITY\":\"OWENSBORO\",\"ZIP\":\"42301\",\"PHONE\":\"270-685-4179\",\"CONTACT\":\"BILL SMITH\",\"XY_SOURCE\":\"FSA FILES AND KYTC ROAD CENTERLINES\",\"XY_ISSUES\":\" \",\"ATT_SOURCE\":\"TIM BRADSHAW\",\"ATT_ISSUES\":\" \",\"ADDNAME\":\"GRADD\"}]}]";

                //<14 columns
                //string jsonArrayAsString = "[{\"alias\":{\"RDNAME\":\"RD Name\",\"RDSUFFIX\":\"RD SUFFIX\",\"CITY\":\"CITY\",\"ZIP\":\"ZIP\",\"PHONE\":\"PHONE\",\"CONTACT\":\"CONTACT\",\"XY_SOURCE\":\"XY SOURCE\",\"XY_ISSUES\":\"XY ISSUES\",\"ATT_SOURCE\":\"ATT SOURCE\",\"ATT_ISSUES\":\"ATT ISSUES\",\"ADDNAME\":\"ADD NAME\"},\"attributes\":[{\"RDNAME\":\"AIRPORT\",\"RDSUFFIX\":\"RD\",\"CITY\":\"OWENSBORO\",\"ZIP\":\"42301\",\"PHONE\":\"270-685-4179\",\"CONTACT\":\"BILL SMITH\",\"XY_SOURCE\":\"FSA FILES AND KYTC ROAD CENTERLINES\",\"XY_ISSUES\":\" \",\"ATT_SOURCE\":\"TIM BRADSHAW\",\"ATT_ISSUES\":\" \",\"ADDNAME\":\"GRADD\"}, {\"RDNAME\":\"AIRPORT\",\"RDSUFFIX\":\"RD\",\"CITY\":\"OWENSBORO\",\"ZIP\":\"42301\",\"PHONE\":\"270-685-4179\",\"CONTACT\":\"BILL SMITH\",\"XY_SOURCE\":\"FSA FILES AND KYTC ROAD CENTERLINES\",\"XY_ISSUES\":\" \",\"ATT_SOURCE\":\"TIM BRADSHAW\",\"ATT_ISSUES\":\" \",\"ADDNAME\":\"ABC\"}]}]";

                JArray jsonArray = JArray.Parse(searchResult);

                foreach (JObject content in jsonArray.Children<JObject>()["alias"])
                {
                    var columnCount = content.Count;
                    if (columnCount > 10)
                        columnCount = 10;

                    //columnCount = 10;
                    table = new PdfPTable(columnCount);
                    table.TotalWidth = 540f;
                    table.LockedWidth = true;
                    table.SpacingBefore = 0f;
                    table.SpacingAfter = 10f;
                    table.HorizontalAlignment = Element.ALIGN_LEFT;

                    foreach (JProperty prop in content.Properties())
                    {
                        if (columnCount > 0)
                        {
                            var name = prop.Name;
                            var value = prop.Value;
                            string value2 = Convert.ToString(prop.Value);

                            table.AddCell(PhraseCell(new Phrase(Convert.ToString(prop.Value), FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));

                            columnCount--;
                        }
                        else
                            break;
                    }
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 10f;
                    cell.Border = 1;
                    table.AddCell(cell);
                    document.Add(table);
                }


                foreach (JArray content in jsonArray.Children<JObject>()["attributes"])
                {
                    foreach (JObject props in content.Children<JObject>())
                    {
                        var columnCount = props.Count;
                        if (columnCount > 10)
                            columnCount = 10;

                        //columnCount = 10;
                        table = new PdfPTable(columnCount);
                        table.TotalWidth = 540f;
                        table.LockedWidth = true;
                        table.SpacingBefore = 10f;
                        table.SpacingAfter = 10f;
                        table.HorizontalAlignment = Element.ALIGN_LEFT;

                        foreach (JProperty prop in props.Properties())
                        {
                            if (columnCount > 0)
                            {
                                var name = prop.Name;
                                var value = prop.Value;

                                table.AddCell(PhraseCell(new Phrase(Convert.ToString(prop.Value), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

                                columnCount--;
                            }
                            else
                                break;
                        }

                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 10f;
                        cell.Border = 1;
                        table.AddCell(cell);
                        document.Add(table);
                    }
                }




                document.Close();
               
                byte[] bytes = memoryStream.ToArray();
                //System.IO.File.WriteAllBytes(@"e:\TestOut5.pdf", bytes);
                memoryStream.Close();
                return bytes;

               
              
            }
        }

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = Color.WHITE;
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 0f;
            return cell;
        }
        private static PdfPCell ImageCell(string path, float scale, int align)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("HttpContext.Current.Server.MapPath(path)");
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = Color.WHITE;
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 0f;
            return cell;
        }

    }
}
