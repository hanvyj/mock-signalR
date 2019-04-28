using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using SignalRChat.Hubs;

namespace mock_signalR.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class SendController : ControllerBase {
    private IHubContext<MockHub> _chatHubContext;

    public SendController(IHubContext<MockHub> chatHubContext) {
      _chatHubContext = chatHubContext;
    }

    // POST api/send
    [HttpPost("{method}")]
    public void Post(string method, [FromBody] JObject value) {
      _chatHubContext.Clients.All.SendAsync(method, value);
    }

  }
}