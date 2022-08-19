using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000E70 RID: 3696
	[ExecuteInEditMode]
	public class Flowchart : MonoBehaviour, ISubstitutionHandler
	{
		// Token: 0x0600681B RID: 26651 RVA: 0x0028BC65 File Offset: 0x00289E65
		protected virtual void LevelWasLoaded()
		{
			Flowchart.eventSystemPresent = false;
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x0028BC6D File Offset: 0x00289E6D
		protected virtual void Start()
		{
			this.CheckEventSystem();
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x0028BC78 File Offset: 0x00289E78
		protected virtual void CheckEventSystem()
		{
			if (Flowchart.eventSystemPresent)
			{
				return;
			}
			if (Object.FindObjectOfType<EventSystem>() == null)
			{
				GameObject gameObject = Resources.Load<GameObject>("Prefabs/EventSystem");
				if (gameObject != null)
				{
					Object.Instantiate<GameObject>(gameObject).name = "EventSystem";
				}
			}
			Flowchart.eventSystemPresent = true;
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x0028BCC4 File Offset: 0x00289EC4
		private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
		{
			this.LevelWasLoaded();
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x0028BCCC File Offset: 0x00289ECC
		protected virtual void OnEnable()
		{
			if (!Flowchart.cachedFlowcharts.Contains(this))
			{
				Flowchart.cachedFlowcharts.Add(this);
				SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
			}
			this.CheckItemIds();
			this.CleanupComponents();
			this.UpdateVersion();
			StringSubstituter.RegisterHandler(this);
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x0028BD1A File Offset: 0x00289F1A
		protected virtual void OnDisable()
		{
			Flowchart.cachedFlowcharts.Remove(this);
			SceneManager.activeSceneChanged -= new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
			StringSubstituter.UnregisterHandler(this);
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x0028BD40 File Offset: 0x00289F40
		protected virtual void UpdateVersion()
		{
			if (this.version == 1)
			{
				return;
			}
			Component[] components = base.GetComponents<Component>();
			for (int i = 0; i < components.Length; i++)
			{
				IUpdateable updateable = components[i] as IUpdateable;
				if (updateable != null)
				{
					updateable.UpdateToVersion(this.version, 1);
				}
			}
			this.version = 1;
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x0028BD8C File Offset: 0x00289F8C
		protected virtual void CheckItemIds()
		{
			List<int> list = new List<int>();
			foreach (Block block in base.GetComponents<Block>())
			{
				if (block.ItemId == -1 || list.Contains(block.ItemId))
				{
					block.ItemId = this.NextItemId();
				}
				list.Add(block.ItemId);
			}
			foreach (Command command in base.GetComponents<Command>())
			{
				if (command.ItemId == -1 || list.Contains(command.ItemId))
				{
					command.ItemId = this.NextItemId();
				}
				list.Add(command.ItemId);
			}
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x0028BE40 File Offset: 0x0028A040
		protected virtual void CleanupComponents()
		{
			this.variables.RemoveAll((Variable item) => item == null);
			if (this.selectedBlocks == null)
			{
				this.selectedBlocks = new List<Block>();
			}
			if (this.selectedCommands == null)
			{
				this.selectedCommands = new List<Command>();
			}
			this.selectedBlocks.RemoveAll((Block item) => item == null);
			this.selectedCommands.RemoveAll((Command item) => item == null);
			foreach (Variable variable in base.GetComponents<Variable>())
			{
				if (!this.variables.Contains(variable))
				{
					Object.DestroyImmediate(variable);
				}
			}
			Block[] components2 = base.GetComponents<Block>();
			foreach (Command command in base.GetComponents<Command>())
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
					Object.DestroyImmediate(command);
				}
			}
			foreach (EventHandler eventHandler in base.GetComponents<EventHandler>())
			{
				bool flag2 = false;
				for (int m = 0; m < components2.Length; m++)
				{
					if (components2[m]._EventHandler == eventHandler)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					Object.DestroyImmediate(eventHandler);
				}
			}
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x0028BFDC File Offset: 0x0028A1DC
		protected virtual Block CreateBlockComponent(GameObject parent)
		{
			return parent.AddComponent<Block>();
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06006825 RID: 26661 RVA: 0x0028BFE4 File Offset: 0x0028A1E4
		public static List<Flowchart> CachedFlowcharts
		{
			get
			{
				return Flowchart.cachedFlowcharts;
			}
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x0028BFEC File Offset: 0x0028A1EC
		public static void BroadcastFungusMessage(string messageName)
		{
			MessageReceived[] array = Object.FindObjectsOfType<MessageReceived>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnSendFungusMessage(messageName);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06006827 RID: 26663 RVA: 0x0028C016 File Offset: 0x0028A216
		// (set) Token: 0x06006828 RID: 26664 RVA: 0x0028C01E File Offset: 0x0028A21E
		public virtual Vector2 ScrollPos
		{
			get
			{
				return this.scrollPos;
			}
			set
			{
				this.scrollPos = value;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06006829 RID: 26665 RVA: 0x0028C027 File Offset: 0x0028A227
		// (set) Token: 0x0600682A RID: 26666 RVA: 0x0028C02F File Offset: 0x0028A22F
		public virtual Vector2 VariablesScrollPos
		{
			get
			{
				return this.variablesScrollPos;
			}
			set
			{
				this.variablesScrollPos = value;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600682B RID: 26667 RVA: 0x0028C038 File Offset: 0x0028A238
		// (set) Token: 0x0600682C RID: 26668 RVA: 0x0028C040 File Offset: 0x0028A240
		public virtual bool VariablesExpanded
		{
			get
			{
				return this.variablesExpanded;
			}
			set
			{
				this.variablesExpanded = value;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600682D RID: 26669 RVA: 0x0028C049 File Offset: 0x0028A249
		// (set) Token: 0x0600682E RID: 26670 RVA: 0x0028C051 File Offset: 0x0028A251
		public virtual float BlockViewHeight
		{
			get
			{
				return this.blockViewHeight;
			}
			set
			{
				this.blockViewHeight = value;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600682F RID: 26671 RVA: 0x0028C05A File Offset: 0x0028A25A
		// (set) Token: 0x06006830 RID: 26672 RVA: 0x0028C062 File Offset: 0x0028A262
		public virtual float Zoom
		{
			get
			{
				return this.zoom;
			}
			set
			{
				this.zoom = value;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06006831 RID: 26673 RVA: 0x0028C06B File Offset: 0x0028A26B
		// (set) Token: 0x06006832 RID: 26674 RVA: 0x0028C073 File Offset: 0x0028A273
		public virtual Rect ScrollViewRect
		{
			get
			{
				return this.scrollViewRect;
			}
			set
			{
				this.scrollViewRect = value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06006833 RID: 26675 RVA: 0x0028C07C File Offset: 0x0028A27C
		// (set) Token: 0x06006834 RID: 26676 RVA: 0x0028C0A1 File Offset: 0x0028A2A1
		public virtual Block SelectedBlock
		{
			get
			{
				if (this.selectedBlocks == null || this.selectedBlocks.Count == 0)
				{
					return null;
				}
				return this.selectedBlocks[0];
			}
			set
			{
				this.ClearSelectedBlocks();
				this.AddSelectedBlock(value);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06006835 RID: 26677 RVA: 0x0028C0B0 File Offset: 0x0028A2B0
		// (set) Token: 0x06006836 RID: 26678 RVA: 0x0028C0B8 File Offset: 0x0028A2B8
		public virtual List<Block> SelectedBlocks
		{
			get
			{
				return this.selectedBlocks;
			}
			set
			{
				this.selectedBlocks = value;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06006837 RID: 26679 RVA: 0x0028C0C1 File Offset: 0x0028A2C1
		public virtual List<Command> SelectedCommands
		{
			get
			{
				return this.selectedCommands;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06006838 RID: 26680 RVA: 0x0028C0C9 File Offset: 0x0028A2C9
		public virtual List<Variable> Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06006839 RID: 26681 RVA: 0x0028C0D1 File Offset: 0x0028A2D1
		public virtual int VariableCount
		{
			get
			{
				return this.variables.Count;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600683A RID: 26682 RVA: 0x0028C0DE File Offset: 0x0028A2DE
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600683B RID: 26683 RVA: 0x0028C0E6 File Offset: 0x0028A2E6
		public virtual float StepPause
		{
			get
			{
				return this.stepPause;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x0600683C RID: 26684 RVA: 0x0028C0EE File Offset: 0x0028A2EE
		public virtual bool ColorCommands
		{
			get
			{
				return this.colorCommands;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600683D RID: 26685 RVA: 0x0028C0F6 File Offset: 0x0028A2F6
		public virtual bool SaveSelection
		{
			get
			{
				return this.saveSelection;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600683E RID: 26686 RVA: 0x0028C0FE File Offset: 0x0028A2FE
		public virtual string LocalizationId
		{
			get
			{
				return this.localizationId;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600683F RID: 26687 RVA: 0x0028C106 File Offset: 0x0028A306
		public virtual bool ShowLineNumbers
		{
			get
			{
				return this.showLineNumbers;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06006840 RID: 26688 RVA: 0x0028C10E File Offset: 0x0028A30E
		public virtual LuaEnvironment LuaEnv
		{
			get
			{
				return this.luaEnvironment;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06006841 RID: 26689 RVA: 0x0028C116 File Offset: 0x0028A316
		public virtual string LuaBindingName
		{
			get
			{
				return this.luaBindingName;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06006843 RID: 26691 RVA: 0x0028C127 File Offset: 0x0028A327
		// (set) Token: 0x06006842 RID: 26690 RVA: 0x0028C11E File Offset: 0x0028A31E
		public virtual Vector2 CenterPosition { get; set; }

		// Token: 0x17000860 RID: 2144
		// (set) Token: 0x06006844 RID: 26692 RVA: 0x0028C12F File Offset: 0x0028A32F
		public int Version
		{
			set
			{
				this.version = value;
			}
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x0028C138 File Offset: 0x0028A338
		public bool IsActive()
		{
			return base.gameObject.activeInHierarchy;
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x0028AE40 File Offset: 0x00289040
		public string GetName()
		{
			return base.gameObject.name;
		}

		// Token: 0x06006847 RID: 26695 RVA: 0x0028C148 File Offset: 0x0028A348
		public string GetParentName()
		{
			string text;
			if (base.transform.parent != null)
			{
				text = base.transform.parent.name;
			}
			else
			{
				text = base.transform.name;
			}
			if (text.Contains("(Clone)"))
			{
				text = text.Replace("(Clone)", "");
			}
			return text;
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x0028C1A8 File Offset: 0x0028A3A8
		public int NextItemId()
		{
			int num = -1;
			foreach (Block block in base.GetComponents<Block>())
			{
				num = Math.Max(num, block.ItemId);
			}
			foreach (Command command in base.GetComponents<Command>())
			{
				num = Math.Max(num, command.ItemId);
			}
			return num + 1;
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x0028C210 File Offset: 0x0028A410
		public virtual Block CreateBlock(Vector2 position)
		{
			Block block = this.CreateBlockComponent(base.gameObject);
			block._NodeRect = new Rect(position.x, position.y, 0f, 0f);
			block.BlockName = this.GetUniqueBlockKey(block.BlockName, block);
			block.ItemId = this.NextItemId();
			return block;
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x0028C26C File Offset: 0x0028A46C
		public virtual Block FindBlock(string blockName)
		{
			foreach (Block block in base.GetComponents<Block>())
			{
				if (block.BlockName == blockName)
				{
					return block;
				}
			}
			return null;
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x0028C2A3 File Offset: 0x0028A4A3
		public virtual bool HasBlock(string blockName)
		{
			return this.FindBlock(blockName) != null;
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x0028C2B2 File Offset: 0x0028A4B2
		public virtual bool ExecuteIfHasBlock(string blockName)
		{
			if (this.HasBlock(blockName))
			{
				this.ExecuteBlock(blockName);
				return true;
			}
			return false;
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x0028C2C8 File Offset: 0x0028A4C8
		public virtual void ExecuteBlock(string blockName)
		{
			Block block = this.FindBlock(blockName);
			if (block == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Fungus出现异常，所在Flowchart ",
					this.GetParentName(),
					"，Block ",
					blockName,
					" 不存在"
				}));
				return;
			}
			if (!this.ExecuteBlock(block, 0, null))
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Fungus出现警告，所在Flowchart ",
					this.GetParentName(),
					"，Block ",
					blockName,
					" 执行失败"
				}));
			}
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x0028C35C File Offset: 0x0028A55C
		public virtual void StopBlock(string blockName)
		{
			Block block = this.FindBlock(blockName);
			if (block == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Fungus出现异常，所在Flowchart ",
					this.GetParentName(),
					"，Block ",
					blockName,
					" 不存在"
				}));
				return;
			}
			if (block.IsExecuting())
			{
				block.Stop();
			}
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x0028C3C0 File Offset: 0x0028A5C0
		public virtual bool ExecuteBlock(Block block, int commandIndex = 0, Action onComplete = null)
		{
			if (block == null)
			{
				Debug.LogError("Fungus出现异常，所在Flowchart " + this.GetParentName() + "，Block 不能为null");
				return false;
			}
			if (block.gameObject != base.gameObject)
			{
				Debug.LogError("Fungus出现异常，所在Flowchart " + this.GetParentName() + "，Block must belong to the same gameobject as this Flowchart");
				return false;
			}
			if (block.IsExecuting())
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Fungus出现警告，所在Flowchart ",
					this.GetParentName(),
					"，Block ",
					block.BlockName,
					"无法执行，因为已经在运行中"
				}));
				return false;
			}
			base.StartCoroutine(block.Execute(commandIndex, onComplete));
			return true;
		}

		// Token: 0x06006850 RID: 26704 RVA: 0x0028C478 File Offset: 0x0028A678
		public virtual void StopAllBlocks()
		{
			foreach (Block block in base.GetComponents<Block>())
			{
				if (block.IsExecuting())
				{
					block.Stop();
				}
			}
		}

		// Token: 0x06006851 RID: 26705 RVA: 0x0028C4AC File Offset: 0x0028A6AC
		public virtual void SendFungusMessage(string messageName)
		{
			MessageReceived[] components = base.GetComponents<MessageReceived>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].OnSendFungusMessage(messageName);
			}
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x0028C4D8 File Offset: 0x0028A6D8
		public virtual string GetUniqueVariableKey(string originalKey, Variable ignoreVariable = null)
		{
			int num = 0;
			string text = new string((from c in originalKey
			where char.IsLetterOrDigit(c) || c == '_'
			select c).ToArray<char>());
			text = text.TrimStart(new char[]
			{
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9'
			});
			if (text.Length == 0)
			{
				text = "Var";
			}
			string text2 = text;
			bool flag;
			do
			{
				flag = false;
				for (int i = 0; i < this.variables.Count; i++)
				{
					Variable variable = this.variables[i];
					if (!(variable == null) && !(variable == ignoreVariable) && variable.Key != null && variable.Key.Equals(text2, StringComparison.CurrentCultureIgnoreCase))
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

		// Token: 0x06006853 RID: 26707 RVA: 0x0028C5B4 File Offset: 0x0028A7B4
		public virtual string GetUniqueBlockKey(string originalKey, Block ignoreBlock = null)
		{
			int num = 0;
			string text = originalKey.Trim();
			if (text.Length == 0)
			{
				text = "New Block";
			}
			Block[] components = base.GetComponents<Block>();
			string text2 = text;
			bool flag;
			do
			{
				flag = false;
				foreach (Block block in components)
				{
					if (!(block == ignoreBlock) && block.BlockName != null && block.BlockName.Equals(text2, StringComparison.CurrentCultureIgnoreCase))
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

		// Token: 0x06006854 RID: 26708 RVA: 0x0028C638 File Offset: 0x0028A838
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
					if (!(label == null) && !(label == ignoreLabel) && label.Key.Equals(text2, StringComparison.CurrentCultureIgnoreCase))
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

		// Token: 0x06006855 RID: 26709 RVA: 0x0028C6D4 File Offset: 0x0028A8D4
		public Variable GetVariable(string key)
		{
			for (int i = 0; i < this.variables.Count; i++)
			{
				Variable variable = this.variables[i];
				if (variable != null && variable.Key == key)
				{
					return variable;
				}
			}
			return null;
		}

		// Token: 0x06006856 RID: 26710 RVA: 0x0028C720 File Offset: 0x0028A920
		public T GetVariable<T>(string key) where T : Variable
		{
			for (int i = 0; i < this.variables.Count; i++)
			{
				Variable variable = this.variables[i];
				if (variable != null && variable.Key == key)
				{
					return variable as T;
				}
			}
			Debug.LogWarning("Variable " + key + " not found.");
			return default(T);
		}

		// Token: 0x06006857 RID: 26711 RVA: 0x0028C794 File Offset: 0x0028A994
		public void SetVariable<T>(string key, T newvariable) where T : Variable
		{
			for (int i = 0; i < this.variables.Count; i++)
			{
				Variable variable = this.variables[i];
				if (variable != null && variable.Key == key && variable as T != null)
				{
					return;
				}
			}
			Debug.LogWarning("Variable " + key + " not found.");
		}

		// Token: 0x06006858 RID: 26712 RVA: 0x0028C80C File Offset: 0x0028AA0C
		public virtual bool HasVariable(string key)
		{
			for (int i = 0; i < this.variables.Count; i++)
			{
				Variable variable = this.variables[i];
				if (variable != null && variable.Key == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006859 RID: 26713 RVA: 0x0028C858 File Offset: 0x0028AA58
		public virtual string[] GetVariableNames()
		{
			string[] array = new string[this.variables.Count];
			for (int i = 0; i < this.variables.Count; i++)
			{
				Variable variable = this.variables[i];
				if (variable != null)
				{
					array[i] = variable.Key;
				}
			}
			return array;
		}

		// Token: 0x0600685A RID: 26714 RVA: 0x0028C8AC File Offset: 0x0028AAAC
		public virtual List<Variable> GetPublicVariables()
		{
			List<Variable> list = new List<Variable>();
			for (int i = 0; i < this.variables.Count; i++)
			{
				Variable variable = this.variables[i];
				if (variable != null && variable.Scope == VariableScope.Public)
				{
					list.Add(variable);
				}
			}
			return list;
		}

		// Token: 0x0600685B RID: 26715 RVA: 0x0028C8FC File Offset: 0x0028AAFC
		public virtual bool GetBooleanVariable(string key)
		{
			return this.GetVariable<BooleanVariable>(key) != null && this.GetVariable<BooleanVariable>(key).Value;
		}

		// Token: 0x0600685C RID: 26716 RVA: 0x0028C91C File Offset: 0x0028AB1C
		public virtual void SetBooleanVariable(string key, bool value)
		{
			BooleanVariable variable = this.GetVariable<BooleanVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x0600685D RID: 26717 RVA: 0x0028C941 File Offset: 0x0028AB41
		public virtual int GetIntegerVariable(string key)
		{
			if (this.GetVariable<IntegerVariable>(key) != null)
			{
				return this.GetVariable<IntegerVariable>(key).Value;
			}
			return 0;
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x0028C960 File Offset: 0x0028AB60
		public virtual void SetIntegerVariable(string key, int value)
		{
			IntegerVariable variable = this.GetVariable<IntegerVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x0600685F RID: 26719 RVA: 0x0028C985 File Offset: 0x0028AB85
		public virtual float GetFloatVariable(string key)
		{
			if (this.GetVariable<FloatVariable>(key) != null)
			{
				return this.GetVariable<FloatVariable>(key).Value;
			}
			return 0f;
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x0028C9A8 File Offset: 0x0028ABA8
		public virtual void SetFloatVariable(string key, float value)
		{
			FloatVariable variable = this.GetVariable<FloatVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x06006861 RID: 26721 RVA: 0x0028C9CD File Offset: 0x0028ABCD
		public virtual string GetStringVariable(string key)
		{
			if (this.GetVariable<StringVariable>(key) != null)
			{
				return this.GetVariable<StringVariable>(key).Value;
			}
			return "";
		}

		// Token: 0x06006862 RID: 26722 RVA: 0x0028C9F0 File Offset: 0x0028ABF0
		public virtual void SetStringVariable(string key, string value)
		{
			StringVariable variable = this.GetVariable<StringVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x06006863 RID: 26723 RVA: 0x0028CA15 File Offset: 0x0028AC15
		public virtual GameObject GetGameObjectVariable(string key)
		{
			if (this.GetVariable<GameObjectVariable>(key) != null)
			{
				return this.GetVariable<GameObjectVariable>(key).Value;
			}
			return null;
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x0028CA34 File Offset: 0x0028AC34
		public virtual void SetGameObjectVariable(string key, GameObject value)
		{
			GameObjectVariable variable = this.GetVariable<GameObjectVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x0028CA59 File Offset: 0x0028AC59
		public virtual Transform GetTransformVariable(string key)
		{
			if (this.GetVariable<TransformVariable>(key) != null)
			{
				return this.GetVariable<TransformVariable>(key).Value;
			}
			return null;
		}

		// Token: 0x06006866 RID: 26726 RVA: 0x0028CA78 File Offset: 0x0028AC78
		public virtual void SetTransformVariable(string key, Transform value)
		{
			TransformVariable variable = this.GetVariable<TransformVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x06006867 RID: 26727 RVA: 0x0028CAA0 File Offset: 0x0028ACA0
		public virtual void UpdateHideFlags()
		{
			if (this.hideComponents)
			{
				foreach (Block block in base.GetComponents<Block>())
				{
					block.hideFlags = 2;
					if (block.gameObject != base.gameObject)
					{
						block.hideFlags = 1;
					}
				}
				Command[] components2 = base.GetComponents<Command>();
				for (int j = 0; j < components2.Length; j++)
				{
					components2[j].hideFlags = 2;
				}
				EventHandler[] components3 = base.GetComponents<EventHandler>();
				for (int k = 0; k < components3.Length; k++)
				{
					components3[k].hideFlags = 2;
				}
				return;
			}
			foreach (MonoBehaviour monoBehaviour in base.GetComponents<MonoBehaviour>())
			{
				if (!(monoBehaviour == null))
				{
					monoBehaviour.hideFlags = 0;
					monoBehaviour.gameObject.hideFlags = 0;
				}
			}
		}

		// Token: 0x06006868 RID: 26728 RVA: 0x0028CB7E File Offset: 0x0028AD7E
		public virtual void ClearSelectedCommands()
		{
			this.selectedCommands.Clear();
		}

		// Token: 0x06006869 RID: 26729 RVA: 0x0028CB8B File Offset: 0x0028AD8B
		public virtual void AddSelectedCommand(Command command)
		{
			if (!this.selectedCommands.Contains(command))
			{
				this.selectedCommands.Add(command);
			}
		}

		// Token: 0x0600686A RID: 26730 RVA: 0x0028CBA8 File Offset: 0x0028ADA8
		public virtual void ClearSelectedBlocks()
		{
			if (this.selectedBlocks == null)
			{
				this.selectedBlocks = new List<Block>();
			}
			for (int i = 0; i < this.selectedBlocks.Count; i++)
			{
				Block block = this.selectedBlocks[i];
				if (block != null)
				{
					block.IsSelected = false;
				}
			}
			this.selectedBlocks.Clear();
		}

		// Token: 0x0600686B RID: 26731 RVA: 0x0028CC06 File Offset: 0x0028AE06
		public virtual void AddSelectedBlock(Block block)
		{
			if (!this.selectedBlocks.Contains(block))
			{
				block.IsSelected = true;
				this.selectedBlocks.Add(block);
			}
		}

		// Token: 0x0600686C RID: 26732 RVA: 0x0028CC29 File Offset: 0x0028AE29
		public virtual bool DeselectBlock(Block block)
		{
			if (this.selectedBlocks.Contains(block))
			{
				this.DeselectBlockNoCheck(block);
				return true;
			}
			return false;
		}

		// Token: 0x0600686D RID: 26733 RVA: 0x0028CC43 File Offset: 0x0028AE43
		public virtual void DeselectBlockNoCheck(Block b)
		{
			b.IsSelected = false;
			this.selectedBlocks.Remove(b);
		}

		// Token: 0x0600686E RID: 26734 RVA: 0x0028CC5C File Offset: 0x0028AE5C
		public void UpdateSelectedCache()
		{
			this.selectedBlocks.Clear();
			Block[] components = base.gameObject.GetComponents<Block>();
			this.selectedBlocks = (from x in components
			where x.IsSelected
			select x).ToList<Block>();
		}

		// Token: 0x0600686F RID: 26735 RVA: 0x0028CCB0 File Offset: 0x0028AEB0
		public virtual void Reset(bool resetCommands, bool resetVariables)
		{
			if (resetCommands)
			{
				Command[] components = base.GetComponents<Command>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].OnReset();
				}
			}
			if (resetVariables)
			{
				for (int j = 0; j < this.variables.Count; j++)
				{
					this.variables[j].OnReset();
				}
			}
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x0028CD08 File Offset: 0x0028AF08
		public virtual bool IsCommandSupported(CommandInfoAttribute commandInfo)
		{
			for (int i = 0; i < this.hideCommands.Count; i++)
			{
				string strB = this.hideCommands[i];
				if (string.Compare(commandInfo.Category, strB, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(commandInfo.CommandName, strB, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006871 RID: 26737 RVA: 0x0028CD5C File Offset: 0x0028AF5C
		public virtual bool HasExecutingBlocks()
		{
			Block[] components = base.GetComponents<Block>();
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i].IsExecuting())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006872 RID: 26738 RVA: 0x0028CD8C File Offset: 0x0028AF8C
		public virtual List<Block> GetExecutingBlocks()
		{
			List<Block> list = new List<Block>();
			foreach (Block block in base.GetComponents<Block>())
			{
				if (block.IsExecuting())
				{
					list.Add(block);
				}
			}
			return list;
		}

		// Token: 0x06006873 RID: 26739 RVA: 0x0028CDC8 File Offset: 0x0028AFC8
		public virtual string SubstituteVariables(string input)
		{
			if (this.stringSubstituer == null)
			{
				this.stringSubstituer = new StringSubstituter(5);
			}
			Regex regex = new Regex("\\{SayStcVal=\\d*\\}");
			using (IEnumerator enumerator = Regex.Matches(input, "\\{SayStcVal=\\d*\\}").GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num;
					if (int.TryParse(((Match)enumerator.Current).Value.Replace("{SayStcVal=", "").Replace("}", ""), out num))
					{
						int num2 = GlobalValue.Get(num, this.GetParentName() + " Flowchart.SubstituteVariables(" + input + ")");
						foreach (JSONObject jsonobject in jsonData.instance.StaticValueSay.list)
						{
							if (jsonobject["StaticID"].I == num && jsonobject["staticValue"].I == num2)
							{
								input = regex.Replace(input, jsonobject["ChinaText"].Str, 1);
								break;
							}
						}
					}
				}
			}
			StringBuilder stringBuilder = this.stringSubstituer._StringBuilder;
			stringBuilder.Length = 0;
			stringBuilder.Append(input);
			Regex regex2 = new Regex("{\\$.*?}");
			bool flag = false;
			MatchCollection matchCollection = regex2.Matches(input);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				Match match = matchCollection[i];
				string b = match.Value.Substring(2, match.Value.Length - 3);
				for (int j = 0; j < this.variables.Count; j++)
				{
					Variable variable = this.variables[j];
					if (!(variable == null) && variable.Scope == VariableScope.Private && variable.Key == b)
					{
						string newValue = variable.ToString();
						stringBuilder.Replace(match.Value, newValue);
						flag = true;
					}
				}
			}
			flag |= this.stringSubstituer.SubstituteStrings(stringBuilder);
			if (flag)
			{
				return stringBuilder.ToString();
			}
			return input;
		}

		// Token: 0x06006874 RID: 26740 RVA: 0x0028D018 File Offset: 0x0028B218
		public virtual void DetermineSubstituteVariables(string str, List<Variable> vars)
		{
			MatchCollection matchCollection = new Regex("{\\$.*?}").Matches(str);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				Match match = matchCollection[i];
				Variable variable = this.GetVariable(match.Value.Substring(2, match.Value.Length - 3));
				if (variable != null)
				{
					vars.Add(variable);
				}
			}
		}

		// Token: 0x06006875 RID: 26741 RVA: 0x0028D080 File Offset: 0x0028B280
		[MoonSharpHidden]
		public virtual bool SubstituteStrings(StringBuilder input)
		{
			Regex regex = new Regex("{\\$.*?}");
			bool result = false;
			MatchCollection matchCollection = regex.Matches(input.ToString());
			for (int i = 0; i < matchCollection.Count; i++)
			{
				Match match = matchCollection[i];
				string b = match.Value.Substring(2, match.Value.Length - 3);
				for (int j = 0; j < this.variables.Count; j++)
				{
					Variable variable = this.variables[j];
					if (!(variable == null) && variable.Scope == VariableScope.Public && variable.Key == b)
					{
						string newValue = variable.ToString();
						input.Replace(match.Value, newValue);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x040058C5 RID: 22725
		public const string SubstituteVariableRegexString = "{\\$.*?}";

		// Token: 0x040058C6 RID: 22726
		[HideInInspector]
		[SerializeField]
		protected int version;

		// Token: 0x040058C7 RID: 22727
		[HideInInspector]
		[SerializeField]
		protected Vector2 scrollPos;

		// Token: 0x040058C8 RID: 22728
		[HideInInspector]
		[SerializeField]
		protected Vector2 variablesScrollPos;

		// Token: 0x040058C9 RID: 22729
		[HideInInspector]
		[SerializeField]
		protected bool variablesExpanded = true;

		// Token: 0x040058CA RID: 22730
		[HideInInspector]
		[SerializeField]
		protected float blockViewHeight = 400f;

		// Token: 0x040058CB RID: 22731
		[HideInInspector]
		[SerializeField]
		protected float zoom = 1f;

		// Token: 0x040058CC RID: 22732
		[HideInInspector]
		[SerializeField]
		protected Rect scrollViewRect;

		// Token: 0x040058CD RID: 22733
		[HideInInspector]
		[SerializeField]
		protected List<Block> selectedBlocks = new List<Block>();

		// Token: 0x040058CE RID: 22734
		[HideInInspector]
		[SerializeField]
		protected List<Command> selectedCommands = new List<Command>();

		// Token: 0x040058CF RID: 22735
		[HideInInspector]
		[SerializeField]
		protected List<Variable> variables = new List<Variable>();

		// Token: 0x040058D0 RID: 22736
		[TextArea(3, 5)]
		[Tooltip("Description text displayed in the Flowchart editor window")]
		[SerializeField]
		protected string description = "";

		// Token: 0x040058D1 RID: 22737
		[Range(0f, 5f)]
		[Tooltip("Adds a pause after each execution step to make it easier to visualise program flow. Editor only, has no effect in platform builds.")]
		[SerializeField]
		protected float stepPause;

		// Token: 0x040058D2 RID: 22738
		[Tooltip("Use command color when displaying the command list in the Fungus Editor window")]
		[SerializeField]
		protected bool colorCommands = true;

		// Token: 0x040058D3 RID: 22739
		[Tooltip("Hides the Flowchart block and command components in the inspector. Deselect to inspect the block and command components that make up the Flowchart.")]
		[SerializeField]
		protected bool hideComponents = true;

		// Token: 0x040058D4 RID: 22740
		[Tooltip("Saves the selected block and commands when saving the scene. Helps avoid version control conflicts if you've only changed the active selection.")]
		[SerializeField]
		protected bool saveSelection = true;

		// Token: 0x040058D5 RID: 22741
		[Tooltip("Unique identifier for this flowchart in localized string keys. If no id is specified then the name of the Flowchart object will be used.")]
		[SerializeField]
		protected string localizationId = "";

		// Token: 0x040058D6 RID: 22742
		[Tooltip("Display line numbers in the command list in the Block inspector.")]
		[SerializeField]
		protected bool showLineNumbers;

		// Token: 0x040058D7 RID: 22743
		[Tooltip("List of commands to hide in the Add Command menu. Use this to restrict the set of commands available when editing a Flowchart.")]
		[SerializeField]
		protected List<string> hideCommands = new List<string>();

		// Token: 0x040058D8 RID: 22744
		[Tooltip("Lua Environment to be used by default for all Execute Lua commands in this Flowchart")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x040058D9 RID: 22745
		[Tooltip("The ExecuteLua command adds a global Lua variable with this name bound to the flowchart prior to executing.")]
		[SerializeField]
		protected string luaBindingName = "flowchart";

		// Token: 0x040058DA RID: 22746
		protected static List<Flowchart> cachedFlowcharts = new List<Flowchart>();

		// Token: 0x040058DB RID: 22747
		protected static bool eventSystemPresent;

		// Token: 0x040058DC RID: 22748
		protected StringSubstituter stringSubstituer;
	}
}
