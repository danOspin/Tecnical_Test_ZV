﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using ZV.Api.Controllers.Helper;
namespace ZV.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDataLoaderController : ControllerBase
    {
        private readonly string apiurl = "http://pbiz.zonavirtual.com/api/";
        
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
                        
                        JsonDocument JsonTransactions = JsonDocument.Parse(JsonConvert.SerializeObject(allTransactions));
                           
                        return Ok(JsonTransactions);
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
