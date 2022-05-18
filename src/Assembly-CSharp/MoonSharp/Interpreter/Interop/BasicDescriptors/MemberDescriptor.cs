using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x0200114E RID: 4430
	public static class MemberDescriptor
	{
		// Token: 0x06006B8E RID: 27534 RVA: 0x00046A39 File Offset: 0x00044C39
		public static bool HasAllFlags(this MemberDescriptorAccess access, MemberDescriptorAccess flag)
		{
			return (access & flag) == flag;
		}

		// Token: 0x06006B8F RID: 27535 RVA: 0x0004941E File Offset: 0x0004761E
		public static bool CanRead(this IMemberDescriptor desc)
		{
			return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanRead);
		}

		// Token: 0x06006B90 RID: 27536 RVA: 0x0004942C File Offset: 0x0004762C
		public static bool CanWrite(this IMemberDescriptor desc)
		{
			return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanWrite);
		}

		// Token: 0x06006B91 RID: 27537 RVA: 0x0004943A File Offset: 0x0004763A
		public static bool CanExecute(this IMemberDescriptor desc)
		{
			return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanExecute);
		}

		// Token: 0x06006B92 RID: 27538 RVA: 0x00049448 File Offset: 0x00047648
		public static DynValue GetGetterCallbackAsDynValue(this IMemberDescriptor desc, Script script, object obj)
		{
			return DynValue.NewCallback((ScriptExecutionContext p1, CallbackArguments p2) => desc.GetValue(script, obj), null);
		}

		// Token: 0x06006B93 RID: 27539 RVA: 0x00049475 File Offset: 0x00047675
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

		// Token: 0x06006B94 RID: 27540 RVA: 0x002945C4 File Offset: 0x002927C4
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
