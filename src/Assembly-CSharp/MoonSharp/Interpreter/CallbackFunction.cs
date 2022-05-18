using System;
using System.Collections.Generic;
using System.Reflection;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200105D RID: 4189
	public sealed class CallbackFunction : RefIdObject
	{
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060064A4 RID: 25764 RVA: 0x000452A9 File Offset: 0x000434A9
		// (set) Token: 0x060064A5 RID: 25765 RVA: 0x000452B1 File Offset: 0x000434B1
		public string Name { get; private set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060064A6 RID: 25766 RVA: 0x000452BA File Offset: 0x000434BA
		// (set) Token: 0x060064A7 RID: 25767 RVA: 0x000452C2 File Offset: 0x000434C2
		public Func<ScriptExecutionContext, CallbackArguments, DynValue> ClrCallback { get; private set; }

		// Token: 0x060064A8 RID: 25768 RVA: 0x000452CB File Offset: 0x000434CB
		public CallbackFunction(Func<ScriptExecutionContext, CallbackArguments, DynValue> callBack, string name = null)
		{
			this.ClrCallback = callBack;
			this.Name = name;
		}

		// Token: 0x060064A9 RID: 25769 RVA: 0x00281D80 File Offset: 0x0027FF80
		public DynValue Invoke(ScriptExecutionContext executionContext, IList<DynValue> args, bool isMethodCall = false)
		{
			if (isMethodCall)
			{
				ColonOperatorBehaviour colonOperatorClrCallbackBehaviour = executionContext.GetScript().Options.ColonOperatorClrCallbackBehaviour;
				if (colonOperatorClrCallbackBehaviour == ColonOperatorBehaviour.TreatAsColon)
				{
					isMethodCall = false;
				}
				else if (colonOperatorClrCallbackBehaviour == ColonOperatorBehaviour.TreatAsDotOnUserData)
				{
					isMethodCall = (args.Count > 0 && args[0].Type == DataType.UserData);
				}
			}
			return this.ClrCallback(executionContext, new CallbackArguments(args, isMethodCall));
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060064AA RID: 25770 RVA: 0x000452E1 File Offset: 0x000434E1
		// (set) Token: 0x060064AB RID: 25771 RVA: 0x000452E8 File Offset: 0x000434E8
		public static InteropAccessMode DefaultAccessMode
		{
			get
			{
				return CallbackFunction.m_DefaultAccessMode;
			}
			set
			{
				if (value == InteropAccessMode.Default || value == InteropAccessMode.HideMembers || value == InteropAccessMode.BackgroundOptimized)
				{
					throw new ArgumentException("DefaultAccessMode");
				}
				CallbackFunction.m_DefaultAccessMode = value;
			}
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x00045307 File Offset: 0x00043507
		public static CallbackFunction FromDelegate(Script script, Delegate del, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = CallbackFunction.m_DefaultAccessMode;
			}
			return new MethodMemberDescriptor(del.Method, accessMode).GetCallbackFunction(script, del.Target);
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x0004532C File Offset: 0x0004352C
		public static CallbackFunction FromMethodInfo(Script script, MethodInfo mi, object obj = null, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = CallbackFunction.m_DefaultAccessMode;
			}
			return new MethodMemberDescriptor(mi, accessMode).GetCallbackFunction(script, obj);
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060064AE RID: 25774 RVA: 0x00045347 File Offset: 0x00043547
		// (set) Token: 0x060064AF RID: 25775 RVA: 0x0004534F File Offset: 0x0004354F
		public object AdditionalData { get; set; }

		// Token: 0x060064B0 RID: 25776 RVA: 0x00281DE0 File Offset: 0x0027FFE0
		public static bool CheckCallbackSignature(MethodInfo mi, bool requirePublicVisibility)
		{
			ParameterInfo[] parameters = mi.GetParameters();
			return parameters.Length == 2 && parameters[0].ParameterType == typeof(ScriptExecutionContext) && parameters[1].ParameterType == typeof(CallbackArguments) && mi.ReturnType == typeof(DynValue) && (requirePublicVisibility || mi.IsPublic);
		}

		// Token: 0x04005DFF RID: 24063
		private static InteropAccessMode m_DefaultAccessMode = InteropAccessMode.LazyOptimized;
	}
}
