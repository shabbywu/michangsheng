using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000D00 RID: 3328
	public class UnityAssetsScriptLoader : ScriptLoaderBase
	{
		// Token: 0x06005D29 RID: 23849 RVA: 0x00262251 File Offset: 0x00260451
		public UnityAssetsScriptLoader(string assetsPath = null)
		{
			assetsPath = (assetsPath ?? "MoonSharp/Scripts");
			this.LoadResourcesWithReflection(assetsPath);
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x00262277 File Offset: 0x00260477
		public UnityAssetsScriptLoader(Dictionary<string, string> scriptToCodeMap)
		{
			this.m_Resources = scriptToCodeMap;
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x00262294 File Offset: 0x00260494
		private void LoadResourcesWithReflection(string assetsPath)
		{
			try
			{
				Type type = Type.GetType("UnityEngine.Resources, UnityEngine");
				Type type2 = Type.GetType("UnityEngine.TextAsset, UnityEngine");
				MethodInfo getMethod = Framework.Do.GetGetMethod(Framework.Do.GetProperty(type2, "name"));
				MethodInfo getMethod2 = Framework.Do.GetGetMethod(Framework.Do.GetProperty(type2, "text"));
				Array array = (Array)Framework.Do.GetMethod(type, "LoadAll", new Type[]
				{
					typeof(string),
					typeof(Type)
				}).Invoke(null, new object[]
				{
					assetsPath,
					type2
				});
				for (int i = 0; i < array.Length; i++)
				{
					object value = array.GetValue(i);
					string key = getMethod.Invoke(value, null) as string;
					string value2 = getMethod2.Invoke(value, null) as string;
					this.m_Resources.Add(key, value2);
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Error initializing UnityScriptLoader : {0}", arg);
			}
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x002623AC File Offset: 0x002605AC
		private string GetFileName(string filename)
		{
			int num = Math.Max(filename.LastIndexOf('\\'), filename.LastIndexOf('/'));
			if (num > 0)
			{
				filename = filename.Substring(num + 1);
			}
			return filename;
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x002623DF File Offset: 0x002605DF
		public override object LoadFile(string file, Table globalContext)
		{
			file = this.GetFileName(file);
			if (this.m_Resources.ContainsKey(file))
			{
				return this.m_Resources[file];
			}
			throw new Exception(string.Format("Cannot load script '{0}'. By default, scripts should be .txt files placed under a Assets/Resources/{1} directory.\r\nIf you want scripts to be put in another directory or another way, use a custom instance of UnityAssetsScriptLoader or implement\r\nyour own IScriptLoader (possibly extending ScriptLoaderBase).", file, "MoonSharp/Scripts"));
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x0026241A File Offset: 0x0026061A
		public override bool ScriptFileExists(string file)
		{
			file = this.GetFileName(file);
			return this.m_Resources.ContainsKey(file);
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x00262431 File Offset: 0x00260631
		public string[] GetLoadedScripts()
		{
			return this.m_Resources.Keys.ToArray<string>();
		}

		// Token: 0x040053D5 RID: 21461
		private Dictionary<string, string> m_Resources = new Dictionary<string, string>();

		// Token: 0x040053D6 RID: 21462
		public const string DEFAULT_PATH = "MoonSharp/Scripts";
	}
}
