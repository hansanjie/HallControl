using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace MainApp.Helper
{
    public class FTPHelpr
    {
        private string _ip;
        private int _port;
        System.Net.FtpWebRequest _request;
        public FTPHelpr(string ip,int port)
        {
            _ip = ip;
            _port = port;
        }

        internal void UpLoadFile(string fullFileName, string uploadedName)
        {
            string fileName = uploadedName+new FileInfo(fullFileName).Extension;
            string uri = string.Format("ftp://{0}:{1}/{2}", _ip, _port, fileName);
            _request = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(uri));
            //request.Credentials = new System.Net.NetworkCredential(sftpUsername, sftpPassword);
            _request.KeepAlive = false;
            _request.UseBinary = true;
            _request.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            _request.UseBinary = true;
            System.IO.FileStream fs = new System.IO.FileStream(fullFileName, System.IO.FileMode.Open);
            _request.ContentLength = fs.Length;
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            System.IO.Stream stream = _request.GetRequestStream();
            UploadObject uo = new UploadObject { stream = stream, fs = fs };
            stream.BeginWrite(buffer, 0, buffer.Length, UploadCallBack, uo);
        }

        internal void UpLoadFile(string fullFileName, string uploadedName,string dirName)
        {
            string fileName = uploadedName + new FileInfo(fullFileName).Extension;
            string uri = string.Format("ftp://{0}:{1}/{2}/{3}", _ip, _port,dirName, fileName);
            _request = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(uri));
            //request.Credentials = new System.Net.NetworkCredential(sftpUsername, sftpPassword);
            _request.KeepAlive = false;
            _request.UseBinary = true;
            _request.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            _request.UseBinary = true;
            System.IO.FileStream fs = new System.IO.FileStream(fullFileName, System.IO.FileMode.Open);
            _request.ContentLength = fs.Length;
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            System.IO.Stream stream = _request.GetRequestStream();
            UploadObject uo = new UploadObject { stream = stream, fs = fs };
            stream.BeginWrite(buffer, 0, buffer.Length, UploadCallBack, uo);
        }

        public void CreateDir(string dirName)
        {
            try
            {
                string uri = string.Format("ftp://{0}:{1}/{2}", _ip, _port, dirName);
                //Connect(uri);//连接       
                _request = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(uri));
                _request.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)_request.GetResponse();
                response.Close();
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }

        }

        private void UploadCallBack(IAsyncResult ar)
        {
            var obj = ar.AsyncState as UploadObject;
            obj.stream.Close();
            obj.fs.Close();
        }

        public class UploadObject
        {
            public System.IO.Stream stream;
            public System.IO.FileStream fs;
        }
    }

    
}