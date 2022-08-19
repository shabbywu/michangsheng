using System;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000D45 RID: 3397
	public sealed class ParameterDescriptor : IWireableDescriptor
	{
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06005FBA RID: 24506 RVA: 0x0026C9F0 File Offset: 0x0026ABF0
		// (set) Token: 0x06005FBB RID: 24507 RVA: 0x0026C9F8 File Offset: 0x0026ABF8
		public string Name { get; private set; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06005FBC RID: 24508 RVA: 0x0026CA01 File Offset: 0x0026AC01
		// (set) Token: 0x06005FBD RID: 24509 RVA: 0x0026CA09 File Offset: 0x0026AC09
		public Type Type { get; private set; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x0026CA12 File Offset: 0x0026AC12
		// (set) Token: 0x06005FBF RID: 24511 RVA: 0x0026CA1A File Offset: 0x0026AC1A
		public bool HasDefaultValue { get; private set; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06005FC0 RID: 24512 RVA: 0x0026CA23 File Offset: 0x0026AC23
		// (set) Token: 0x06005FC1 RID: 24513 RVA: 0x0026CA2B File Offset: 0x0026AC2B
		public object DefaultValue { get; private set; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06005FC2 RID: 24514 RVA: 0x0026CA34 File Offset: 0x0026AC34
		// (set) Token: 0x06005FC3 RID: 24515 RVA: 0x0026CA3C File Offset: 0x0026AC3C
		public bool IsOut { get; private set; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06005FC4 RID: 24516 RVA: 0x0026CA45 File Offset: 0x0026AC45
		// (set) Token: 0x06005FC5 RID: 24517 RVA: 0x0026CA4D File Offset: 0x0026AC4D
		public bool IsRef { get; private set; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x0026CA56 File Offset: 0x0026AC56
		// (set) Token: 0x06005FC7 RID: 24519 RVA: 0x0026CA5E File Offset: 0x0026AC5E
		public bool IsVarArgs { get; private set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06005FC8 RID: 24520 RVA: 0x0026CA67 File Offset: 0x0026AC67
		public bool HasBeenRestricted
		{
			get
			{
				return this.m_OriginalType != null;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06005FC9 RID: 24521 RVA: 0x0026CA75 File Offset: 0x0026AC75
		public Type OriginalType
		{
			get
			{
				return this.m_OriginalType ?? this.Type;
			}
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x0026CA87 File Offset: 0x0026AC87
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

		// Token: 0x06005FCB RID: 24523 RVA: 0x0026CAC4 File Offset: 0x0026ACC4
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

		// Token: 0x06005FCC RID: 24524 RVA: 0x0026CB20 File Offset: 0x0026AD20
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

		// Token: 0x06005FCD RID: 24525 RVA: 0x0026CBB9 File Offset: 0x0026ADB9
		public override string ToString()
		{
			return string.Format("{0} {1}{2}", this.Type.Name, this.Name, this.HasDefaultValue ? " = ..." : "");
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x0026CBEC File Offset: 0x0026ADEC
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

		// Token: 0x06005FCF RID: 24527 RVA: 0x0026CC50 File Offset: 0x0026AE50
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

		// Token: 0x040054A8 RID: 21672
		private Type m_OriginalType;
	}
}
