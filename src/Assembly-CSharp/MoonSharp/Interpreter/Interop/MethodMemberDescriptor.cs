using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Diagnostics;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D20 RID: 3360
	public class MethodMemberDescriptor : FunctionMemberDescriptorBase, IOptimizableDescriptor, IWireableDescriptor
	{
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06005E1D RID: 24093 RVA: 0x00265291 File Offset: 0x00263491
		// (set) Token: 0x06005E1E RID: 24094 RVA: 0x00265299 File Offset: 0x00263499
		public MethodBase MethodInfo { get; private set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x002652A2 File Offset: 0x002634A2
		// (set) Token: 0x06005E20 RID: 24096 RVA: 0x002652AA File Offset: 0x002634AA
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06005E21 RID: 24097 RVA: 0x002652B3 File Offset: 0x002634B3
		// (set) Token: 0x06005E22 RID: 24098 RVA: 0x002652BB File Offset: 0x002634BB
		public bool IsConstructor { get; private set; }

		// Token: 0x06005E23 RID: 24099 RVA: 0x002652C4 File Offset: 0x002634C4
		public MethodMemberDescriptor(MethodBase methodBase, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			MethodMemberDescriptor.CheckMethodIsCompatible(methodBase, true);
			this.IsConstructor = (methodBase is ConstructorInfo);
			this.MethodInfo = methodBase;
			bool isStatic = methodBase.IsStatic || this.IsConstructor;
			if (this.IsConstructor)
			{
				this.m_IsAction = false;
			}
			else
			{
				this.m_IsAction = (((MethodInfo)methodBase).ReturnType == typeof(void));
			}
			ParameterInfo[] parameters = methodBase.GetParameters();
			ParameterDescriptor[] array;
			if (this.MethodInfo.DeclaringType.IsArray)
			{
				this.m_IsArrayCtor = true;
				int arrayRank = this.MethodInfo.DeclaringType.GetArrayRank();
				array = new ParameterDescriptor[arrayRank];
				for (int i = 0; i < arrayRank; i++)
				{
					array[i] = new ParameterDescriptor("idx" + i.ToString(), typeof(int), false, null, false, false, false);
				}
			}
			else
			{
				array = (from pi in parameters
				select new ParameterDescriptor(pi)).ToArray<ParameterDescriptor>();
			}
			bool isExtensionMethod = methodBase.IsStatic && array.Length != 0 && methodBase.GetCustomAttributes(typeof(ExtensionAttribute), false).Any<object>();
			base.Initialize(methodBase.Name, isStatic, array, isExtensionMethod);
			if (Script.GlobalOptions.Platform.IsRunningOnAOT())
			{
				accessMode = InteropAccessMode.Reflection;
			}
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = UserData.DefaultAccessMode;
			}
			if (accessMode == InteropAccessMode.HideMembers)
			{
				throw new ArgumentException("Invalid accessMode");
			}
			if (array.Any((ParameterDescriptor p) => p.Type.IsByRef))
			{
				accessMode = InteropAccessMode.Reflection;
			}
			this.AccessMode = accessMode;
			if (this.AccessMode == InteropAccessMode.Preoptimized)
			{
				((IOptimizableDescriptor)this).Optimize();
			}
		}

		// Token: 0x06005E24 RID: 24100 RVA: 0x0026547C File Offset: 0x0026367C
		public static MethodMemberDescriptor TryCreateIfVisible(MethodBase methodBase, InteropAccessMode accessMode, bool forceVisibility = false)
		{
			if (!MethodMemberDescriptor.CheckMethodIsCompatible(methodBase, false))
			{
				return null;
			}
			if (forceVisibility || (methodBase.GetVisibilityFromAttributes() ?? methodBase.IsPublic))
			{
				return new MethodMemberDescriptor(methodBase, accessMode);
			}
			return null;
		}

		// Token: 0x06005E25 RID: 24101 RVA: 0x002654C4 File Offset: 0x002636C4
		public static bool CheckMethodIsCompatible(MethodBase methodBase, bool throwException)
		{
			if (methodBase.ContainsGenericParameters)
			{
				if (throwException)
				{
					throw new ArgumentException("Method cannot contain unresolved generic parameters");
				}
				return false;
			}
			else
			{
				if (!methodBase.GetParameters().Any((ParameterInfo p) => p.ParameterType.IsPointer))
				{
					MethodInfo methodInfo = methodBase as MethodInfo;
					if (methodInfo != null)
					{
						if (methodInfo.ReturnType.IsPointer)
						{
							if (throwException)
							{
								throw new ArgumentException("Method cannot have a pointer return type");
							}
							return false;
						}
						else if (Framework.Do.IsGenericTypeDefinition(methodInfo.ReturnType))
						{
							if (throwException)
							{
								throw new ArgumentException("Method cannot have an unresolved generic return type");
							}
							return false;
						}
					}
					return true;
				}
				if (throwException)
				{
					throw new ArgumentException("Method cannot contain pointer parameters");
				}
				return false;
			}
		}

		// Token: 0x06005E26 RID: 24102 RVA: 0x00265578 File Offset: 0x00263778
		public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			this.CheckAccess(MemberDescriptorAccess.CanExecute, obj);
			if (this.AccessMode == InteropAccessMode.LazyOptimized && this.m_OptimizedFunc == null && this.m_OptimizedAction == null)
			{
				((IOptimizableDescriptor)this).Optimize();
			}
			List<int> outParams = null;
			object[] array = base.BuildArgumentList(script, obj, context, args, out outParams);
			object retv;
			if (this.m_OptimizedFunc != null)
			{
				retv = this.m_OptimizedFunc(obj, array);
			}
			else if (this.m_OptimizedAction != null)
			{
				this.m_OptimizedAction(obj, array);
				retv = DynValue.Void;
			}
			else if (this.m_IsAction)
			{
				this.MethodInfo.Invoke(obj, array);
				retv = DynValue.Void;
			}
			else if (this.IsConstructor)
			{
				retv = ((ConstructorInfo)this.MethodInfo).Invoke(array);
			}
			else
			{
				retv = this.MethodInfo.Invoke(obj, array);
			}
			return FunctionMemberDescriptorBase.BuildReturnValue(script, outParams, array, retv);
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x00265644 File Offset: 0x00263844
		void IOptimizableDescriptor.Optimize()
		{
			ParameterDescriptor[] parameters = base.Parameters;
			if (this.AccessMode == InteropAccessMode.Reflection)
			{
				return;
			}
			MethodInfo methodInfo = this.MethodInfo as MethodInfo;
			if (methodInfo == null)
			{
				return;
			}
			using (PerformanceStatistics.StartGlobalStopwatch(PerformanceCounter.AdaptersCompilation))
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), "pars");
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "instance");
				UnaryExpression instance = Expression.Convert(parameterExpression2, this.MethodInfo.DeclaringType);
				Expression[] array = new Expression[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameters[i].OriginalType.IsByRef)
					{
						throw new InternalErrorException("Out/Ref params cannot be precompiled.");
					}
					BinaryExpression expression = Expression.ArrayIndex(parameterExpression, Expression.Constant(i));
					array[i] = Expression.Convert(expression, parameters[i].OriginalType);
				}
				Expression expression2;
				if (base.IsStatic)
				{
					expression2 = Expression.Call(methodInfo, array);
				}
				else
				{
					expression2 = Expression.Call(instance, methodInfo, array);
				}
				if (this.m_IsAction)
				{
					Expression<Action<object, object[]>> expression3 = Expression.Lambda<Action<object, object[]>>(expression2, new ParameterExpression[]
					{
						parameterExpression2,
						parameterExpression
					});
					Interlocked.Exchange<Action<object, object[]>>(ref this.m_OptimizedAction, expression3.Compile());
				}
				else
				{
					Expression<Func<object, object[], object>> expression4 = Expression.Lambda<Func<object, object[], object>>(Expression.Convert(expression2, typeof(object)), new ParameterExpression[]
					{
						parameterExpression2,
						parameterExpression
					});
					Interlocked.Exchange<Func<object, object[], object>>(ref this.m_OptimizedFunc, expression4.Compile());
				}
			}
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x002657D4 File Offset: 0x002639D4
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("name", DynValue.NewString(base.Name));
			t.Set("ctor", DynValue.NewBoolean(this.IsConstructor));
			t.Set("special", DynValue.NewBoolean(this.MethodInfo.IsSpecialName));
			t.Set("visibility", DynValue.NewString(this.MethodInfo.GetClrVisibility()));
			if (this.IsConstructor)
			{
				t.Set("ret", DynValue.NewString(((ConstructorInfo)this.MethodInfo).DeclaringType.FullName));
			}
			else
			{
				t.Set("ret", DynValue.NewString(((MethodInfo)this.MethodInfo).ReturnType.FullName));
			}
			if (this.m_IsArrayCtor)
			{
				t.Set("arraytype", DynValue.NewString(this.MethodInfo.DeclaringType.GetElementType().FullName));
			}
			t.Set("decltype", DynValue.NewString(this.MethodInfo.DeclaringType.FullName));
			t.Set("static", DynValue.NewBoolean(base.IsStatic));
			t.Set("extension", DynValue.NewBoolean(base.ExtensionMethodType != null));
			DynValue dynValue = DynValue.NewPrimeTable();
			t.Set("params", dynValue);
			int num = 0;
			foreach (ParameterDescriptor parameterDescriptor in base.Parameters)
			{
				DynValue dynValue2 = DynValue.NewPrimeTable();
				dynValue.Table.Set(++num, dynValue2);
				parameterDescriptor.PrepareForWiring(dynValue2.Table);
			}
		}

		// Token: 0x0400542C RID: 21548
		private Func<object, object[], object> m_OptimizedFunc;

		// Token: 0x0400542D RID: 21549
		private Action<object, object[]> m_OptimizedAction;

		// Token: 0x0400542E RID: 21550
		private bool m_IsAction;

		// Token: 0x0400542F RID: 21551
		private bool m_IsArrayCtor;
	}
}
