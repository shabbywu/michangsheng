using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using Google.Protobuf;
using script.YarnEditor.Component.TriggerData;
using script.YarnEditor.Mod;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yarn.Compiler;
using Yarn.Unity;

namespace script.YarnEditor.Manager
{
	// Token: 0x02000AAD RID: 2733
	public class StoryManager : MonoBehaviour
	{
		// Token: 0x060045ED RID: 17901 RVA: 0x00032020 File Offset: 0x00030220
		private void Show()
		{
			if (!Directory.Exists(this.GetBepPath()))
			{
				return;
			}
			this.InitStory();
			this.UpdateDrop();
			this.UI.SetActive(true);
			base.transform.SetAsLastSibling();
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x00032053 File Offset: 0x00030253
		private void Hide()
		{
			this.UI.SetActive(false);
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x001DDB54 File Offset: 0x001DBD54
		private void UpdateDrop()
		{
			this.ModDrop.options.Clear();
			this.ModDrop.onValueChanged.RemoveAllListeners();
			this.CurStory = null;
			this.CurYarn = null;
			this.ModList = new List<string>();
			foreach (Story story in this.StoryList)
			{
				this.ModList.Add(story.ModName);
			}
			this.ModDrop.AddOptions(this.ModList);
			this.ModDrop.onValueChanged.AddListener(new UnityAction<int>(this.SelectMod));
			if (this.ModDrop.options.Count > 0)
			{
				this.SelectMod(0);
			}
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x001DDC34 File Offset: 0x001DBE34
		private void SelectMod(int id)
		{
			this.CurStory = this.StoryList[id];
			this.YarnDrop.options.Clear();
			this.YarnDrop.onValueChanged.RemoveAllListeners();
			this.YarnList = new List<string>();
			foreach (string item in this.CurStory.YarnDict.Keys)
			{
				this.YarnList.Add(item);
			}
			this.YarnDrop.AddOptions(this.YarnList);
			this.YarnDrop.value = 0;
			this.YarnDrop.onValueChanged.AddListener(new UnityAction<int>(this.SelectYarn));
			if (this.YarnDrop.options.Count > 0)
			{
				this.SelectYarn(0);
			}
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x001DDD28 File Offset: 0x001DBF28
		private void SelectYarn(int id)
		{
			this.CurYarn = this.CurStory.YarnDict[this.YarnList[id]];
			this.CurTriggerConfig = this.CurStory.YarnDict[this.YarnList[id]].TriggerConfig;
			if (this.CurTriggerConfig == null)
			{
				this.CurTriggerConfig = new TriggerConfig();
			}
			this.UpdateTriggerUI();
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x001DDD98 File Offset: 0x001DBF98
		private void UpdateTriggerUI()
		{
			this.TriggerTypeDrop.value = this.CurTriggerConfig.Type;
			this.ValueToggle.isOn = this.CurTriggerConfig.IsValue;
			this.InputNpcId.text = this.CurTriggerConfig.NpcId;
			this.InputSceneName.text = this.CurTriggerConfig.SceneName;
			this.InputValueKey.text = this.CurTriggerConfig.ValueId;
			this.InputValue.text = this.CurTriggerConfig.Value;
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x001DDE2C File Offset: 0x001DC02C
		public void SaveCurTrigger()
		{
			if (this.CurYarn == null)
			{
				this.LogError("没有选中Yarn");
				return;
			}
			if (this.TriggerTypeDrop.value == 0 && (this.InputNpcId.text == "" || this.InputNpcId.text.Length < 1))
			{
				this.LogError("未填触发NPCId");
				return;
			}
			if (this.TriggerTypeDrop.value == 1 && (this.InputSceneName.text == "" || this.InputSceneName.text.Length < 1))
			{
				this.LogError("未填触发场景");
				return;
			}
			this.CurTriggerConfig.Type = this.TriggerTypeDrop.value;
			this.CurTriggerConfig.IsValue = this.ValueToggle.isOn;
			this.CurTriggerConfig.NpcId = this.InputNpcId.text;
			this.CurTriggerConfig.SceneName = this.InputSceneName.text;
			this.CurTriggerConfig.ValueId = this.InputValueKey.text;
			this.CurTriggerConfig.Value = this.InputValue.text;
			this.CurYarn.TriggerConfig = this.CurTriggerConfig;
			this.CurYarn.SaveTrigger();
			this.Log("保存成功");
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x001DDF80 File Offset: 0x001DC180
		public void CheckError()
		{
			if (!this.IsInit)
			{
				this.LogError("初始化失败");
			}
			foreach (Story story in this.StoryList)
			{
				foreach (string key in story.YarnDict.Keys)
				{
					if (!this.CheckCompiler(story.YarnDict[key].Path))
					{
						this.LogError("编译失败，错误文件：" + story.YarnDict[key].Path);
						return;
					}
				}
			}
			this.Log("编译完成，Yarn没有错误！");
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x00032061 File Offset: 0x00030261
		public void Log(string msg)
		{
			TextMeshProUGUI errorText = this.ErrorText;
			errorText.text = errorText.text + msg + "\n";
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x0003207F File Offset: 0x0003027F
		public void ClearLog()
		{
			this.ErrorText.text = "";
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x00032091 File Offset: 0x00030291
		public void DeleteTrigger()
		{
			this.CurTriggerConfig = new TriggerConfig();
			this.CurYarn.TriggerConfig = this.CurTriggerConfig;
			this.CurYarn.SaveTrigger();
			this.UpdateTriggerUI();
			this.Log("删除成功");
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x000320CB File Offset: 0x000302CB
		private void Awake()
		{
			StoryManager.Inst = this;
			Object.DontDestroyOnLoad(base.gameObject);
			this.Init();
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x000320E4 File Offset: 0x000302E4
		private void OnDestroy()
		{
			StoryManager.Inst = null;
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x001DE06C File Offset: 0x001DC26C
		public void Init()
		{
			this.IsEnd = true;
			if (!Directory.Exists(this.GetBepPath()))
			{
				return;
			}
			this.InitStory();
			this.CoreSystem.onCommand.AddListener(new UnityAction<string>(this.OnCommand));
			this.CoreSystem.onNodeStart.AddListener(new UnityAction<string>(this.OnNodeStart));
			this.CoreSystem.onNodeComplete.AddListener(new UnityAction<string>(this.OnNodeEnd));
			this.CoreSystem.onDialogueComplete.AddListener(new UnityAction(this.OnStoryEnd));
			this.IsInit = true;
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x001DE10C File Offset: 0x001DC30C
		public void InitStory()
		{
			this.StoryList = new List<Story>();
			string bepPath = this.GetBepPath();
			foreach (DirectoryInfo directoryInfo in new DirectoryInfo(bepPath).GetDirectories())
			{
				if (directoryInfo.Name.Contains("mod") && Directory.Exists(directoryInfo.FullName + "/Yarn") && Directory.Exists(directoryInfo.FullName + "/Yarn"))
				{
					Story story = new Story();
					story.ModName = directoryInfo.Name;
					story.YarnDict = new Dictionary<string, Yarn>();
					foreach (FileInfo fileInfo in new DirectoryInfo(this.AddBasePath(bepPath, directoryInfo.Name)).GetFiles())
					{
						if (fileInfo.Name.Contains(".yarn") && !fileInfo.Name.Contains(".meta"))
						{
							Yarn yarn = new Yarn();
							yarn.Name = fileInfo.Name.Replace(".yarn", "");
							yarn.Path = fileInfo.FullName;
							string text = fileInfo.FullName.Replace(".yarn", ".trigger");
							if (File.Exists(text))
							{
								try
								{
									FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read);
									TriggerConfig triggerConfig = (TriggerConfig)new BinaryFormatter().Deserialize(fileStream);
									fileStream.Close();
									yarn.TriggerConfig = triggerConfig;
									goto IL_19C;
								}
								catch (Exception ex)
								{
									this.LogError("无效的触发器" + text);
									Debug.LogError(ex);
									goto IL_19C;
								}
								goto IL_183;
							}
							goto IL_183;
							IL_19C:
							yarn.TriggerConfig.Path = yarn.Path;
							story.YarnDict.Add(yarn.Name, yarn);
							goto IL_1C4;
							IL_183:
							yarn.TriggerConfig = new TriggerConfig();
							yarn.TriggerConfig.IsNull = true;
							goto IL_19C;
						}
						IL_1C4:;
					}
					this.StoryList.Add(story);
				}
			}
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x000320EC File Offset: 0x000302EC
		public string AddBasePath(string BasePath, string ModName)
		{
			return BasePath + "/" + ModName + "/Yarn";
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x000320FF File Offset: 0x000302FF
		public string GetBepPath()
		{
			return Application.dataPath + "/../BepInEx/plugins/Next/";
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x00032110 File Offset: 0x00030310
		public string GetWorkShop()
		{
			return "";
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x001DE318 File Offset: 0x001DC518
		public bool CheckTrigger(string sceneName)
		{
			if (!this.IsInit)
			{
				return false;
			}
			foreach (Story story in this.StoryList)
			{
				foreach (string key in story.YarnDict.Keys)
				{
					Yarn yarn = story.YarnDict[key];
					if (!yarn.TriggerConfig.IsNull && yarn.TriggerConfig.Type == 1 && yarn.TriggerConfig.CanTrigger(sceneName))
					{
						this.CurStory = story;
						this.StartYarn(yarn);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x001DE408 File Offset: 0x001DC608
		public bool CheckTrigger(int npcId)
		{
			if (!this.IsInit)
			{
				return false;
			}
			foreach (Story story in this.StoryList)
			{
				foreach (string key in story.YarnDict.Keys)
				{
					Yarn yarn = story.YarnDict[key];
					if (!yarn.TriggerConfig.IsNull && yarn.TriggerConfig.Type == 1 && yarn.TriggerConfig.CanTrigger(npcId.ToString()))
					{
						this.CurStory = story;
						this.StartYarn(yarn);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x001DE4FC File Offset: 0x001DC6FC
		public void StartYarn(Yarn yarn)
		{
			if (this.IsEnd)
			{
				this.TempValueSystem.Clear();
				this.NextYarn = "";
				if (!this.CheckCompiler(yarn.Path))
				{
					this.LogError("编译失败" + yarn.Name);
					this.OnStoryEnd();
					return;
				}
				this.LoadYarn(yarn.Path);
				this.CoreSystem.ReStart();
				this.CoreSystem.StartDialogue("Init");
				this.IsEnd = false;
			}
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x00032117 File Offset: 0x00030317
		public void ReInit()
		{
			this.IsEnd = true;
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x00032120 File Offset: 0x00030320
		public void StopCurStory()
		{
			this.CoreSystem.Stop();
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x0003212D File Offset: 0x0003032D
		private void OnCommand(string command)
		{
			UnityAction beforeCommandStart = this.BeforeCommandStart;
			if (beforeCommandStart != null)
			{
				beforeCommandStart.Invoke();
			}
			Debug.Log(command);
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x00032146 File Offset: 0x00030346
		private void OnNodeStart(string node)
		{
			UnityAction nodeStart = this.NodeStart;
			if (nodeStart != null)
			{
				nodeStart.Invoke();
			}
			Debug.Log(node);
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x0003215F File Offset: 0x0003035F
		private void OnNodeEnd(string node)
		{
			UnityAction nodeEnd = this.NodeEnd;
			if (nodeEnd != null)
			{
				nodeEnd.Invoke();
			}
			Debug.Log(node);
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x001DE580 File Offset: 0x001DC780
		private void OnStoryEnd()
		{
			UnityAction storyEnd = this.StoryEnd;
			if (storyEnd != null)
			{
				storyEnd.Invoke();
			}
			this.CurYarn = null;
			this.IsEnd = true;
			this.StopCurStory();
			if (this.NextYarn != "")
			{
				if (this.CurStory.YarnDict.ContainsKey(this.NextYarn))
				{
					this.StartYarn(this.CurStory.YarnDict[this.NextYarn]);
					return;
				}
			}
			else
			{
				UnityAction oldTalk = this.OldTalk;
				if (oldTalk != null)
				{
					oldTalk.Invoke();
				}
				this.OldTalk = null;
			}
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x001DE614 File Offset: 0x001DC814
		private void LoadYarn(string fileName)
		{
			YarnProject yarnProject = this.CoreSystem.yarnProject;
			CompilationJob compilationJob = CompilationJob.CreateFromString(fileName, File.ReadAllText(fileName), null);
			compilationJob.VariableDeclarations = null;
			CompilationResult compilationResult = Compiler.Compile(compilationJob);
			Localization localization = ScriptableObject.CreateInstance<Localization>();
			string defaultLanguage = CultureInfo.CurrentCulture.Name;
			localization.LocaleCode = defaultLanguage;
			IEnumerable<StringTableEntry> enumerable = compilationResult.StringTable.Select(delegate(KeyValuePair<string, StringInfo> x)
			{
				StringTableEntry result = default(StringTableEntry);
				result.ID = x.Key;
				result.Language = defaultLanguage;
				result.Text = x.Value.text;
				result.File = x.Value.fileName;
				result.Node = x.Value.nodeName;
				result.LineNumber = x.Value.lineNumber.ToString();
				result.Lock = StoryManager.GetHashString(x.Value.text, 8);
				return result;
			});
			localization.AddLocalizedStrings(enumerable);
			yarnProject.baseLocalization = localization;
			yarnProject.localizations = new List<Localization>();
			yarnProject.localizations.Add(yarnProject.baseLocalization);
			localization.name = "Default (" + defaultLanguage + ")";
			byte[] compiledYarnProgram = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (CodedOutputStream codedOutputStream = new CodedOutputStream(memoryStream))
				{
					compilationResult.Program.WriteTo(codedOutputStream);
					codedOutputStream.Flush();
					compiledYarnProgram = memoryStream.ToArray();
				}
			}
			yarnProject.compiledYarnProgram = compiledYarnProgram;
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x001DE748 File Offset: 0x001DC948
		public bool CheckCompiler(string path)
		{
			CompilationJob compilationJob = CompilationJob.CreateFromString(path, File.ReadAllText(path), null);
			compilationJob.VariableDeclarations = null;
			CompilationResult compilationResult = Compiler.Compile(compilationJob);
			IEnumerable<Diagnostic> source = from d in compilationResult.Diagnostics
			where d.Severity == 0
			select d;
			if (source.Count<Diagnostic>() > 0)
			{
				using (IEnumerator<IGrouping<string, Diagnostic>> enumerator = (from e in source
				group e by e.FileName).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						foreach (string str in from e in enumerator.Current
						select e.ToString())
						{
							this.LogError("Error compiling: " + str);
							Debug.LogError("Error compiling: " + str);
						}
					}
				}
				return false;
			}
			if (compilationResult.Program == null)
			{
				Debug.LogError("Internal error: Failed to compile: resulting program was null, but compiler did not report errors.");
				return false;
			}
			return true;
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x00032178 File Offset: 0x00030378
		public void LogError(string msg)
		{
			TextMeshProUGUI errorText = this.ErrorText;
			errorText.text = errorText.text + "<color=#FF0000>" + msg + "</color>\n";
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x0003219B File Offset: 0x0003039B
		public void SetGoalValue(string key, string value)
		{
			Tools.instance.getPlayer().StreamData.SaveValueManager.SetValue(key, value);
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x000321B8 File Offset: 0x000303B8
		public string GetGoalValue(string key)
		{
			return Tools.instance.getPlayer().StreamData.SaveValueManager.GetValue(key);
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x001DE898 File Offset: 0x001DCA98
		private static string GetHashString(string inputString, int limitCharacters = -1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in StoryManager.GetHash(inputString))
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			if (limitCharacters == -1)
			{
				return stringBuilder.ToString();
			}
			return stringBuilder.ToString(0, Mathf.Min(stringBuilder.Length, limitCharacters));
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x001DE8F8 File Offset: 0x001DCAF8
		private static byte[] GetHash(string inputString)
		{
			byte[] result;
			using (HashAlgorithm hashAlgorithm = SHA256.Create())
			{
				result = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
			}
			return result;
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x000042DD File Offset: 0x000024DD
		private void Update()
		{
		}

		// Token: 0x04003E22 RID: 15906
		public DialogueRunner CoreSystem;

		// Token: 0x04003E23 RID: 15907
		public InMemoryVariableStorage TempValueSystem;

		// Token: 0x04003E24 RID: 15908
		public List<Story> StoryList = new List<Story>();

		// Token: 0x04003E25 RID: 15909
		public Story CurStory;

		// Token: 0x04003E26 RID: 15910
		public Yarn CurYarn;

		// Token: 0x04003E27 RID: 15911
		public GameObject UI;

		// Token: 0x04003E28 RID: 15912
		public static StoryManager Inst;

		// Token: 0x04003E29 RID: 15913
		public bool IsEnd = true;

		// Token: 0x04003E2A RID: 15914
		public bool IsInit;

		// Token: 0x04003E2B RID: 15915
		public UnityAction BeforeCommandStart;

		// Token: 0x04003E2C RID: 15916
		public UnityAction NodeStart;

		// Token: 0x04003E2D RID: 15917
		public UnityAction NodeEnd;

		// Token: 0x04003E2E RID: 15918
		public UnityAction StoryEnd;

		// Token: 0x04003E2F RID: 15919
		public TextMeshProUGUI ErrorText;

		// Token: 0x04003E30 RID: 15920
		public TMP_Dropdown ModDrop;

		// Token: 0x04003E31 RID: 15921
		public List<string> ModList = new List<string>();

		// Token: 0x04003E32 RID: 15922
		public string CurMod;

		// Token: 0x04003E33 RID: 15923
		public TMP_Dropdown YarnDrop;

		// Token: 0x04003E34 RID: 15924
		public List<string> YarnList = new List<string>();

		// Token: 0x04003E35 RID: 15925
		public TMP_Dropdown TriggerTypeDrop;

		// Token: 0x04003E36 RID: 15926
		public Toggle ValueToggle;

		// Token: 0x04003E37 RID: 15927
		public TMP_InputField InputNpcId;

		// Token: 0x04003E38 RID: 15928
		public TMP_InputField InputSceneName;

		// Token: 0x04003E39 RID: 15929
		public TMP_InputField InputValueKey;

		// Token: 0x04003E3A RID: 15930
		public TMP_InputField InputValue;

		// Token: 0x04003E3B RID: 15931
		public TriggerConfig CurTriggerConfig;

		// Token: 0x04003E3C RID: 15932
		public string NextYarn = "";

		// Token: 0x04003E3D RID: 15933
		public UnityAction OldTalk;
	}
}
