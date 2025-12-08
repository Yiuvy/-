using System.Text.Json;
using Makarevich_proj.Services.contracts_interfaces_;
using Makarevich_sol_Domain.Entities;
using Makarevich_sol_Domain.Models;
using System.Web;

namespace Makarevich_proj.Services
{
    public class ApiProductService(HttpClient httpClient) : IProductService
    {
        //public async Task<ResponseData<Doctor>> CreateProductAsync(Doctor product, IFormFile? formFile)
        //{
        //    var serializerOptions = new JsonSerializerOptions()
        //    {
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    };

        //    //послать запрос к Апи для сохранения объекта
        //    var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, product);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return new ResponseData<Doctor>
        //        {
        //            Success = false,
        //            ErrorMessage = $"Can't find the object:{response.StatusCode}"
        //        };
        //    }
        //    //если фай изображения передан клиентом
        //    if (formFile !=null)
        //    {
        //        //получить созданный объект из ответа API-сервиса
        //        var doctor = await response.Content.ReadFromJsonAsync<Doctor>();
        //        //создать объект запроса
        //        var request = new HttpRequestMessage
        //        {
        //            Method = HttpMethod.Post,
        //            RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/{doctor.Id}")
        //        };
        //    }

        //}

        public async Task<ResponseData<Doctor>> CreateProductAsync(Doctor product, IFormFile? formFile)
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // послать POST-запрос для создания объекта
            var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, product);

            if (!response.IsSuccessStatusCode)
            {
                return new ResponseData<Doctor>
                {
                    Success = false,
                    ErrorMessage = $"Can't create object: {response.StatusCode}"

                };
            }

            // Получить созданный объект Doctor из ответа API
            var createdDoctor = await response.Content.ReadFromJsonAsync<Doctor>();

            if (createdDoctor == null)
            {
                return new ResponseData<Doctor>
                {
                    Success = false,
                    ErrorMessage = $"Failed to deserialize created doctor."
                };
            }

            // Если есть файл изображения, отправляем его
            if (formFile != null)
            {
                // Создаем MultipartFormDataContent для загрузки файла
                using var content = new MultipartFormDataContent();

                // Содержимое файла
                var fileContent = new StreamContent(formFile.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(formFile.ContentType);

                // добавляем файл в контент, указывая название поля, имя файла и содержимое
                content.Add(fileContent, "file", formFile.FileName);

                // предположим, что для загрузки изображения у вас отдельный эндпоинт, например, /api/doctors/{id}/uploadImage
                var uploadResponse = await httpClient.PostAsync($"api/doctors/{createdDoctor.Id}/uploadImage", content);

                if (!uploadResponse.IsSuccessStatusCode)
                {
                    return new ResponseData<Doctor>
                    {
                        Success = false,
                        ErrorMessage = $"Image upload failed: {uploadResponse.StatusCode}"
                    };
                }
            }

            // Возвращаем успешно созданного доктора
            return new ResponseData<Doctor>
            {
                Data = createdDoctor,
                Success = true
            };
        }
        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        //public Task<ResponseData<ListModel<Doctor>>> GetProductListAsync(string? ClinicNormalizedName, int pageNo = 1)
        //{
        //    throw new NotImplementedException();
        //}

        public Task UpdateProductAsync(int id, Doctor product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<ListModel<Doctor>>> GetProductListAsync(string? ClinicNormalizedName, int pageNo = 1)
        {
            var baseUri = httpClient.BaseAddress?.ToString() ?? "";

            // Формируем параметры запроса вручную, лучше с помощью UriBuilder и QueryHelpers (из ASP.NET)
            var queryParams = new Dictionary<string, string>
    {
        { "pageNo", pageNo.ToString() }
    };

            if (!string.IsNullOrEmpty(ClinicNormalizedName))
            {
                queryParams.Add("ClinicNormalizedName", ClinicNormalizedName);
            }

            // Библиотека Microsoft.AspNetCore.WebUtilities предоставляет QueryHelpers, если у вас она доступна
            // Если нет, сформируем вручную:
            var queryString = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

            var fullUri = baseUri.EndsWith("?") || baseUri.EndsWith("&") ?
                baseUri + queryString :
                baseUri + (baseUri.Contains("?") ? "&" : "?") + queryString;

            var result = await httpClient.GetAsync(fullUri);

            if (result.IsSuccessStatusCode)
            {
                try
                {
                    var responseData = await result.Content.ReadFromJsonAsync<ResponseData<ListModel<Doctor>>>();
                    if (responseData != null)
                        return responseData;
                    else
                        return new ResponseData<ListModel<Doctor>>
                        {
                            Success = false,
                            ErrorMessage = "Ответ API пуст."
                        };
                }
                catch (Exception ex)
                {
                    return new ResponseData<ListModel<Doctor>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка десериализации ответа: {ex.Message}"
                    };
                }
            }

            return new ResponseData<ListModel<Doctor>>
            {
                Success = false,
                ErrorMessage = $"Ошибка API: {result.StatusCode}"
            };

            //var uri = httpClient.BaseAddress;

            //var queryData = new Dictionary<string, string>();
            //queryData.Add("pageNo", pageNo.ToString());
            //if (!String.IsNullOrEmpty(ClinicNormalizedName))
            //{
            //    queryData.Add("category", ClinicNormalizedName);
            //}
            //var query = QueryString.Create(queryData);


            //var result = await httpClient.GetAsync(uri + query.Value);
            //if (result.IsSuccessStatusCode)
            //{
            //    return await result.Content
            //    .ReadFromJsonAsync<ResponseData<ListModel<Doctor>>>();
            //}
            //;

            //var response = new ResponseData<ListModel<Doctor>>
            //{ Success = false, ErrorMessage = "Ошибка чтения API" };
            //return response;
        }


    }
}
