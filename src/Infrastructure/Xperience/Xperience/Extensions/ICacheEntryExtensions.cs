using System;
using System.Linq;
using System.Threading;
using CMS.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace BlogTemplate.Infrastructure.Xperience.Extensions
{

    public static class ICacheEntryExtensions
    {

        private static void OnDependencyEvicted( CancellationTokenSource tokenSource, object keyValue )
        {
            CacheHelper.RemoveDependencyCallback( keyValue.ToString() );

            tokenSource?.Cancel();
            tokenSource?.Dispose();
        }

        /// <summary> Registers the specified <paramref name="entry"/> with a dependency on the given Kentico dependency keys. </summary>
        /// <param name="entry"> The entry to set a dependency for. </param>
        /// <param name="dependencies"> The dependency keys. </param>
        public static ICacheEntry SetCMSDependency( this ICacheEntry entry, params string[] dependencies )
        {
            if( dependencies?.Any() != true )
            {
                return entry;
            }

            string key = $"cachecallback|{Guid.NewGuid()}";
            var tokenSource = new CancellationTokenSource();

            CacheHelper.RegisterDependencyCallback(
               key,
               CacheHelper.GetCacheDependency( dependencies ),
               tokenSource,
               OnDependencyEvicted,
               key
            );

            entry.AddExpirationToken( new CancellationChangeToken( tokenSource.Token ) );
            return entry;
        }

    }

}
