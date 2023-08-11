using System;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop;

public class StandardEnumUserDataDescriptor : DispatchingUserDataDescriptor
{
	private Func<object, ulong> m_EnumToULong;

	private Func<ulong, object> m_ULongToEnum;

	private Func<object, long> m_EnumToLong;

	private Func<long, object> m_LongToEnum;

	public Type UnderlyingType { get; private set; }

	public bool IsUnsigned { get; private set; }

	public bool IsFlags { get; private set; }

	public StandardEnumUserDataDescriptor(Type enumType, string friendlyName = null, string[] names = null, object[] values = null, Type underlyingType = null)
		: base(enumType, friendlyName)
	{
		if (!Framework.Do.IsEnum(enumType))
		{
			throw new ArgumentException("enumType must be an enum!");
		}
		UnderlyingType = underlyingType ?? Enum.GetUnderlyingType(enumType);
		IsUnsigned = UnderlyingType == typeof(byte) || UnderlyingType == typeof(ushort) || UnderlyingType == typeof(uint) || UnderlyingType == typeof(ulong);
		names = names ?? Enum.GetNames(base.Type);
		values = values ?? Enum.GetValues(base.Type).OfType<object>().ToArray();
		FillMemberList(names, values);
	}

	private void FillMemberList(string[] names, object[] values)
	{
		for (int i = 0; i < names.Length; i++)
		{
			string name = names[i];
			DynValue value = UserData.Create(values.GetValue(i), this);
			AddDynValue(name, value);
		}
		Attribute[] customAttributes = Framework.Do.GetCustomAttributes(base.Type, typeof(FlagsAttribute), inherit: true);
		if (customAttributes != null && customAttributes.Length != 0)
		{
			IsFlags = true;
			AddEnumMethod("flagsAnd", DynValue.NewCallback(Callback_And));
			AddEnumMethod("flagsOr", DynValue.NewCallback(Callback_Or));
			AddEnumMethod("flagsXor", DynValue.NewCallback(Callback_Xor));
			AddEnumMethod("flagsNot", DynValue.NewCallback(Callback_BwNot));
			AddEnumMethod("hasAll", DynValue.NewCallback(Callback_HasAll));
			AddEnumMethod("hasAny", DynValue.NewCallback(Callback_HasAny));
		}
	}

	private void AddEnumMethod(string name, DynValue dynValue)
	{
		if (!HasMember(name))
		{
			AddDynValue(name, dynValue);
		}
		if (!HasMember("__" + name))
		{
			AddDynValue("__" + name, dynValue);
		}
	}

	private long GetValueSigned(DynValue dv)
	{
		CreateSignedConversionFunctions();
		if (dv.Type == DataType.Number)
		{
			return (long)dv.Number;
		}
		if (dv.Type != DataType.UserData || dv.UserData.Descriptor != this || dv.UserData.Object == null)
		{
			throw new ScriptRuntimeException("Enum userdata or number expected, or enum is not of the correct type.");
		}
		return m_EnumToLong(dv.UserData.Object);
	}

	private ulong GetValueUnsigned(DynValue dv)
	{
		CreateUnsignedConversionFunctions();
		if (dv.Type == DataType.Number)
		{
			return (ulong)dv.Number;
		}
		if (dv.Type != DataType.UserData || dv.UserData.Descriptor != this || dv.UserData.Object == null)
		{
			throw new ScriptRuntimeException("Enum userdata or number expected, or enum is not of the correct type.");
		}
		return m_EnumToULong(dv.UserData.Object);
	}

	private DynValue CreateValueSigned(long value)
	{
		CreateSignedConversionFunctions();
		return UserData.Create(m_LongToEnum(value), this);
	}

	private DynValue CreateValueUnsigned(ulong value)
	{
		CreateUnsignedConversionFunctions();
		return UserData.Create(m_ULongToEnum(value), this);
	}

	private void CreateSignedConversionFunctions()
	{
		if (m_EnumToLong != null && m_LongToEnum != null)
		{
			return;
		}
		if (UnderlyingType == typeof(sbyte))
		{
			m_EnumToLong = (object o) => (sbyte)o;
			m_LongToEnum = (long o) => (sbyte)o;
			return;
		}
		if (UnderlyingType == typeof(short))
		{
			m_EnumToLong = (object o) => (short)o;
			m_LongToEnum = (long o) => (short)o;
			return;
		}
		if (UnderlyingType == typeof(int))
		{
			m_EnumToLong = (object o) => (int)o;
			m_LongToEnum = (long o) => (int)o;
			return;
		}
		if (UnderlyingType == typeof(long))
		{
			m_EnumToLong = (object o) => (long)o;
			m_LongToEnum = (long o) => o;
			return;
		}
		throw new ScriptRuntimeException("Unexpected enum underlying type : {0}", UnderlyingType.FullName);
	}

