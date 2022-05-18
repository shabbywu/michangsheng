using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02001151 RID: 4433
	public interface IOverloadableMemberDescriptor : IMemberDescriptor
	{
		// Token: 0x06006B98 RID: 27544
		DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args);

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06006B99 RID: 27545
		Type ExtensionMethodType { get; }

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06006B9A RID: 27546
		ParameterDescriptor[] Parameters { get; }

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06006B9B RID: 27547
		Type VarArgsArrayType { get; }

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06006B9C RID: 27548
		Type VarArgsElementType { get; }

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06006B9D RID: 27549
		string SortDiscriminant { get; }
	}
}
