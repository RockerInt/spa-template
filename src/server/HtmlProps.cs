using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace spa_template
{
    public class HtmlProps
    {
        public HtmlProps(string wwwroot, string instrumentationKey)
        {
            scripts = ToRelativeStringArray(Directory.EnumerateFiles(wwwroot, "*.js"));
            styles = ToRelativeStringArray(Directory.EnumerateFiles(wwwroot, "*.css"));
            inlineScripts = Array.Empty<string>();
            this.instrumentationKey = instrumentationKey;
        }
        
        public string[] styles { get; set; }
        public string[] scripts { get; set; }
        public string[] inlineScripts { get; set; }
        public string instrumentationKey { get; set; }

        private string [] ToRelativeStringArray(IEnumerable<string> files){
            return files.OrderBy(_=>_).Select(file => $"/{Path.GetFileName(file)}?v={ComputeHash(file)}").ToArray();
        }

        private string ComputeHash(string file)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(File.ReadAllBytes(file));
            return BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();
        }
    }
}