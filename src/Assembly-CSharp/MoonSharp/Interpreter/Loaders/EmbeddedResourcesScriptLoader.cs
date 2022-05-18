using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x020010D9 RID: 4313
	public class EmbeddedResourcesScriptLoader : ScriptLoaderBase
	{
		// Token: 0x06006829 RID: 26665 RVA: 0x0028B010 File Offset: 0x00289210
		public EmbeddedResourcesScriptLoader(Assembly resourceAssembly = null)
		{
			if (resourceAssembly == null)
			{
				resourceAssembly = Assembly.GetCallingAssembly();
			}
			this.m_ResourceAssembly = resourceAssembly;
			this.m_Namespace = this.m_ResourceAssembly.FullName.Split(new char[]
			{
				','
			}).First<string>();
			this.m_ResourceNames = new HashSet<string>(this.m_ResourceAssembly.GetManifestResourceNames());
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x00047805 File Offset: 0x00045A05
		private string FileNameToResource(string file)
		{
			file = file.Replace('/', '.');
			file = file.Replace('\\', '.');
			return this.m_Namespace + "." + file;
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x00047830 File Offset: 0x00045A30
		public override bool ScriptFileExists(string name)
		{
			name = this.FileNameToResource(name);
			return this.m_ResourceNames.Contains(name);
		}

		// Token: 0x0600682C RID: 26668 RVA: 0x00047847 File Offset: 0x00045A47
		public override object LoadFile(string file, Table globalContext)
		{
			file = this.FileNameToResource(file);
			return this.m_ResourceAssembly.GetManifestResourceStream(file);
		}

		// Token: 0x04005FD6 RID: 24534
		private Assembly m_ResourceAssembly;

		// Token: 0x04005FD7 RID: 24535
		private HashSet<string> m_ResourceNames;

		// Token: 0x04005FD8 RID: 24536
		private string m_Namespace;
	}
}
