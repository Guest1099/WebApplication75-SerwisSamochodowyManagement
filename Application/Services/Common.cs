namespace Application.Services
{
    public static class Common
    {
        /// <summary>
        /// Zamienia zdjęcie pobrane z sieci na byte[]
        /// </summary>
        public static byte[] GetImageBytesAsync(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                byte[] imageBytes;

                using (var response = httpClient.GetAsync(imageUrl).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        imageBytes = response.Content.ReadAsByteArrayAsync().Result;
                    }
                    else
                    {
                        imageBytes = new byte[0];
                    }
                }

                return imageBytes;
            }
        }
    }
}
