using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using Google.Protobuf;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yarn;
using Yarn.Compiler;
using Yarn.Unity;
using script.YarnEditor.Component.TriggerData;
using script.YarnEditor.Mod;

namespace script.YarnEditor.Manager;

public class StoryManager : MonoBehaviour
{
	public DialogueRunner CoreSystem;

	public InMemoryVariableStorage TempValueSystem;

	public List<Story> StoryList = new List<Story>();

	public Story CurStory;

	public Yarn CurYarn;

	public GameObject UI;

	public static StoryManager Inst;

	public bool IsEnd = true;

	public bool IsInit;

	public UnityAction BeforeCommandStart;

	public UnityAction NodeStart;

	public UnityAction NodeEnd;

	public UnityAction StoryEnd;

	public TextMeshProUGUI ErrorText;

	public TMP_Dropdown ModDrop;

	public List<string> ModList = new List<string>();

	public string CurMod;

	public TMP_Dropdown YarnDrop;

	public List<string> YarnList = new List<string>();

	public TMP_Dropdown TriggerTypeDrop;

	public Toggle ValueToggle;

	public TMP_InputField InputNpcId;

	public TMP_InputField InputSceneName;

	public TMP_InputField InputValueKey;

	public TMP_InputField InputValue;

	public TriggerConfig CurTriggerConfig;

	public string NextYarn = "";

	public UnityAction OldTalk;

	private void Show()
	{
		if (Directory.Exists(GetBepPath()))
		{
			InitStory();
			UpdateDrop();
			UI.SetActive(true);
			((Component)this).transform.SetAsLastSibling();
		}
	}

	private void Hide()
	{
		UI.SetActive(false);
	}

	private void UpdateDrop()
	{
		ModDrop.options.Clear();
		((UnityEventBase)ModDrop.onValueChanged).RemoveAllListeners();
		CurStory = null;
		CurYarn = null;
		ModList = new List<string>();
		foreach (Story story in StoryList)
		{
			ModList.Add(story.ModName);
		}
		ModDrop.AddOptions(ModList);
		((UnityEvent<int>)(object)ModDrop.onValueChanged).AddListener((UnityAction<int>)SelectMod);
		if (ModDrop.options.Count > 0)
		{
			SelectMod(0);
		}
	}

	private void SelectMod(int id)
	{
		CurStory = StoryList[id];
		YarnDrop.options.Clear();
		((UnityEventBase)YarnDrop.onValueChanged).RemoveAllListeners();
		YarnList = new List<string>();
		foreach (string key in CurStory.YarnDict.Keys)
		{
			YarnList.Add(key);
		}
		YarnDrop.AddOptions(YarnList);
		YarnDrop.value = 0;
		((UnityEvent<int>)(object)YarnDrop.onValueChanged).AddListener((UnityAction<int>)SelectYarn);
		if (YarnDrop.options.Count > 0)
		{
			SelectYarn(0);
		}
	}

	private void SelectYarn(int id)
	{
		CurYarn = CurStory.YarnDict[YarnList[id]];
		CurTriggerConfig = CurStory.YarnDict[YarnList[id]].TriggerConfig;
		if (CurTriggerConfig == null)
		{
			CurTriggerConfig = new TriggerConfig();
		}
		UpdateTriggerUI();
	}

	private void UpdateTriggerUI()
	{
		TriggerTypeDrop.value = CurTriggerConfig.Type;
		ValueToggle.isOn = CurTriggerConfig.IsValue;
		InputNpcId.text = CurTriggerConfig.NpcId;
		InputSceneName.text = CurTriggerConfig.SceneName;
		InputValueKey.text = CurTriggerConfig.ValueId;
		InputValue.text = CurTriggerConfig.Value;
	}

	public void SaveCurTrigger()
	{
		if (CurYarn == null)
		{
			LogError("没有选中Yarn");
			return;
		}
		if (TriggerTypeDrop.value == 0 && (InputNpcId.text == "" || InputNpcId.text.Length < 1))
		{
			LogError("未填触发NPCId");
			return;
		}
		if (TriggerTypeDrop.value == 1 && (InputSceneName.text == "" || InputSceneName.text.Length < 1))
		{
			LogError("未填触发场景");
			return;
		}
		CurTriggerConfig.Type = TriggerTypeDrop.value;
		CurTriggerConfig.IsValue = ValueToggle.isOn;
		CurTriggerConfig.NpcId = InputNpcId.text;
		CurTriggerConfig.SceneName = InputSceneName.text;
		CurTriggerConfig.ValueId = InputValueKey.text;
		CurTriggerConfig.Value = InputValue.text;
		CurYarn.TriggerConfig = CurTriggerConfig;
		CurYarn.SaveTrigger();
		Log("保存成功");
	}

	public void CheckError()
	{
		if (!IsInit)
		{
			LogError("初始化失败");
		}
		foreach (Story story in StoryList)
		{
			foreach (string key in story.YarnDict.Keys)
			{
				if (!CheckCompiler(story.YarnDict[key].Path))
				{
					LogError("编译失败，错误文件：" + story.YarnDict[key].Path);
					return;
				}
			}
		}
		Log("编译完成，Yarn没有错误！");
	}

