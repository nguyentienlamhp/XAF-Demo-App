using DevExpress.ExpressApp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.Common
{

        public static class UserSessionStore
        {
            // Dictionary an toàn cho đa luồng
            private static readonly ConcurrentDictionary<Guid, ConcurrentDictionary<string, object>> _sessions = new();

            public static void SetValue(string key, object value)
            {
                var userId = GetCurrentUserId();
                if (userId == Guid.Empty) return;

                var userSession = _sessions.GetOrAdd(userId, _ => new ConcurrentDictionary<string, object>());
                userSession[key] = value;
            }

            public static T GetValue<T>(string key)
            {
                var userId = GetCurrentUserId();
                if (userId == Guid.Empty) return default;

                if (_sessions.TryGetValue(userId, out var userSession) && userSession.TryGetValue(key, out var value))
                    return (T)value;
                return default;
            }

            private static Guid GetCurrentUserId()
            {
                try
                {
                    // SecuritySystem.CurrentUserId thường là Guid hoặc int tùy DB
                    if (SecuritySystem.CurrentUserId is Guid g) return g;
                    if (Guid.TryParse(SecuritySystem.CurrentUserId?.ToString(), out var parsed)) return parsed;
                }
                catch { }
                return Guid.Empty;
            }
        }
    }

