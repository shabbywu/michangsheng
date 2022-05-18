using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010FF RID: 4351
	public abstract class FunctionMemberDescriptorBase : IOverloadableMemberDescriptor, IMemberDescriptor
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060068FE RID: 26878 RVA: 0x00047F90 File Offset: 0x00046190
		// (set) Token: 0x060068FF RID: 26879 RVA: 0x00047F98 File Offset: 0x00046198
		public bool IsStatic { get; private set; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06006900 RID: 26880 RVA: 0x00047FA1 File Offset: 0x000461A1
		// (set) Token: 0x06006901 RID: 26881 RVA: 0x00047FA9 File Offset: 0x000461A9
		public string Name { get; private set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06006902 RID: 26882 RVA: 0x00047FB2 File Offset: 0x000461B2
		// (set) Token: 0x06006903 RID: 26883 RVA: 0x00047FBA File Offset: 0x000461BA
		public string SortDiscriminant { get; private set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06006904 RID: 26884 RVA: 0x00047FC3 File Offset: 0x000461C3
		// (set) Token: 0x06006905 RID: 26885 RVA: 0x00047FCB File Offset: 0x000461CB
		public ParameterDescriptor[] Parameters { get; private set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06006906 RID: 26886 RVA: 0x00047FD4 File Offset: 0x000461D4
		// (set) Token: 0x06006907 RID: 26887 RVA: 0x00047FDC File Offset: 0x000461DC
		public Type ExtensionMethodType { get; private set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06006908 RID: 26888 RVA: 0x00047FE5 File Offset: 0x000461E5
		// (set) Token: 0x06006909 RID: 26889 RVA: 0x00047FED File Offset: 0x000461ED
		public Type VarArgsArrayType { get; private set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600690A RID: 26890 RVA: 0x00047FF6 File Offset: 0x000461F6
		// (set) Token: 0x0600690B RID: 26891 RVA: 0x00047FFE File Offset: 0x000461FE
		public Type VarArgsElementType { get; private set; }

		// Token: 0x0600690C RID: 26892 RVA: 0x0028CBD0 File Offset: 0x0028ADD0
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

		// Token: 0x0600690D RID: 26893 RVA: 0x00048007 File Offset: 0x00046207
		public Func<ScriptExecutionContext, CallbackArguments, DynValue> GetCallback(Script script, object obj = null)
		{
			return (ScriptExecutionContext c, CallbackArguments a) => this.Execute(script, obj, c, a);
		}

		// Token: 0x0600690E RID: 26894 RVA: 0x0004802E File Offset: 0x0004622E
		public CallbackFunction GetCallbackFunction(Script script, object obj = null)
		{
			return new CallbackFunction(this.GetCallback(script, obj), this.Name);
		}

		// Token: 0x0600690F RID: 26895 RVA: 0x00048043 File Offset: 0x00046243
		public DynValue GetCallbackAsDynValue(Script script, object obj = null)
		{
			return DynValue.NewCallback(this.GetCallbackFunction(script, obj));
		}

		// Token: 0x06006910 RID: 26896 RVA: 0x00048052 File Offset: 0x00046252
		public static DynValue CreateCallbackDynValue(Script script, MethodInfo mi, object obj = null)
		{
			return new MethodMemberDescriptor(mi, InteropAccessMode.Default).GetCallbackAsDynValue(script, obj);
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x0028CCA8 File Offset: 0x0028AEA8
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

		// Token: 0x06006912 RID: 26898 RVA: 0x0028CEDC File Offset: 0x0028B0DC
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

		// Token: 0x06006913 RID: 26899
		public abstract DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args);

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06006914 RID: 26900 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;
			}
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x00048062 File Offset: 0x00046262
		public virtual DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			return this.GetCallbackAsDynValue(script, obj);
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x00048074 File Offset: 0x00046274
		public virtual void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}
	}
}
