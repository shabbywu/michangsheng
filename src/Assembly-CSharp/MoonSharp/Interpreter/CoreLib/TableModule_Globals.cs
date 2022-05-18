using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x020011A1 RID: 4513
	[MoonSharpModule]
	public class TableModule_Globals
	{
		// Token: 0x06006E72 RID: 28274 RVA: 0x0004B3FF File Offset: 0x000495FF
		[MoonSharpModuleMethod]
		public static DynValue unpack(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return TableModule.unpack(executionContext, args);
		}

		// Token: 0x06006E73 RID: 28275 RVA: 0x0004B408 File Offset: 0x00049608
		[MoonSharpModuleMethod]
		public static DynValue pack(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return TableModule.pack(executionContext, args);
		}
	}
}
