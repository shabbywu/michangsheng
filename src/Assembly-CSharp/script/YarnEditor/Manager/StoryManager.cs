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
	// Token: 0x020009C8 RID: 2504
	public class StoryManager : MonoBehaviour
	{
		// Token: 0x06004598 RID: 17816 RVA: 0x001D7BE9 File Offset: 0x001D5DE9
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

		// Token: 0x06004599 RID: 17817 RVA: 0x001D7C1C File Offset: 0x001D5E1C
		private void Hide()
		{
			this.UI.SetActive(false);
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x001D7C2C File Offset: 0x001D5E2C
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

		// Token: 0x0600459B RID: 17819 RVA: 0x001D7D0C File Offset: 0x001D5F0C
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

		// Token: 0x0600459C RID: 17820 RVA: 0x001D7E00 File Offset: 0x001D6000
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

		// Token: 0x0600459D RID: 17821 RVA: 0x001D7E70 File Offset: 0x001D6070
		private void UpdateTriggerUI()
		{
			this.TriggerTypeDrop.value = this.CurTriggerConfig.Type;
			this.ValueToggle.isOn = this.CurTriggerConfig.IsValue;
			this.InputNpcId.text = this.CurTriggerConfig.NpcId;
			this.InputSceneName.text = this.CurTriggerConfig.SceneName;
			this.InputValueKey.text = this.CurTriggerConfig.ValueId;
			this.InputValue.text = this.CurTriggerConfig.Value;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x001D7F04 File Offset: 0x001D6104
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

		// Token: 0x0600459F RID: 17823 RVA: 0x001D8058 File Offset: 0x001D6258
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

		// Token: 0x060045A0 RID: 17824 RVA: 0x001D8144 File Offset: 0x001D6344
		public void Log(string msg)
		{
			TextMeshProUGUI errorText = this.ErrorText;
			errorText.text = errorText.text + msg + "\n";
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x001D8162 File Offset: 0x001D6362
		public void ClearLog()
		{
			this.ErrorText.text = "";
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x001D8174 File Offset: 0x001D6374
		public void DeleteTrigger()
		{
			this.CurTriggerConfig = new TriggerConfig();
			this.CurYarn.TriggerConfig = this.CurTriggerConfig;
			this.CurYarn.SaveTrigger();
			this.UpdateTriggerUI();
			this.Log("删除成功");
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x001D81AE File Offset: 0x001D63AE
		private void Awake()
		{
			StoryManager.Inst = this;
			Object.DontDestroyOnLoad(base.gameObject);
			this.Init();
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x001D81C7 File Offset: 0x001D63C7
		private void OnDestroy()
		{
			StoryManager.Inst = null;
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x001D81D0 File Offset: 0x001D63D0
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

		// Token: 0x060045A6 RID: 17830 RVA: 0x001D8270 File Offset: 0x001D6470
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

		// Token: 0x060045A7 RID: 17831 RVA: 0x001D847C File Offset: 0x001D667C
		public string AddBasePath(string BasePath, string ModName)
		{
			return BasePath + "/" + ModName + "/Yarn";
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x001D848F File Offset: 0x001D668F
		public string GetBepPath()
		{
			return Application.dataPath + "/../BepInEx/plugins/Next/";
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x001D84A0 File Offset: 0x001D66A0
		public string GetWorkShop()
		{
			return "";
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x001D84A8 File Offset: 0x001D66A8
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

		// Token: 0x060045AB RID: 17835 RVA: 0x001D8598 File Offset: 0x001D6798
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

		// Token: 0x060045AC RID: 17836 RVA: 0x001D868C File Offset: 0x001D688C
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

		// Token: 0x060045AD RID: 17837 RVA: 0x001D8710 File Offset: 0x001D6910
		public void ReInit()
		{
			this.IsEnd = true;
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x001D8719 File Offset: 0x001D6919
		public void StopCurStory()
		{
			this.CoreSystem.Stop();
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x001D8726 File Offset: 0x001D6926
		private void OnCommand(string command)
		{
			UnityAction beforeCommandStart = this.BeforeCommandStart;
			if (beforeCommandStart != null)
			{
				beforeCommandStart.Invoke();
			}
			Debug.Log(command);
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x001D873F File Offset: 0x001D693F
		private void OnNodeStart(string node)
		{
			UnityAction nodeStart = this.NodeStart;
			if (nodeStart != null)
			{
				nodeStart.Invoke();
			}
			Debug.Log(node);
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x001D8758 File Offset: 0x001D6958
		private void OnNodeEnd(string node)
		{
			UnityAction nodeEnd = this.NodeEnd;
			if (nodeEnd != null)
			{
				nodeEnd.Invoke();
			}
			Debug.Log(node);
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x001D8774 File Offset: 0x001D6974
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

		// Token: 0x060045B3 RID: 17843 RVA: 0x001D8808 File Offset: 0x001D6A08
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

		// Token: 0x060045B4 RID: 17844 RVA: 0x001D893C File Offset: 0x001D6B3C
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

		// Token: 0x060045B5 RID: 17845 RVA: 0x001D8A8C File Offset: 0x001D6C8C
		public void LogError(string msg)
		{
			TextMeshProUGUI errorText = this.ErrorText;
			errorText.text = errorText.text + "<color=#FF0000>" + msg + "</color>\n";
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x001D8AAF File Offset: 0x001D6CAF
		public void SetGoalValue(string key, string value)
		{
			Tools.instance.getPlayer().StreamData.SaveValueManager.SetValue(key, value);
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x001D8ACC File Offset: 0x001D6CCC
		public string GetGoalValue(string key)
		{
			return Tools.instance.getPlayer().StreamData.SaveValueManager.GetValue(key);
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x001D8AE8 File Offset: 0x001D6CE8
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

		// Token: 0x060045B9 RID: 17849 RVA: 0x001D8B48 File Offset: 0x001D6D48
		private static byte[] GetHash(string inputString)
		{
			byte[] result;
			using (HashAlgorithm hashAlgorithm = SHA256.Create())
			{
				result = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
			}
			return result;
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x00004095 File Offset: 0x00002295
		private void Update()
		{
		}

		// Token: 0x04004716 RID: 18198
		public DialogueRunner CoreSystem;

		// Token: 0x04004717 RID: 18199
		public InMemoryVariableStorage TempValueSystem;

		// Token: 0x04004718 RID: 18200
		public List<Story> StoryList = new List<Story>();

		// Token: 0x04004719 RID: 18201
		public Story CurStory;

		// Token: 0x0400471A RID: 18202
		public Yarn CurYarn;

		// Token: 0x0400471B RID: 18203
		public GameObject UI;

		// Token: 0x0400471C RID: 18204
		public static StoryManager Inst;

		// Token: 0x0400471D RID: 18205
		public bool IsEnd = true;

		// Token: 0x0400471E RID: 18206
		public bool IsInit;

		// Token: 0x0400471F RID: 18207
		public UnityAction BeforeCommandStart;

		// Token: 0x04004720 RID: 18208
		public UnityAction NodeStart;

		// Token: 0x04004721 RID: 18209
		public UnityAction NodeEnd;

		// Token: 0x04004722 RID: 18210
		public UnityAction StoryEnd;

		// Token: 0x04004723 RID: 18211
		public TextMeshProUGUI ErrorText;

		// Token: 0x04004724 RID: 18212
		public TMP_Dropdown ModDrop;

		// Token: 0x04004725 RID: 18213
		public List<string> ModList = new List<string>();

		// Token: 0x04004726 RID: 18214
		public string CurMod;

		// Token: 0x04004727 RID: 18215
		public TMP_Dropdown YarnDrop;

		// Token: 0x04004728 RID: 18216
		public List<string> YarnList = new List<string>();

		// Token: 0x04004729 RID: 18217
		public TMP_Dropdown TriggerTypeDrop;

		// Token: 0x0400472A RID: 18218
		public Toggle ValueToggle;

		// Token: 0x0400472B RID: 18219
		public TMP_InputField InputNpcId;

		// Token: 0x0400472C RID: 18220
		public TMP_InputField InputSceneName;

		// Token: 0x0400472D RID: 18221
		public TMP_InputField InputValueKey;

		// Token: 0x0400472E RID: 18222
		public TMP_InputField InputValue;

		// Token: 0x0400472F RID: 18223
		public TriggerConfig CurTriggerConfig;

		// Token: 0x04004730 RID: 18224
		public string NextYarn = "";

		// Token: 0x04004731 RID: 18225
		public UnityAction OldTalk;
	}
}
