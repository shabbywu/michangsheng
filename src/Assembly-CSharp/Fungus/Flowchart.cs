using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Fungus;

[ExecuteInEditMode]
public class Flowchart : MonoBehaviour, ISubstitutionHandler
{
	public const string SubstituteVariableRegexString = "{\\$.*?}";

	[HideInInspector]
	[SerializeField]
	protected int version;

	[HideInInspector]
	[SerializeField]
	protected Vector2 scrollPos;

	[HideInInspector]
	[SerializeField]
	protected Vector2 variablesScrollPos;

	[HideInInspector]
	[SerializeField]
	protected bool variablesExpanded = true;

	[HideInInspector]
	[SerializeField]
	protected float blockViewHeight = 400f;

	[HideInInspector]
	[SerializeField]
	protected float zoom = 1f;

	[HideInInspector]
	[SerializeField]
	protected Rect scrollViewRect;

	[HideInInspector]
	[SerializeField]
	protected List<Block> selectedBlocks = new List<Block>();

	[HideInInspector]
	[SerializeField]
	protected List<Command> selectedCommands = new List<Command>();

	[HideInInspector]
	[SerializeField]
	protected List<Variable> variables = new List<Variable>();

	[TextArea(3, 5)]
	[Tooltip("Description text displayed in the Flowchart editor window")]
	[SerializeField]
	protected string description = "";

	[Range(0f, 5f)]
	[Tooltip("Adds a pause after each execution step to make it easier to visualise program flow. Editor only, has no effect in platform builds.")]
	[SerializeField]
	protected float stepPause;

	[Tooltip("Use command color when displaying the command list in the Fungus Editor window")]
	[SerializeField]
	protected bool colorCommands = true;

	[Tooltip("Hides the Flowchart block and command components in the inspector. Deselect to inspect the block and command components that make up the Flowchart.")]
	[SerializeField]
	protected bool hideComponents = true;

	[Tooltip("Saves the selected block and commands when saving the scene. Helps avoid version control conflicts if you've only changed the active selection.")]
	[SerializeField]
	protected bool saveSelection = true;

	[Tooltip("Unique identifier for this flowchart in localized string keys. If no id is specified then the name of the Flowchart object will be used.")]
	[SerializeField]
	protected string localizationId = "";

	[Tooltip("Display line numbers in the command list in the Block inspector.")]
	[SerializeField]
	protected bool showLineNumbers;

	[Tooltip("List of commands to hide in the Add Command menu. Use this to restrict the set of commands available when editing a Flowchart.")]
	[SerializeField]
	protected List<string> hideCommands = new List<string>();

	[Tooltip("Lua Environment to be used by default for all Execute Lua commands in this Flowchart")]
	[SerializeField]
	protected LuaEnvironment luaEnvironment;

	[Tooltip("The ExecuteLua command adds a global Lua variable with this name bound to the flowchart prior to executing.")]
	[SerializeField]
	protected string luaBindingName = "flowchart";

	protected static List<Flowchart> cachedFlowcharts = new List<Flowchart>();

	protected static bool eventSystemPresent;

	protected StringSubstituter stringSubstituer;

	public static List<Flowchart> CachedFlowcharts => cachedFlowcharts;

