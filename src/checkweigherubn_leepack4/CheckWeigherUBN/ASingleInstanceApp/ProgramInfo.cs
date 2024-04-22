using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CheckWeigherUBN.ASingleInstanceApp
{
	class ProgramInfo
	{
		static public string AssemblyGuid
		{
			get
			{
				object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
				if (attributes.Length == 0)
				{
					return String.Empty;
				}
				return ((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value;
			}
		}
	}
}
