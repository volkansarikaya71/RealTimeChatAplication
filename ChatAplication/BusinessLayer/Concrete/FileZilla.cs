using FluentFTP;
using FluentFTP.Helpers;
using FluentFTP.Rules;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ChatApi.DAL
{
    public class FileZilla
    {
        public static string ftpAddress = "ftp://89.117.169.37/";
        static string ftpUsername = "u717747209.volkan";
        static string ftpPassword = "KnYiiLOATuTqWPl1";
        public static string FtpUrl = " ";



        #region message kısmı resim,video,ses kaydı,winrar vb yükleme işlemi.
        public async static Task<string> addData(string location, string folderName)
        {
            var client = new AsyncFtpClient(ftpAddress, ftpUsername, ftpPassword);
            await client.AutoConnect();

            string fileName = Path.GetFileName(location);

            string ftpDirectory = "Kullanici_Verileri/" + folderName;
            string ftpPath = ftpDirectory + "/" + fileName;

            bool directoryExists = await client.DirectoryExists(ftpDirectory);
            if (!directoryExists)
            {
                await client.CreateDirectory(ftpDirectory);
            }

            bool fileExists = await client.FileExists(ftpPath);
            int counter = 1;
            while (fileExists)
            {
                // Dosya varsa, ismini değiştir
                string baseFileName = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                string newFileName = $"{baseFileName}_{counter}{extension}";
                ftpPath = ftpDirectory + "/" + newFileName;
                fileExists = await client.FileExists(ftpPath);
                counter++;
            }

            string fixedLocation = location.Replace("\\", "/");
            await client.UploadFile(fixedLocation, ftpPath, FtpRemoteExists.Overwrite, false, FtpVerify.Retry);

            await client.Disconnect();

            return ftpAddress + ftpPath;
        }
        #endregion

        #region Resim indirme işlemi
        public async static Task<bool> dowloadData(string location,string PhoneNumber)
        {

            var client = new AsyncFtpClient(ftpAddress, ftpUsername, ftpPassword);
            await client.AutoConnect();
            #region Gelen lokasyonun içinden telefon numarası ve resim ismini alma
            string cleanLocation = location.Replace("ftp://", "");
            string ftbPhoneNumber = cleanLocation.Split('/')[2];
            string dataName = cleanLocation.Split('/').Last();
            #endregion

            string filePath = $"/Kullanici_Verileri/{ftbPhoneNumber}/{dataName}"; 
            bool fileExists = await client.FileExists(filePath);

            if (!fileExists)
            {
                Console.WriteLine("Dosya bulunamadı.");
                return false;
            }

            string fileName = Path.GetFileName(location);
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string targetPath = Path.Combine(baseDirectory, $"Data/{PhoneNumber}", fileName);

            // Dosyayı indir
            await client.DownloadFile(targetPath, filePath);

            await client.Disconnect();
            return true;
        }
        #endregion


































    }
}
