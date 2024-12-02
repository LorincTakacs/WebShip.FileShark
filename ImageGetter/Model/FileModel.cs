using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageGetter.Model
{
    public class FileModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Path {  get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; }
        public byte[] Data { get; set; }
        public string Content { get; set; }

        public async Task<byte[]> DownloadFromUrl(object statusCell=null, object descCell=null)
        {
            using(HttpClient client = new HttpClient())
            {
                var data = await client.GetByteArrayAsync(Url);
                return data;                
            }
        }
        
        //TODO: megkell szüntessema filekoat, vagy maradhatnak???
    }
}
