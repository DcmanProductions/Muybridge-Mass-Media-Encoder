using System;
using System.Reflection;
using System.Drawing;
using System.Windows;

namespace ChaseLabs.Updater
{
    public interface ISharpUpdateable
    {
        string ApplicationName { get; }
        string ApplicationID { get; }
        Assembly ApplicationAssembly { get; }
        System.Windows.Media.ImageSource ApplicationIcon { get; }
        Uri UpdateXmlLocation { get; }
        Window Context { get; }

    }
}
