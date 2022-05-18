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
	// Token: 0x020012CD RID: 4813
	[ExecuteInEditMode]
	public class Flowchart : MonoBehaviour, ISubstitutionHandler
	{
		// Token: 0x060074CD RID: 29901 RVA: 0x0004FBF1 File Offset: 0x0004DDF1
		protected virtual void LevelWasLoaded()
		{
			Flowchart.eventSystemPresent = false;
		}

		// Token: 0x060074CE RID: 29902 RVA: 0x0004FBF9 File Offset: 0x0004DDF9
		protected virtual void Start()
		{
			this.CheckEventSystem();
		}

		// Token: 0x060074CF RID: 29903 RVA: 0x002AE584 File Offset: 0x002AC784
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

		// Token: 0x060074D0 RID: 29904 RVA: 0x0004FC01 File Offset: 0x0004DE01
		private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
		{
			this.LevelWasLoaded();
		}

		// Token: 0x060074D1 RID: 29905 RVA: 0x002AE5D0 File Offset: 0x002AC7D0
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

		// Token: 0x060074D2 RID: 29906 RVA: 0x0004FC09 File Offset: 0x0004DE09
		protected virtual void OnDisable()
		{
			Flowchart.cachedFlowcharts.Remove(this);
			SceneManager.activeSceneChanged -= new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
			StringSubstituter.UnregisterHandler(this);
		}

		// Token: 0x060074D3 RID: 29907 RVA: 0x002AE620 File Offset: 0x002AC820
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

		// Token: 0x060074D4 RID: 29908 RVA: 0x002AE66C File Offset: 0x002AC86C
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

		// Token: 0x060074D5 RID: 29909 RVA: 0x002AE720 File Offset: 0x002AC920
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

		// Token: 0x060074D6 RID: 29910 RVA: 0x0004FC2E File Offset: 0x0004DE2E
		protected virtual Block CreateBlockComponent(GameObject parent)
		{
			return parent.AddComponent<Block>();
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060074D7 RID: 29911 RVA: 0x0004FC36 File Offset: 0x0004DE36
		public static List<Flowchart> CachedFlowcharts
		{
			get
			{
				return Flowchart.cachedFlowcharts;
			}
		}

		// Token: 0x060074D8 RID: 29912 RVA: 0x002AE8BC File Offset: 0x002ACABC
		public static void BroadcastFungusMessage(string messageName)
		{
			MessageReceived[] array = Object.FindObjectsOfType<MessageReceived>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnSendFungusMessage(messageName);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060074D9 RID: 29913 RVA: 0x0004FC3D File Offset: 0x0004DE3D
		// (set) Token: 0x060074DA RID: 29914 RVA: 0x0004FC45 File Offset: 0x0004DE45
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

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060074DB RID: 29915 RVA: 0x0004FC4E File Offset: 0x0004DE4E
		// (set) Token: 0x060074DC RID: 29916 RVA: 0x0004FC56 File Offset: 0x0004DE56
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

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060074DD RID: 29917 RVA: 0x0004FC5F File Offset: 0x0004DE5F
		// (set) Token: 0x060074DE RID: 29918 RVA: 0x0004FC67 File Offset: 0x0004DE67
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

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060074DF RID: 29919 RVA: 0x0004FC70 File Offset: 0x0004DE70
		// (set) Token: 0x060074E0 RID: 29920 RVA: 0x0004FC78 File Offset: 0x0004DE78
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

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060074E1 RID: 29921 RVA: 0x0004FC81 File Offset: 0x0004DE81
		// (set) Token: 0x060074E2 RID: 29922 RVA: 0x0004FC89 File Offset: 0x0004DE89
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

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060074E3 RID: 29923 RVA: 0x0004FC92 File Offset: 0x0004DE92
		// (set) Token: 0x060074E4 RID: 29924 RVA: 0x0004FC9A File Offset: 0x0004DE9A
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

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060074E5 RID: 29925 RVA: 0x0004FCA3 File Offset: 0x0004DEA3
		// (set) Token: 0x060074E6 RID: 29926 RVA: 0x0004FCC8 File Offset: 0x0004DEC8
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

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060074E7 RID: 29927 RVA: 0x0004FCD7 File Offset: 0x0004DED7
		// (set) Token: 0x060074E8 RID: 29928 RVA: 0x0004FCDF File Offset: 0x0004DEDF
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

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060074E9 RID: 29929 RVA: 0x0004FCE8 File Offset: 0x0004DEE8
		public virtual List<Command> SelectedCommands
		{
			get
			{
				return this.selectedCommands;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060074EA RID: 29930 RVA: 0x0004FCF0 File Offset: 0x0004DEF0
		public virtual List<Variable> Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060074EB RID: 29931 RVA: 0x0004FCF8 File Offset: 0x0004DEF8
		public virtual int VariableCount
		{
			get
			{
				return this.variables.Count;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060074EC RID: 29932 RVA: 0x0004FD05 File Offset: 0x0004DF05
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060074ED RID: 29933 RVA: 0x0004FD0D File Offset: 0x0004DF0D
		public virtual float StepPause
		{
			get
			{
				return this.stepPause;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060074EE RID: 29934 RVA: 0x0004FD15 File Offset: 0x0004DF15
		public virtual bool ColorCommands
		{
			get
			{
				return this.colorCommands;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060074EF RID: 29935 RVA: 0x0004FD1D File Offset: 0x0004DF1D
		public virtual bool SaveSelection
		{
			get
			{
				return this.saveSelection;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060074F0 RID: 29936 RVA: 0x0004FD25 File Offset: 0x0004DF25
		public virtual string LocalizationId
		{
			get
			{
				return this.localizationId;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060074F1 RID: 29937 RVA: 0x0004FD2D File Offset: 0x0004DF2D
		public virtual bool ShowLineNumbers
		{
			get
			{
				return this.showLineNumbers;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060074F2 RID: 29938 RVA: 0x0004FD35 File Offset: 0x0004DF35
		public virtual LuaEnvironment LuaEnv
		{
			get
			{
				return this.luaEnvironment;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060074F3 RID: 29939 RVA: 0x0004FD3D File Offset: 0x0004DF3D
		public virtual string LuaBindingName
		{
			get
			{
				return this.luaBindingName;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060074F5 RID: 29941 RVA: 0x0004FD4E File Offset: 0x0004DF4E
		// (set) Token: 0x060074F4 RID: 29940 RVA: 0x0004FD45 File Offset: 0x0004DF45
		public virtual Vector2 CenterPosition { get; set; }

		// Token: 0x17000AD1 RID: 2769
		// (set) Token: 0x060074F6 RID: 29942 RVA: 0x0004FD56 File Offset: 0x0004DF56
		public int Version
		{
			set
			{
				this.version = value;
			}
		}

		// Token: 0x060074F7 RID: 29943 RVA: 0x0004FD5F File Offset: 0x0004DF5F
		public bool IsActive()
		{
			return base.gameObject.activeInHierarchy;
		}

		// Token: 0x060074F8 RID: 29944 RVA: 0x0004F634 File Offset: 0x0004D834
		public string GetName()
		{
			return base.gameObject.name;
		}

		// Token: 0x060074F9 RID: 29945 RVA: 0x002AE8E8 File Offset: 0x002ACAE8
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

		// Token: 0x060074FA RID: 29946 RVA: 0x002AE948 File Offset: 0x002ACB48
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

		// Token: 0x060074FB RID: 29947 RVA: 0x002AE9B0 File Offset: 0x002ACBB0
		public virtual Block CreateBlock(Vector2 position)
		{
			Block block = this.CreateBlockComponent(base.gameObject);
			block._NodeRect = new Rect(position.x, position.y, 0f, 0f);
			block.BlockName = this.GetUniqueBlockKey(block.BlockName, block);
			block.ItemId = this.NextItemId();
			return block;
		}

		// Token: 0x060074FC RID: 29948 RVA: 0x002AEA0C File Offset: 0x002ACC0C
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

		// Token: 0x060074FD RID: 29949 RVA: 0x0004FD6C File Offset: 0x0004DF6C
		public virtual bool HasBlock(string blockName)
		{
			return this.FindBlock(blockName) != null;
		}

		// Token: 0x060074FE RID: 29950 RVA: 0x0004FD7B File Offset: 0x0004DF7B
		public virtual bool ExecuteIfHasBlock(string blockName)
		{
			if (this.HasBlock(blockName))
			{
				this.ExecuteBlock(blockName);
				return true;
			}
			return false;
		}

		// Token: 0x060074FF RID: 29951 RVA: 0x002AEA44 File Offset: 0x002ACC44
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

		// Token: 0x06007500 RID: 29952 RVA: 0x002AEAD8 File Offset: 0x002ACCD8
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

		// Token: 0x06007501 RID: 29953 RVA: 0x002AEB3C File Offset: 0x002ACD3C
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

		// Token: 0x06007502 RID: 29954 RVA: 0x002AEBF4 File Offset: 0x002ACDF4
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

		// Token: 0x06007503 RID: 29955 RVA: 0x002AEC28 File Offset: 0x002ACE28
		public virtual void SendFungusMessage(string messageName)
		{
			MessageReceived[] components = base.GetComponents<MessageReceived>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].OnSendFungusMessage(messageName);
			}
		}

		// Token: 0x06007504 RID: 29956 RVA: 0x002AEC54 File Offset: 0x002ACE54
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

		// Token: 0x06007505 RID: 29957 RVA: 0x002AED30 File Offset: 0x002ACF30
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

		// Token: 0x06007506 RID: 29958 RVA: 0x002AEDB4 File Offset: 0x002ACFB4
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

		// Token: 0x06007507 RID: 29959 RVA: 0x002AEE50 File Offset: 0x002AD050
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

		// Token: 0x06007508 RID: 29960 RVA: 0x002AEE9C File Offset: 0x002AD09C
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

		// Token: 0x06007509 RID: 29961 RVA: 0x002AEF10 File Offset: 0x002AD110
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

		// Token: 0x0600750A RID: 29962 RVA: 0x002AEF88 File Offset: 0x002AD188
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

		// Token: 0x0600750B RID: 29963 RVA: 0x002AEFD4 File Offset: 0x002AD1D4
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

		// Token: 0x0600750C RID: 29964 RVA: 0x002AF028 File Offset: 0x002AD228
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

		// Token: 0x0600750D RID: 29965 RVA: 0x0004FD90 File Offset: 0x0004DF90
		public virtual bool GetBooleanVariable(string key)
		{
			return this.GetVariable<BooleanVariable>(key) != null && this.GetVariable<BooleanVariable>(key).Value;
		}

		// Token: 0x0600750E RID: 29966 RVA: 0x002AF078 File Offset: 0x002AD278
		public virtual void SetBooleanVariable(string key, bool value)
		{
			BooleanVariable variable = this.GetVariable<BooleanVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x0600750F RID: 29967 RVA: 0x0004FDAF File Offset: 0x0004DFAF
		public virtual int GetIntegerVariable(string key)
		{
			if (this.GetVariable<IntegerVariable>(key) != null)
			{
				return this.GetVariable<IntegerVariable>(key).Value;
			}
			return 0;
		}

		// Token: 0x06007510 RID: 29968 RVA: 0x002AF0A0 File Offset: 0x002AD2A0
		public virtual void SetIntegerVariable(string key, int value)
		{
			IntegerVariable variable = this.GetVariable<IntegerVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x0004FDCE File Offset: 0x0004DFCE
		public virtual float GetFloatVariable(string key)
		{
			if (this.GetVariable<FloatVariable>(key) != null)
			{
				return this.GetVariable<FloatVariable>(key).Value;
			}
			return 0f;
		}

		// Token: 0x06007512 RID: 29970 RVA: 0x002AF0C8 File Offset: 0x002AD2C8
		public virtual void SetFloatVariable(string key, float value)
		{
			FloatVariable variable = this.GetVariable<FloatVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x06007513 RID: 29971 RVA: 0x0004FDF1 File Offset: 0x0004DFF1
		public virtual string GetStringVariable(string key)
		{
			if (this.GetVariable<StringVariable>(key) != null)
			{
				return this.GetVariable<StringVariable>(key).Value;
			}
			return "";
		}

		// Token: 0x06007514 RID: 29972 RVA: 0x002AF0F0 File Offset: 0x002AD2F0
		public virtual void SetStringVariable(string key, string value)
		{
			StringVariable variable = this.GetVariable<StringVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x06007515 RID: 29973 RVA: 0x0004FE14 File Offset: 0x0004E014
		public virtual GameObject GetGameObjectVariable(string key)
		{
			if (this.GetVariable<GameObjectVariable>(key) != null)
			{
				return this.GetVariable<GameObjectVariable>(key).Value;
			}
			return null;
		}

		// Token: 0x06007516 RID: 29974 RVA: 0x002AF118 File Offset: 0x002AD318
		public virtual void SetGameObjectVariable(string key, GameObject value)
		{
			GameObjectVariable variable = this.GetVariable<GameObjectVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x06007517 RID: 29975 RVA: 0x0004FE33 File Offset: 0x0004E033
		public virtual Transform GetTransformVariable(string key)
		{
			if (this.GetVariable<TransformVariable>(key) != null)
			{
				return this.GetVariable<TransformVariable>(key).Value;
			}
			return null;
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x002AF140 File Offset: 0x002AD340
		public virtual void SetTransformVariable(string key, Transform value)
		{
			TransformVariable variable = this.GetVariable<TransformVariable>(key);
			if (variable != null)
			{
				variable.Value = value;
			}
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x002AF168 File Offset: 0x002AD368
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

		// Token: 0x0600751A RID: 29978 RVA: 0x0004FE52 File Offset: 0x0004E052
		public virtual void ClearSelectedCommands()
		{
			this.selectedCommands.Clear();
		}

		// Token: 0x0600751B RID: 29979 RVA: 0x0004FE5F File Offset: 0x0004E05F
		public virtual void AddSelectedCommand(Command command)
		{
			if (!this.selectedCommands.Contains(command))
			{
				this.selectedCommands.Add(command);
			}
		}

		// Token: 0x0600751C RID: 29980 RVA: 0x002AF248 File Offset: 0x002AD448
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

		// Token: 0x0600751D RID: 29981 RVA: 0x0004FE7B File Offset: 0x0004E07B
		public virtual void AddSelectedBlock(Block block)
		{
			if (!this.selectedBlocks.Contains(block))
			{
				block.IsSelected = true;
				this.selectedBlocks.Add(block);
			}
		}

		// Token: 0x0600751E RID: 29982 RVA: 0x0004FE9E File Offset: 0x0004E09E
		public virtual bool DeselectBlock(Block block)
		{
			if (this.selectedBlocks.Contains(block))
			{
				this.DeselectBlockNoCheck(block);
				return true;
			}
			return false;
		}

		// Token: 0x0600751F RID: 29983 RVA: 0x0004FEB8 File Offset: 0x0004E0B8
		public virtual void DeselectBlockNoCheck(Block b)
		{
			b.IsSelected = false;
			this.selectedBlocks.Remove(b);
		}

		// Token: 0x06007520 RID: 29984 RVA: 0x002AF2A8 File Offset: 0x002AD4A8
		public void UpdateSelectedCache()
		{
			this.selectedBlocks.Clear();
			Block[] components = base.gameObject.GetComponents<Block>();
			this.selectedBlocks = (from x in components
			where x.IsSelected
			select x).ToList<Block>();
		}

		// Token: 0x06007521 RID: 29985 RVA: 0x002AF2FC File Offset: 0x002AD4FC
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

		// Token: 0x06007522 RID: 29986 RVA: 0x002AF354 File Offset: 0x002AD554
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

		// Token: 0x06007523 RID: 29987 RVA: 0x002AF3A8 File Offset: 0x002AD5A8
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

		// Token: 0x06007524 RID: 29988 RVA: 0x002AF3D8 File Offset: 0x002AD5D8
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

		// Token: 0x06007525 RID: 29989 RVA: 0x002AF414 File Offset: 0x002AD614
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

		// Token: 0x06007526 RID: 29990 RVA: 0x002AF664 File Offset: 0x002AD864
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

		// Token: 0x06007527 RID: 29991 RVA: 0x002AF6CC File Offset: 0x002AD8CC
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

		// Token: 0x0400665D RID: 26205
		public const string SubstituteVariableRegexString = "{\\$.*?}";

		// Token: 0x0400665E RID: 26206
		[HideInInspector]
		[SerializeField]
		protected int version;

		// Token: 0x0400665F RID: 26207
		[HideInInspector]
		[SerializeField]
		protected Vector2 scrollPos;

		// Token: 0x04006660 RID: 26208
		[HideInInspector]
		[SerializeField]
		protected Vector2 variablesScrollPos;

		// Token: 0x04006661 RID: 26209
		[HideInInspector]
		[SerializeField]
		protected bool variablesExpanded = true;

		// Token: 0x04006662 RID: 26210
		[HideInInspector]
		[SerializeField]
		protected float blockViewHeight = 400f;

		// Token: 0x04006663 RID: 26211
		[HideInInspector]
		[SerializeField]
		protected float zoom = 1f;

		// Token: 0x04006664 RID: 26212
		[HideInInspector]
		[SerializeField]
		protected Rect scrollViewRect;

		// Token: 0x04006665 RID: 26213
		[HideInInspector]
		[SerializeField]
		protected List<Block> selectedBlocks = new List<Block>();

		// Token: 0x04006666 RID: 26214
		[HideInInspector]
		[SerializeField]
		protected List<Command> selectedCommands = new List<Command>();

		// Token: 0x04006667 RID: 26215
		[HideInInspector]
		[SerializeField]
		protected List<Variable> variables = new List<Variable>();

		// Token: 0x04006668 RID: 26216
		[TextArea(3, 5)]
		[Tooltip("Description text displayed in the Flowchart editor window")]
		[SerializeField]
		protected string description = "";

		// Token: 0x04006669 RID: 26217
		[Range(0f, 5f)]
		[Tooltip("Adds a pause after each execution step to make it easier to visualise program flow. Editor only, has no effect in platform builds.")]
		[SerializeField]
		protected float stepPause;

		// Token: 0x0400666A RID: 26218
		[Tooltip("Use command color when displaying the command list in the Fungus Editor window")]
		[SerializeField]
		protected bool colorCommands = true;

		// Token: 0x0400666B RID: 26219
		[Tooltip("Hides the Flowchart block and command components in the inspector. Deselect to inspect the block and command components that make up the Flowchart.")]
		[SerializeField]
		protected bool hideComponents = true;

		// Token: 0x0400666C RID: 26220
		[Tooltip("Saves the selected block and commands when saving the scene. Helps avoid version control conflicts if you've only changed the active selection.")]
		[SerializeField]
		protected bool saveSelection = true;

		// Token: 0x0400666D RID: 26221
		[Tooltip("Unique identifier for this flowchart in localized string keys. If no id is specified then the name of the Flowchart object will be used.")]
		[SerializeField]
		protected string localizationId = "";

		// Token: 0x0400666E RID: 26222
		[Tooltip("Display line numbers in the command list in the Block inspector.")]
		[SerializeField]
		protected bool showLineNumbers;

		// Token: 0x0400666F RID: 26223
		[Tooltip("List of commands to hide in the Add Command menu. Use this to restrict the set of commands available when editing a Flowchart.")]
		[SerializeField]
		protected List<string> hideCommands = new List<string>();

		// Token: 0x04006670 RID: 26224
		[Tooltip("Lua Environment to be used by default for all Execute Lua commands in this Flowchart")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x04006671 RID: 26225
		[Tooltip("The ExecuteLua command adds a global Lua variable with this name bound to the flowchart prior to executing.")]
		[SerializeField]
		protected string luaBindingName = "flowchart";

		// Token: 0x04006672 RID: 26226
		protected static List<Flowchart> cachedFlowcharts = new List<Flowchart>();

		// Token: 0x04006673 RID: 26227
		protected static bool eventSystemPresent;

		// Token: 0x04006674 RID: 26228
		protected StringSubstituter stringSubstituer;
	}
}
