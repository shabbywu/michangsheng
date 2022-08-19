using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D1C RID: 3356
	public class ObjectCallbackMemberDescriptor : FunctionMemberDescriptorBase
	{
		// Token: 0x06005DE9 RID: 24041 RVA: 0x002645BE File Offset: 0x002627BE
		public ObjectCallbackMemberDescriptor(string funcName) : this(funcName, (object o, ScriptExecutionContext c, CallbackArguments a) => DynValue.Void, new ParameterDescriptor[0])
		{
		}

		// Token: 0x06005DEA RID: 24042 RVA: 0x002645EC File Offset: 0x002627EC
		public ObjectCallbackMemberDescriptor(string funcName, Func<object, ScriptExecutionContext, CallbackArguments, object> callBack) : this(funcName, callBack, new ParameterDescriptor[0])
		{
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x002645FC File Offset: 0x002627FC
		public ObjectCallbackMemberDescriptor(string funcName, Func<object, ScriptExecutionContext, CallbackArguments, object> callBack, ParameterDescriptor[] parameters)
		{
			this.m_CallbackFunc = callBack;
			base.Initialize(funcName, false, parameters, false);
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x00264618 File Offset: 0x00262818
		public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			if (this.m_CallbackFunc != null)
			{
				object obj2 = this.m_CallbackFunc(obj, context, args);
				return ClrToScriptConversions.ObjectToDynValue(script, obj2);
			}
			return DynValue.Void;
		}

		// Token: 0x04005415 RID: 21525
		private Func<object, ScriptExecutionContext, CallbackArguments, object> m_CallbackFunc;
	}
}
