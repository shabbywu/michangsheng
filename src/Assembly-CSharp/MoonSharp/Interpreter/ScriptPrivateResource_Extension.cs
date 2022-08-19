using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C9F RID: 3231
	internal static class ScriptPrivateResource_Extension
	{
		// Token: 0x06005A66 RID: 23142 RVA: 0x002582B4 File Offset: 0x002564B4
		public static void CheckScriptOwnership(this IScriptPrivateResource containingResource, DynValue[] values)
		{
			foreach (DynValue value in values)
			{
				containingResource.CheckScriptOwnership(value);
			}
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x002582DC File Offset: 0x002564DC
		public static void CheckScriptOwnership(this IScriptPrivateResource containingResource, DynValue value)
		{
			if (value != null)
			{
				IScriptPrivateResource asPrivateResource = value.GetAsPrivateResource();
				if (asPrivateResource != null)
				{
					containingResource.CheckScriptOwnership(asPrivateResource);
				}
			}
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x002582FD File Offset: 0x002564FD
		public static void CheckScriptOwnership(this IScriptPrivateResource resource, Script script)
		{
			if (resource.OwnerScript != null && resource.OwnerScript != script && script != null)
			{
				throw new ScriptRuntimeException("Attempt to access a resource owned by a script, from another script");
			}
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x00258320 File Offset: 0x00256520
		public static void CheckScriptOwnership(this IScriptPrivateResource containingResource, IScriptPrivateResource itemResource)
		{
			if (itemResource != null)
			{
				if (containingResource.OwnerScript != null && containingResource.OwnerScript != itemResource.OwnerScript && itemResource.OwnerScript != null)
				{
					throw new ScriptRuntimeException("Attempt to perform operations with resources owned by different scripts.");
				}
				if (containingResource.OwnerScript == null && itemResource.OwnerScript != null)
				{
					throw new ScriptRuntimeException("Attempt to perform operations with a script private resource on a shared resource.");
				}
			}
		}
	}
}
