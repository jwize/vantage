using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShake;
using Vantage.Web.Models;

namespace Vantage.Web.Services
{
    
    public class ReplacementLinkService
    {
        public ImmutableList<ReplacementLink> ReplacementLinks { get; set; }
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Dictionary<int, string> _hashedComments = new();

        public ReplacementLinkService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            
        }
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
                }).ToImmutableList();
        }

        public string FindHashed(string content)
        {
            var hash = content.GetHashCode();
            return _hashedComments[hash];
        }

        public void Hash(string content)
        {
            var hash = content.GetHashCode();
            if (!_hashedComments.ContainsKey(hash))
            {
                _hashedComments.Add(hash, string.Empty);
            }

            var output = content;
            foreach (var replacement in ReplacementLinks)
            {
                output = output.Replace(replacement.Keyword, $"<a target='_blank' href='{replacement.Hyperlink}'>{replacement.Keyword}</a>");
            }
            _hashedComments[hash] = output;
        }


        public void Refresh(List<Comment> comments)
        {
            foreach (var comment in comments)
            {
                Hash(comment.Content);
            }
        }
        public void RemovedReplacementsWithKeyword(string keyword)
        {
            var removed = _hashedComments.Where(h => h.Value.ToLower().Contains(keyword.ToLower())).Select(h => h.Key);
            foreach (var hash in removed)
            {
                _hashedComments.Remove(hash);
            }

            var link = ReplacementLinks.FirstOrDefault(r => r.Keyword == keyword);
            ReplacementLinks = ReplacementLinks.Remove(link);
        }

        public void Add(IOnReplacementLinkCreated_Created created)
        {
            ReplacementLinks = ReplacementLinks.Add(new ReplacementLink()
            {
                Keyword = created?.Keyword,
                Hyperlink = created?.Hyperlink
            });
        }
    }
}