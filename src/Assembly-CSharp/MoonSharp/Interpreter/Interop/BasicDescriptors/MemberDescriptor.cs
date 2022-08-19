using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000D41 RID: 3393
	public static class MemberDescriptor
	{
		// Token: 0x06005FAC RID: 24492 RVA: 0x00259EFE File Offset: 0x002580FE
		public static bool HasAllFlags(this MemberDescriptorAccess access, MemberDescriptorAccess flag)
		{
			return (access & flag) == flag;
		}

		// Token: 0x06005FAD RID: 24493 RVA: 0x0026C8E1 File Offset: 0x0026AAE1
		public static bool CanRead(this IMemberDescriptor desc)
		{
			return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanRead);
		}

		// Token: 0x06005FAE RID: 24494 RVA: 0x0026C8EF File Offset: 0x0026AAEF
		public static bool CanWrite(this IMemberDescriptor desc)
		{
			return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanWrite);
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x0026C8FD File Offset: 0x0026AAFD
		public static bool CanExecute(this IMemberDescriptor desc)
		{
			return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanExecute);
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x0026C90B File Offset: 0x0026AB0B
		public static DynValue GetGetterCallbackAsDynValue(this IMemberDescriptor desc, Script script, object obj)
		{
			return DynValue.NewCallback((ScriptExecutionContext p1, CallbackArguments p2) => desc.GetValue(script, obj), null);
		}

		// Token: 0x06005FB1 RID: 24497 RVA: 0x0026C938 File Offset: 0x0026AB38
		public static IMemberDescriptor WithAccessOrNull(this IMemberDescriptor desc, MemberDescriptorAccess access)
		{
			if (desc == null)
			{
				return null;
			}
			if (desc.MemberAccess.HasAllFlags(access))
			{
				return desc;
			}
			return null;
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x0026C950 File Offset: 0x0026AB50
		public static void CheckAccess(this IMemberDescriptor desc, MemberDescriptorAccess access, object obj)
		{
			if (!desc.IsStatic && obj == null)
			{
				throw ScriptRuntimeException.AccessInstanceMemberOnStatics(desc);
			}
			if (access.HasAllFlags(MemberDescriptorAccess.CanExecute) && !desc.CanExecute())
			{
				throw new ScriptRuntimeException("userdata member {0} cannot be called.", new object[]
				{
					desc.Name
				});
			}
			if (access.HasAllFlags(MemberDescriptorAccess.CanWrite) && !desc.CanWrite())
			{
				throw new ScriptRuntimeException("userdata member {0} cannot be assigned to.", new object[]
				{
					desc.Name
				});
			}
			if (access.HasAllFlags(MemberDescriptorAccess.CanRead) && !desc.CanRead())
			{
				throw new ScriptRuntimeException("userdata member {0} cannot be read from.", new object[]
				{
					desc.Name
				});
			}
		}
	}
}
