using Mindscape.Raygun4Net.AspNetCore;
using NLog.Config;
using NLog.Targets;
using System;

namespace NLog.Raygun.AspNetCore
{
    [Target("RayGunAspNetCore")]
    public class RayGunTarget : TargetWithLayout
    {
        [RequiredParameter]
        public string ApiKey { get; set; }

        [RequiredParameter]
        public string Tags { get; set; }

        [RequiredParameter]
        public string IgnoreFormFieldNames { get; set; }

        [RequiredParameter]
        public string IgnoreCookieNames { get; set; }

        [RequiredParameter]
        public string IgnoreServerVariableNames { get; set; }

        [RequiredParameter]
        public string IgnoreHeaderNames { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            var logMessage = Layout.Render(logEvent);

            var exception = new RaygunException(logMessage, logEvent.Exception);

            var client = CreateRaygunClient();

            SendMessage(client, exception);
        }

        private RaygunClient CreateRaygunClient()
        {
            var client = new RaygunClient(ApiKey);

            client.IgnoreFormFieldNames(SplitValues(IgnoreFormFieldNames));
            client.IgnoreCookieNames(SplitValues(IgnoreCookieNames));
            client.IgnoreHeaderNames(SplitValues(IgnoreHeaderNames));
            client.IgnoreServerVariableNames(SplitValues(IgnoreServerVariableNames));

            return client;
        }

        private void SendMessage(RaygunClient client, Exception exception)
        {
            if (!string.IsNullOrWhiteSpace(Tags))
            {
                var tags = Tags.Split(',');
                client.SendInBackground(exception, tags);
            }
            else
            {
                client.SendInBackground(exception);
            }
        }

        private string[] SplitValues(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Split(',');
            }

            return new[] { "" };
        }
    }
}