	public virtual Vector2 ScrollPos
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return scrollPos;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			scrollPos = value;
		}
	}

	public virtual Vector2 VariablesScrollPos
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return variablesScrollPos;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			variablesScrollPos = value;
		}
	}

	public virtual bool VariablesExpanded
	{
		get
		{
			return variablesExpanded;
		}
		set
		{
			variablesExpanded = value;
		}
	}

	public virtual float BlockViewHeight
	{
		get
		{
			return blockViewHeight;
		}
		set
		{
			blockViewHeight = value;
		}
	}

	public virtual float Zoom
	{
		get
		{
			return zoom;
		}
		set
		{
			zoom = value;
		}
	}

	public virtual Rect ScrollViewRect
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return scrollViewRect;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			scrollViewRect = value;
		}
	}

	public virtual Block SelectedBlock
	{
		get
		{
			if (selectedBlocks == null || selectedBlocks.Count == 0)
			{
				return null;
			}
			return selectedBlocks[0];
		}
		set
		{
			ClearSelectedBlocks();
			AddSelectedBlock(value);
		}
	}

	public virtual List<Block> SelectedBlocks
	{
		get
		{
			return selectedBlocks;
		}
		set
		{
			selectedBlocks = value;
		}
	}

	public virtual List<Command> SelectedCommands => selectedCommands;

	public virtual List<Variable> Variables => variables;

	public virtual int VariableCount => variables.Count;

	public virtual string Description => description;

	public virtual float StepPause => stepPause;

	public virtual bool ColorCommands => colorCommands;

	public virtual bool SaveSelection => saveSelection;

	public virtual string LocalizationId => localizationId;

	public virtual bool ShowLineNumbers => showLineNumbers;

	public virtual LuaEnvironment LuaEnv => luaEnvironment;

	public virtual string LuaBindingName => luaBindingName;

	public virtual Vector2 CenterPosition { get; set; }

	public int Version
	{
		set
		{
			version = value;
		}
	}

	protected virtual void LevelWasLoaded()
	{
		eventSystemPresent = false;
	}

	protected virtual void Start()
	{
		CheckEventSystem();
	}

	protected virtual void CheckEventSystem()
	{
		if (eventSystemPresent)
		{
			return;
		}
		if ((Object)(object)Object.FindObjectOfType<EventSystem>() == (Object)null)
		{
			GameObject val = Resources.Load<GameObject>("Prefabs/EventSystem");
			if ((Object)(object)val != (Object)null)
			{
				((Object)Object.Instantiate<GameObject>(val)).name = "EventSystem";
			}
		}
		eventSystemPresent = true;
	}

	private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
	{
		LevelWasLoaded();
	}

	protected virtual void OnEnable()
	{
		if (!cachedFlowcharts.Contains(this))
		{
			cachedFlowcharts.Add(this);
			SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
		}
		CheckItemIds();
		CleanupComponents();
		UpdateVersion();
		StringSubstituter.RegisterHandler(this);
	}

	protected virtual void OnDisable()
	{
		cachedFlowcharts.Remove(this);
		SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
		StringSubstituter.UnregisterHandler(this);
	}

	protected virtual void UpdateVersion()
	{
		if (version == 1)
		{
			return;
		}
		Component[] components = ((Component)this).GetComponents<Component>();
		for (int i = 0; i < components.Length; i++)
		{
			if (components[i] is IUpdateable updateable)
			{
				updateable.UpdateToVersion(version, 1);
			}
		}
		version = 1;
	}

	protected virtual void CheckItemIds()
	{
		List<int> list = new List<int>();
		Block[] components = ((Component)this).GetComponents<Block>();
		foreach (Block block in components)
		{
			if (block.ItemId == -1 || list.Contains(block.ItemId))
			{
				block.ItemId = NextItemId();
			}
			list.Add(block.ItemId);
		}
		Command[] components2 = ((Component)this).GetComponents<Command>();
		foreach (Command command in components2)
		{
			if (command.ItemId == -1 || list.Contains(command.ItemId))
			{
				command.ItemId = NextItemId();
			}
			list.Add(command.ItemId);
		}
	}

	protected virtual void CleanupComponents()
	{
		variables.RemoveAll((Variable item) => (Object)(object)item == (Object)null);
		if (selectedBlocks == null)
		{
			selectedBlocks = new List<Block>();
		}
		if (selectedCommands == null)
		{
			selectedCommands = new List<Command>();
		}
		selectedBlocks.RemoveAll((Block item) => (Object)(object)item == (Object)null);
		selectedCommands.RemoveAll((Command item) => (Object)(object)item == (Object)null);
		Variable[] components = ((Component)this).GetComponents<Variable>();
		foreach (Variable variable in components)
		{
			if (!variables.Contains(variable))
			{
				Object.DestroyImmediate((Object)(object)variable);
			}
		}
		Block[] components2 = ((Component)this).GetComponents<Block>();
		Command[] components3 = ((Component)this).GetComponents<Command>();
		foreach (Command command in components3)
		{
			bool flag = false;
			for (int k = 0; k < components2.Length; k++)
			{
				if (components2[k].CommandList.Contains(command))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Object.DestroyImmediate((Object)(object)command);
			}
		}
		EventHandler[] components4 = ((Component)this).GetComponents<EventHandler>();
		foreach (EventHandler eventHandler in components4)
		{
			bool flag2 = false;
			for (int m = 0; m < components2.Length; m++)
			{
				if ((Object)(object)components2[m]._EventHandler == (Object)(object)eventHandler)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				Object.DestroyImmediate((Object)(object)eventHandler);
			}
		}
	}

	protected virtual Block CreateBlockComponent(GameObject parent)
	{
		return parent.AddComponent<Block>();
	}

	public static void BroadcastFungusMessage(string messageName)
	{
		MessageReceived[] array = Object.FindObjectsOfType<MessageReceived>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnSendFungusMessage(messageName);
		}
	}

	public bool IsActive()
	{
		return ((Component)this).gameObject.activeInHierarchy;
	}

	public string GetName()
	{
		return ((Object)((Component)this).gameObject).name;
	}

	public string GetParentName()
	{
		string text = ((!((Object)(object)((Component)this).transform.parent != (Object)null)) ? ((Object)((Component)this).transform).name : ((Object)((Component)this).transform.parent).name);
		if (text.Contains("(Clone)"))
		{
			text = text.Replace("(Clone)", "");
		}
		return text;
	}

	public int NextItemId()
	{
		int num = -1;
		Block[] components = ((Component)this).GetComponents<Block>();
		foreach (Block block in components)
		{
			num = Math.Max(num, block.ItemId);
		}
		Command[] components2 = ((Component)this).GetComponents<Command>();
		foreach (Command command in components2)
		{
			num = Math.Max(num, command.ItemId);
		}
		return num + 1;
	}

	public virtual Block CreateBlock(Vector2 position)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Block block = CreateBlockComponent(((Component)this).gameObject);
		block._NodeRect = new Rect(position.x, position.y, 0f, 0f);
		block.BlockName = GetUniqueBlockKey(block.BlockName, block);
		block.ItemId = NextItemId();
		return block;
	}

	public virtual Block FindBlock(string blockName)
	{
		Block[] components = ((Component)this).GetComponents<Block>();
		foreach (Block block in components)
		{
			if (block.BlockName == blockName)
			{
				return block;
			}
		}
		return null;
	}

	public virtual bool HasBlock(string blockName)
	{
		return (Object)(object)FindBlock(blockName) != (Object)null;
	}

	public virtual bool ExecuteIfHasBlock(string blockName)
	{
		if (HasBlock(blockName))
		{
			ExecuteBlock(blockName);
			return true;
		}
		return false;
	}

	public virtual void ExecuteBlock(string blockName)
	{
		Block block = FindBlock(blockName);
		if ((Object)(object)block == (Object)null)
		{
			Debug.LogError((object)("Fungus出现异常，所在Flowchart " + GetParentName() + "，Block " + blockName + " 不存在"));
		}
		else if (!ExecuteBlock(block))
		{
			Debug.LogWarning((object)("Fungus出现警告，所在Flowchart " + GetParentName() + "，Block " + blockName + " 执行失败"));
		}
	}

	public virtual void StopBlock(string blockName)
	{
		Block block = FindBlock(blockName);
		if ((Object)(object)block == (Object)null)
		{
			Debug.LogError((object)("Fungus出现异常，所在Flowchart " + GetParentName() + "，Block " + blockName + " 不存在"));
		}
		else if (block.IsExecuting())
		{
			block.Stop();
		}
	}

	public virtual bool ExecuteBlock(Block block, int commandIndex = 0, Action onComplete = null)
	{
		if ((Object)(object)block == (Object)null)
		{
			Debug.LogError((object)("Fungus出现异常，所在Flowchart " + GetParentName() + "，Block 不能为null"));
			return false;
		}
		if ((Object)(object)((Component)block).gameObject != (Object)(object)((Component)this).gameObject)
		{
			Debug.LogError((object)("Fungus出现异常，所在Flowchart " + GetParentName() + "，Block must belong to the same gameobject as this Flowchart"));
			return false;
		}
		if (block.IsExecuting())
		{
			Debug.LogWarning((object)("Fungus出现警告，所在Flowchart " + GetParentName() + "，Block " + block.BlockName + "无法执行，因为已经在运行中"));
			return false;
		}
		((MonoBehaviour)this).StartCoroutine(block.Execute(commandIndex, onComplete));
		return true;
	}

	public virtual void StopAllBlocks()
	{
		Block[] components = ((Component)this).GetComponents<Block>();
		foreach (Block block in components)
		{
			if (block.IsExecuting())
			{
				block.Stop();
			}
		}
	}

	public virtual void SendFungusMessage(string messageName)
	{
		MessageReceived[] components = ((Component)this).GetComponents<MessageReceived>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].OnSendFungusMessage(messageName);
		}
	}

	public virtual string GetUniqueVariableKey(string originalKey, Variable ignoreVariable = null)
	{
		int num = 0;
		string source = originalKey;
		source = new string(source.Where((char c) => char.IsLetterOrDigit(c) || c == '_').ToArray());
		source = source.TrimStart('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
		if (source.Length == 0)
		{
			source = "Var";
		}
		string text = source;
		bool flag;
		do
		{
			flag = false;
			for (int i = 0; i < variables.Count; i++)
			{
				Variable variable = variables[i];
				if (!((Object)(object)variable == (Object)null) && !((Object)(object)variable == (Object)(object)ignoreVariable) && variable.Key != null && variable.Key.Equals(text, StringComparison.CurrentCultureIgnoreCase))
				{
					flag = true;
					num++;
					text = source + num;
				}
			}
		}
		while (flag);
		return text;
	}

	public virtual string GetUniqueBlockKey(string originalKey, Block ignoreBlock = null)
	{
		int num = 0;
		string text = originalKey.Trim();
		if (text.Length == 0)
		{
			text = "New Block";
		}
		Block[] components = ((Component)this).GetComponents<Block>();
		string text2 = text;
		bool flag;
		do
		{
			flag = false;
			foreach (Block block in components)
			{
				if (!((Object)(object)block == (Object)(object)ignoreBlock) && block.BlockName != null && block.BlockName.Equals(text2, StringComparison.CurrentCultureIgnoreCase))
				{
					flag = true;
					num++;
					text2 = text + num;
				}
			}
		}
		while (flag);
		return text2;
	}

	public virtual string GetUniqueLabelKey(string originalKey, Label ignoreLabel)
	{
		int num = 0;
		string text = originalKey.Trim();
		if (text.Length == 0)
		{
			text = "New Label";
		}
		Block parentBlock = ignoreLabel.ParentBlock;
		string text2 = text;
		bool flag;
		do
		{
			flag = false;
			List<Command> commandList = parentBlock.CommandList;
			for (int i = 0; i < commandList.Count; i++)
			{
				Label label = commandList[i] as Label;
				if (!((Object)(object)label == (Object)null) && !((Object)(object)label == (Object)(object)ignoreLabel) && label.Key.Equals(text2, StringComparison.CurrentCultureIgnoreCase))
				{
					flag = true;
					num++;
					text2 = text + num;
				}
			}
		}
		while (flag);
		return text2;
	}

	public Variable GetVariable(string key)
	{
		for (int i = 0; i < variables.Count; i++)
		{
			Variable variable = variables[i];
			if ((Object)(object)variable != (Object)null && variable.Key == key)
			{
				return variable;
			}
		}
		return null;
	}

	public T GetVariable<T>(string key) where T : Variable
	{
		for (int i = 0; i < variables.Count; i++)
		{
			Variable variable = variables[i];
			if ((Object)(object)variable != (Object)null && variable.Key == key)
			{
				return variable as T;
			}
		}
		Debug.LogWarning((object)("Variable " + key + " not found."));
		return null;
	}

	public void SetVariable<T>(string key, T newvariable) where T : Variable
	{
		for (int i = 0; i < variables.Count; i++)
		{
			Variable variable = variables[i];
			if ((Object)(object)variable != (Object)null && variable.Key == key && (Object)(object)(variable as T) != (Object)null)
			{
				return;
			}
		}
		Debug.LogWarning((object)("Variable " + key + " not found."));
	}

	public virtual bool HasVariable(string key)
	{
		for (int i = 0; i < variables.Count; i++)
		{
			Variable variable = variables[i];
			if ((Object)(object)variable != (Object)null && variable.Key == key)
			{
				return true;
			}
		}
		return false;
	}

	public virtual string[] GetVariableNames()
	{
		string[] array = new string[variables.Count];
		for (int i = 0; i < variables.Count; i++)
		{
			Variable variable = variables[i];
			if ((Object)(object)variable != (Object)null)
			{
				array[i] = variable.Key;
			}
		}
		return array;
	}

	public virtual List<Variable> GetPublicVariables()
	{
		List<Variable> list = new List<Variable>();
		for (int i = 0; i < variables.Count; i++)
		{
			Variable variable = variables[i];
			if ((Object)(object)variable != (Object)null && variable.Scope == VariableScope.Public)
			{
				list.Add(variable);
			}
		}
		return list;
	}

	public virtual bool GetBooleanVariable(string key)
	{
		if ((Object)(object)GetVariable<BooleanVariable>(key) != (Object)null)
		{
			return GetVariable<BooleanVariable>(key).Value;
		}
		return false;
	}

	public virtual void SetBooleanVariable(string key, bool value)
	{
		BooleanVariable variable = GetVariable<BooleanVariable>(key);
		if ((Object)(object)variable != (Object)null)
		{
			variable.Value = value;
		}
	}

	public virtual int GetIntegerVariable(string key)
	{
		if ((Object)(object)GetVariable<IntegerVariable>(key) != (Object)null)
		{
			return GetVariable<IntegerVariable>(key).Value;
		}
		return 0;
	}

	public virtual void SetIntegerVariable(string key, int value)
	{
		IntegerVariable variable = GetVariable<IntegerVariable>(key);
		if ((Object)(object)variable != (Object)null)
		{
			variable.Value = value;
		}
	}

	public virtual float GetFloatVariable(string key)
	{
		if ((Object)(object)GetVariable<FloatVariable>(key) != (Object)null)
		{
			return GetVariable<FloatVariable>(key).Value;
		}
		return 0f;
	}

	public virtual void SetFloatVariable(string key, float value)
	{
		FloatVariable variable = GetVariable<FloatVariable>(key);
		if ((Object)(object)variable != (Object)null)
		{
			variable.Value = value;
		}
	}

	public virtual string GetStringVariable(string key)
	{
		if ((Object)(object)GetVariable<StringVariable>(key) != (Object)null)
		{
			return GetVariable<StringVariable>(key).Value;
		}
		return "";
	}

	public virtual void SetStringVariable(string key, string value)
	{
		StringVariable variable = GetVariable<StringVariable>(key);
		if ((Object)(object)variable != (Object)null)
		{
			variable.Value = value;
		}
	}

	public virtual GameObject GetGameObjectVariable(string key)
	{
		if ((Object)(object)GetVariable<GameObjectVariable>(key) != (Object)null)
		{
			return GetVariable<GameObjectVariable>(key).Value;
		}
		return null;
	}

	public virtual void SetGameObjectVariable(string key, GameObject value)
	{
		GameObjectVariable variable = GetVariable<GameObjectVariable>(key);
		if ((Object)(object)variable != (Object)null)
		{
			variable.Value = value;
		}
	}

	public virtual Transform GetTransformVariable(string key)
	{
		if ((Object)(object)GetVariable<TransformVariable>(key) != (Object)null)
		{
			return GetVariable<TransformVariable>(key).Value;
		}
		return null;
	}

	public virtual void SetTransformVariable(string key, Transform value)
	{
		TransformVariable variable = GetVariable<TransformVariable>(key);
		if ((Object)(object)variable != (Object)null)
		{
			variable.Value = value;
		}
	}

	public virtual void UpdateHideFlags()
	{
		if (hideComponents)
		{
			Block[] components = ((Component)this).GetComponents<Block>();
			foreach (Block block in components)
			{
				((Object)block).hideFlags = (HideFlags)2;
				if ((Object)(object)((Component)block).gameObject != (Object)(object)((Component)this).gameObject)
				{
					((Object)block).hideFlags = (HideFlags)1;
				}
			}
			Command[] components2 = ((Component)this).GetComponents<Command>();
			for (int j = 0; j < components2.Length; j++)
			{
				((Object)components2[j]).hideFlags = (HideFlags)2;
			}
			EventHandler[] components3 = ((Component)this).GetComponents<EventHandler>();
			for (int k = 0; k < components3.Length; k++)
			{
				((Object)components3[k]).hideFlags = (HideFlags)2;
			}
			return;
		}
		MonoBehaviour[] components4 = ((Component)this).GetComponents<MonoBehaviour>();
		foreach (MonoBehaviour val in components4)
		{
			if (!((Object)(object)val == (Object)null))
			{
				((Object)val).hideFlags = (HideFlags)0;
				((Object)((Component)val).gameObject).hideFlags = (HideFlags)0;
			}
		}
	}

	public virtual void ClearSelectedCommands()
	{
		selectedCommands.Clear();
	}

	public virtual void AddSelectedCommand(Command command)
	{
		if (!selectedCommands.Contains(command))
		{
			selectedCommands.Add(command);
		}
	}

	public virtual void ClearSelectedBlocks()
	{
		if (selectedBlocks == null)
		{
			selectedBlocks = new List<Block>();
		}
		for (int i = 0; i < selectedBlocks.Count; i++)
		{
			Block block = selectedBlocks[i];
			if ((Object)(object)block != (Object)null)
			{
				block.IsSelected = false;
			}
		}
		selectedBlocks.Clear();
	}

	public virtual void AddSelectedBlock(Block block)
	{
		if (!selectedBlocks.Contains(block))
		{
			block.IsSelected = true;
			selectedBlocks.Add(block);
		}
	}

	public virtual bool DeselectBlock(Block block)
	{
		if (selectedBlocks.Contains(block))
		{
			DeselectBlockNoCheck(block);
			return true;
		}
		return false;
	}

	public virtual void DeselectBlockNoCheck(Block b)
	{
		b.IsSelected = false;
		selectedBlocks.Remove(b);
	}

	public void UpdateSelectedCache()
	{
		selectedBlocks.Clear();
		Block[] components = ((Component)this).gameObject.GetComponents<Block>();
		selectedBlocks = components.Where((Block x) => x.IsSelected).ToList();
	}

	public virtual void Reset(bool resetCommands, bool resetVariables)
	{
		if (resetCommands)
		{
			Command[] components = ((Component)this).GetComponents<Command>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].OnReset();
			}
		}
		if (resetVariables)
		{
			for (int j = 0; j < variables.Count; j++)
			{
				variables[j].OnReset();
			}
		}
	}

	public virtual bool IsCommandSupported(CommandInfoAttribute commandInfo)
	{
		for (int i = 0; i < hideCommands.Count; i++)
		{
			string strB = hideCommands[i];
			if (string.Compare(commandInfo.Category, strB, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(commandInfo.CommandName, strB, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return false;
			}
		}
		return true;
	}

	public virtual bool HasExecutingBlocks()
	{
		Block[] components = ((Component)this).GetComponents<Block>();
		for (int i = 0; i < components.Length; i++)
		{
			if (components[i].IsExecuting())
			{
				return true;
			}
		}
		return false;
	}

	public virtual List<Block> GetExecutingBlocks()
	{
		List<Block> list = new List<Block>();
		Block[] components = ((Component)this).GetComponents<Block>();
		foreach (Block block in components)
		{
			if (block.IsExecuting())
			{
				list.Add(block);
			}
		}
		return list;
	}

	public virtual string SubstituteVariables(string input)
	{
		if (stringSubstituer == null)
		{
			stringSubstituer = new StringSubstituter();
		}
		Regex regex = new Regex("\\{SayStcVal=\\d*\\}");
		foreach (Match item in Regex.Matches(input, "\\{SayStcVal=\\d*\\}"))
		{
			if (!int.TryParse(item.Value.Replace("{SayStcVal=", "").Replace("}", ""), out var result))
			{
				continue;
			}
			int num = GlobalValue.Get(result, GetParentName() + " Flowchart.SubstituteVariables(" + input + ")");
			foreach (JSONObject item2 in jsonData.instance.StaticValueSay.list)
			{
				if (item2["StaticID"].I == result && item2["staticValue"].I == num)
				{
					input = regex.Replace(input, item2["ChinaText"].Str, 1);
					break;
				}
			}
		}
		StringBuilder stringBuilder = stringSubstituer._StringBuilder;
		stringBuilder.Length = 0;
		stringBuilder.Append(input);
		Regex regex2 = new Regex("{\\$.*?}");
		bool flag = false;
		MatchCollection matchCollection = regex2.Matches(input);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			string text = match.Value.Substring(2, match.Value.Length - 3);
			for (int j = 0; j < variables.Count; j++)
			{
				Variable variable = variables[j];
				if (!((Object)(object)variable == (Object)null) && variable.Scope == VariableScope.Private && variable.Key == text)
				{
					string newValue = ((object)variable).ToString();
					stringBuilder.Replace(match.Value, newValue);
					flag = true;
				}
			}
		}
		if (flag | stringSubstituer.SubstituteStrings(stringBuilder))
		{
			return stringBuilder.ToString();
		}
		return input;
	}

	public virtual void DetermineSubstituteVariables(string str, List<Variable> vars)
	{
		MatchCollection matchCollection = new Regex("{\\$.*?}").Matches(str);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			Variable variable = GetVariable(match.Value.Substring(2, match.Value.Length - 3));
			if ((Object)(object)variable != (Object)null)
			{
				vars.Add(variable);
			}
		}
	}

	[MoonSharpHidden]
	public virtual bool SubstituteStrings(StringBuilder input)
	{
		Regex regex = new Regex("{\\$.*?}");
		bool result = false;
		MatchCollection matchCollection = regex.Matches(input.ToString());
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			string text = match.Value.Substring(2, match.Value.Length - 3);
			for (int j = 0; j < variables.Count; j++)
			{
				Variable variable = variables[j];
				if (!((Object)(object)variable == (Object)null) && variable.Scope == VariableScope.Public && variable.Key == text)
				{
					string newValue = ((object)variable).ToString();
					input.Replace(match.Value, newValue);
					result = true;
				}
			}
		}
		return result;
	}
}
