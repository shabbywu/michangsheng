using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200106C RID: 4204
	internal static class ScriptPrivateResource_Extension
	{
		// Token: 0x06006550 RID: 25936 RVA: 0x00282DF8 File Offset: 0x00280FF8
		public static void CheckScriptOwnership(this IScriptPrivateResource containingResource, DynValue[] values)
		{
			foreach (DynValue value in values)
			{
				containingResource.CheckScriptOwnership(value);
			}
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x00282E20 File Offset: 0x00281020
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

		// Token: 0x06006552 RID: 25938 RVA: 0x00045B68 File Offset: 0x00043D68
		public static void CheckScriptOwnership(this IScriptPrivateResource resource, Script script)
		{
			if (resource.OwnerScript != null && resource.OwnerScript != script && script != null)
			{
				throw new ScriptRuntimeException("Attempt to access a resource owned by a script, from another script");
			}
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x00282E44 File Offset: 0x00281044
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
