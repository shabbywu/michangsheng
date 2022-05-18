using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Loaders;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x020010D5 RID: 4309
	public static class PlatformAutoDetector
	{
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06006805 RID: 26629 RVA: 0x0004769C File Offset: 0x0004589C
		// (set) Token: 0x06006806 RID: 26630 RVA: 0x000476A3 File Offset: 0x000458A3
		public static bool IsRunningOnMono { get; private set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06006807 RID: 26631 RVA: 0x000476AB File Offset: 0x000458AB
		// (set) Token: 0x06006808 RID: 26632 RVA: 0x000476B2 File Offset: 0x000458B2
		public static bool IsRunningOnClr4 { get; private set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06006809 RID: 26633 RVA: 0x000476BA File Offset: 0x000458BA
		// (set) Token: 0x0600680A RID: 26634 RVA: 0x000476C1 File Offset: 0x000458C1
		public static bool IsRunningOnUnity { get; private set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600680B RID: 26635 RVA: 0x000476C9 File Offset: 0x000458C9
		// (set) Token: 0x0600680C RID: 26636 RVA: 0x000476D0 File Offset: 0x000458D0
		public static bool IsPortableFramework { get; private set; }

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600680D RID: 26637 RVA: 0x000476D8 File Offset: 0x000458D8
		// (set) Token: 0x0600680E RID: 26638 RVA: 0x000476DF File Offset: 0x000458DF
		public static bool IsUnityNative { get; private set; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600680F RID: 26639 RVA: 0x000476E7 File Offset: 0x000458E7
		// (set) Token: 0x06006810 RID: 26640 RVA: 0x000476EE File Offset: 0x000458EE
		public static bool IsUnityIL2CPP { get; private set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06006811 RID: 26641 RVA: 0x0028AE48 File Offset: 0x00289048
		public static bool IsRunningOnAOT
		{
			get
			{
				if (PlatformAutoDetector.m_IsRunningOnAOT == null)
				{
					try
					{
						Expression.Lambda<Func<int>>(Expression.Constant(5, typeof(int)), Array.Empty<ParameterExpression>()).Compile();
						PlatformAutoDetector.m_IsRunningOnAOT = new bool?(false);
					}
					catch (Exception)
					{
						PlatformAutoDetector.m_IsRunningOnAOT = new bool?(true);
					}
				}
				return PlatformAutoDetector.m_IsRunningOnAOT.Value;
			}
		}

		// Token: 0x06006812 RID: 26642 RVA: 0x0028AEBC File Offset: 0x002890BC
		private static void AutoDetectPlatformFlags()
		{
			if (PlatformAutoDetector.m_AutoDetectionsDone)
			{
				return;
			}
			PlatformAutoDetector.IsRunningOnUnity = AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly a) => a.SafeGetTypes()).Any((Type t) => t.FullName.StartsWith("UnityEngine."));
			PlatformAutoDetector.IsRunningOnMono = (Type.GetType("Mono.Runtime") != null);
			PlatformAutoDetector.IsRunningOnClr4 = (Type.GetType("System.Lazy`1") != null);
			PlatformAutoDetector.m_AutoDetectionsDone = true;
		}

		// Token: 0x06006813 RID: 26643 RVA: 0x000476F6 File Offset: 0x000458F6
		internal static IPlatformAccessor GetDefaultPlatform()
		{
			PlatformAutoDetector.AutoDetectPlatformFlags();
			if (PlatformAutoDetector.IsRunningOnUnity)
			{
				return new LimitedPlatformAccessor();
			}
			return new StandardPlatformAccessor();
		}

		// Token: 0x06006814 RID: 26644 RVA: 0x0004770F File Offset: 0x0004590F
		internal static IScriptLoader GetDefaultScriptLoader()
		{
			PlatformAutoDetector.AutoDetectPlatformFlags();
			if (PlatformAutoDetector.IsRunningOnUnity)
			{
				return new UnityAssetsScriptLoader(null);
			}
			return new FileSystemScriptLoader();
		}

		// Token: 0x04005FC7 RID: 24519
		private static bool? m_IsRunningOnAOT;

		// Token: 0x04005FC8 RID: 24520
		private static bool m_AutoDetectionsDone;
	}
}
