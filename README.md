NLog.Raygun.AspNetCore
======================

A custom [NLog] target that will push errors to [Raygun] on Asp .Net Core projects.

[NLog]: http://nlog-project.org/
[Raygun]: https://raygun.com/

## Configuration

To use this library you need to configure NLog.

### NLog Configuration

Your `NLog.config` should look something like this:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <extensions>
    <!-- Add the assembly -->
    <add assembly="NLog.Raygun.AspNetCore"/>
  </extensions>
  <targets>
    <target 
      name="RayGunTarget" 
      type="RayGunAspNetCore" 
      ApiKey="" 
      Tags="" 
      IgnoreFormFieldNames="" 
      IgnoreCookieNames="" 
      IgnoreServerVariableNames="" 
      IgnoreHeaderNames="" 
      UserIdentityInfo="" 
      UseExecutingAssemblyVersion="false" 
      ApplicationVersion="" 
      layout="${uppercase:${level}} ${message} ${exception:format=ToString,StackTrace}${newline}"
        />
  </targets>
  <rules>
    <!-- Set up the logger. -->
    <logger name="*" minlevel="Warn" writeTo="RayGunTarget" />
  </rules>
</nlog>
```
