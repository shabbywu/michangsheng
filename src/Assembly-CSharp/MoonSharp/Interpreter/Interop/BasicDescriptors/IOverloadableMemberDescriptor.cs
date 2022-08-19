using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000D43 RID: 3395
	public interface IOverloadableMemberDescriptor : IMemberDescriptor
	{
		// Token: 0x06005FB4 RID: 24500
		DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args);

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06005FB5 RID: 24501
		Type ExtensionMethodType { get; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06005FB6 RID: 24502
		ParameterDescriptor[] Parameters { get; }

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06005FB7 RID: 24503
		Type VarArgsArrayType { get; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06005FB8 RID: 24504
		Type VarArgsElementType { get; }

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06005FB9 RID: 24505
		string SortDiscriminant { get; }
	}
}
