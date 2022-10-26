using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Scribe_Factory_Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Repositories
{
    public class AWSHelper
    {
        AmazonPollyClient pollyClient = null;
        BasicAWSCredentials BasicAWSCredentials = null;
        public AWSHelper(string accesskey, string secretKey)
        {
            BasicAWSCredentials = new BasicAWSCredentials(accesskey, secretKey);

            pollyClient = new AmazonPollyClient(BasicAWSCredentials, RegionEndpoint.USEast1);
            GetAllVoiceType();

        }

        private void GetAllVoiceType()
        {
            if (StaticVoiceDetails.AllVoices == null)
            {
                var VoiceList = pollyClient.DescribeVoicesAsync(new DescribeVoicesRequest()).Result;
                List<VoiceModel> voiceModels = new List<VoiceModel>();
                StaticVoiceDetails.AllVoices = VoiceList.Voices.Select(x => new VoiceModel { Engine = x.SupportedEngines, Gender = x.Gender, Id = x.Id, Name = x.Name, LanguageCode = x.LanguageCode, LanguageName = x.LanguageName }).ToList();

            }
        }
        public List<VoiceModel> GetLanguagesAgainstGender(string gender)
        {
            return StaticVoiceDetails.AllVoices.Where(x => x.Gender == gender).Select(x => new VoiceModel { LanguageCode = x.LanguageCode, LanguageName = x.LanguageName }).ToList();
        }
        public List<VoiceModel> GetVoicesAgainstLanguage(string LanguageCode, string gender)
        {
            return StaticVoiceDetails.AllVoices.Where(x => x.LanguageCode == LanguageCode && x.Gender == gender).Select(x => new VoiceModel { Id = x.Id, Name = x.Name }).ToList();
        }
        public bool ConvertTextToSpeech(string text, string languageCode, string VoiceId, string Engine, string bucketName, string FileName)
        {
            try
            {
                var synthesizeSpeechResponse = pollyClient.SynthesizeSpeechAsync(new SynthesizeSpeechRequest
                {
                    LanguageCode = languageCode,
                    OutputFormat = "mp3",
                    SampleRate = "16000",
                    Text = text,
                    TextType = "text",
                    VoiceId = VoiceId,
                    Engine = Engine
                }).Result;
                return sendMyFileToS3(synthesizeSpeechResponse.AudioStream, bucketName, "", FileName);

                //AudioHelper audioHelper = new AudioHelper();
                //return audioHelper.SaveMp3(synthesizeSpeechResponse, "Polly.mp3").Result;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void ConvertTextToSpeech(string text, string languageCode, string VoiceId, string Engine,string filename)
        {
            try
            {
                var synthesizeSpeechResponse = pollyClient.SynthesizeSpeechAsync(new SynthesizeSpeechRequest
                {
                    LanguageCode = languageCode,
                    OutputFormat = "mp3",
                    SampleRate = "16000",
                    Text = text,
                    TextType = "text",
                    VoiceId = VoiceId,
                    Engine = Engine
                }).Result;
                var audiostream = synthesizeSpeechResponse.AudioStream;
                var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\", filename);
                FileStream destination = File.Open(path, FileMode.Create);
                audiostream.CopyTo(destination);
                destination.Close();

                //AudioHelper audioHelper = new AudioHelper();
                //return audioHelper.SaveMp3(synthesizeSpeechResponse, "Polly.mp3").Result;
            }
            catch (Exception e)
            {
                
            }
        }


        public bool sendMyFileToS3(System.IO.Stream localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            IAmazonS3 client = new AmazonS3Client(BasicAWSCredentials, RegionEndpoint.USWest2);
            TransferUtility utility = new TransferUtility(client);
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName; //no subdirectory just bucket name  
            }
            else
            {   // subdirectory and bucket name  
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3; //file name up in S3  
            MemoryStream ms = new MemoryStream();
            localFilePath.CopyTo(ms);
            request.InputStream = ms;
            utility.Upload(request); //commensing the transfer  

            return true; //indicate that the file was sent  
        }

        public void GetFileFromS3(string documentPath, string bucketname)
        {
            using (FileStream fileDownloaded = new FileStream(documentPath, FileMode.Create, FileAccess.Read))
            {
                TransferUtility fileTransferUtility =
    new TransferUtility(
        new AmazonS3Client(BasicAWSCredentials, RegionEndpoint.USWest2));

                // Note the 'fileName' is the 'key' of the object in S3 (which is usually just the file name)
                fileTransferUtility.Download("", bucketname, documentPath);
            }

        }
        public void DownloadObject(string documentPath, string bucketname)
        {

            string accessKey = System.Configuration.ConfigurationManager.AppSettings["AWSAccessKey"];
            string secretKey = System.Configuration.ConfigurationManager.AppSettings["AWSSecretKey"];
            AmazonS3Client s3Client = new AmazonS3Client(BasicAWSCredentials, RegionEndpoint.USWest2);
            string objectKey = documentPath;
            //EMR is folder name of the image inside the bucket 
            GetObjectRequest request = new GetObjectRequest();
            request.BucketName = bucketname;
            request.Key = objectKey;
            GetObjectResponse response = s3Client.GetObjectAsync(request).Result;
            response.WriteResponseStreamToFileAsync(documentPath, false, new System.Threading.CancellationToken()).Wait();
            
        }
    }
}
