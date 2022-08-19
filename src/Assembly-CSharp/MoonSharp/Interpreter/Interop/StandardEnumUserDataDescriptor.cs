using System;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D24 RID: 3364
	public class StandardEnumUserDataDescriptor : DispatchingUserDataDescriptor
	{
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06005E68 RID: 24168 RVA: 0x00266B21 File Offset: 0x00264D21
		// (set) Token: 0x06005E69 RID: 24169 RVA: 0x00266B29 File Offset: 0x00264D29
		public Type UnderlyingType { get; private set; }

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06005E6A RID: 24170 RVA: 0x00266B32 File Offset: 0x00264D32
		// (set) Token: 0x06005E6B RID: 24171 RVA: 0x00266B3A File Offset: 0x00264D3A
		public bool IsUnsigned { get; private set; }

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06005E6C RID: 24172 RVA: 0x00266B43 File Offset: 0x00264D43
		// (set) Token: 0x06005E6D RID: 24173 RVA: 0x00266B4B File Offset: 0x00264D4B
		public bool IsFlags { get; private set; }

		// Token: 0x06005E6E RID: 24174 RVA: 0x00266B54 File Offset: 0x00264D54
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

		// Token: 0x06005E6F RID: 24175 RVA: 0x00266C30 File Offset: 0x00264E30
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

		// Token: 0x06005E70 RID: 24176 RVA: 0x00266D43 File Offset: 0x00264F43
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

		// Token: 0x06005E71 RID: 24177 RVA: 0x00266D7C File Offset: 0x00264F7C
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

		// Token: 0x06005E72 RID: 24178 RVA: 0x00266DE8 File Offset: 0x00264FE8
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

		// Token: 0x06005E73 RID: 24179 RVA: 0x00266E51 File Offset: 0x00265051
		private DynValue CreateValueSigned(long value)
		{
			this.CreateSignedConversionFunctions();
			return UserData.Create(this.m_LongToEnum(value), this);
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x00266E6B File Offset: 0x0026506B
		private DynValue CreateValueUnsigned(ulong value)
		{
			this.CreateUnsignedConversionFunctions();
			return UserData.Create(this.m_ULongToEnum(value), this);
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x00266E88 File Offset: 0x00265088
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

		// Token: 0x06005E76 RID: 24182 RVA: 0x00267050 File Offset: 0x00265250
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

		// Token: 0x06005E77 RID: 24183 RVA: 0x00267218 File Offset: 0x00265418
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

		// Token: 0x06005E78 RID: 24184 RVA: 0x00267268 File Offset: 0x00265468
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

		// Token: 0x06005E79 RID: 24185 RVA: 0x002672B8 File Offset: 0x002654B8
		private DynValue PerformBinaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long, long> operation)
		{
			return this.PerformBinaryOperationS(funcName, ctx, args, (long v1, long v2) => this.CreateValueSigned(operation(v1, v2)));
		}

		// Token: 0x06005E7A RID: 24186 RVA: 0x002672F0 File Offset: 0x002654F0
		private DynValue PerformBinaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong, ulong> operation)
		{
			return this.PerformBinaryOperationU(funcName, ctx, args, (ulong v1, ulong v2) => this.CreateValueUnsigned(operation(v1, v2)));
		}

		// Token: 0x06005E7B RID: 24187 RVA: 0x00267328 File Offset: 0x00265528
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

		// Token: 0x06005E7C RID: 24188 RVA: 0x00267374 File Offset: 0x00265574
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

		// Token: 0x06005E7D RID: 24189 RVA: 0x002673C0 File Offset: 0x002655C0
		internal DynValue Callback_Or(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("or", ctx, args, (ulong v1, ulong v2) => v1 | v2);
			}
			return this.PerformBinaryOperationS("or", ctx, args, (long v1, long v2) => v1 | v2);
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x00267430 File Offset: 0x00265630
		internal DynValue Callback_And(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("and", ctx, args, (ulong v1, ulong v2) => v1 & v2);
			}
			return this.PerformBinaryOperationS("and", ctx, args, (long v1, long v2) => v1 & v2);
		}

		// Token: 0x06005E7F RID: 24191 RVA: 0x002674A0 File Offset: 0x002656A0
		internal DynValue Callback_Xor(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("xor", ctx, args, (ulong v1, ulong v2) => v1 ^ v2);
			}
			return this.PerformBinaryOperationS("xor", ctx, args, (long v1, long v2) => v1 ^ v2);
		}

		// Token: 0x06005E80 RID: 24192 RVA: 0x00267510 File Offset: 0x00265710
		internal DynValue Callback_BwNot(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformUnaryOperationU("not", ctx, args, (ulong v1) => ~v1);
			}
			return this.PerformUnaryOperationS("not", ctx, args, (long v1) => ~v1);
		}

		// Token: 0x06005E81 RID: 24193 RVA: 0x00267580 File Offset: 0x00265780
		internal DynValue Callback_HasAll(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("hasAll", ctx, args, (ulong v1, ulong v2) => DynValue.NewBoolean((v1 & v2) == v2));
			}
			return this.PerformBinaryOperationS("hasAll", ctx, args, (long v1, long v2) => DynValue.NewBoolean((v1 & v2) == v2));
		}

		// Token: 0x06005E82 RID: 24194 RVA: 0x002675F0 File Offset: 0x002657F0
		internal DynValue Callback_HasAny(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("hasAny", ctx, args, (ulong v1, ulong v2) => DynValue.NewBoolean((v1 & v2) > 0UL));
			}
			return this.PerformBinaryOperationS("hasAny", ctx, args, (long v1, long v2) => DynValue.NewBoolean((v1 & v2) != 0L));
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x0026765E File Offset: 0x0026585E
		public override bool IsTypeCompatible(Type type, object obj)
		{
			if (obj != null)
			{
				return base.Type == type;
			}
			return base.IsTypeCompatible(type, obj);
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x00267678 File Offset: 0x00265878
		public override DynValue MetaIndex(Script script, object obj, string metaname)
		{
			if (metaname == "__concat" && this.IsFlags)
			{
				return DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_Or), null);
			}
			return null;
		}

		// Token: 0x04005448 RID: 21576
		private Func<object, ulong> m_EnumToULong;

		// Token: 0x04005449 RID: 21577
		private Func<ulong, object> m_ULongToEnum;

		// Token: 0x0400544A RID: 21578
		private Func<object, long> m_EnumToLong;

		// Token: 0x0400544B RID: 21579
		private Func<long, object> m_LongToEnum;
	}
}
