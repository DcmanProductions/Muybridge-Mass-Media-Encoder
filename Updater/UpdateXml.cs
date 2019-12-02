using System;
using System.Net;
using System.Xml;

namespace ChaseLabs.Updater
{
    internal class UpdateXml
    {
        private Version version;
        private Uri uri;
        private string fileName;
        private string md5;
        private string changelog;
        private string launchArgs;

        internal Version Version { get => version; set => version = value; }
        internal Uri Uri { get => uri; set => uri = value; }
        internal string FileName { get => fileName; set => fileName = value; }
        internal string Md5 { get => md5; set => md5 = value; }
        internal string Changelog { get => changelog; set => changelog = value; }
        internal string LaunchArgs { get => launchArgs; set => launchArgs = value; }

        internal UpdateXml(Version version, Uri uri, string fileName, string md5, string changelog, string launchArgs)
        {
            this.version = version;
            this.uri = uri;
            this.fileName = fileName;
            this.md5 = md5;
            this.changelog = changelog;
            this.launchArgs = launchArgs;
        }

        internal bool IsNewerThan(Version version)
        {
            return this.version > version;
        }

        internal static bool ExistsOnServer(Uri location)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(location.AbsoluteUri);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                resp.Close();
                return resp.StatusCode == HttpStatusCode.OK;
            }
            catch { return false; }
        }

        internal static UpdateXml Parse(Uri location, string appID)
        {
            Version version = null;
            string url = "", fileName = "", md5 = "", changelog = "", launchArgs = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(location.AbsoluteUri);

                XmlNode node = doc.DocumentElement.SelectSingleNode($"//update[@appId='{appID}'");

                if (node == null)
                    return null;

                version = Version.Parse(node["version"].InnerText);
                url = node["url"].InnerText;
                fileName = node["fileName"].InnerText;
                md5 = node["md5"].InnerText;
                changelog = node["changelog"].InnerText;
                launchArgs = node["launchArgs"].InnerText;
                return new UpdateXml(version, new Uri(url), fileName, md5, changelog, launchArgs);
            }
            catch
            {
                return null;
            }
        }
    }

}
