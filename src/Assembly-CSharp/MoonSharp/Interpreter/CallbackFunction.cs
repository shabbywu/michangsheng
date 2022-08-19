using System;
using System.Collections.Generic;
using System.Reflection;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C97 RID: 3223
	public sealed class CallbackFunction : RefIdObject
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060059E2 RID: 23010 RVA: 0x00256E83 File Offset: 0x00255083
		// (set) Token: 0x060059E3 RID: 23011 RVA: 0x00256E8B File Offset: 0x0025508B
		public string Name { get; private set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060059E4 RID: 23012 RVA: 0x00256E94 File Offset: 0x00255094
		// (set) Token: 0x060059E5 RID: 23013 RVA: 0x00256E9C File Offset: 0x0025509C
		public Func<ScriptExecutionContext, CallbackArguments, DynValue> ClrCallback { get; private set; }

		// Token: 0x060059E6 RID: 23014 RVA: 0x00256EA5 File Offset: 0x002550A5
		public CallbackFunction(Func<ScriptExecutionContext, CallbackArguments, DynValue> callBack, string name = null)
		{
			this.ClrCallback = callBack;
			this.Name = name;
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x00256EBC File Offset: 0x002550BC
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

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060059E8 RID: 23016 RVA: 0x00256F1A File Offset: 0x0025511A
		// (set) Token: 0x060059E9 RID: 23017 RVA: 0x00256F21 File Offset: 0x00255121
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

		// Token: 0x060059EA RID: 23018 RVA: 0x00256F40 File Offset: 0x00255140
		public static CallbackFunction FromDelegate(Script script, Delegate del, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = CallbackFunction.m_DefaultAccessMode;
			}
			return new MethodMemberDescriptor(del.Method, accessMode).GetCallbackFunction(script, del.Target);
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x00256F65 File Offset: 0x00255165
		public static CallbackFunction FromMethodInfo(Script script, MethodInfo mi, object obj = null, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = CallbackFunction.m_DefaultAccessMode;
			}
			return new MethodMemberDescriptor(mi, accessMode).GetCallbackFunction(script, obj);
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060059EC RID: 23020 RVA: 0x00256F80 File Offset: 0x00255180
		// (set) Token: 0x060059ED RID: 23021 RVA: 0x00256F88 File Offset: 0x00255188
		public object AdditionalData { get; set; }

		// Token: 0x060059EE RID: 23022 RVA: 0x00256F94 File Offset: 0x00255194
		public static bool CheckCallbackSignature(MethodInfo mi, bool requirePublicVisibility)
		{
			ParameterInfo[] parameters = mi.GetParameters();
			return parameters.Length == 2 && parameters[0].ParameterType == typeof(ScriptExecutionContext) && parameters[1].ParameterType == typeof(CallbackArguments) && mi.ReturnType == typeof(DynValue) && (requirePublicVisibility || mi.IsPublic);
		}

		// Token: 0x04005250 RID: 21072
		private static InteropAccessMode m_DefaultAccessMode = InteropAccessMode.LazyOptimized;
	}
}
