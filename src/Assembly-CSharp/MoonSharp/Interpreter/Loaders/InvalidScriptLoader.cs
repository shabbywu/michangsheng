using System;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x020010DB RID: 4315
	internal class InvalidScriptLoader : IScriptLoader
	{
		// Token: 0x06006830 RID: 26672 RVA: 0x00047871 File Offset: 0x00045A71
		internal InvalidScriptLoader(string frameworkname)
		{
			this.m_Error = string.Format("Loading scripts from files is not automatically supported on {0}. \r\nPlease implement your own IScriptLoader (possibly, extending ScriptLoaderBase for easier implementation),\r\nuse a preexisting loader like EmbeddedResourcesScriptLoader or UnityAssetsScriptLoader or load scripts from strings.", frameworkname);
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x0004788A File Offset: 0x00045A8A
		public object LoadFile(string file, Table globalContext)
		{
			throw new PlatformNotSupportedException(this.m_Error);
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		public string ResolveFileName(string filename, Table globalContext)
		{
			return filename;
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x0004788A File Offset: 0x00045A8A
		public string ResolveModuleName(string modname, Table globalContext)
		{
			throw new PlatformNotSupportedException(this.m_Error);
		}

		// Token: 0x04005FD9 RID: 24537
		private string m_Error;
	}
}
