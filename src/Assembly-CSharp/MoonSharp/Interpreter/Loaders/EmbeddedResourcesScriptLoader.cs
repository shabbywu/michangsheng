using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000CFB RID: 3323
	public class EmbeddedResourcesScriptLoader : ScriptLoaderBase
	{
		// Token: 0x06005D0F RID: 23823 RVA: 0x00261FC4 File Offset: 0x002601C4
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

		// Token: 0x06005D10 RID: 23824 RVA: 0x0026202A File Offset: 0x0026022A
		private string FileNameToResource(string file)
		{
			file = file.Replace('/', '.');
			file = file.Replace('\\', '.');
			return this.m_Namespace + "." + file;
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x00262055 File Offset: 0x00260255
		public override bool ScriptFileExists(string name)
		{
			name = this.FileNameToResource(name);
			return this.m_ResourceNames.Contains(name);
		}

		// Token: 0x06005D12 RID: 23826 RVA: 0x0026206C File Offset: 0x0026026C
		public override object LoadFile(string file, Table globalContext)
		{
			file = this.FileNameToResource(file);
			return this.m_ResourceAssembly.GetManifestResourceStream(file);
		}

		// Token: 0x040053CF RID: 21455
		private Assembly m_ResourceAssembly;

		// Token: 0x040053D0 RID: 21456
		private HashSet<string> m_ResourceNames;

		// Token: 0x040053D1 RID: 21457
		private string m_Namespace;
	}
}
