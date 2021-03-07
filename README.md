# HolidayOptimizer

There are two cache implementations in project
- with Redis
- InMemoryCache

InMemoryCache are using in app by default.

To use Redis:

replace in Startup.cs:
            //services.AddTransient<ICacheProvider<Dictionary<string, string>>, RedisCache>();
            services.AddTransient<ICacheProvider<Dictionary<string, string>>, InMemoryCache>();
with
            services.AddTransient<ICacheProvider<Dictionary<string, string>>, RedisCache>();
            //services.AddTransient<ICacheProvider<Dictionary<string, string>>, InMemoryCache>();
            
and update "config" field in RedisCache.cs with the correct value


TODO list:

- add cache for user requests
- add "Bonus question" implementation))
- add more advanced tests
