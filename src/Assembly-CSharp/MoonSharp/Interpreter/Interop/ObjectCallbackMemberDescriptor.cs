using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02001102 RID: 4354
	public class ObjectCallbackMemberDescriptor : FunctionMemberDescriptorBase
	{
		// Token: 0x0600691D RID: 26909 RVA: 0x000480B2 File Offset: 0x000462B2
		public ObjectCallbackMemberDescriptor(string funcName) : this(funcName, (object o, ScriptExecutionContext c, CallbackArguments a) => DynValue.Void, new ParameterDescriptor[0])
		{
		}

		// Token: 0x0600691E RID: 26910 RVA: 0x000480E0 File Offset: 0x000462E0
		public ObjectCallbackMemberDescriptor(string funcName, Func<object, ScriptExecutionContext, CallbackArguments, object> callBack) : this(funcName, callBack, new ParameterDescriptor[0])
		{
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x000480F0 File Offset: 0x000462F0
		public ObjectCallbackMemberDescriptor(string funcName, Func<object, ScriptExecutionContext, CallbackArguments, object> callBack, ParameterDescriptor[] parameters)
		{
			this.m_CallbackFunc = callBack;
			base.Initialize(funcName, false, parameters, false);
		}

		// Token: 0x06006920 RID: 26912 RVA: 0x0028CF58 File Offset: 0x0028B158
		public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			if (this.m_CallbackFunc != null)
			{
				object obj2 = this.m_CallbackFunc(obj, context, args);
				return ClrToScriptConversions.ObjectToDynValue(script, obj2);
			}
			return DynValue.Void;
		}

		// Token: 0x04006031 RID: 24625
		private Func<object, ScriptExecutionContext, CallbackArguments, object> m_CallbackFunc;
	}
}
