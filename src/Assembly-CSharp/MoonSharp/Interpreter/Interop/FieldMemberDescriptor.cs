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
	// Token: 0x02001119 RID: 4377
	public class FieldMemberDescriptor : IMemberDescriptor, IOptimizableDescriptor, IWireableDescriptor
	{
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06006998 RID: 27032 RVA: 0x00048237 File Offset: 0x00046437
		// (set) Token: 0x06006999 RID: 27033 RVA: 0x0004823F File Offset: 0x0004643F
		public FieldInfo FieldInfo { get; private set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600699A RID: 27034 RVA: 0x00048248 File Offset: 0x00046448
		// (set) Token: 0x0600699B RID: 27035 RVA: 0x00048250 File Offset: 0x00046450
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x0600699C RID: 27036 RVA: 0x00048259 File Offset: 0x00046459
		// (set) Token: 0x0600699D RID: 27037 RVA: 0x00048261 File Offset: 0x00046461
		public bool IsStatic { get; private set; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x0600699E RID: 27038 RVA: 0x0004826A File Offset: 0x0004646A
		// (set) Token: 0x0600699F RID: 27039 RVA: 0x00048272 File Offset: 0x00046472
		public string Name { get; private set; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060069A0 RID: 27040 RVA: 0x0004827B File Offset: 0x0004647B
		// (set) Token: 0x060069A1 RID: 27041 RVA: 0x00048283 File Offset: 0x00046483
		public bool IsConst { get; private set; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060069A2 RID: 27042 RVA: 0x0004828C File Offset: 0x0004648C
		// (set) Token: 0x060069A3 RID: 27043 RVA: 0x00048294 File Offset: 0x00046494
		public bool IsReadonly { get; private set; }

		// Token: 0x060069A4 RID: 27044 RVA: 0x0028D998 File Offset: 0x0028BB98
		public static FieldMemberDescriptor TryCreateIfVisible(FieldInfo fi, InteropAccessMode accessMode)
		{
			if (fi.GetVisibilityFromAttributes() ?? fi.IsPublic)
			{
				return new FieldMemberDescriptor(fi, accessMode);
			}
			return null;
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x0028D9D0 File Offset: 0x0028BBD0
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

		// Token: 0x060069A6 RID: 27046 RVA: 0x0028DA6C File Offset: 0x0028BC6C
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

		// Token: 0x060069A7 RID: 27047 RVA: 0x0028DADC File Offset: 0x0028BCDC
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

		// Token: 0x060069A8 RID: 27048 RVA: 0x0028DBD8 File Offset: 0x0028BDD8
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

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060069A9 RID: 27049 RVA: 0x0004829D File Offset: 0x0004649D
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

		// Token: 0x060069AA RID: 27050 RVA: 0x000482B2 File Offset: 0x000464B2
		void IOptimizableDescriptor.Optimize()
		{
			if (this.m_OptimizedGetter == null)
			{
				this.OptimizeGetter();
			}
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x0028DCD8 File Offset: 0x0028BED8
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

		// Token: 0x04006049 RID: 24649
		private object m_ConstValue;

		// Token: 0x0400604A RID: 24650
		private Func<object, object> m_OptimizedGetter;
	}
}
