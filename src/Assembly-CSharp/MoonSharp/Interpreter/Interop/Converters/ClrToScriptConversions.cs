using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x02001142 RID: 4418
	internal static class ClrToScriptConversions
	{
		// Token: 0x06006B39 RID: 27449 RVA: 0x00292968 File Offset: 0x00290B68
		internal static DynValue TryObjectToTrivialDynValue(Script script, object obj)
		{
			if (obj == null)
			{
				return DynValue.Nil;
			}
			if (obj is DynValue)
			{
				return (DynValue)obj;
			}
			Type type = obj.GetType();
			if (obj is bool)
			{
				return DynValue.NewBoolean((bool)obj);
			}
			if (obj is string || obj is StringBuilder || obj is char)
			{
				return DynValue.NewString(obj.ToString());
			}
			if (NumericConversions.NumericTypes.Contains(type))
			{
				return DynValue.NewNumber(NumericConversions.TypeToDouble(type, obj));
			}
			if (obj is Table)
			{
				return DynValue.NewTable((Table)obj);
			}
			return null;
		}

		// Token: 0x06006B3A RID: 27450 RVA: 0x002929FC File Offset: 0x00290BFC
		internal static DynValue TryObjectToSimpleDynValue(Script script, object obj)
		{
			if (obj == null)
			{
				return DynValue.Nil;
			}
			if (obj is DynValue)
			{
				return (DynValue)obj;
			}
			Func<Script, object, DynValue> clrToScriptCustomConversion = Script.GlobalOptions.CustomConverters.GetClrToScriptCustomConversion(obj.GetType());
			if (clrToScriptCustomConversion != null)
			{
				DynValue dynValue = clrToScriptCustomConversion(script, obj);
				if (dynValue != null)
				{
					return dynValue;
				}
			}
			Type type = obj.GetType();
			if (obj is bool)
			{
				return DynValue.NewBoolean((bool)obj);
			}
			if (obj is string || obj is StringBuilder || obj is char)
			{
				return DynValue.NewString(obj.ToString());
			}
			if (obj is Closure)
			{
				return DynValue.NewClosure((Closure)obj);
			}
			if (NumericConversions.NumericTypes.Contains(type))
			{
				return DynValue.NewNumber(NumericConversions.TypeToDouble(type, obj));
			}
			if (obj is Table)
			{
				return DynValue.NewTable((Table)obj);
			}
			if (obj is CallbackFunction)
			{
				return DynValue.NewCallback((CallbackFunction)obj);
			}
			if (obj is Delegate)
			{
				Delegate @delegate = (Delegate)obj;
				if (CallbackFunction.CheckCallbackSignature(@delegate.Method, false))
				{
					return DynValue.NewCallback((Func<ScriptExecutionContext, CallbackArguments, DynValue>)@delegate, null);
				}
			}
			return null;
		}

		// Token: 0x06006B3B RID: 27451 RVA: 0x00292B08 File Offset: 0x00290D08
		internal static DynValue ObjectToDynValue(Script script, object obj)
		{
			DynValue dynValue = ClrToScriptConversions.TryObjectToSimpleDynValue(script, obj);
			if (dynValue != null)
			{
				return dynValue;
			}
			dynValue = UserData.Create(obj);
			if (dynValue != null)
			{
				return dynValue;
			}
			if (obj is Type)
			{
				dynValue = UserData.CreateStatic(obj as Type);
			}
			if (obj is Enum)
			{
				return DynValue.NewNumber(NumericConversions.TypeToDouble(Enum.GetUnderlyingType(obj.GetType()), obj));
			}
			if (dynValue != null)
			{
				return dynValue;
			}
			if (obj is Delegate)
			{
				return DynValue.NewCallback(CallbackFunction.FromDelegate(script, (Delegate)obj, InteropAccessMode.Default));
			}
			if (obj is MethodInfo)
			{
				MethodInfo methodInfo = (MethodInfo)obj;
				if (methodInfo.IsStatic)
				{
					return DynValue.NewCallback(CallbackFunction.FromMethodInfo(script, methodInfo, null, InteropAccessMode.Default));
				}
			}
			if (obj is IList)
			{
				return DynValue.NewTable(TableConversions.ConvertIListToTable(script, (IList)obj));
			}
			if (obj is IDictionary)
			{
				return DynValue.NewTable(TableConversions.ConvertIDictionaryToTable(script, (IDictionary)obj));
			}
			DynValue dynValue2 = ClrToScriptConversions.EnumerationToDynValue(script, obj);
			if (dynValue2 != null)
			{
				return dynValue2;
			}
			throw ScriptRuntimeException.ConvertObjectFailed(obj);
		}

		// Token: 0x06006B3C RID: 27452 RVA: 0x00292BF0 File Offset: 0x00290DF0
		public static DynValue EnumerationToDynValue(Script script, object obj)
		{
			if (obj is IEnumerable)
			{
				IEnumerable enumerable = (IEnumerable)obj;
				return EnumerableWrapper.ConvertIterator(script, enumerable.GetEnumerator());
			}
			if (obj is IEnumerator)
			{
				IEnumerator enumerator = (IEnumerator)obj;
				return EnumerableWrapper.ConvertIterator(script, enumerator);
			}
			return null;
		}
	}
}
