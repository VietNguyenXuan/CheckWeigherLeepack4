using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlacialComponents.Controls
{
  public class GLEmdeddedcontrolTypes
  {
  }
  public class EmbeddedComboxData
  {
    public object Tag = null;
    public List<string> ComboxData = new List<string>();

    public EmbeddedComboxData(object tag, List<string> comboxData)
    {
      Tag = tag;
      ComboxData = comboxData;
    }
  }
}
