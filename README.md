**How To Run**
- Host on IIS/IIS Express / Docker
- Go to *[baseAddress]/swagger/index.html* to get the documentation

**Endpoints**
- [GET] api/HackerNews?howMany=


**Specs**
- a .NET Core-based API;
- Can be used either by a front-end app or called programatically;
- Uses Middleware to handle exceptions;
- Utilizes Microsoft caching mechanism to store news items for 10 mins to take some weight off the base API.
- Provides a viewmodel for the news items;

**Assumptions**
- News item url given by the base API is a correct url;
- Time given by the base API is a correct unix timestamp;
- Any news item can be cached for 10 mins and should not change its content within this period of time;
- The base API does not return random errors and does not have to be called a few times;

**Possible Improvements**
- Use a more robust and production ready, multinstance cache, like Redis
- Use Polly to recall the base API, in case of errors;
- Validate Unix timestamp provided by the base API;
- Validate news item url provided by the base API;
- Adjust news item caching time - more research necessary;
