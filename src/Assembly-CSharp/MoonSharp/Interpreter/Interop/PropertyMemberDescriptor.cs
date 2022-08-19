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
	// Token: 0x02000D22 RID: 3362
	public class PropertyMemberDescriptor : IMemberDescriptor, IOptimizableDescriptor, IWireableDescriptor
	{
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06005E42 RID: 24130 RVA: 0x00266254 File Offset: 0x00264454
		// (set) Token: 0x06005E43 RID: 24131 RVA: 0x0026625C File Offset: 0x0026445C
		public PropertyInfo PropertyInfo { get; private set; }

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x00266265 File Offset: 0x00264465
		// (set) Token: 0x06005E45 RID: 24133 RVA: 0x0026626D File Offset: 0x0026446D
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x00266276 File Offset: 0x00264476
		// (set) Token: 0x06005E47 RID: 24135 RVA: 0x0026627E File Offset: 0x0026447E
		public bool IsStatic { get; private set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x00266287 File Offset: 0x00264487
		// (set) Token: 0x06005E49 RID: 24137 RVA: 0x0026628F File Offset: 0x0026448F
		public string Name { get; private set; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06005E4A RID: 24138 RVA: 0x00266298 File Offset: 0x00264498
		public bool CanRead
		{
			get
			{
				return this.m_Getter != null;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x002662A6 File Offset: 0x002644A6
		public bool CanWrite
		{
			get
			{
				return this.m_Setter != null;
			}
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x002662B4 File Offset: 0x002644B4
		public static PropertyMemberDescriptor TryCreateIfVisible(PropertyInfo pi, InteropAccessMode accessMode)
		{
			MethodInfo getMethod = Framework.Do.GetGetMethod(pi);
			MethodInfo setMethod = Framework.Do.GetSetMethod(pi);
			bool? visibilityFromAttributes = pi.GetVisibilityFromAttributes();
			bool? visibilityFromAttributes2 = getMethod.GetVisibilityFromAttributes();
			bool? visibilityFromAttributes3 = setMethod.GetVisibilityFromAttributes();
			if (visibilityFromAttributes != null)
			{
				return PropertyMemberDescriptor.TryCreate(pi, accessMode, (visibilityFromAttributes2 ?? visibilityFromAttributes.Value) ? getMethod : null, (visibilityFromAttributes3 ?? visibilityFromAttributes.Value) ? setMethod : null);
			}
			return PropertyMemberDescriptor.TryCreate(pi, accessMode, (visibilityFromAttributes2 ?? getMethod.IsPublic) ? getMethod : null, (visibilityFromAttributes3 ?? setMethod.IsPublic) ? setMethod : null);
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x0026638F File Offset: 0x0026458F
		private static PropertyMemberDescriptor TryCreate(PropertyInfo pi, InteropAccessMode accessMode, MethodInfo getter, MethodInfo setter)
		{
			if (getter == null && setter == null)
			{
				return null;
			}
			return new PropertyMemberDescriptor(pi, accessMode, getter, setter);
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x002663AE File Offset: 0x002645AE
		public PropertyMemberDescriptor(PropertyInfo pi, InteropAccessMode accessMode) : this(pi, accessMode, Framework.Do.GetGetMethod(pi), Framework.Do.GetSetMethod(pi))
		{
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x002663D0 File Offset: 0x002645D0
		public PropertyMemberDescriptor(PropertyInfo pi, InteropAccessMode accessMode, MethodInfo getter, MethodInfo setter)
		{
			if (getter == null && setter == null)
			{
				throw new ArgumentNullException("getter and setter cannot both be null");
			}
			if (Script.GlobalOptions.Platform.IsRunningOnAOT())
			{
				accessMode = InteropAccessMode.Reflection;
			}
			this.PropertyInfo = pi;
			this.AccessMode = accessMode;
			this.Name = pi.Name;
			this.m_Getter = getter;
			this.m_Setter = setter;
			this.IsStatic = (this.m_Getter ?? this.m_Setter).IsStatic;
			if (this.AccessMode == InteropAccessMode.Preoptimized)
			{
				this.OptimizeGetter();
				this.OptimizeSetter();
			}
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x00266470 File Offset: 0x00264670
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			if (this.m_Getter == null)
			{
				throw new ScriptRuntimeException("userdata property '{0}.{1}' cannot be read from.", new object[]
				{
					this.PropertyInfo.DeclaringType.Name,
					this.Name
				});
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
				obj2 = this.m_Getter.Invoke(this.IsStatic ? null : obj, null);
			}
			return ClrToScriptConversions.ObjectToDynValue(script, obj2);
		}

		// Token: 0x06005E51 RID: 24145 RVA: 0x00266510 File Offset: 0x00264710
		internal void OptimizeGetter()
		{
			using (PerformanceStatistics.StartGlobalStopwatch(PerformanceCounter.AdaptersCompilation))
			{
				if (this.m_Getter != null)
				{
					if (this.IsStatic)
					{
						ParameterExpression parameterExpression;
						Expression<Func<object, object>> expression = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.Property(null, this.PropertyInfo), typeof(object)), new ParameterExpression[]
						{
							parameterExpression
						});
						Interlocked.Exchange<Func<object, object>>(ref this.m_OptimizedGetter, expression.Compile());
					}
					else
					{
						ParameterExpression parameterExpression2;
						Expression<Func<object, object>> expression2 = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.Property(Expression.Convert(parameterExpression2, this.PropertyInfo.DeclaringType), this.PropertyInfo), typeof(object)), new ParameterExpression[]
						{
							parameterExpression2
						});
						Interlocked.Exchange<Func<object, object>>(ref this.m_OptimizedGetter, expression2.Compile());
					}
				}
			}
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x00266614 File Offset: 0x00264814
		internal void OptimizeSetter()
		{
			using (PerformanceStatistics.StartGlobalStopwatch(PerformanceCounter.AdaptersCompilation))
			{
				if (this.m_Setter != null && !Framework.Do.IsValueType(this.PropertyInfo.DeclaringType))
				{
					MethodInfo setMethod = Framework.Do.GetSetMethod(this.PropertyInfo);
					if (this.IsStatic)
					{
						ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "dummy");
						ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "val");
						UnaryExpression arg = Expression.Convert(parameterExpression2, this.PropertyInfo.PropertyType);
						Expression<Action<object, object>> expression = Expression.Lambda<Action<object, object>>(Expression.Call(setMethod, arg), new ParameterExpression[]
						{
							parameterExpression,
							parameterExpression2
						});
						Interlocked.Exchange<Action<object, object>>(ref this.m_OptimizedSetter, expression.Compile());
					}
					else
					{
						ParameterExpression parameterExpression3 = Expression.Parameter(typeof(object), "obj");
						ParameterExpression parameterExpression4 = Expression.Parameter(typeof(object), "val");
						Expression instance = Expression.Convert(parameterExpression3, this.PropertyInfo.DeclaringType);
						UnaryExpression unaryExpression = Expression.Convert(parameterExpression4, this.PropertyInfo.PropertyType);
						Expression<Action<object, object>> expression2 = Expression.Lambda<Action<object, object>>(Expression.Call(instance, setMethod, new Expression[]
						{
							unaryExpression
						}), new ParameterExpression[]
						{
							parameterExpression3,
							parameterExpression4
						});
						Interlocked.Exchange<Action<object, object>>(ref this.m_OptimizedSetter, expression2.Compile());
					}
				}
			}
		}

		// Token: 0x06005E53 RID: 24147 RVA: 0x00266794 File Offset: 0x00264994
		public void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
			if (this.m_Setter == null)
			{
				throw new ScriptRuntimeException("userdata property '{0}.{1}' cannot be written to.", new object[]
				{
					this.PropertyInfo.DeclaringType.Name,
					this.Name
				});
			}
			object obj2 = ScriptToClrConversions.DynValueToObjectOfType(v, this.PropertyInfo.PropertyType, null, false);
			try
			{
				if (obj2 is double)
				{
					obj2 = NumericConversions.DoubleToType(this.PropertyInfo.PropertyType, (double)obj2);
				}
				if (this.AccessMode == InteropAccessMode.LazyOptimized && this.m_OptimizedSetter == null)
				{
					this.OptimizeSetter();
				}
				if (this.m_OptimizedSetter != null)
				{
					this.m_OptimizedSetter(obj, obj2);
				}
				else
				{
					this.m_Setter.Invoke(this.IsStatic ? null : obj, new object[]
					{
						obj2
					});
				}
			}
			catch (ArgumentException)
			{
				throw ScriptRuntimeException.UserDataArgumentTypeMismatch(v.Type, this.PropertyInfo.PropertyType);
			}
			catch (InvalidCastException)
			{
				throw ScriptRuntimeException.UserDataArgumentTypeMismatch(v.Type, this.PropertyInfo.PropertyType);
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06005E54 RID: 24148 RVA: 0x002668B8 File Offset: 0x00264AB8
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				MemberDescriptorAccess memberDescriptorAccess = (MemberDescriptorAccess)0;
				if (this.m_Setter != null)
				{
					memberDescriptorAccess |= MemberDescriptorAccess.CanWrite;
				}
				if (this.m_Getter != null)
				{
					memberDescriptorAccess |= MemberDescriptorAccess.CanRead;
				}
				return memberDescriptorAccess;
			}
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x002668EC File Offset: 0x00264AEC
		void IOptimizableDescriptor.Optimize()
		{
			this.OptimizeGetter();
			this.OptimizeSetter();
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x002668FC File Offset: 0x00264AFC
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("visibility", DynValue.NewString(this.PropertyInfo.GetClrVisibility()));
			t.Set("name", DynValue.NewString(this.Name));
			t.Set("static", DynValue.NewBoolean(this.IsStatic));
			t.Set("read", DynValue.NewBoolean(this.CanRead));
			t.Set("write", DynValue.NewBoolean(this.CanWrite));
			t.Set("decltype", DynValue.NewString(this.PropertyInfo.DeclaringType.FullName));
			t.Set("declvtype", DynValue.NewBoolean(Framework.Do.IsValueType(this.PropertyInfo.DeclaringType)));
			t.Set("type", DynValue.NewString(this.PropertyInfo.PropertyType.FullName));
		}

		// Token: 0x0400543E RID: 21566
		private MethodInfo m_Getter;

		// Token: 0x0400543F RID: 21567
		private MethodInfo m_Setter;

		// Token: 0x04005440 RID: 21568
		private Func<object, object> m_OptimizedGetter;

		// Token: 0x04005441 RID: 21569
		private Action<object, object> m_OptimizedSetter;
	}
}
