using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D1B RID: 3355
	public abstract class FunctionMemberDescriptorBase : IOverloadableMemberDescriptor, IMemberDescriptor
	{
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06005DCF RID: 24015 RVA: 0x00264149 File Offset: 0x00262349
		// (set) Token: 0x06005DD0 RID: 24016 RVA: 0x00264151 File Offset: 0x00262351
		public bool IsStatic { get; private set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06005DD1 RID: 24017 RVA: 0x0026415A File Offset: 0x0026235A
		// (set) Token: 0x06005DD2 RID: 24018 RVA: 0x00264162 File Offset: 0x00262362
		public string Name { get; private set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0026416B File Offset: 0x0026236B
		// (set) Token: 0x06005DD4 RID: 24020 RVA: 0x00264173 File Offset: 0x00262373
		public string SortDiscriminant { get; private set; }

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x0026417C File Offset: 0x0026237C
		// (set) Token: 0x06005DD6 RID: 24022 RVA: 0x00264184 File Offset: 0x00262384
		public ParameterDescriptor[] Parameters { get; private set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x0026418D File Offset: 0x0026238D
		// (set) Token: 0x06005DD8 RID: 24024 RVA: 0x00264195 File Offset: 0x00262395
		public Type ExtensionMethodType { get; private set; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06005DD9 RID: 24025 RVA: 0x0026419E File Offset: 0x0026239E
		// (set) Token: 0x06005DDA RID: 24026 RVA: 0x002641A6 File Offset: 0x002623A6
		public Type VarArgsArrayType { get; private set; }

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06005DDB RID: 24027 RVA: 0x002641AF File Offset: 0x002623AF
		// (set) Token: 0x06005DDC RID: 24028 RVA: 0x002641B7 File Offset: 0x002623B7
		public Type VarArgsElementType { get; private set; }

		// Token: 0x06005DDD RID: 24029 RVA: 0x002641C0 File Offset: 0x002623C0
		protected void Initialize(string funcName, bool isStatic, ParameterDescriptor[] parameters, bool isExtensionMethod)
		{
			this.Name = funcName;
			this.IsStatic = isStatic;
			this.Parameters = parameters;
			if (isExtensionMethod)
			{
				this.ExtensionMethodType = this.Parameters[0].Type;
			}
			if (this.Parameters.Length != 0 && this.Parameters[this.Parameters.Length - 1].IsVarArgs)
			{
				this.VarArgsArrayType = this.Parameters[this.Parameters.Length - 1].Type;
				this.VarArgsElementType = this.Parameters[this.Parameters.Length - 1].Type.GetElementType();
			}
			this.SortDiscriminant = string.Join(":", (from pi in this.Parameters
			select pi.Type.FullName).ToArray<string>());
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x00264296 File Offset: 0x00262496
		public Func<ScriptExecutionContext, CallbackArguments, DynValue> GetCallback(Script script, object obj = null)
		{
			return (ScriptExecutionContext c, CallbackArguments a) => this.Execute(script, obj, c, a);
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x002642BD File Offset: 0x002624BD
		public CallbackFunction GetCallbackFunction(Script script, object obj = null)
		{
			return new CallbackFunction(this.GetCallback(script, obj), this.Name);
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x002642D2 File Offset: 0x002624D2
		public DynValue GetCallbackAsDynValue(Script script, object obj = null)
		{
			return DynValue.NewCallback(this.GetCallbackFunction(script, obj));
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x002642E1 File Offset: 0x002624E1
		public static DynValue CreateCallbackDynValue(Script script, MethodInfo mi, object obj = null)
		{
			return new MethodMemberDescriptor(mi, InteropAccessMode.Default).GetCallbackAsDynValue(script, obj);
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x002642F4 File Offset: 0x002624F4
		protected virtual object[] BuildArgumentList(Script script, object obj, ScriptExecutionContext context, CallbackArguments args, out List<int> outParams)
		{
			ParameterDescriptor[] parameters = this.Parameters;
			object[] array = new object[parameters.Length];
			int num = args.IsMethodCall ? 1 : 0;
			outParams = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (parameters[i].Type.IsByRef)
				{
					if (outParams == null)
					{
						outParams = new List<int>();
					}
					outParams.Add(i);
				}
				if (this.ExtensionMethodType != null && obj != null && i == 0)
				{
					array[i] = obj;
				}
				else if (parameters[i].Type == typeof(Script))
				{
					array[i] = script;
				}
				else if (parameters[i].Type == typeof(ScriptExecutionContext))
				{
					array[i] = context;
				}
				else if (parameters[i].Type == typeof(CallbackArguments))
				{
					array[i] = args.SkipMethodCall();
				}
				else if (parameters[i].IsOut)
				{
					array[i] = null;
				}
				else if (i == parameters.Length - 1 && this.VarArgsArrayType != null)
				{
					List<DynValue> list = new List<DynValue>();
					for (;;)
					{
						DynValue dynValue = args.RawGet(num, false);
						num++;
						if (dynValue == null)
						{
							break;
						}
						list.Add(dynValue);
					}
					if (list.Count == 1)
					{
						DynValue dynValue2 = list[0];
						if (dynValue2.Type == DataType.UserData && dynValue2.UserData.Object != null && Framework.Do.IsAssignableFrom(this.VarArgsArrayType, dynValue2.UserData.Object.GetType()))
						{
							array[i] = dynValue2.UserData.Object;
							goto IL_218;
						}
					}
					Array array2 = Array.CreateInstance(this.VarArgsElementType, list.Count);
					for (int j = 0; j < list.Count; j++)
					{
						array2.SetValue(ScriptToClrConversions.DynValueToObjectOfType(list[j], this.VarArgsElementType, null, false), j);
					}
					array[i] = array2;
				}
				else
				{
					DynValue value = args.RawGet(num, false) ?? DynValue.Void;
					array[i] = ScriptToClrConversions.DynValueToObjectOfType(value, parameters[i].Type, parameters[i].DefaultValue, parameters[i].HasDefaultValue);
					num++;
				}
				IL_218:;
			}
			return array;
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x00264528 File Offset: 0x00262728
		protected static DynValue BuildReturnValue(Script script, List<int> outParams, object[] pars, object retv)
		{
			if (outParams == null)
			{
				return ClrToScriptConversions.ObjectToDynValue(script, retv);
			}
			DynValue[] array = new DynValue[outParams.Count + 1];
			if (retv is DynValue && ((DynValue)retv).IsVoid())
			{
				array[0] = DynValue.Nil;
			}
			else
			{
				array[0] = ClrToScriptConversions.ObjectToDynValue(script, retv);
			}
			for (int i = 0; i < outParams.Count; i++)
			{
				array[i + 1] = ClrToScriptConversions.ObjectToDynValue(script, pars[outParams[i]]);
			}
			return DynValue.NewTuple(array);
		}

		// Token: 0x06005DE4 RID: 24036
		public abstract DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args);

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06005DE5 RID: 24037 RVA: 0x0016F21F File Offset: 0x0016D41F
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;
			}
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x002645A2 File Offset: 0x002627A2
		public virtual DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			return this.GetCallbackAsDynValue(script, obj);
		}

		// Token: 0x06005DE7 RID: 24039 RVA: 0x002645B4 File Offset: 0x002627B4
		public virtual void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}
	}
}
