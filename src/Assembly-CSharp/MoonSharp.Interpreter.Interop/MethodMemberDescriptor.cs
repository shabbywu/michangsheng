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

namespace MoonSharp.Interpreter.Interop;

public class MethodMemberDescriptor : FunctionMemberDescriptorBase, IOptimizableDescriptor, IWireableDescriptor
{
	private Func<object, object[], object> m_OptimizedFunc;

	private Action<object, object[]> m_OptimizedAction;

	private bool m_IsAction;

	private bool m_IsArrayCtor;

	public MethodBase MethodInfo { get; private set; }

	public InteropAccessMode AccessMode { get; private set; }

	public bool IsConstructor { get; private set; }

	public MethodMemberDescriptor(MethodBase methodBase, InteropAccessMode accessMode = InteropAccessMode.Default)
	{
		CheckMethodIsCompatible(methodBase, throwException: true);
		IsConstructor = methodBase is ConstructorInfo;
		MethodInfo = methodBase;
		bool isStatic = methodBase.IsStatic || IsConstructor;
		if (IsConstructor)
		{
			m_IsAction = false;
		}
		else
		{
			m_IsAction = ((MethodInfo)methodBase).ReturnType == typeof(void);
		}
		ParameterInfo[] parameters = methodBase.GetParameters();
		ParameterDescriptor[] array;
		if (MethodInfo.DeclaringType.IsArray)
		{
			m_IsArrayCtor = true;
			int arrayRank = MethodInfo.DeclaringType.GetArrayRank();
			array = new ParameterDescriptor[arrayRank];
			for (int i = 0; i < arrayRank; i++)
			{
				array[i] = new ParameterDescriptor("idx" + i, typeof(int));
			}
		}
		else
		{
			array = parameters.Select((ParameterInfo pi) => new ParameterDescriptor(pi)).ToArray();
		}
		bool isExtensionMethod = methodBase.IsStatic && array.Length != 0 && methodBase.GetCustomAttributes(typeof(ExtensionAttribute), inherit: false).Any();
		Initialize(methodBase.Name, isStatic, array, isExtensionMethod);
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
		AccessMode = accessMode;
		if (AccessMode == InteropAccessMode.Preoptimized)
		{
			((IOptimizableDescriptor)this).Optimize();
		}
	}

	public static MethodMemberDescriptor TryCreateIfVisible(MethodBase methodBase, InteropAccessMode accessMode, bool forceVisibility = false)
	{
		if (!CheckMethodIsCompatible(methodBase, throwException: false))
		{
			return null;
		}
		if (forceVisibility || (methodBase.GetVisibilityFromAttributes() ?? methodBase.IsPublic))
		{
			return new MethodMemberDescriptor(methodBase, accessMode);
		}
		return null;
	}

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
		if (methodBase.GetParameters().Any((ParameterInfo p) => p.ParameterType.IsPointer))
		{
			if (throwException)
			{
				throw new ArgumentException("Method cannot contain pointer parameters");
			}
			return false;
		}
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
			if (Framework.Do.IsGenericTypeDefinition(methodInfo.ReturnType))
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

	public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
	{
		this.CheckAccess(MemberDescriptorAccess.CanExecute, obj);
		if (AccessMode == InteropAccessMode.LazyOptimized && m_OptimizedFunc == null && m_OptimizedAction == null)
		{
			((IOptimizableDescriptor)this).Optimize();
		}
		List<int> outParams = null;
		object[] array = base.BuildArgumentList(script, obj, context, args, out outParams);
		object obj2 = null;
		if (m_OptimizedFunc != null)
		{
			obj2 = m_OptimizedFunc(obj, array);
		}
		else if (m_OptimizedAction != null)
		{
			m_OptimizedAction(obj, array);
			obj2 = DynValue.Void;
		}
		else if (!m_IsAction)
		{
			obj2 = ((!IsConstructor) ? MethodInfo.Invoke(obj, array) : ((ConstructorInfo)MethodInfo).Invoke(array));
		}
		else
		{
			MethodInfo.Invoke(obj, array);
			obj2 = DynValue.Void;
		}
		return FunctionMemberDescriptorBase.BuildReturnValue(script, outParams, array, obj2);
	}

	void IOptimizableDescriptor.Optimize()
	{
		ParameterDescriptor[] parameters = base.Parameters;
		if (AccessMode == InteropAccessMode.Reflection)
		{
			return;
		}
		MethodInfo methodInfo = MethodInfo as MethodInfo;
		if (methodInfo == null)
		{
			return;
		}
		using (PerformanceStatistics.StartGlobalStopwatch(PerformanceCounter.AdaptersCompilation))
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), "pars");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "instance");
			UnaryExpression instance = Expression.Convert(parameterExpression2, MethodInfo.DeclaringType);
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
			Expression expression2 = ((!base.IsStatic) ? Expression.Call(instance, methodInfo, array) : Expression.Call(methodInfo, array));
			if (m_IsAction)
			{
				Expression<Action<object, object[]>> expression3 = Expression.Lambda<Action<object, object[]>>(expression2, new ParameterExpression[2] { parameterExpression2, parameterExpression });
				Interlocked.Exchange(ref m_OptimizedAction, expression3.Compile());
			}
			else
			{
				Expression<Func<object, object[], object>> expression4 = Expression.Lambda<Func<object, object[], object>>(Expression.Convert(expression2, typeof(object)), new ParameterExpression[2] { parameterExpression2, parameterExpression });
				Interlocked.Exchange(ref m_OptimizedFunc, expression4.Compile());
			}
		}
	}

	public void PrepareForWiring(Table t)
	{
		t.Set("class", DynValue.NewString(GetType().FullName));
		t.Set("name", DynValue.NewString(base.Name));
		t.Set("ctor", DynValue.NewBoolean(IsConstructor));
		t.Set("special", DynValue.NewBoolean(MethodInfo.IsSpecialName));
		t.Set("visibility", DynValue.NewString(MethodInfo.GetClrVisibility()));
		if (IsConstructor)
		{
			t.Set("ret", DynValue.NewString(((ConstructorInfo)MethodInfo).DeclaringType.FullName));
		}
		else
		{
			t.Set("ret", DynValue.NewString(((MethodInfo)MethodInfo).ReturnType.FullName));
		}
		if (m_IsArrayCtor)
		{
			t.Set("arraytype", DynValue.NewString(MethodInfo.DeclaringType.GetElementType().FullName));
		}
		t.Set("decltype", DynValue.NewString(MethodInfo.DeclaringType.FullName));
		t.Set("static", DynValue.NewBoolean(base.IsStatic));
		t.Set("extension", DynValue.NewBoolean(base.ExtensionMethodType != null));
		DynValue dynValue = DynValue.NewPrimeTable();
		t.Set("params", dynValue);
		int num = 0;
		ParameterDescriptor[] parameters = base.Parameters;
		foreach (ParameterDescriptor obj in parameters)
		{
			DynValue dynValue2 = DynValue.NewPrimeTable();
			dynValue.Table.Set(++num, dynValue2);
			obj.PrepareForWiring(dynValue2.Table);
		}
	}
}
