using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShake;
using Vantage.Web.Models;

namespace Vantage.Web.Services
{
    public class ReplacementLinkService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReplacementLinkService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            
        }
        public List<ReplacementLink> ReplacementLinks = new();
        public async Task InitializeReplacementLinks()
        {
            var client = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<IChatClient>() ?? throw new Exception("Client cannot be null");

            var result = await client.GetReplacementLinks.ExecuteAsync();
            result.EnsureNoErrors();

            // build initial list.
            ReplacementLinks = result.Data?.ReplacementLink?.Select(l =>
                new ReplacementLink
                {
                    Keyword = l?.Keyword,
                    Hyperlink = l?.Hyperlink
                }).ToList();
        }
    }
}