using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using ZV.Api.Controllers.Helper;
using ZV.Application.Dtos.Request;
using ZV.Application.Interfaces;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Utilities.Static;
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
        private readonly IClientApplication _clientApplication;

        public TransactionDataLoaderController(IUserInfoApplication userInfoApplication, ICommerceApplication commerceApplication, ITransactionApplication transactionApplication, IClientApplication clientApplication)
        {
            _userInfoApplication = userInfoApplication;
            _commerceApplication = commerceApplication;
            _transactionApplication = transactionApplication;
            _clientApplication = clientApplication;
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
                    HashSet<CommerceRequestDto> newCommerces = new HashSet<CommerceRequestDto>();
                   
                    foreach (var item in allTransactions)
                    {
                        newUsers.Add(new UserInfoRequestDto(item));
                        newCommerces.Add(new CommerceRequestDto(item));
                        newTransactions.Add(new TransactionRequestDto(item));
                    }

                    int failInsertion = 0;
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
                    return StatusCode(500, new { message = "Error 500: Server error", detail = ex.Message });
                }
                else
                    return BadRequest($"Http query error: {ex.Message}");
            }
            catch (System.Text.Json.JsonException ex)
            {
                return BadRequest($"Deserialization error: {ex.Message}");
            }
        }

        //string name, string pass, string userid
        [HttpPost("LoginVerification")]
        public async Task<IActionResult> LoginVerification([FromBody] ClientFilterRequest clientBodyRequest) //[FromBody] string userid
        {
            try
            {

                var existingClient = await _clientApplication.ClientById(clientBodyRequest.clientid);
                switch (existingClient.Message)
                {
                    case ReplyMessage.MESSAGE_QUERY_EMPTY:
                        return StatusCode(404, new { message = "Error 404: Client Not Found" });

                    case ReplyMessage.MESSAGE_NO_PASS:
                        return StatusCode(200, new { message = "Client found, but need to activate Credentials" });

                    case ReplyMessage.MESSAGE_QUERY:
                        //TODO:
                        // dos caminos, credenciales incorrectas o consulta de transacciones
                        //Debería retornar de una vez las cuentas por pagar.
                        return StatusCode(200, new { message = "Ready to Login" });

                    default:
                        return StatusCode(500, new { message = "Unknown message" + existingClient.Message });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal Server error", detail = e.Message });
            }
        }
        [HttpPost("SetCredentials")]
        public async Task<IActionResult> SetCredentials([FromBody] ClientFilterRequest clientBodyRequest) //[FromBody] string userid
        {
            try
            {
                if (string.IsNullOrEmpty(clientBodyRequest.username) || string.IsNullOrEmpty(clientBodyRequest.pass))
                {
                    return StatusCode(400, new { message = "Bad Request, check usedname o password fields" });
                }

                var createCreds = await _clientApplication.CreateCredentials(clientBodyRequest);
                //var checkClient = await _clientApplication.ListTransaction(clientBodyRequest);
                if (createCreds.IsSuccess)
                {
                    return StatusCode(200, new { data = "Credenciales creadas Exitosamente" });
                }
                else
                    return StatusCode(500, new { data = "Error al crear credenciales" });

                /*switch (existingClient.Message)
                {
                    case ReplyMessage.MESSAGE_QUERY_EMPTY:
                        return StatusCode(404, new { message = "Error 404: Client Not Found" });

                    case ReplyMessage.MESSAGE_NO_PASS:
                        return StatusCode(200, new { message = "Client found, but need to activate Credentials" });

                    case ReplyMessage.MESSAGE_QUERY:
                        //TODO:
                        // dos caminos, credenciales incorrectas o consulta de transacciones
                        //Debería retornar de una vez las cuentas por pagar.
                        return StatusCode(200, new { message = "Succesful Login" });

                    default:
                        return StatusCode(500, new { message = "Unknown message" + existingClient.Message });
                }*/
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal Server error", detail = e.Message });
            }
        }
        [HttpPost("LoginClient")]
        public async Task<IActionResult> LoginClient([FromBody] ClientFilterRequest clientBodyRequest) //[FromBody] string userid
        {
            try
            {
                if (string.IsNullOrEmpty(clientBodyRequest.username) || string.IsNullOrEmpty(clientBodyRequest.pass))
                {
                    return StatusCode(400, new { message = "Bad Request, check usedname o password fields" });
                }

                var checkCreds = await _clientApplication.CheckCredentials(clientBodyRequest);
                //var checkClient = await _clientApplication.ListTransaction(clientBodyRequest);
                if (checkCreds.IsSuccess)
                {
                    return StatusCode(200, new { data = "Login Exitoso" });
                }
                else
                    return StatusCode(500, new { data = "Error al verificar credenciales" });

                /*switch (existingClient.Message)
                {
                    case ReplyMessage.MESSAGE_QUERY_EMPTY:
                        return StatusCode(404, new { message = "Error 404: Client Not Found" });

                    case ReplyMessage.MESSAGE_NO_PASS:
                        return StatusCode(200, new { message = "Client found, but need to activate Credentials" });

                    case ReplyMessage.MESSAGE_QUERY:
                        //TODO:
                        // dos caminos, credenciales incorrectas o consulta de transacciones
                        //Debería retornar de una vez las cuentas por pagar.
                        return StatusCode(200, new { message = "Succesful Login" });

                    default:
                        return StatusCode(500, new { message = "Unknown message" + existingClient.Message });
                }*/
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal Server error", detail = e.Message });
            }
        }

        [HttpPost("Transactions")]
        public async Task<IActionResult> GetTransactions([FromBody] TransactionsPerClientRequestDto transactionsPerClientRequest)
        {
            try
            {
                var transactionAppResult = await _transactionApplication.ListTransaction(transactionsPerClientRequest);
                switch (transactionAppResult.Message)
                {
                    case ReplyMessage.MESSAGE_QUERY_EMPTY:
                        return StatusCode(200, new { message = transactionAppResult.Message });

                    case ReplyMessage.MESSAGE_QUERY:
                        //TODO:
                        // dos caminos, credenciales incorrectas o consulta de transacciones
                        //Debería retornar de una vez las cuentas por pagar.
                        return Ok(transactionAppResult);

                    default:
                        return StatusCode(500, new { message = "Unknown message " + transactionAppResult.Message});
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal Server error", detail = e.Message });
            }
            //return StatusCode(200, new { message = "Client found, but need to activate Credentials" });
        }

        /*[HttpPost("UpdateTransaction")]
        public async Task<IActionResult> PutTransactions([FromBody] TransactionsPerClientRequestDto transactionsPerClientRequest)
        {
            try
            {
                var transactionAppResult = await _transactionApplication.ListTransaction(transactionsPerClientRequest);
                switch (transactionAppResult.Message)
                {
                    case ReplyMessage.MESSAGE_QUERY_EMPTY:
                        return StatusCode(200, new { message = transactionAppResult.Message });

                    case ReplyMessage.MESSAGE_QUERY:
                        //TODO:
                        // dos caminos, credenciales incorrectas o consulta de transacciones
                        //Debería retornar de una vez las cuentas por pagar.
                        return Ok(transactionAppResult);

                    default:
                        return StatusCode(500, new { message = "Unknown message " + transactionAppResult.Message });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal Server error", detail = e.Message });
            }
            //return StatusCode(200, new { message = "Client found, but need to activate Credentials" });
        }
        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionsPerClientRequestDto transactionsPerClientRequest)
        {
            try
            {
                var transactionAppResult = await _transactionApplication.ListTransaction(transactionsPerClientRequest);
                switch (transactionAppResult.Message)
                {
                    case ReplyMessage.MESSAGE_QUERY_EMPTY:
                        return StatusCode(200, new { message = transactionAppResult.Message });

                    case ReplyMessage.MESSAGE_QUERY:
                        //TODO:
                        // dos caminos, credenciales incorrectas o consulta de transacciones
                        //Debería retornar de una vez las cuentas por pagar.
                        return Ok(transactionAppResult);

                    default:
                        return StatusCode(500, new { message = "Unknown message " + transactionAppResult.Message });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal Server error", detail = e.Message });
            }
            //return StatusCode(200, new { message = "Client found, but need to activate Credentials" });
        }
        */
    }
}
    /*switch (existingClient.Message)
                {
                    case ReplyMessage.MESSAGE_QUERY_EMPTY:
                        return StatusCode(404, new { message = "Error 404: Client Not Found" });

                    case ReplyMessage.MESSAGE_NO_PASS:
                        return StatusCode(200, new { message = "Client found, but need to activate Credentials" });

                    case ReplyMessage.MESSAGE_QUERY:
                        //TODO:
                        // dos caminos, credenciales incorrectas o consulta de transacciones
                        //Debería retornar de una vez las cuentas por pagar.
                        return StatusCode(200, new { message = "Succesful Login" });

                    default:
                        return StatusCode(500, new { message = "Unknown message" + existingClient.Message });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal Server error", detail = e.Message });
            }
        }*/
        /*[HttpPost("Transactions")]
        public async Task<IActionResult> AddTransaction()
        {

        }*/
    


