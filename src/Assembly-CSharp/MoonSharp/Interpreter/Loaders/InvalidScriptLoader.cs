using System;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000CFD RID: 3325
	internal class InvalidScriptLoader : IScriptLoader
	{
		// Token: 0x06005D16 RID: 23830 RVA: 0x00262096 File Offset: 0x00260296
		internal InvalidScriptLoader(string frameworkname)
		{
			this.m_Error = string.Format("Loading scripts from files is not automatically supported on {0}. \r\nPlease implement your own IScriptLoader (possibly, extending ScriptLoaderBase for easier implementation),\r\nuse a preexisting loader like EmbeddedResourcesScriptLoader or UnityAssetsScriptLoader or load scripts from strings.", frameworkname);
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x002620AF File Offset: 0x002602AF
		public object LoadFile(string file, Table globalContext)
		{
			throw new PlatformNotSupportedException(this.m_Error);
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x001086F1 File Offset: 0x001068F1
		public string ResolveFileName(string filename, Table globalContext)
		{
			return filename;
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x002620AF File Offset: 0x002602AF
		public string ResolveModuleName(string modname, Table globalContext)
		{
			throw new PlatformNotSupportedException(this.m_Error);
		}

		// Token: 0x040053D2 RID: 21458
		private string m_Error;
	}
}
