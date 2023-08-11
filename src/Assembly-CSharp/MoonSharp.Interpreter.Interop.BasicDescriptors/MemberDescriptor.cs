namespace MoonSharp.Interpreter.Interop.BasicDescriptors;

public static class MemberDescriptor
{
	public static bool HasAllFlags(this MemberDescriptorAccess access, MemberDescriptorAccess flag)
	{
		return (access & flag) == flag;
	}

	public static bool CanRead(this IMemberDescriptor desc)
	{
		return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanRead);
	}

	public static bool CanWrite(this IMemberDescriptor desc)
	{
		return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanWrite);
	}

	public static bool CanExecute(this IMemberDescriptor desc)
	{
		return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanExecute);
	}

	public static DynValue GetGetterCallbackAsDynValue(this IMemberDescriptor desc, Script script, object obj)
	{
		return DynValue.NewCallback((ScriptExecutionContext p1, CallbackArguments p2) => desc.GetValue(script, obj));
	}

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

	public static void CheckAccess(this IMemberDescriptor desc, MemberDescriptorAccess access, object obj)
	{
		if (!desc.IsStatic && obj == null)
		{
			throw ScriptRuntimeException.AccessInstanceMemberOnStatics(desc);
		}
		if (access.HasAllFlags(MemberDescriptorAccess.CanExecute) && !desc.CanExecute())
		{
			throw new ScriptRuntimeException("userdata member {0} cannot be called.", desc.Name);
		}
		if (access.HasAllFlags(MemberDescriptorAccess.CanWrite) && !desc.CanWrite())
		{
			throw new ScriptRuntimeException("userdata member {0} cannot be assigned to.", desc.Name);
		}
		if (access.HasAllFlags(MemberDescriptorAccess.CanRead) && !desc.CanRead())
		{
			throw new ScriptRuntimeException("userdata member {0} cannot be read from.", desc.Name);
		}
	}
}
