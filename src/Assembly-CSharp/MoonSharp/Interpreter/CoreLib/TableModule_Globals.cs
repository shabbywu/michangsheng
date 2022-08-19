using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D83 RID: 3459
	[MoonSharpModule]
	public class TableModule_Globals
	{
		// Token: 0x06006249 RID: 25161 RVA: 0x00277BC1 File Offset: 0x00275DC1
		[MoonSharpModuleMethod]
		public static DynValue unpack(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return TableModule.unpack(executionContext, args);
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x00277BCA File Offset: 0x00275DCA
		[MoonSharpModuleMethod]
		public static DynValue pack(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return TableModule.pack(executionContext, args);
		}
	}
}
