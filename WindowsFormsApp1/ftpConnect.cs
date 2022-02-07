using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class ftpConnect
    {
        private FtpWebRequest request;
        private byte[] fileData;
        private string Loggin = "sa246943_alws";
        private string Paswword = "mNC6Eix648hD";
        private Stream reqStream;
        private string ftpString = "ftp://sa246943.ftp.tools/";
        private string dbPath = "C:\\AnnalandBD\\ALDB.db";

        public ftpConnect(string log, string pas, string ftpstr) {
            Loggin = log;
            Paswword = pas;
            ftpString = ftpstr;
        }

        public void UploadFileDB(string dbPath)
        {
            try
            {
                request = (FtpWebRequest)WebRequest.Create(ftpString + "ALDB.db");
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(Loggin, Paswword);
            }
            catch (WebException ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

            try
            {
                fileData = File.ReadAllBytes(dbPath);
                request.ContentLength = fileData.Length;
                reqStream = request.GetRequestStream();
                reqStream.Write(fileData, 0, fileData.Length);
                reqStream.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            
        }

    }
}
