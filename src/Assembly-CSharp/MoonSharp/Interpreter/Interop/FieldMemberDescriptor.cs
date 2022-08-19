using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Diagnostics;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D1F RID: 3359
	public class FieldMemberDescriptor : IMemberDescriptor, IOptimizableDescriptor, IWireableDescriptor
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06005E09 RID: 24073 RVA: 0x00264D8D File Offset: 0x00262F8D
		// (set) Token: 0x06005E0A RID: 24074 RVA: 0x00264D95 File Offset: 0x00262F95
		public FieldInfo FieldInfo { get; private set; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06005E0B RID: 24075 RVA: 0x00264D9E File Offset: 0x00262F9E
		// (set) Token: 0x06005E0C RID: 24076 RVA: 0x00264DA6 File Offset: 0x00262FA6
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x00264DAF File Offset: 0x00262FAF
		// (set) Token: 0x06005E0E RID: 24078 RVA: 0x00264DB7 File Offset: 0x00262FB7
		public bool IsStatic { get; private set; }

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x00264DC0 File Offset: 0x00262FC0
		// (set) Token: 0x06005E10 RID: 24080 RVA: 0x00264DC8 File Offset: 0x00262FC8
		public string Name { get; private set; }

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x00264DD1 File Offset: 0x00262FD1
		// (set) Token: 0x06005E12 RID: 24082 RVA: 0x00264DD9 File Offset: 0x00262FD9
		public bool IsConst { get; private set; }

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x00264DE2 File Offset: 0x00262FE2
		// (set) Token: 0x06005E14 RID: 24084 RVA: 0x00264DEA File Offset: 0x00262FEA
		public bool IsReadonly { get; private set; }

		// Token: 0x06005E15 RID: 24085 RVA: 0x00264DF4 File Offset: 0x00262FF4
		public static FieldMemberDescriptor TryCreateIfVisible(FieldInfo fi, InteropAccessMode accessMode)
		{
			if (fi.GetVisibilityFromAttributes() ?? fi.IsPublic)
			{
				return new FieldMemberDescriptor(fi, accessMode);
			}
			return null;
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x00264E2C File Offset: 0x0026302C
		public FieldMemberDescriptor(FieldInfo fi, InteropAccessMode accessMode)
		{
			if (Script.GlobalOptions.Platform.IsRunningOnAOT())
			{
				accessMode = InteropAccessMode.Reflection;
			}
			this.FieldInfo = fi;
			this.AccessMode = accessMode;
			this.Name = fi.Name;
			this.IsStatic = this.FieldInfo.IsStatic;
			if (this.FieldInfo.IsLiteral)
			{
				this.IsConst = true;
				this.m_ConstValue = this.FieldInfo.GetValue(null);
			}
			else
			{
				this.IsReadonly = this.FieldInfo.IsInitOnly;
			}
			if (this.AccessMode == InteropAccessMode.Preoptimized)
			{
				this.OptimizeGetter();
			}
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x00264EC8 File Offset: 0x002630C8
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			if (this.IsConst)
			{
				return ClrToScriptConversions.ObjectToDynValue(script, this.m_ConstValue);
			}
			if (this.AccessMode == InteropAccessMode.LazyOptimized && this.m_OptimizedGetter == null)
			{
				this.OptimizeGetter();
			}
			object obj2;
			if (this.m_OptimizedGetter != null)
			{
				obj2 = this.m_OptimizedGetter(obj);
			}
			else
			{
				obj2 = this.FieldInfo.GetValue(obj);
			}
			return ClrToScriptConversions.ObjectToDynValue(script, obj2);
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x00264F38 File Offset: 0x00263138
		internal void OptimizeGetter()
		{
			if (this.IsConst)
			{
				return;
			}
			using (PerformanceStatistics.StartGlobalStopwatch(PerformanceCounter.AdaptersCompilation))
			{
				if (this.IsStatic)
				{
					ParameterExpression parameterExpression;
					Expression<Func<object, object>> expression = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.Field(null, this.FieldInfo), typeof(object)), new ParameterExpression[]
					{
						parameterExpression
					});
					Interlocked.Exchange<Func<object, object>>(ref this.m_OptimizedGetter, expression.Compile());
				}
				else
				{
					ParameterExpression parameterExpression2;
					Expression<Func<object, object>> expression2 = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.Field(Expression.Convert(parameterExpression2, this.FieldInfo.DeclaringType), this.FieldInfo), typeof(object)), new ParameterExpression[]
					{
						parameterExpression2
					});
					Interlocked.Exchange<Func<object, object>>(ref this.m_OptimizedGetter, expression2.Compile());
				}
			}
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x00265034 File Offset: 0x00263234
		public void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
			if (this.IsReadonly || this.IsConst)
			{
				throw new ScriptRuntimeException("userdata field '{0}.{1}' cannot be written to.", new object[]
				{
					this.FieldInfo.DeclaringType.Name,
					this.Name
				});
			}
			object obj2 = ScriptToClrConversions.DynValueToObjectOfType(v, this.FieldInfo.FieldType, null, false);
			try
			{
				if (obj2 is double)
				{
					obj2 = NumericConversions.DoubleToType(this.FieldInfo.FieldType, (double)obj2);
				}
				this.FieldInfo.SetValue(this.IsStatic ? null : obj, obj2);
			}
			catch (ArgumentException)
			{
				throw ScriptRuntimeException.UserDataArgumentTypeMismatch(v.Type, this.FieldInfo.FieldType);
			}
			catch (InvalidCastException)
			{
				throw ScriptRuntimeException.UserDataArgumentTypeMismatch(v.Type, this.FieldInfo.FieldType);
			}
			catch (FieldAccessException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06005E1A RID: 24090 RVA: 0x00265134 File Offset: 0x00263334
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				if (this.IsReadonly || this.IsConst)
				{
					return MemberDescriptorAccess.CanRead;
				}
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite;
			}
		}

		// Token: 0x06005E1B RID: 24091 RVA: 0x00265149 File Offset: 0x00263349
		void IOptimizableDescriptor.Optimize()
		{
			if (this.m_OptimizedGetter == null)
			{
				this.OptimizeGetter();
			}
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x0026515C File Offset: 0x0026335C
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("visibility", DynValue.NewString(this.FieldInfo.GetClrVisibility()));
			t.Set("name", DynValue.NewString(this.Name));
			t.Set("static", DynValue.NewBoolean(this.IsStatic));
			t.Set("const", DynValue.NewBoolean(this.IsConst));
			t.Set("readonly", DynValue.NewBoolean(this.IsReadonly));
			t.Set("decltype", DynValue.NewString(this.FieldInfo.DeclaringType.FullName));
			t.Set("declvtype", DynValue.NewBoolean(Framework.Do.IsValueType(this.FieldInfo.DeclaringType)));
			t.Set("type", DynValue.NewString(this.FieldInfo.FieldType.FullName));
			t.Set("read", DynValue.NewBoolean(true));
			t.Set("write", DynValue.NewBoolean(!this.IsConst && !this.IsReadonly));
		}

		// Token: 0x04005427 RID: 21543
		private object m_ConstValue;

		// Token: 0x04005428 RID: 21544
		private Func<object, object> m_OptimizedGetter;
	}
}
