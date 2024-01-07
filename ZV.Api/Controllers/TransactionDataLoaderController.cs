using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using ZV.Api.Controllers.Helper;
using ZV.Application.Dtos.Request;
using ZV.Application.Interfaces;
namespace ZV.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDataLoaderController : ControllerBase
    {
        private readonly string apiurl = "http://pbiz.zonavirtual.com/api/";

        private readonly IUserInfoApplication _userInfoApplication;
        private readonly ICommerceApplication _commerceApplication;
        private readonly ITransactionApplication _transactionApplication;

        public TransactionDataLoaderController(IUserInfoApplication userInfoApplication, ICommerceApplication commerceApplication, ITransactionApplication transactionApplication)
        {
            _userInfoApplication = userInfoApplication;
            _commerceApplication = commerceApplication;
            _transactionApplication = transactionApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListRawTransactions()
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiurl);
                        HttpContent _content = new StringContent("{}", Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync("Prueba/Consulta", _content);
                        response.EnsureSuccessStatusCode();
                        
                         
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<RawTransaction> allTransactions = JsonConvert.DeserializeObject<List<RawTransaction>>(jsonResponse);
                        HashSet<UserInfoRequestDto> newUsers = new HashSet<UserInfoRequestDto>();
                        HashSet<TransactionRequestDto> newTransactions = new HashSet<TransactionRequestDto>();
                        HashSet<CommerceRequestDto> newCommerce = new HashSet<CommerceRequestDto>(); 
                        foreach (var item in allTransactions)
                        {
                            newUsers.Add(new UserInfoRequestDto(item));
                            newTransactions.Add(new TransactionRequestDto(item));
                            newCommerce.Add(new CommerceRequestDto(item));
                        }
                        int failInsertion = 0;
                        foreach (var user in newUsers) 
                        {
                            var userResponse = await _userInfoApplication.RegisterUser(user);
                            if (!userResponse.IsSuccess)
                            {
                                failInsertion++;
                            }
                        }
                    var results = new
                    {
                        detectedUsers = allTransactions.Count(),
                        uniqueUsers = newUsers.Count(),
                        failInsertion = failInsertion
                    };
                    string jsonResult = JsonConvert.SerializeObject(results, Formatting.Indented);
                    //JsonDocument JsonTransactions = JsonDocument.Parse(JsonConvert.SerializeObject(allTransactions));

                    return Ok(jsonResult);
                    }
                }
                catch (HttpRequestException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        return StatusCode(500, new { message = "Internal Server Error", detail = ex.Message });
                    }
                    else
                        return BadRequest($"Http query error: {ex.Message}");
                }
                catch (System.Text.Json.JsonException ex)
                {
                    return BadRequest($"Deserialization error: {ex.Message}");
                }
            }

        [HttpGet]
        public async Task<IActionResult> ListBackUpTransactions()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://gist.githubusercontent.com/danOspin/0fc0935442167c100a588f2b31c74009/raw/");
                    HttpContent _content = new StringContent("{}", Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync("d5fca6431292c19579148cde94e30881da70c531/gistfile1.txt");
                    response.EnsureSuccessStatusCode();


                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<RawTransaction> allTransactions = JsonConvert.DeserializeObject<List<RawTransaction>>(jsonResponse);
                    HashSet<UserInfoRequestDto> newUsers = new HashSet<UserInfoRequestDto>();
                    HashSet<TransactionRequestDto> newTransactions = new HashSet<TransactionRequestDto>();
                    HashSet<CommerceRequestDto> newCommerces = new HashSet<CommerceRequestDto>();
                    foreach (var item in allTransactions)
                    {
                        newUsers.Add(new UserInfoRequestDto(item));
                        newTransactions.Add(new TransactionRequestDto(item));
                        newCommerces.Add(new CommerceRequestDto(item));
                    }
                    int failInsertion = 0;
                    /*foreach (var user in newUsers)
                    {
                        var userResponse = await _userInfoApplication.RegisterUser(user);
                        if (!userResponse.IsSuccess)
                        {
                            failInsertion++;
                        }
                    }*/
                    //Se desactiva temporalmente
                    var userResponse = await _userInfoApplication.RegisterUsers(newUsers);
                    var commerceResponse = await _commerceApplication.RegisterMultipleCommerces(newCommerces);
                    var transactionResponse = await _transactionApplication.RegisterMultipleTransactions(newTransactions);

                    var results = new
                    {
                        detectedUsers = allTransactions.Count(),
                        uniqueUsers = newUsers.Count(),
                        failInsertion = failInsertion
                    };
                    string jsonResult = JsonConvert.SerializeObject(results, Formatting.Indented);
                    //JsonDocument JsonTransactions = JsonDocument.Parse(JsonConvert.SerializeObject(allTransactions));

                    return Ok(jsonResult);
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return StatusCode(500, new { message = "Internal Server Error", detail = ex.Message });
                }
                else
                    return BadRequest($"Http query error: {ex.Message}");
            }
            catch (System.Text.Json.JsonException ex)
            {
                return BadRequest($"Deserialization error: {ex.Message}");
            }
        }
    }
}


