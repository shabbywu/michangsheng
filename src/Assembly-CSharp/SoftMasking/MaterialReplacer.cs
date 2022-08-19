using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x020006DD RID: 1757
	public static class MaterialReplacer
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x00183520 File Offset: 0x00181720
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

		// Token: 0x0600386E RID: 14446 RVA: 0x00183540 File Offset: 0x00181740
		private static IEnumerable<IMaterialReplacer> CollectGlobalReplacers()
		{
			return from t in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly x) => x.GetTypesSafe())
			where MaterialReplacer.IsMaterialReplacerType(t)
			select MaterialReplacer.TryCreateInstance(t) into t
			where t != null
			select t;
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x001835E7 File Offset: 0x001817E7
		private static bool IsMaterialReplacerType(Type t)
		{
			return !(t is TypeBuilder) && !t.IsAbstract && t.IsDefined(typeof(GlobalMaterialReplacerAttribute), false) && typeof(IMaterialReplacer).IsAssignableFrom(t);
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x00183624 File Offset: 0x00181824
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

		// Token: 0x06003871 RID: 14449 RVA: 0x00183674 File Offset: 0x00181874
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

		// Token: 0x040030D0 RID: 12496
		private static List<IMaterialReplacer> _globalReplacers;
	}
}
