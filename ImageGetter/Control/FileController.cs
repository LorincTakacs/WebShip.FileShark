using ImageGetter.Model;
using System;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace ImageGetter.Handlers
{
    public class Record
    {
        public string URL { get; set; }
        public string Name { get; set; }
        public string Format { get; set; } 
                

    }
    internal class FileController
    {
        private Form1 _form;
        private string _outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileContainer"); //Default        
        public Form1 Form { get { return _form; } set { _form = value; } }
        public string OutputDirectory { get { return _outputDirectory; } set { _outputDirectory = value; } }
        public FileController(Form1 form)
        {
            this.Form = form;
        }
        public async Task DownloadFiles(DataGridView table)
        {
            foreach (DataGridViewRow row in table.Rows)
            {
                try {
                    var url = row.Cells["URL"].Value?.ToString();

                    if (!string.IsNullOrEmpty(url))
                    {
                        FileModel file = new FileModel();
                        file.Url = url;
                        file.Name = row.Cells["Név"].Value?.ToString(); ;
                        file.Format = (row.Cells["Kiterjesztés"].Value?.ToString() == "" || row.Cells["Kiterjesztés"].Value == null) ? "jpg" : row.Cells["Kiterjesztés"].Value?.ToString();

                        byte[] fileData = await file.DownloadFromUrl(row.Cells["Status"].Value, row.Cells["Leírás"].Value);
                        string originalFormat = this.GetFileFormat(fileData);
                        if (string.IsNullOrEmpty(file.Format) && originalFormat != "JPEG")
                        {
                            fileData = this.ConvertFileToTarget(fileData, "jpg");
                        }
                        else if (originalFormat != file.Format)
                        {
                            //TODO: Máksülönben van ott vmi és kell az egyéb konverzió
                            //TODO: lehet csak 2.0-ban...
                        }

                        file.Data = fileData;

                        this.SaveFile(file);
                        row.Cells["Status"].Value = "Sikeres letöltés";
                        row.Cells["Status"].Style.BackColor = Color.LightGreen;

                    } else
                    {
                        throw new Exception("Hiányzó url");
                    }

                } catch (Exception ex) {
                    row.Cells["Status"].Value = "Sikertelen letöltés";
                    row.Cells["Status"].Style.BackColor = Color.Red;
                    row.Cells["Leírás"].Value = ex.Message;
                }
            }
        }

        public List<Record> ReadCsvFromString(string content)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", // Set the correct delimiter
                HeaderValidated = null, // Ignore missing headers if necessary
                MissingFieldFound = null // Ignore missing fields
            };

            using (var reader = new StringReader(content))
            using (var csv = new CsvReader(reader, config))
            {
                return new List<Record>(csv.GetRecords<Record>());
            }
        }

        public async Task ImportFromExcel(DataGridView table)
        {
            try
            {
              FileModel readedFile = await this.ReadFile();
              if(readedFile.Format != null) {
                    switch (readedFile.Format)
                    {
                        case "xlsx":
                            //TODO: Handle xlsx
                            throw new Exception("Xlsx formátum feldolgozása még fejlesztés alatt áll!");
                            //break;

                        case "csv":
                            List<Record> data = this.ReadCsvFromString(readedFile.Content);
                            foreach(var record in data)
                            {                                
                                int rowIndex = table.Rows.Add();
                                table.Rows[rowIndex].Cells["URL"].Value = record.URL;
                                table.Rows[rowIndex].Cells["Név"].Value = record.Name;
                                table.Rows[rowIndex].Cells["Kiterjesztés"].Value = record.Format;                                
                            }
                            break;
                        default:
                           throw new Exception("Nem feldolgozható formátum");
                    }
              } else {
                    throw new Exception("Nem választott fájlt!");
              }
            } catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a fájlok importálásakor \n" + ex.Message);
            }
        }

        public async Task<FileModel> ReadFile()
        {
            FileModel f = new FileModel();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "CSV (*.csv)|*.csv|Minden fájl (*.*)|*.*";
                openFileDialog.FilterIndex = 1; //?
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    f.Path = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    f.Format = Path.GetExtension(f.Path)?.ToLower().Split('.')[1]; //Get the extension

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        f.Content = await reader.ReadToEndAsync();
                    }
                }
            }

            return f;
        }

        public void SaveFile(FileModel f)
        {
            if (f == null || f.Data.Length == 0) throw new ArgumentException("Hibás file, mentés sikertelen");
                        
            if(!Directory.Exists(OutputDirectory))
            {
                Directory.CreateDirectory(OutputDirectory);
            }            
            string path = Path.Combine(OutputDirectory, $"{f.Name}.{f.Format}");
            File.WriteAllBytes(path, f.Data);            

        }

        private byte[] ConvertFileToTarget(byte[] f, string format)
        {
            SKEncodedImageFormat finalFormat = SKEncodedImageFormat.Jpeg;
            switch (format)
            {
                case "valami":
                    finalFormat = SKEncodedImageFormat.Png;
                    break;
                default:
                    break;
            }

            using (var inputStream = new MemoryStream(f))
            using (var bitmap = SKBitmap.Decode(inputStream)) // Decode the image
            using (var outputStream = new MemoryStream())     // Prepare for output
            {
                // Encode the image as JPEG with high quality
                bitmap.Encode(outputStream, finalFormat, 100);
                return outputStream.ToArray(); // Return JPEG data as byte array
            }
        }

        private string GetFileFormat(byte[] fileMap)
        {
            //VAGY B.TERV NUGET PACKAGE HeyRed.Mime;

            if (fileMap == null || fileMap.Length < 4) return null;

            string hexSignature = BitConverter.ToString(fileMap.Take(8).ToArray()).Replace("-", "");

            if (hexSignature.StartsWith("FFD8FF"))
                return "JPEG";
            else if (hexSignature.StartsWith("89504E47"))
                return "PNG";
            else if (hexSignature.StartsWith("47494638"))
                return "GIF";
            else if (hexSignature.StartsWith("424D"))
                return "BMP";
            else if (hexSignature.StartsWith("52494646") && Encoding.ASCII.GetString(fileMap, 8, 4) == "WEBP")
                return "WEBP";
            else if (hexSignature.StartsWith("25504446"))
                return "PDF";
            else if (hexSignature.StartsWith("504B0304"))
                return "ZIP";


            return null;
        }
    }
}
