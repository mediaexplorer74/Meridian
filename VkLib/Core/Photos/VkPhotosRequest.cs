using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkLib.Core.Photos
{
    public class VkPhotosRequest
    {
        private readonly Vk _vkontakte;

        internal VkPhotosRequest(Vk vkontakte)
        {
            _vkontakte = vkontakte;
        }

        public async Task<string> GetMessagesUploadServer()
        {
            if (_vkontakte.AccessToken == null || string.IsNullOrEmpty(_vkontakte.AccessToken.Token) || _vkontakte.AccessToken.HasExpired)
                throw new Exception("Access token is not valid.");

            var parameters = new Dictionary<string, string>();

            _vkontakte.SignMethod(parameters);

            var response = await VkRequest.GetAsync(VkConst.MethodBase + "photos.getMessagesUploadServer", parameters);

            return response["response"]["upload_url"]?.Value<string>();
        }

        public async Task<string> GetWallUploadServer(long albumId = 0, long groupId = 0)
        {
            var parameters = new Dictionary<string, string>();

            if (albumId > 0)
                parameters.Add("aid", albumId.ToString());

            if (groupId > 0)
                parameters.Add("group_id", groupId.ToString());

            _vkontakte.SignMethod(parameters);

            var response = await VkRequest.GetAsync(VkConst.MethodBase + "photos.getWallUploadServer", parameters);

            return response["response"]["upload_url"]?.Value<string>();
        }

        public async Task<VkUploadPhotoResponse> UploadPhoto(string url, string fileName, Stream photoStream)
        {
            var client = new HttpClient();

            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            var content = new MultipartFormDataContent(boundary);

            var fileContent = new StreamContent(photoStream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                FileName = fileName,
                Name = "photo"
            };

            content.Add(fileContent);

            var responseMessage = await client.PostAsync(new Uri(url), content);
            byte[] bytes = await responseMessage.Content.ReadAsByteArrayAsync();

            Encoding encoding = Encoding.UTF8; //VK returns windows-1251 which causes exception on Win10
            string response = encoding.GetString(bytes, 0, bytes.Length);
            var json = JObject.Parse(response);
            return VkUploadPhotoResponse.FromJson(json);
        }

        public async Task<VkPhoto> SaveWallPhoto(string server, string photo, string hash, long userId = 0, long groupId = 0)
        {
            var parameters = new Dictionary<string, string>();

            parameters.Add("server", server);
            parameters.Add("photo", photo);
            parameters.Add("hash", hash);

            if (userId != 0)
                parameters.Add("user_id", userId.ToString());

            if (groupId != 0)
                parameters.Add("group_id", groupId.ToString());

            parameters.Add("access_token", _vkontakte.AccessToken.Token);

            var response = await VkRequest.GetAsync(VkConst.MethodBase + "photos.saveWallPhoto", parameters);

            if (response["response"] != null)
                return VkPhoto.FromJson(response["response"].First);

            return null;
        }

        public async Task<VkPhoto> SaveMessagePhoto(string server, string photo, string hash)
        {
            if (_vkontakte.AccessToken == null || string.IsNullOrEmpty(_vkontakte.AccessToken.Token) || _vkontakte.AccessToken.HasExpired)
                throw new Exception("Access token is not valid.");

            var parameters = new Dictionary<string, string>();
            parameters.Add("server", server);
            parameters.Add("photo", photo);
            parameters.Add("hash", hash);

            _vkontakte.SignMethod(parameters);

            var response = await VkRequest.GetAsync(VkConst.MethodBase + "photos.saveMessagesPhoto", parameters);

            if (response["response"] != null)
                return VkPhoto.FromJson(response["response"].First);

            return null;
        }
    }
}
