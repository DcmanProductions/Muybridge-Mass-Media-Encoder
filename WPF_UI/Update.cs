using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using ChaseLabs.Updater;
namespace MMME
{
    class Update : ISharpUpdateable
    {
        public string ApplicationName => "Muybridge Mass Media Encoder";

        public string ApplicationID => "mmme";

        public Assembly ApplicationAssembly => Assembly.GetExecutingAssembly();

        public System.Drawing.Icon ApplicationIcon => ApplicationIcon;

        public Uri UpdateXmlLocation => new Uri("https://raw.githubusercontent.com/DcmanProductions/Muybridge-Mass-Media-Encoder/master/update.xml");

        public Window Context => throw new NotImplementedException();

        ImageSource ISharpUpdateable.ApplicationIcon => throw new NotImplementedException();
    }
}
