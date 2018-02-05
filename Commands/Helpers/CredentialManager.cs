using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Helpers
{
    public class CredentialManager
    {
        string path;

        public CredentialManager(string moduleBase)
        {
            path = moduleBase;
        }

        public SPOnlineConnection Get(string url)
        {
            var uri = new Uri(url);
            var entryKey = $"PnPPS_{uri.Host}";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.FileName = System.IO.Path.Combine(path, "tools\\creds.exe");
            startInfo.Arguments = $"-s -t PnPPS_{uri.Host} -g";
            startInfo.RedirectStandardOutput = true;
            var process = Process.Start(startInfo);
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                var splitted = line.Split(new string[] { ": " },StringSplitOptions.None);
                if (splitted[0] == "Credential")
                {
                    var credString = splitted[1];
                    StringBuilder builder = new StringBuilder();
                    for (int q=0; q<credString.Length;q = q + 2)
                    {
                        var step = credString.Substring(q, 2);
                        var intValue = int.Parse(step, System.Globalization.NumberStyles.HexNumber);
                        builder.Append(Convert.ToChar(intValue));
                    }
                    var d = JsonConvert.DeserializeObject<Dictionary<string, string>>(builder.ToString());
                    var context = new SPOnlineConnection();
                    context.AccessToken = d["accesstoken"];
                    context.RefreshToken = d["refreshtoken"];
                    context.ExpiresIn = new DateTime(long.Parse(d["expiration"]));
                    context.Url = url;
                    return context;
                }
            }
            return null;
        }
        public void Add(string url, string accessToken, string refreshToken, DateTime expiration)
        {
            var uri = new Uri(url);
            var d = new Dictionary<string, string>();
            d.Add("accesstoken", accessToken);
            d.Add("refreshtoken", refreshToken);
            d.Add("expiration", expiration.Ticks.ToString());
            var p = JsonConvert.SerializeObject(d);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in p.ToCharArray())
            {
                stringBuilder.Append(((Int16)c).ToString("x"));
            }
            String pHex = stringBuilder.ToString();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.FileName = System.IO.Path.Combine(path, "tools\\creds.exe");
            startInfo.Arguments = $"-a -t PnPPS_{uri.Host} -p {pHex}";
            var process = Process.Start(startInfo);
        }

        public bool Remove(string url)
        {
            return false;
        }
    }
}
