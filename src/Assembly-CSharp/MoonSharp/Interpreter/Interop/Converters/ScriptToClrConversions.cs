using System;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x02001144 RID: 4420
	internal static class ScriptToClrConversions
	{
		// Token: 0x06006B40 RID: 27456 RVA: 0x00292F6C File Offset: 0x0029116C
		internal static object DynValueToObject(DynValue value)
		{
			Func<DynValue, object> scriptToClrCustomConversion = Script.GlobalOptions.CustomConverters.GetScriptToClrCustomConversion(value.Type, typeof(object));
			if (scriptToClrCustomConversion != null)
			{
				object obj = scriptToClrCustomConversion(value);
				if (obj != null)
				{
					return obj;
				}
			}
			switch (value.Type)
			{
			case DataType.Nil:
			case DataType.Void:
				return null;
			case DataType.Boolean:
				return value.Boolean;
			case DataType.Number:
				return value.Number;
			case DataType.String:
				return value.String;
			case DataType.Function:
				return value.Function;
			case DataType.Table:
				return value.Table;
			case DataType.Tuple:
				return value.Tuple;
			case DataType.UserData:
				if (value.UserData.Object != null)
				{
					return value.UserData.Object;
				}
				if (value.UserData.Descriptor != null)
				{
					return value.UserData.Descriptor.Type;
				}
				return null;
			case DataType.ClrFunction:
				return value.Callback;
			}
			throw ScriptRuntimeException.ConvertObjectFailed(value.Type);
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x00293068 File Offset: 0x00291268
		internal static object DynValueToObjectOfType(DynValue value, Type desiredType, object defaultValue, bool isOptional)
		{
			if (desiredType.IsByRef)
			{
				desiredType = desiredType.GetElementType();
			}
			Func<DynValue, object> scriptToClrCustomConversion = Script.GlobalOptions.CustomConverters.GetScriptToClrCustomConversion(value.Type, desiredType);
			if (scriptToClrCustomConversion != null)
			{
				object obj = scriptToClrCustomConversion(value);
				if (obj != null)
				{
					return obj;
				}
			}
			if (desiredType == typeof(DynValue))
			{
				return value;
			}
			if (desiredType == typeof(object))
			{
				return ScriptToClrConversions.DynValueToObject(value);
			}
			StringConversions.StringSubtype stringSubtype = StringConversions.GetStringSubtype(desiredType);
			string text = null;
			Type underlyingType = Nullable.GetUnderlyingType(desiredType);
			Type left = null;
			if (underlyingType != null)
			{
				left = desiredType;
				desiredType = underlyingType;
			}
			switch (value.Type)
			{
			case DataType.Nil:
				if (!Framework.Do.IsValueType(desiredType))
				{
					return null;
				}
				if (left != null)
				{
					return null;
				}
				if (isOptional)
				{
					return defaultValue;
				}
				break;
			case DataType.Void:
				if (isOptional)
				{
					return defaultValue;
				}
				if (!Framework.Do.IsValueType(desiredType) || left != null)
				{
					return null;
				}
				break;
			case DataType.Boolean:
				if (desiredType == typeof(bool))
				{
					return value.Boolean;
				}
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					text = value.Boolean.ToString();
				}
				break;
			case DataType.Number:
				if (Framework.Do.IsEnum(desiredType))
				{
					return NumericConversions.DoubleToType(Enum.GetUnderlyingType(desiredType), value.Number);
				}
				if (NumericConversions.NumericTypes.Contains(desiredType))
				{
					return NumericConversions.DoubleToType(desiredType, value.Number);
				}
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					text = value.Number.ToString();
				}
				break;
			case DataType.String:
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					text = value.String;
				}
				break;
			case DataType.Function:
				if (desiredType == typeof(Closure))
				{
					return value.Function;
				}
				if (desiredType == typeof(ScriptFunctionDelegate))
				{
					return value.Function.GetDelegate();
				}
				break;
			case DataType.Table:
			{
				if (desiredType == typeof(Table) || Framework.Do.IsAssignableFrom(desiredType, typeof(Table)))
				{
					return value.Table;
				}
				object obj2 = TableConversions.ConvertTableToType(value.Table, desiredType);
				if (obj2 != null)
				{
					return obj2;
				}
				break;
			}
			case DataType.UserData:
				if (value.UserData.Object != null)
				{
					object @object = value.UserData.Object;
					IUserDataDescriptor descriptor = value.UserData.Descriptor;
					if (descriptor.IsTypeCompatible(desiredType, @object))
					{
						return @object;
					}
					if (stringSubtype != StringConversions.StringSubtype.None)
					{
						text = descriptor.AsString(@object);
					}
				}
				break;
			case DataType.ClrFunction:
				if (desiredType == typeof(CallbackFunction))
				{
					return value.Callback;
				}
				if (desiredType == typeof(Func<ScriptExecutionContext, CallbackArguments, DynValue>))
				{
					return value.Callback.ClrCallback;
				}
				break;
			}
			if (stringSubtype != StringConversions.StringSubtype.None && text != null)
			{
				return StringConversions.ConvertString(stringSubtype, text, desiredType, value.Type);
			}
			throw ScriptRuntimeException.ConvertObjectFailed(value.Type, desiredType);
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x00293340 File Offset: 0x00291540
		internal static int DynValueToObjectOfTypeWeight(DynValue value, Type desiredType, bool isOptional)
		{
			if (desiredType.IsByRef)
			{
				desiredType = desiredType.GetElementType();
			}
			if (Script.GlobalOptions.CustomConverters.GetScriptToClrCustomConversion(value.Type, desiredType) != null)
			{
				return 100;
			}
			if (desiredType == typeof(DynValue))
			{
				return 100;
			}
			if (desiredType == typeof(object))
			{
				return 100;
			}
			StringConversions.StringSubtype stringSubtype = StringConversions.GetStringSubtype(desiredType);
			Type underlyingType = Nullable.GetUnderlyingType(desiredType);
			Type left = null;
			if (underlyingType != null)
			{
				left = desiredType;
				desiredType = underlyingType;
			}
			switch (value.Type)
			{
			case DataType.Nil:
				if (!Framework.Do.IsValueType(desiredType))
				{
					return 100;
				}
				if (left != null)
				{
					return 100;
				}
				if (isOptional)
				{
					return 25;
				}
				break;
			case DataType.Void:
				if (isOptional)
				{
					return 50;
				}
				if (!Framework.Do.IsValueType(desiredType) || left != null)
				{
					return 25;
				}
				break;
			case DataType.Boolean:
				if (desiredType == typeof(bool))
				{
					return 100;
				}
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					return 5;
				}
				break;
			case DataType.Number:
				if (Framework.Do.IsEnum(desiredType))
				{
					return 90;
				}
				if (NumericConversions.NumericTypes.Contains(desiredType))
				{
					return ScriptToClrConversions.GetNumericTypeWeight(desiredType);
				}
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					return 50;
				}
				break;
			case DataType.String:
				if (stringSubtype == StringConversions.StringSubtype.String)
				{
					return 100;
				}
				if (stringSubtype == StringConversions.StringSubtype.StringBuilder)
				{
					return 99;
				}
				if (stringSubtype == StringConversions.StringSubtype.Char)
				{
					return 98;
				}
				break;
			case DataType.Function:
				if (desiredType == typeof(Closure))
				{
					return 100;
				}
				if (desiredType == typeof(ScriptFunctionDelegate))
				{
					return 100;
				}
				break;
			case DataType.Table:
				if (desiredType == typeof(Table) || Framework.Do.IsAssignableFrom(desiredType, typeof(Table)))
				{
					return 100;
				}
				if (TableConversions.CanConvertTableToType(value.Table, desiredType))
				{
					return 90;
				}
				break;
			case DataType.UserData:
				if (value.UserData.Object != null)
				{
					object @object = value.UserData.Object;
					if (value.UserData.Descriptor.IsTypeCompatible(desiredType, @object))
					{
						return 100;
					}
					if (stringSubtype != StringConversions.StringSubtype.None)
					{
						return 5;
					}
				}
				break;
			case DataType.ClrFunction:
				if (desiredType == typeof(CallbackFunction))
				{
					return 100;
				}
				if (desiredType == typeof(Func<ScriptExecutionContext, CallbackArguments, DynValue>))
				{
					return 100;
				}
				break;
			}
			return 0;
		}

		// Token: 0x06006B43 RID: 27459 RVA: 0x0004918D File Offset: 0x0004738D
		private static int GetNumericTypeWeight(Type desiredType)
		{
			if (desiredType == typeof(double) || desiredType == typeof(decimal))
			{
				return 100;
			}
			return 99;
		}

		// Token: 0x040060DF RID: 24799
		internal const int WEIGHT_MAX_VALUE = 100;

		// Token: 0x040060E0 RID: 24800
		internal const int WEIGHT_CUSTOM_CONVERTER_MATCH = 100;

		// Token: 0x040060E1 RID: 24801
		internal const int WEIGHT_EXACT_MATCH = 100;

		// Token: 0x040060E2 RID: 24802
		internal const int WEIGHT_STRING_TO_STRINGBUILDER = 99;

		// Token: 0x040060E3 RID: 24803
		internal const int WEIGHT_STRING_TO_CHAR = 98;

		// Token: 0x040060E4 RID: 24804
		internal const int WEIGHT_NIL_TO_NULLABLE = 100;

		// Token: 0x040060E5 RID: 24805
		internal const int WEIGHT_NIL_TO_REFTYPE = 100;

		// Token: 0x040060E6 RID: 24806
		internal const int WEIGHT_VOID_WITH_DEFAULT = 50;

		// Token: 0x040060E7 RID: 24807
		internal const int WEIGHT_VOID_WITHOUT_DEFAULT = 25;

		// Token: 0x040060E8 RID: 24808
		internal const int WEIGHT_NIL_WITH_DEFAULT = 25;

		// Token: 0x040060E9 RID: 24809
		internal const int WEIGHT_BOOL_TO_STRING = 5;

		// Token: 0x040060EA RID: 24810
		internal const int WEIGHT_NUMBER_TO_STRING = 50;

		// Token: 0x040060EB RID: 24811
		internal const int WEIGHT_NUMBER_TO_ENUM = 90;

		// Token: 0x040060EC RID: 24812
		internal const int WEIGHT_USERDATA_TO_STRING = 5;

		// Token: 0x040060ED RID: 24813
		internal const int WEIGHT_TABLE_CONVERSION = 90;

		// Token: 0x040060EE RID: 24814
		internal const int WEIGHT_NUMBER_DOWNCAST = 99;

		// Token: 0x040060EF RID: 24815
		internal const int WEIGHT_NO_MATCH = 0;

		// Token: 0x040060F0 RID: 24816
		internal const int WEIGHT_NO_EXTRA_PARAMS_BONUS = 100;

		// Token: 0x040060F1 RID: 24817
		internal const int WEIGHT_EXTRA_PARAMS_MALUS = 2;

		// Token: 0x040060F2 RID: 24818
		internal const int WEIGHT_BYREF_BONUSMALUS = -10;

		// Token: 0x040060F3 RID: 24819
		internal const int WEIGHT_VARARGS_MALUS = 1;

		// Token: 0x040060F4 RID: 24820
		internal const int WEIGHT_VARARGS_EMPTY = 40;
	}
}
