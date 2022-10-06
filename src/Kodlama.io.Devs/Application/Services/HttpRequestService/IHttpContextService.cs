using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HttpRequestService
{
    public interface IHttpContextService
    {
        public string? GetIpAddress();
    }
}
