using System;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02001153 RID: 4435
	public sealed class ParameterDescriptor : IWireableDescriptor
	{
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06006B9E RID: 27550 RVA: 0x000494A6 File Offset: 0x000476A6
		// (set) Token: 0x06006B9F RID: 27551 RVA: 0x000494AE File Offset: 0x000476AE
		public string Name { get; private set; }

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06006BA0 RID: 27552 RVA: 0x000494B7 File Offset: 0x000476B7
		// (set) Token: 0x06006BA1 RID: 27553 RVA: 0x000494BF File Offset: 0x000476BF
		public Type Type { get; private set; }

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06006BA2 RID: 27554 RVA: 0x000494C8 File Offset: 0x000476C8
		// (set) Token: 0x06006BA3 RID: 27555 RVA: 0x000494D0 File Offset: 0x000476D0
		public bool HasDefaultValue { get; private set; }

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06006BA4 RID: 27556 RVA: 0x000494D9 File Offset: 0x000476D9
		// (set) Token: 0x06006BA5 RID: 27557 RVA: 0x000494E1 File Offset: 0x000476E1
		public object DefaultValue { get; private set; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06006BA6 RID: 27558 RVA: 0x000494EA File Offset: 0x000476EA
		// (set) Token: 0x06006BA7 RID: 27559 RVA: 0x000494F2 File Offset: 0x000476F2
		public bool IsOut { get; private set; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06006BA8 RID: 27560 RVA: 0x000494FB File Offset: 0x000476FB
		// (set) Token: 0x06006BA9 RID: 27561 RVA: 0x00049503 File Offset: 0x00047703
		public bool IsRef { get; private set; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06006BAA RID: 27562 RVA: 0x0004950C File Offset: 0x0004770C
		// (set) Token: 0x06006BAB RID: 27563 RVA: 0x00049514 File Offset: 0x00047714
		public bool IsVarArgs { get; private set; }

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06006BAC RID: 27564 RVA: 0x0004951D File Offset: 0x0004771D
		public bool HasBeenRestricted
		{
			get
			{
				return this.m_OriginalType != null;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06006BAD RID: 27565 RVA: 0x0004952B File Offset: 0x0004772B
		public Type OriginalType
		{
			get
			{
				return this.m_OriginalType ?? this.Type;
			}
		}

		// Token: 0x06006BAE RID: 27566 RVA: 0x0004953D File Offset: 0x0004773D
		public ParameterDescriptor(string name, Type type, bool hasDefaultValue = false, object defaultValue = null, bool isOut = false, bool isRef = false, bool isVarArgs = false)
		{
			this.Name = name;
			this.Type = type;
			this.HasDefaultValue = hasDefaultValue;
			this.DefaultValue = defaultValue;
			this.IsOut = isOut;
			this.IsRef = isRef;
			this.IsVarArgs = isVarArgs;
		}

		// Token: 0x06006BAF RID: 27567 RVA: 0x00294664 File Offset: 0x00292864
		public ParameterDescriptor(string name, Type type, bool hasDefaultValue, object defaultValue, bool isOut, bool isRef, bool isVarArgs, Type typeRestriction)
		{
			this.Name = name;
			this.Type = type;
			this.HasDefaultValue = hasDefaultValue;
			this.DefaultValue = defaultValue;
			this.IsOut = isOut;
			this.IsRef = isRef;
			this.IsVarArgs = isVarArgs;
			if (typeRestriction != null)
			{
				this.RestrictType(typeRestriction);
			}
		}

		// Token: 0x06006BB0 RID: 27568 RVA: 0x002946C0 File Offset: 0x002928C0
		public ParameterDescriptor(ParameterInfo pi)
		{
			this.Name = pi.Name;
			this.Type = pi.ParameterType;
			this.HasDefaultValue = !Framework.Do.IsDbNull(pi.DefaultValue);
			this.DefaultValue = pi.DefaultValue;
			this.IsOut = pi.IsOut;
			this.IsRef = pi.ParameterType.IsByRef;
			this.IsVarArgs = (pi.ParameterType.IsArray && pi.GetCustomAttributes(typeof(ParamArrayAttribute), true).Any<object>());
		}

		// Token: 0x06006BB1 RID: 27569 RVA: 0x0004957A File Offset: 0x0004777A
		public override string ToString()
		{
			return string.Format("{0} {1}{2}", this.Type.Name, this.Name, this.HasDefaultValue ? " = ..." : "");
		}

		// Token: 0x06006BB2 RID: 27570 RVA: 0x0029475C File Offset: 0x0029295C
		public void RestrictType(Type type)
		{
			if (this.IsOut || this.IsRef || this.IsVarArgs)
			{
				throw new InvalidOperationException("Cannot restrict a ref/out or varargs param");
			}
			if (!Framework.Do.IsAssignableFrom(this.Type, type))
			{
				throw new InvalidOperationException("Specified operation is not a restriction");
			}
			this.m_OriginalType = this.Type;
			this.Type = type;
		}

		// Token: 0x06006BB3 RID: 27571 RVA: 0x002947C0 File Offset: 0x002929C0
		public void PrepareForWiring(Table table)
		{
			table.Set("name", DynValue.NewString(this.Name));
			if (this.Type.IsByRef)
			{
				table.Set("type", DynValue.NewString(this.Type.GetElementType().FullName));
			}
			else
			{
				table.Set("type", DynValue.NewString(this.Type.FullName));
			}
			if (this.OriginalType.IsByRef)
			{
				table.Set("origtype", DynValue.NewString(this.OriginalType.GetElementType().FullName));
			}
			else
			{
				table.Set("origtype", DynValue.NewString(this.OriginalType.FullName));
			}
			table.Set("default", DynValue.NewBoolean(this.HasDefaultValue));
			table.Set("out", DynValue.NewBoolean(this.IsOut));
			table.Set("ref", DynValue.NewBoolean(this.IsRef));
			table.Set("varargs", DynValue.NewBoolean(this.IsVarArgs));
			table.Set("restricted", DynValue.NewBoolean(this.HasBeenRestricted));
		}

		// Token: 0x04006121 RID: 24865
		private Type m_OriginalType;
	}
}
