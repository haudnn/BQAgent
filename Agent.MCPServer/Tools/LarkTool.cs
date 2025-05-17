using System.Text;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace Agent.MCPServer.Tools;


public class LarkTool
{
  private readonly string _appId = "cli_a8adb21f18f9502f";
  private readonly string _appSecret = "daq0d9DgaZsLF2Eahbpo9eXlja1zsVL0";
  private readonly string _baseUrl = "https://open.larksuite.com/open-apis/";
  private readonly HttpClient _httpClient;

  public LarkTool()
  {
    _httpClient = new HttpClient
    {
      BaseAddress = new Uri(_baseUrl)
    };
  }

  [McpServerTool]
  //public async Task<string> SendLarkMessage(string recipientId, string message)
  public async Task<string> SendLarkMessage()
  {
    // Lấy access token
    var token = await GetAccessToken();

    // var payload = new
    // {
    //   receive_id = recipientId,
    //   msg_type = "text",
    //   content = new { text = message }
    // };

    var payload = new
    {
      receive_id = "ou_0f1c3e2a4b5d4f8a",
      msg_type = "text",
      content = "{\'text\':\'test content\'}"
    };

    var request = new HttpRequestMessage(HttpMethod.Post, "im/v1/messages?receive_id_type=open_id");
    request.Headers.Add("Authorization", $"Bearer {token}");
    request.Content = new StringContent(
        JsonSerializer.Serialize(payload),
        Encoding.UTF8,
        "application/json");

    var response = await _httpClient.SendAsync(request);
    return await response.Content.ReadAsStringAsync();
  }

  private async Task<string> GetAccessToken()
  {
    var payload = new
    {
      app_id = _appId,
      app_secret = _appSecret
    };

    var request = new HttpRequestMessage(HttpMethod.Post, "auth/v3/tenant_access_token/internal/")
    {
      Content = new StringContent(
          JsonSerializer.Serialize(payload),
          Encoding.UTF8,
          "application/json")
    };

    var response = await _httpClient.SendAsync(request);
    var content = await response.Content.ReadAsStringAsync();
    var json = JsonSerializer.Deserialize<JsonElement>(content);
    return json.GetProperty("tenant_access_token").GetString();
  }
}