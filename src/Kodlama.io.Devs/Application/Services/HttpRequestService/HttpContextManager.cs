using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HttpRequestService
{
    public class HttpContextManager : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetIpAddress()
        {
            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For")) return _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}
