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
	// Token: 0x02001121 RID: 4385
	public class PropertyMemberDescriptor : IMemberDescriptor, IOptimizableDescriptor, IWireableDescriptor
	{
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060069DE RID: 27102 RVA: 0x000484AC File Offset: 0x000466AC
		// (set) Token: 0x060069DF RID: 27103 RVA: 0x000484B4 File Offset: 0x000466B4
		public PropertyInfo PropertyInfo { get; private set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060069E0 RID: 27104 RVA: 0x000484BD File Offset: 0x000466BD
		// (set) Token: 0x060069E1 RID: 27105 RVA: 0x000484C5 File Offset: 0x000466C5
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060069E2 RID: 27106 RVA: 0x000484CE File Offset: 0x000466CE
		// (set) Token: 0x060069E3 RID: 27107 RVA: 0x000484D6 File Offset: 0x000466D6
		public bool IsStatic { get; private set; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060069E4 RID: 27108 RVA: 0x000484DF File Offset: 0x000466DF
		// (set) Token: 0x060069E5 RID: 27109 RVA: 0x000484E7 File Offset: 0x000466E7
		public string Name { get; private set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060069E6 RID: 27110 RVA: 0x000484F0 File Offset: 0x000466F0
		public bool CanRead
		{
			get
			{
				return this.m_Getter != null;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060069E7 RID: 27111 RVA: 0x000484FE File Offset: 0x000466FE
		public bool CanWrite
		{
			get
			{
				return this.m_Setter != null;
			}
		}

		// Token: 0x060069E8 RID: 27112 RVA: 0x0028EC58 File Offset: 0x0028CE58
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

		// Token: 0x060069E9 RID: 27113 RVA: 0x0004850C File Offset: 0x0004670C
		private static PropertyMemberDescriptor TryCreate(PropertyInfo pi, InteropAccessMode accessMode, MethodInfo getter, MethodInfo setter)
		{
			if (getter == null && setter == null)
			{
				return null;
			}
			return new PropertyMemberDescriptor(pi, accessMode, getter, setter);
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x0004852B File Offset: 0x0004672B
		public PropertyMemberDescriptor(PropertyInfo pi, InteropAccessMode accessMode) : this(pi, accessMode, Framework.Do.GetGetMethod(pi), Framework.Do.GetSetMethod(pi))
		{
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x0028ED34 File Offset: 0x0028CF34
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

		// Token: 0x060069EC RID: 27116 RVA: 0x0028EDD4 File Offset: 0x0028CFD4
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

		// Token: 0x060069ED RID: 27117 RVA: 0x0028EE74 File Offset: 0x0028D074
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

		// Token: 0x060069EE RID: 27118 RVA: 0x0028EF78 File Offset: 0x0028D178
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

		// Token: 0x060069EF RID: 27119 RVA: 0x0028F0F8 File Offset: 0x0028D2F8
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

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060069F0 RID: 27120 RVA: 0x0028F21C File Offset: 0x0028D41C
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

		// Token: 0x060069F1 RID: 27121 RVA: 0x0004854B File Offset: 0x0004674B
		void IOptimizableDescriptor.Optimize()
		{
			this.OptimizeGetter();
			this.OptimizeSetter();
		}

		// Token: 0x060069F2 RID: 27122 RVA: 0x0028F250 File Offset: 0x0028D450
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

		// Token: 0x0400606E RID: 24686
		private MethodInfo m_Getter;

		// Token: 0x0400606F RID: 24687
		private MethodInfo m_Setter;

		// Token: 0x04006070 RID: 24688
		private Func<object, object> m_OptimizedGetter;

		// Token: 0x04006071 RID: 24689
		private Action<object, object> m_OptimizedSetter;
	}
}
