using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KeyStore
{
    public class GithubOptions : IDisposable
    {
        public string Username = "";
        public string Password = "";
        public string Url = "";
        public string RawUrl = "";
        public string RepoName = "";
        public string UniqueFolder = Guid.NewGuid().ToString();

        public GithubOptions(IConfiguration Configuration)
        {
            Username = Configuration.GetValue<string>("Username");
            Password = Configuration.GetValue<string>("Password");
            Url = Configuration.GetValue<string>("Url");
            RawUrl = Configuration.GetValue<string>("RawUrl");

            RepoName = UniqueFolder + "/" + Url.Split("/").Last().Replace(".git", "");
            Directory.CreateDirectory(UniqueFolder);

            ConfigureGit();
        }

        private void ConfigureGit()
        {
            var results = $"git config --global user.email \"dylanstrohschein@gmail.com\" && git config --global user.name \"Dylan Strohschein\"".Bash();
        }

        private void PullCode()
        {
            if (Directory.Exists(RepoName))
            {
                Directory.Delete(RepoName);
            }

            var cloneResults = $"cd {UniqueFolder} && git clone https://{Username}:{Password}@{Url.Replace("https://", "")} --depth=1".Bash();
        }

        private void PushCode()
        {
            var results = $"cd {RepoName} && git add . && git commit -m \"Added Key\" && git push https://{Username}:{Password}@{Url.Replace("https://", "")}".Bash();
        }

        public bool SaveKeyValue(string Key, string Value)
        {
            PullCode();

            if(File.Exists(RepoName + "/" + Key))
            {
                return false;
            }
            else
            {
                File.WriteAllText(RepoName + "/" + Key, Value);
                PushCode();
                return true;
            }
        }

        public string GetValue(string Key)
        {
            using (WebClient c = new WebClient())
            {
                return c.DownloadString(RawUrl + "/" + Key);
            }
        }

        public void Dispose()
        {
            Directory.Delete(UniqueFolder, true);
        }
    }
}