	private void CreateUnsignedConversionFunctions()
	{
		if (m_EnumToULong != null && m_ULongToEnum != null)
		{
			return;
		}
		if (UnderlyingType == typeof(byte))
		{
			m_EnumToULong = (object o) => (byte)o;
			m_ULongToEnum = (ulong o) => (byte)o;
			return;
		}
		if (UnderlyingType == typeof(ushort))
		{
			m_EnumToULong = (object o) => (ushort)o;
			m_ULongToEnum = (ulong o) => (ushort)o;
			return;
		}
		if (UnderlyingType == typeof(uint))
		{
			m_EnumToULong = (object o) => (uint)o;
			m_ULongToEnum = (ulong o) => (uint)o;
			return;
		}
		if (UnderlyingType == typeof(ulong))
		{
			m_EnumToULong = (object o) => (ulong)o;
			m_ULongToEnum = (ulong o) => o;
			return;
		}
		throw new ScriptRuntimeException("Unexpected enum underlying type : {0}", UnderlyingType.FullName);
	}

	private DynValue PerformBinaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long, DynValue> operation)
	{
		if (args.Count != 2)
		{
			throw new ScriptRuntimeException("Enum.{0} expects two arguments", funcName);
		}
		long valueSigned = GetValueSigned(args[0]);
		long valueSigned2 = GetValueSigned(args[1]);
		return operation(valueSigned, valueSigned2);
	}

	private DynValue PerformBinaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong, DynValue> operation)
	{
		if (args.Count != 2)
		{
			throw new ScriptRuntimeException("Enum.{0} expects two arguments", funcName);
		}
		ulong valueUnsigned = GetValueUnsigned(args[0]);
		ulong valueUnsigned2 = GetValueUnsigned(args[1]);
		return operation(valueUnsigned, valueUnsigned2);
	}

	private DynValue PerformBinaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long, long> operation)
	{
		return PerformBinaryOperationS(funcName, ctx, args, (long v1, long v2) => CreateValueSigned(operation(v1, v2)));
	}

	private DynValue PerformBinaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong, ulong> operation)
	{
		return PerformBinaryOperationU(funcName, ctx, args, (ulong v1, ulong v2) => CreateValueUnsigned(operation(v1, v2)));
	}

	private DynValue PerformUnaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long> operation)
	{
		if (args.Count != 1)
		{
			throw new ScriptRuntimeException("Enum.{0} expects one argument.", funcName);
		}
		long valueSigned = GetValueSigned(args[0]);
		long value = operation(valueSigned);
		return CreateValueSigned(value);
	}

	private DynValue PerformUnaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong> operation)
	{
		if (args.Count != 1)
		{
			throw new ScriptRuntimeException("Enum.{0} expects one argument.", funcName);
		}
		ulong valueUnsigned = GetValueUnsigned(args[0]);
		ulong value = operation(valueUnsigned);
		return CreateValueUnsigned(value);
	}

	internal DynValue Callback_Or(ScriptExecutionContext ctx, CallbackArguments args)
	{
		if (IsUnsigned)
		{
			return PerformBinaryOperationU("or", ctx, args, (ulong v1, ulong v2) => v1 | v2);
		}
		return PerformBinaryOperationS("or", ctx, args, (long v1, long v2) => v1 | v2);
	}

	internal DynValue Callback_And(ScriptExecutionContext ctx, CallbackArguments args)
	{
		if (IsUnsigned)
		{
			return PerformBinaryOperationU("and", ctx, args, (ulong v1, ulong v2) => v1 & v2);
		}
		return PerformBinaryOperationS("and", ctx, args, (long v1, long v2) => v1 & v2);
	}

	internal DynValue Callback_Xor(ScriptExecutionContext ctx, CallbackArguments args)
	{
		if (IsUnsigned)
		{
			return PerformBinaryOperationU("xor", ctx, args, (ulong v1, ulong v2) => v1 ^ v2);
		}
		return PerformBinaryOperationS("xor", ctx, args, (long v1, long v2) => v1 ^ v2);
	}

	internal DynValue Callback_BwNot(ScriptExecutionContext ctx, CallbackArguments args)
	{
		if (IsUnsigned)
		{
			return PerformUnaryOperationU("not", ctx, args, (ulong v1) => ~v1);
		}
		return PerformUnaryOperationS("not", ctx, args, (long v1) => ~v1);
	}

	internal DynValue Callback_HasAll(ScriptExecutionContext ctx, CallbackArguments args)
	{
		if (IsUnsigned)
		{
			return PerformBinaryOperationU("hasAll", ctx, args, (ulong v1, ulong v2) => DynValue.NewBoolean((v1 & v2) == v2));
		}
		return PerformBinaryOperationS("hasAll", ctx, args, (long v1, long v2) => DynValue.NewBoolean((v1 & v2) == v2));
	}

	internal DynValue Callback_HasAny(ScriptExecutionContext ctx, CallbackArguments args)
	{
		if (IsUnsigned)
		{
			return PerformBinaryOperationU("hasAny", ctx, args, (ulong v1, ulong v2) => DynValue.NewBoolean((v1 & v2) != 0));
		}
		return PerformBinaryOperationS("hasAny", ctx, args, (long v1, long v2) => DynValue.NewBoolean((v1 & v2) != 0));
	}

	public override bool IsTypeCompatible(Type type, object obj)
	{
		if (obj != null)
		{
			return base.Type == type;
		}
		return base.IsTypeCompatible(type, obj);
	}

	public override DynValue MetaIndex(Script script, object obj, string metaname)
	{
		if (metaname == "__concat" && IsFlags)
		{
			return DynValue.NewCallback(Callback_Or);
		}
		return null;
	}
}
