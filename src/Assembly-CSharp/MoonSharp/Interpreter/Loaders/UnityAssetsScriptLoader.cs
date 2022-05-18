using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x020010DF RID: 4319
	public class UnityAssetsScriptLoader : ScriptLoaderBase
	{
		// Token: 0x06006847 RID: 26695 RVA: 0x000478D8 File Offset: 0x00045AD8
		public UnityAssetsScriptLoader(string assetsPath = null)
		{
			assetsPath = (assetsPath ?? "MoonSharp/Scripts");
			this.LoadResourcesWithReflection(assetsPath);
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x000478FE File Offset: 0x00045AFE
		public UnityAssetsScriptLoader(Dictionary<string, string> scriptToCodeMap)
		{
			this.m_Resources = scriptToCodeMap;
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x0028B1EC File Offset: 0x002893EC
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

		// Token: 0x0600684A RID: 26698 RVA: 0x0028B304 File Offset: 0x00289504
		private string GetFileName(string filename)
		{
			int num = Math.Max(filename.LastIndexOf('\\'), filename.LastIndexOf('/'));
			if (num > 0)
			{
				filename = filename.Substring(num + 1);
			}
			return filename;
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x00047918 File Offset: 0x00045B18
		public override object LoadFile(string file, Table globalContext)
		{
			file = this.GetFileName(file);
			if (this.m_Resources.ContainsKey(file))
			{
				return this.m_Resources[file];
			}
			throw new Exception(string.Format("Cannot load script '{0}'. By default, scripts should be .txt files placed under a Assets/Resources/{1} directory.\r\nIf you want scripts to be put in another directory or another way, use a custom instance of UnityAssetsScriptLoader or implement\r\nyour own IScriptLoader (possibly extending ScriptLoaderBase).", file, "MoonSharp/Scripts"));
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x00047953 File Offset: 0x00045B53
		public override bool ScriptFileExists(string file)
		{
			file = this.GetFileName(file);
			return this.m_Resources.ContainsKey(file);
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x0004796A File Offset: 0x00045B6A
		public string[] GetLoadedScripts()
		{
			return this.m_Resources.Keys.ToArray<string>();
		}

		// Token: 0x04005FDF RID: 24543
		private Dictionary<string, string> m_Resources = new Dictionary<string, string>();

		// Token: 0x04005FE0 RID: 24544
		public const string DEFAULT_PATH = "MoonSharp/Scripts";
	}
}
