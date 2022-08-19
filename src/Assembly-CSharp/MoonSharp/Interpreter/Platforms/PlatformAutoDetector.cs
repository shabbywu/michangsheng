using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Loaders;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x02000CF8 RID: 3320
	public static class PlatformAutoDetector
	{
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06005CEF RID: 23791 RVA: 0x00261CB5 File Offset: 0x0025FEB5
		// (set) Token: 0x06005CF0 RID: 23792 RVA: 0x00261CBC File Offset: 0x0025FEBC
		public static bool IsRunningOnMono { get; private set; }

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06005CF1 RID: 23793 RVA: 0x00261CC4 File Offset: 0x0025FEC4
		// (set) Token: 0x06005CF2 RID: 23794 RVA: 0x00261CCB File Offset: 0x0025FECB
		public static bool IsRunningOnClr4 { get; private set; }

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06005CF3 RID: 23795 RVA: 0x00261CD3 File Offset: 0x0025FED3
		// (set) Token: 0x06005CF4 RID: 23796 RVA: 0x00261CDA File Offset: 0x0025FEDA
		public static bool IsRunningOnUnity { get; private set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06005CF5 RID: 23797 RVA: 0x00261CE2 File Offset: 0x0025FEE2
		// (set) Token: 0x06005CF6 RID: 23798 RVA: 0x00261CE9 File Offset: 0x0025FEE9
		public static bool IsPortableFramework { get; private set; }

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06005CF7 RID: 23799 RVA: 0x00261CF1 File Offset: 0x0025FEF1
		// (set) Token: 0x06005CF8 RID: 23800 RVA: 0x00261CF8 File Offset: 0x0025FEF8
		public static bool IsUnityNative { get; private set; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06005CF9 RID: 23801 RVA: 0x00261D00 File Offset: 0x0025FF00
		// (set) Token: 0x06005CFA RID: 23802 RVA: 0x00261D07 File Offset: 0x0025FF07
		public static bool IsUnityIL2CPP { get; private set; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06005CFB RID: 23803 RVA: 0x00261D10 File Offset: 0x0025FF10
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

		// Token: 0x06005CFC RID: 23804 RVA: 0x00261D84 File Offset: 0x0025FF84
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

		// Token: 0x06005CFD RID: 23805 RVA: 0x00261E20 File Offset: 0x00260020
		internal static IPlatformAccessor GetDefaultPlatform()
		{
			PlatformAutoDetector.AutoDetectPlatformFlags();
			if (PlatformAutoDetector.IsRunningOnUnity)
			{
				return new LimitedPlatformAccessor();
			}
			return new StandardPlatformAccessor();
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x00261E39 File Offset: 0x00260039
		internal static IScriptLoader GetDefaultScriptLoader()
		{
			PlatformAutoDetector.AutoDetectPlatformFlags();
			if (PlatformAutoDetector.IsRunningOnUnity)
			{
				return new UnityAssetsScriptLoader(null);
			}
			return new FileSystemScriptLoader();
		}

		// Token: 0x040053C3 RID: 21443
		private static bool? m_IsRunningOnAOT;

		// Token: 0x040053C4 RID: 21444
		private static bool m_AutoDetectionsDone;
	}
}
