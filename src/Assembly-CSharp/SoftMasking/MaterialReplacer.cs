using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x02000A0C RID: 2572
	public static class MaterialReplacer
	{
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x0002F754 File Offset: 0x0002D954
		public static IEnumerable<IMaterialReplacer> globalReplacers
		{
			get
			{
				if (MaterialReplacer._globalReplacers == null)
				{
					MaterialReplacer._globalReplacers = MaterialReplacer.CollectGlobalReplacers().ToList<IMaterialReplacer>();
				}
				return MaterialReplacer._globalReplacers;
			}
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x001CAE78 File Offset: 0x001C9078
		private static IEnumerable<IMaterialReplacer> CollectGlobalReplacers()
		{
			return from t in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly x) => x.GetTypesSafe())
			where MaterialReplacer.IsMaterialReplacerType(t)
			select MaterialReplacer.TryCreateInstance(t) into t
			where t != null
			select t;
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x0002F771 File Offset: 0x0002D971
		private static bool IsMaterialReplacerType(Type t)
		{
			return !(t is TypeBuilder) && !t.IsAbstract && t.IsDefined(typeof(GlobalMaterialReplacerAttribute), false) && typeof(IMaterialReplacer).IsAssignableFrom(t);
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x001CAF20 File Offset: 0x001C9120
		private static IMaterialReplacer TryCreateInstance(Type t)
		{
			IMaterialReplacer result;
			try
			{
				result = (IMaterialReplacer)Activator.CreateInstance(t);
			}
			catch (Exception ex)
			{
				Debug.LogErrorFormat("Could not create instance of {0}: {1}", new object[]
				{
					t.Name,
					ex
				});
				result = null;
			}
			return result;
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x001CAF70 File Offset: 0x001C9170
		private static IEnumerable<Type> GetTypesSafe(this Assembly asm)
		{
			IEnumerable<Type> result;
			try
			{
				result = asm.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				result = from t in ex.Types
				where t != null
				select t;
			}
			return result;
		}

		// Token: 0x04003AEB RID: 15083
		private static List<IMaterialReplacer> _globalReplacers;
	}
}
