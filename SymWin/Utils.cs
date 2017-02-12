/*
 * © Marcus van Houdt 2014
 */

using System.IO;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace SymWin
{
   internal static class Utils
   {
      public static T CloneWPFObject<T>(T o)
      {
         using (var xmlReader = XmlReader.Create(new StringReader(XamlWriter.Save(o))))
            return (T)XamlReader.Load(xmlReader);
      }
   }
}
