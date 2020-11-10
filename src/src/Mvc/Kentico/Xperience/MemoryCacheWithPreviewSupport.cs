using System;
using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace BlogTemplate.Mvc.Kentico.Xperience
{

    public sealed class MemoryCacheWithPreviewSupport : IMemoryCache
    {
        #region Fields
        private readonly IMemoryCache cache;
        private readonly IHttpContextAccessor httpAccessor;
        #endregion

        public MemoryCacheWithPreviewSupport(
            IMemoryCache cache,
            IHttpContextAccessor httpAccessor
        )
        {
            this.cache = cache;
            this.httpAccessor = httpAccessor;
        }

        public ICacheEntry CreateEntry( object key )
        {
            var entry = cache.CreateEntry( key );
            if( IsPreview() )
            {
                entry.SetAbsoluteExpiration( TimeSpan.FromSeconds( .1 ) );
            }

            return entry;
        }

        public void Dispose( )
        {
            // we're scoped, don't dispose the cache (it's a singleton)
        }

        private bool IsPreview( )
            => httpAccessor.HttpContext
                ?.Kentico()
                ?.Preview()
                ?.Enabled == true;

        public void Remove( object key )
            => cache.Remove( key );

        public bool TryGetValue( object key, out object value )
        {
            if( IsPreview() )
            {
                value = default;
                return false;
            }

            return cache.TryGetValue( key, out value );
        }

    }

}
