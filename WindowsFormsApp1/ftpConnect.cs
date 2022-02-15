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
        private FtpWebResponse response;
        private byte[] fileData;
        private string Loggin = "";
        private string Paswword = "";
        private Stream reqStream;
        private string ftpString = "";
        private string dbPathIn = "C:\\AnnalandBD\\ALDB.db";
        private string homePath = "C:\\AnnalandBD";
        private string path = @"C:\AnnalandBD\ALDB.db";
        public bool stateOfConection = false;

        public ftpConnect(string log, string pas, string ftpstr) {
            Loggin = log;
            Paswword = pas;
            ftpString = ftpstr;
        }
        public void LoadDB() {

            if (!Directory.Exists(homePath)) { DirectoryInfo di = Directory.CreateDirectory(homePath); }
            if (!File.Exists(dbPathIn))
            {
                try
                {
                    request = (FtpWebRequest)WebRequest.Create(ftpString + "ALDB.db");
                    request.Credentials = new NetworkCredential(Loggin, Paswword);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    response = (FtpWebResponse)request.GetResponse();

                    using (reqStream = response.GetResponseStream())
                    using (Stream fileStream = File.Create(path)){ reqStream.CopyTo(fileStream);}
                    stateOfConection = true;
                    response.Close();
                }
                catch (WebException ex)
                {
                    stateOfConection = false;
                    string status = ((FtpWebResponse)ex.Response).StatusDescription;
                    MessageBox.Show(status);

                }
            }
        }
        public void UploadFileDB()
        {
            try
            {

                request = (FtpWebRequest)WebRequest.Create(ftpString + "ALDB_OLD.db");
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(Loggin, Paswword);
                
                response = (FtpWebResponse)request.GetResponse();
                response.Close();

                request = (FtpWebRequest)WebRequest.Create(ftpString + "ALDB.db");
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.Rename;
                request.Credentials = new NetworkCredential(Loggin, Paswword);
                request.RenameTo = "ALDB_OLD.db";
                response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (WebException ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

            try
            {
                request = (FtpWebRequest)WebRequest.Create(ftpString + "ALDB.db");
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(Loggin, Paswword);

                fileData = File.ReadAllBytes(dbPathIn);
                request.ContentLength = fileData.Length;
                reqStream = request.GetRequestStream();
                reqStream.Write(fileData, 0, fileData.Length);
                reqStream.Close();
                response.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            
        }
        public void UploadFilesIMG(string[] HadImgPath_and_Name, string[] ImgNamePaths, string[] ImgName) {

            request = (FtpWebRequest)WebRequest.Create(ftpString + "img/HadImg/" + HadImgPath_and_Name[1]);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(Loggin, Paswword);
            fileData = File.ReadAllBytes(HadImgPath_and_Name[0]);
            request.ContentLength = fileData.Length;
            reqStream = request.GetRequestStream();
            reqStream.Write(fileData, 0, fileData.Length);
            reqStream.Close();
            

            for (int i = 0; i < ImgNamePaths.Length; i++)
            {
                request = (FtpWebRequest)WebRequest.Create(ftpString + "img/imges/" + ImgName[i]);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(Loggin, Paswword);
                fileData = File.ReadAllBytes(ImgNamePaths[i]);
                request.ContentLength = fileData.Length;
                reqStream = request.GetRequestStream();
                reqStream.Write(fileData, 0, fileData.Length);
                reqStream.Close();

            }
            response.Close();
        }

    }
}