	public void Log(string msg)
	{
		TextMeshProUGUI errorText = ErrorText;
		((TMP_Text)errorText).text = ((TMP_Text)errorText).text + msg + "\n";
	}

	public void ClearLog()
	{
		((TMP_Text)ErrorText).text = "";
	}

	public void DeleteTrigger()
	{
		CurTriggerConfig = new TriggerConfig();
		CurYarn.TriggerConfig = CurTriggerConfig;
		CurYarn.SaveTrigger();
		UpdateTriggerUI();
		Log("删除成功");
	}

	private void Awake()
	{
		Inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		Init();
	}

	private void OnDestroy()
	{
		Inst = null;
	}

	public void Init()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		IsEnd = true;
		if (Directory.Exists(GetBepPath()))
		{
			InitStory();
			((UnityEvent<string>)(object)CoreSystem.onCommand).AddListener((UnityAction<string>)OnCommand);
			((UnityEvent<string>)(object)CoreSystem.onNodeStart).AddListener((UnityAction<string>)OnNodeStart);
			((UnityEvent<string>)(object)CoreSystem.onNodeComplete).AddListener((UnityAction<string>)OnNodeEnd);
			CoreSystem.onDialogueComplete.AddListener(new UnityAction(OnStoryEnd));
			IsInit = true;
		}
	}

	public void InitStory()
	{
		StoryList = new List<Story>();
		string bepPath = GetBepPath();
		DirectoryInfo[] directories = new DirectoryInfo(bepPath).GetDirectories();
		foreach (DirectoryInfo directoryInfo in directories)
		{
			if (!directoryInfo.Name.Contains("mod") || !Directory.Exists(directoryInfo.FullName + "/Yarn") || !Directory.Exists(directoryInfo.FullName + "/Yarn"))
			{
				continue;
			}
			Story story = new Story();
			story.ModName = directoryInfo.Name;
			story.YarnDict = new Dictionary<string, Yarn>();
			FileInfo[] files = new DirectoryInfo(AddBasePath(bepPath, directoryInfo.Name)).GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				if (!fileInfo.Name.Contains(".yarn") || fileInfo.Name.Contains(".meta"))
				{
					continue;
				}
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
					}
					catch (Exception ex)
					{
						LogError("无效的触发器" + text);
						Debug.LogError((object)ex);
					}
				}
				else
				{
					yarn.TriggerConfig = new TriggerConfig();
					yarn.TriggerConfig.IsNull = true;
				}
				yarn.TriggerConfig.Path = yarn.Path;
				story.YarnDict.Add(yarn.Name, yarn);
			}
			StoryList.Add(story);
		}
	}

	public string AddBasePath(string BasePath, string ModName)
	{
		return BasePath + "/" + ModName + "/Yarn";
	}

	public string GetBepPath()
	{
		return Application.dataPath + "/../BepInEx/plugins/Next/";
	}

	public string GetWorkShop()
	{
		return "";
	}

	public bool CheckTrigger(string sceneName)
	{
		if (!IsInit)
		{
			return false;
		}
		foreach (Story story in StoryList)
		{
			foreach (string key in story.YarnDict.Keys)
			{
				Yarn yarn = story.YarnDict[key];
				if (!yarn.TriggerConfig.IsNull && yarn.TriggerConfig.Type == 1 && yarn.TriggerConfig.CanTrigger(sceneName))
				{
					CurStory = story;
					StartYarn(yarn);
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckTrigger(int npcId)
	{
		if (!IsInit)
		{
			return false;
		}
		foreach (Story story in StoryList)
		{
			foreach (string key in story.YarnDict.Keys)
			{
				Yarn yarn = story.YarnDict[key];
				if (!yarn.TriggerConfig.IsNull && yarn.TriggerConfig.Type == 1 && yarn.TriggerConfig.CanTrigger(npcId.ToString()))
				{
					CurStory = story;
					StartYarn(yarn);
					return true;
				}
			}
		}
		return false;
	}

	public void StartYarn(Yarn yarn)
	{
		if (IsEnd)
		{
			((VariableStorageBehaviour)TempValueSystem).Clear();
			NextYarn = "";
			if (!CheckCompiler(yarn.Path))
			{
				LogError("编译失败" + yarn.Name);
				OnStoryEnd();
				return;
			}
			LoadYarn(yarn.Path);
			CoreSystem.ReStart();
			CoreSystem.StartDialogue("Init");
			IsEnd = false;
		}
	}

	public void ReInit()
	{
		IsEnd = true;
	}

	public void StopCurStory()
	{
		CoreSystem.Stop();
	}

	private void OnCommand(string command)
	{
		UnityAction beforeCommandStart = BeforeCommandStart;
		if (beforeCommandStart != null)
		{
			beforeCommandStart.Invoke();
		}
		Debug.Log((object)command);
	}

	private void OnNodeStart(string node)
	{
		UnityAction nodeStart = NodeStart;
		if (nodeStart != null)
		{
			nodeStart.Invoke();
		}
		Debug.Log((object)node);
	}

	private void OnNodeEnd(string node)
	{
		UnityAction nodeEnd = NodeEnd;
		if (nodeEnd != null)
		{
			nodeEnd.Invoke();
		}
		Debug.Log((object)node);
	}

	private void OnStoryEnd()
	{
		UnityAction storyEnd = StoryEnd;
		if (storyEnd != null)
		{
			storyEnd.Invoke();
		}
		CurYarn = null;
		IsEnd = true;
		StopCurStory();
		if (NextYarn != "")
		{
			if (CurStory.YarnDict.ContainsKey(NextYarn))
			{
				StartYarn(CurStory.YarnDict[NextYarn]);
			}
			return;
		}
		UnityAction oldTalk = OldTalk;
		if (oldTalk != null)
		{
			oldTalk.Invoke();
		}
		OldTalk = null;
	}

	private void LoadYarn(string fileName)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		YarnProject yarnProject = CoreSystem.yarnProject;
		CompilationJob val = CompilationJob.CreateFromString(fileName, File.ReadAllText(fileName), (Library)null);
		val.VariableDeclarations = null;
		CompilationResult val2 = Compiler.Compile(val);
		Localization val3 = ScriptableObject.CreateInstance<Localization>();
		string defaultLanguage = CultureInfo.CurrentCulture.Name;
		val3.LocaleCode = defaultLanguage;
		IEnumerable<StringTableEntry> enumerable = ((CompilationResult)(ref val2)).StringTable.Select(delegate(KeyValuePair<string, StringInfo> x)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			StringTableEntry result = default(StringTableEntry);
			result.ID = x.Key;
			result.Language = defaultLanguage;
			result.Text = x.Value.text;
			result.File = x.Value.fileName;
			result.Node = x.Value.nodeName;
			result.LineNumber = x.Value.lineNumber.ToString();
			result.Lock = GetHashString(x.Value.text, 8);
			return result;
		});
		val3.AddLocalizedStrings(enumerable);
		yarnProject.baseLocalization = val3;
		yarnProject.localizations = new List<Localization>();
		yarnProject.localizations.Add(yarnProject.baseLocalization);
		((Object)val3).name = "Default (" + defaultLanguage + ")";
		byte[] compiledYarnProgram = null;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			CodedOutputStream val4 = new CodedOutputStream((Stream)memoryStream);
			try
			{
				((CompilationResult)(ref val2)).Program.WriteTo(val4);
				val4.Flush();
				compiledYarnProgram = memoryStream.ToArray();
			}
			finally
			{
				((IDisposable)val4)?.Dispose();
			}
		}
		yarnProject.compiledYarnProgram = compiledYarnProgram;
	}

	public bool CheckCompiler(string path)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		CompilationJob val = CompilationJob.CreateFromString(path, File.ReadAllText(path), (Library)null);
		val.VariableDeclarations = null;
		CompilationResult val2 = Compiler.Compile(val);
		IEnumerable<Diagnostic> source = ((CompilationResult)(ref val2)).Diagnostics.Where((Diagnostic d) => (int)d.Severity == 0);
		if (source.Count() > 0)
		{
			foreach (IGrouping<string, Diagnostic> item in from e in source
				group e by e.FileName)
			{
				foreach (string item2 in item.Select((Diagnostic e) => ((object)e).ToString()))
				{
					LogError("Error compiling: " + item2);
					Debug.LogError((object)("Error compiling: " + item2));
				}
			}
			return false;
		}
		if (((CompilationResult)(ref val2)).Program == null)
		{
			Debug.LogError((object)"Internal error: Failed to compile: resulting program was null, but compiler did not report errors.");
			return false;
		}
		return true;
	}

	public void LogError(string msg)
	{
		TextMeshProUGUI errorText = ErrorText;
		((TMP_Text)errorText).text = ((TMP_Text)errorText).text + "<color=#FF0000>" + msg + "</color>\n";
	}

	public void SetGoalValue(string key, string value)
	{
		Tools.instance.getPlayer().StreamData.SaveValueManager.SetValue(key, value);
	}

	public string GetGoalValue(string key)
	{
		return Tools.instance.getPlayer().StreamData.SaveValueManager.GetValue(key);
	}

	private static string GetHashString(string inputString, int limitCharacters = -1)
	{
		StringBuilder stringBuilder = new StringBuilder();
		byte[] hash = GetHash(inputString);
		foreach (byte b in hash)
		{
			stringBuilder.Append(b.ToString("x2"));
		}
		if (limitCharacters == -1)
		{
			return stringBuilder.ToString();
		}
		return stringBuilder.ToString(0, Mathf.Min(stringBuilder.Length, limitCharacters));
	}

	private static byte[] GetHash(string inputString)
	{
		using HashAlgorithm hashAlgorithm = SHA256.Create();
		return hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
	}

	private void Update()
	{
	}
}
