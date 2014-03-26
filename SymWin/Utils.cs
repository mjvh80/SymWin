using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

      public static TVisual Clone<TVisual>(this TVisual visual) where TVisual : Visual 
      {
         return CloneWPFObject(visual);
      }
   }
}
