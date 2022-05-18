using System;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02001123 RID: 4387
	public class StandardEnumUserDataDescriptor : DispatchingUserDataDescriptor
	{
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06006A04 RID: 27140 RVA: 0x000485D1 File Offset: 0x000467D1
		// (set) Token: 0x06006A05 RID: 27141 RVA: 0x000485D9 File Offset: 0x000467D9
		public Type UnderlyingType { get; private set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06006A06 RID: 27142 RVA: 0x000485E2 File Offset: 0x000467E2
		// (set) Token: 0x06006A07 RID: 27143 RVA: 0x000485EA File Offset: 0x000467EA
		public bool IsUnsigned { get; private set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06006A08 RID: 27144 RVA: 0x000485F3 File Offset: 0x000467F3
		// (set) Token: 0x06006A09 RID: 27145 RVA: 0x000485FB File Offset: 0x000467FB
		public bool IsFlags { get; private set; }

		// Token: 0x06006A0A RID: 27146 RVA: 0x0028F3D4 File Offset: 0x0028D5D4
		public StandardEnumUserDataDescriptor(Type enumType, string friendlyName = null, string[] names = null, object[] values = null, Type underlyingType = null) : base(enumType, friendlyName)
		{
			if (!Framework.Do.IsEnum(enumType))
			{
				throw new ArgumentException("enumType must be an enum!");
			}
			this.UnderlyingType = (underlyingType ?? Enum.GetUnderlyingType(enumType));
			this.IsUnsigned = (this.UnderlyingType == typeof(byte) || this.UnderlyingType == typeof(ushort) || this.UnderlyingType == typeof(uint) || this.UnderlyingType == typeof(ulong));
			names = (names ?? Enum.GetNames(base.Type));
			values = (values ?? Enum.GetValues(base.Type).OfType<object>().ToArray<object>());
			this.FillMemberList(names, values);
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x0028F4B0 File Offset: 0x0028D6B0
		private void FillMemberList(string[] names, object[] values)
		{
			for (int i = 0; i < names.Length; i++)
			{
				string name = names[i];
				DynValue value = UserData.Create(values.GetValue(i), this);
				base.AddDynValue(name, value);
			}
			Attribute[] customAttributes = Framework.Do.GetCustomAttributes(base.Type, typeof(FlagsAttribute), true);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				this.IsFlags = true;
				this.AddEnumMethod("flagsAnd", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_And), null));
				this.AddEnumMethod("flagsOr", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_Or), null));
				this.AddEnumMethod("flagsXor", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_Xor), null));
				this.AddEnumMethod("flagsNot", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_BwNot), null));
				this.AddEnumMethod("hasAll", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_HasAll), null));
				this.AddEnumMethod("hasAny", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_HasAny), null));
			}
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x00048604 File Offset: 0x00046804
		private void AddEnumMethod(string name, DynValue dynValue)
		{
			if (!base.HasMember(name))
			{
				base.AddDynValue(name, dynValue);
			}
			if (!base.HasMember("__" + name))
			{
				base.AddDynValue("__" + name, dynValue);
			}
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x0028F5C4 File Offset: 0x0028D7C4
		private long GetValueSigned(DynValue dv)
		{
			this.CreateSignedConversionFunctions();
			if (dv.Type == DataType.Number)
			{
				return (long)dv.Number;
			}
			if (dv.Type != DataType.UserData || dv.UserData.Descriptor != this || dv.UserData.Object == null)
			{
				throw new ScriptRuntimeException("Enum userdata or number expected, or enum is not of the correct type.");
			}
			return this.m_EnumToLong(dv.UserData.Object);
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x0028F630 File Offset: 0x0028D830
		private ulong GetValueUnsigned(DynValue dv)
		{
			this.CreateUnsignedConversionFunctions();
			if (dv.Type == DataType.Number)
			{
				return (ulong)dv.Number;
			}
			if (dv.Type != DataType.UserData || dv.UserData.Descriptor != this || dv.UserData.Object == null)
			{
				throw new ScriptRuntimeException("Enum userdata or number expected, or enum is not of the correct type.");
			}
			return this.m_EnumToULong(dv.UserData.Object);
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x0004863C File Offset: 0x0004683C
		private DynValue CreateValueSigned(long value)
		{
			this.CreateSignedConversionFunctions();
			return UserData.Create(this.m_LongToEnum(value), this);
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x00048656 File Offset: 0x00046856
		private DynValue CreateValueUnsigned(ulong value)
		{
			this.CreateUnsignedConversionFunctions();
			return UserData.Create(this.m_ULongToEnum(value), this);
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x0028F69C File Offset: 0x0028D89C
		private void CreateSignedConversionFunctions()
		{
			if (this.m_EnumToLong != null && this.m_LongToEnum != null)
			{
				return;
			}
			if (this.UnderlyingType == typeof(sbyte))
			{
				this.m_EnumToLong = ((object o) => (long)((sbyte)o));
				this.m_LongToEnum = ((long o) => (sbyte)o);
				return;
			}
			if (this.UnderlyingType == typeof(short))
			{
				this.m_EnumToLong = ((object o) => (long)((short)o));
				this.m_LongToEnum = ((long o) => (short)o);
				return;
			}
			if (this.UnderlyingType == typeof(int))
			{
				this.m_EnumToLong = ((object o) => (long)((int)o));
				this.m_LongToEnum = ((long o) => (int)o);
				return;
			}
			if (this.UnderlyingType == typeof(long))
			{
				this.m_EnumToLong = ((object o) => (long)o);
				this.m_LongToEnum = ((long o) => o);
				return;
			}
			throw new ScriptRuntimeException("Unexpected enum underlying type : {0}", new object[]
			{
				this.UnderlyingType.FullName
			});
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x0028F864 File Offset: 0x0028DA64
		private void CreateUnsignedConversionFunctions()
		{
			if (this.m_EnumToULong != null && this.m_ULongToEnum != null)
			{
				return;
			}
			if (this.UnderlyingType == typeof(byte))
			{
				this.m_EnumToULong = ((object o) => (ulong)((byte)o));
				this.m_ULongToEnum = ((ulong o) => (byte)o);
				return;
			}
			if (this.UnderlyingType == typeof(ushort))
			{
				this.m_EnumToULong = ((object o) => (ulong)((ushort)o));
				this.m_ULongToEnum = ((ulong o) => (ushort)o);
				return;
			}
			if (this.UnderlyingType == typeof(uint))
			{
				this.m_EnumToULong = ((object o) => (ulong)((uint)o));
				this.m_ULongToEnum = ((ulong o) => (uint)o);
				return;
			}
			if (this.UnderlyingType == typeof(ulong))
			{
				this.m_EnumToULong = ((object o) => (ulong)o);
				this.m_ULongToEnum = ((ulong o) => o);
				return;
			}
			throw new ScriptRuntimeException("Unexpected enum underlying type : {0}", new object[]
			{
				this.UnderlyingType.FullName
			});
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x0028FA2C File Offset: 0x0028DC2C
		private DynValue PerformBinaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long, DynValue> operation)
		{
			if (args.Count != 2)
			{
				throw new ScriptRuntimeException("Enum.{0} expects two arguments", new object[]
				{
					funcName
				});
			}
			long valueSigned = this.GetValueSigned(args[0]);
			long valueSigned2 = this.GetValueSigned(args[1]);
			return operation(valueSigned, valueSigned2);
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x0028FA7C File Offset: 0x0028DC7C
		private DynValue PerformBinaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong, DynValue> operation)
		{
			if (args.Count != 2)
			{
				throw new ScriptRuntimeException("Enum.{0} expects two arguments", new object[]
				{
					funcName
				});
			}
			ulong valueUnsigned = this.GetValueUnsigned(args[0]);
			ulong valueUnsigned2 = this.GetValueUnsigned(args[1]);
			return operation(valueUnsigned, valueUnsigned2);
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x0028FACC File Offset: 0x0028DCCC
		private DynValue PerformBinaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long, long> operation)
		{
			return this.PerformBinaryOperationS(funcName, ctx, args, (long v1, long v2) => this.CreateValueSigned(operation(v1, v2)));
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x0028FB04 File Offset: 0x0028DD04
		private DynValue PerformBinaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong, ulong> operation)
		{
			return this.PerformBinaryOperationU(funcName, ctx, args, (ulong v1, ulong v2) => this.CreateValueUnsigned(operation(v1, v2)));
		}

		// Token: 0x06006A17 RID: 27159 RVA: 0x0028FB3C File Offset: 0x0028DD3C
		private DynValue PerformUnaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long> operation)
		{
			if (args.Count != 1)
			{
				throw new ScriptRuntimeException("Enum.{0} expects one argument.", new object[]
				{
					funcName
				});
			}
			long valueSigned = this.GetValueSigned(args[0]);
			long value = operation(valueSigned);
			return this.CreateValueSigned(value);
		}

		// Token: 0x06006A18 RID: 27160 RVA: 0x0028FB88 File Offset: 0x0028DD88
		private DynValue PerformUnaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong> operation)
		{
			if (args.Count != 1)
			{
				throw new ScriptRuntimeException("Enum.{0} expects one argument.", new object[]
				{
					funcName
				});
			}
			ulong valueUnsigned = this.GetValueUnsigned(args[0]);
			ulong value = operation(valueUnsigned);
			return this.CreateValueUnsigned(value);
		}

		// Token: 0x06006A19 RID: 27161 RVA: 0x0028FBD4 File Offset: 0x0028DDD4
		internal DynValue Callback_Or(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("or", ctx, args, (ulong v1, ulong v2) => v1 | v2);
			}
			return this.PerformBinaryOperationS("or", ctx, args, (long v1, long v2) => v1 | v2);
		}

		// Token: 0x06006A1A RID: 27162 RVA: 0x0028FC44 File Offset: 0x0028DE44
		internal DynValue Callback_And(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("and", ctx, args, (ulong v1, ulong v2) => v1 & v2);
			}
			return this.PerformBinaryOperationS("and", ctx, args, (long v1, long v2) => v1 & v2);
		}

		// Token: 0x06006A1B RID: 27163 RVA: 0x0028FCB4 File Offset: 0x0028DEB4
		internal DynValue Callback_Xor(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("xor", ctx, args, (ulong v1, ulong v2) => v1 ^ v2);
			}
			return this.PerformBinaryOperationS("xor", ctx, args, (long v1, long v2) => v1 ^ v2);
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x0028FD24 File Offset: 0x0028DF24
		internal DynValue Callback_BwNot(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformUnaryOperationU("not", ctx, args, (ulong v1) => ~v1);
			}
			return this.PerformUnaryOperationS("not", ctx, args, (long v1) => ~v1);
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x0028FD94 File Offset: 0x0028DF94
		internal DynValue Callback_HasAll(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("hasAll", ctx, args, (ulong v1, ulong v2) => DynValue.NewBoolean((v1 & v2) == v2));
			}
			return this.PerformBinaryOperationS("hasAll", ctx, args, (long v1, long v2) => DynValue.NewBoolean((v1 & v2) == v2));
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x0028FE04 File Offset: 0x0028E004
		internal DynValue Callback_HasAny(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("hasAny", ctx, args, (ulong v1, ulong v2) => DynValue.NewBoolean((v1 & v2) > 0UL));
			}
			return this.PerformBinaryOperationS("hasAny", ctx, args, (long v1, long v2) => DynValue.NewBoolean((v1 & v2) != 0L));
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x00048670 File Offset: 0x00046870
		public override bool IsTypeCompatible(Type type, object obj)
		{
			if (obj != null)
			{
				return base.Type == type;
			}
			return base.IsTypeCompatible(type, obj);
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x0004868A File Offset: 0x0004688A
		public override DynValue MetaIndex(Script script, object obj, string metaname)
		{
			if (metaname == "__concat" && this.IsFlags)
			{
				return DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_Or), null);
			}
			return null;
		}

		// Token: 0x04006078 RID: 24696
		private Func<object, ulong> m_EnumToULong;

		// Token: 0x04006079 RID: 24697
		private Func<ulong, object> m_ULongToEnum;

		// Token: 0x0400607A RID: 24698
		private Func<object, long> m_EnumToLong;

		// Token: 0x0400607B RID: 24699
		private Func<long, object> m_LongToEnum;
	}
}
