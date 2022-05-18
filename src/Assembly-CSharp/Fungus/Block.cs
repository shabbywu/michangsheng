using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x020012B9 RID: 4793
	[ExecuteInEditMode]
	[RequireComponent(typeof(Flowchart))]
	[AddComponentMenu("")]
	public class Block : Node
	{
		// Token: 0x060073F2 RID: 29682 RVA: 0x0004F24D File Offset: 0x0004D44D
		protected virtual void Awake()
		{
			this.SetExecutionInfo();
		}

		// Token: 0x060073F3 RID: 29683 RVA: 0x002ACB74 File Offset: 0x002AAD74
		protected virtual void SetExecutionInfo()
		{
			int num = 0;
			for (int i = 0; i < this.commandList.Count; i++)
			{
				Command command = this.commandList[i];
				if (!(command == null))
				{
					command.ParentBlock = this;
					command.CommandIndex = num++;
				}
			}
			this.UpdateIndentLevels();
			this.executionInfoSet = true;
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060073F4 RID: 29684 RVA: 0x0004F255 File Offset: 0x0004D455
		// (set) Token: 0x060073F5 RID: 29685 RVA: 0x0004F25D File Offset: 0x0004D45D
		public bool IsSelected { get; set; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060073F6 RID: 29686 RVA: 0x0004F266 File Offset: 0x0004D466
		// (set) Token: 0x060073F7 RID: 29687 RVA: 0x0004F26E File Offset: 0x0004D46E
		public bool IsFiltered { get; set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060073F8 RID: 29688 RVA: 0x0004F277 File Offset: 0x0004D477
		// (set) Token: 0x060073F9 RID: 29689 RVA: 0x0004F27F File Offset: 0x0004D47F
		public bool IsControlSelected { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060073FA RID: 29690 RVA: 0x0004F288 File Offset: 0x0004D488
		public virtual ExecutionState State
		{
			get
			{
				return this.executionState;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060073FB RID: 29691 RVA: 0x0004F290 File Offset: 0x0004D490
		// (set) Token: 0x060073FC RID: 29692 RVA: 0x0004F298 File Offset: 0x0004D498
		public virtual int ItemId
		{
			get
			{
				return this.itemId;
			}
			set
			{
				this.itemId = value;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060073FD RID: 29693 RVA: 0x0004F2A1 File Offset: 0x0004D4A1
		// (set) Token: 0x060073FE RID: 29694 RVA: 0x0004F2A9 File Offset: 0x0004D4A9
		public virtual string BlockName
		{
			get
			{
				return this.blockName;
			}
			set
			{
				this.blockName = value;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060073FF RID: 29695 RVA: 0x0004F2B2 File Offset: 0x0004D4B2
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06007400 RID: 29696 RVA: 0x0004F2BA File Offset: 0x0004D4BA
		// (set) Token: 0x06007401 RID: 29697 RVA: 0x0004F2C2 File Offset: 0x0004D4C2
		public virtual EventHandler _EventHandler
		{
			get
			{
				return this.eventHandler;
			}
			set
			{
				this.eventHandler = value;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06007402 RID: 29698 RVA: 0x0004F2CB File Offset: 0x0004D4CB
		public virtual Command ActiveCommand
		{
			get
			{
				return this.activeCommand;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06007403 RID: 29699 RVA: 0x0004F2D3 File Offset: 0x0004D4D3
		// (set) Token: 0x06007404 RID: 29700 RVA: 0x0004F2DB File Offset: 0x0004D4DB
		public virtual float ExecutingIconTimer { get; set; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06007405 RID: 29701 RVA: 0x0004F2E4 File Offset: 0x0004D4E4
		public virtual List<Command> CommandList
		{
			get
			{
				return this.commandList;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (set) Token: 0x06007406 RID: 29702 RVA: 0x0004F2EC File Offset: 0x0004D4EC
		public virtual int JumpToCommandIndex
		{
			set
			{
				this.jumpToCommandIndex = value;
			}
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x0004F2F5 File Offset: 0x0004D4F5
		public virtual Flowchart GetFlowchart()
		{
			return base.GetComponent<Flowchart>();
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x0004F2FD File Offset: 0x0004D4FD
		public virtual bool IsExecuting()
		{
			return this.executionState == ExecutionState.Executing;
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x0004F308 File Offset: 0x0004D508
		public virtual int GetExecutionCount()
		{
			return this.executionCount;
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x0004F310 File Offset: 0x0004D510
		public virtual void StartExecution()
		{
			base.StartCoroutine(this.Execute(0, null));
		}

		// Token: 0x0600740B RID: 29707 RVA: 0x0004F321 File Offset: 0x0004D521
		public virtual IEnumerator Execute(int commandIndex = 0, Action onComplete = null)
		{
			new List<Command>();
			if (this.executionState != ExecutionState.Idle)
			{
				Debug.LogWarning(this.BlockName + " cannot be executed, it is already running.");
				yield break;
			}
			this.lastOnCompleteAction = onComplete;
			if (!this.executionInfoSet)
			{
				this.SetExecutionInfo();
			}
			this.executionCount++;
			int executionCountAtStart = this.executionCount;
			Flowchart flowchart = this.GetFlowchart();
			this.executionState = ExecutionState.Executing;
			BlockSignals.DoBlockStart(this);
			this.jumpToCommandIndex = commandIndex;
			int i = 0;
			for (;;)
			{
				if (this.jumpToCommandIndex > -1)
				{
					i = this.jumpToCommandIndex;
					this.jumpToCommandIndex = -1;
				}
				try
				{
					while (i < this.commandList.Count && (!this.commandList[i].enabled || this.commandList[i].GetType() == typeof(Comment) || this.commandList[i].GetType() == typeof(Label)))
					{
						i = this.commandList[i].CommandIndex + 1;
						int num = i + 1;
						int count = this.commandList.Count;
					}
				}
				catch (Exception ex)
				{
					Debug.Log(ex);
				}
				if (i >= this.commandList.Count)
				{
					goto IL_3F7;
				}
				if (this.activeCommand == null)
				{
					this.previousActiveCommandIndex = -1;
				}
				else
				{
					this.previousActiveCommandIndex = this.activeCommand.CommandIndex;
				}
				Command command = this.commandList[i];
				if (command == null)
				{
					break;
				}
				this.activeCommand = command;
				if (flowchart.IsActive() && ((flowchart.SelectedCommands.Count == 0 && i == 0) || (flowchart.SelectedCommands.Count == 1 && flowchart.SelectedCommands[0].CommandIndex == this.previousActiveCommandIndex)))
				{
					flowchart.ClearSelectedCommands();
					flowchart.AddSelectedCommand(this.commandList[i]);
				}
				command.IsExecuting = true;
				command.ExecutingIconTimer = Time.realtimeSinceStartup + 0.5f;
				BlockSignals.DoCommandExecute(this, command, i, this.commandList.Count);
				command.Execute();
				while (this.jumpToCommandIndex == -1)
				{
					yield return null;
				}
				command.IsExecuting = false;
				command = null;
			}
			string text = string.Format("当前block含有{0}个command，当前处于{1}", this.commandList.Count, i);
			if (flowchart.gameObject.transform.parent != null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"fungus遇到空command，已断开循环，当前场景",
					SceneEx.NowSceneName,
					"，Flowchart:",
					flowchart.gameObject.transform.parent.name,
					"，Block:",
					this.BlockName,
					"\n",
					text
				}));
			}
			else
			{
				Debug.LogError(string.Concat(new string[]
				{
					"fungus遇到空command，已断开循环，当前场景",
					SceneEx.NowSceneName,
					"，Flowchart:",
					flowchart.GetName(),
					"，Block:",
					this.BlockName,
					"\n",
					text
				}));
			}
			IL_3F7:
			if (this.State == ExecutionState.Executing && executionCountAtStart == this.executionCount)
			{
				this.ReturnToIdle();
			}
			yield break;
		}

		// Token: 0x0600740C RID: 29708 RVA: 0x0004F33E File Offset: 0x0004D53E
		private void ReturnToIdle()
		{
			this.executionState = ExecutionState.Idle;
			this.activeCommand = null;
			BlockSignals.DoBlockEnd(this);
			if (this.lastOnCompleteAction != null)
			{
				this.lastOnCompleteAction();
			}
			this.lastOnCompleteAction = null;
		}

		// Token: 0x0600740D RID: 29709 RVA: 0x0004F36E File Offset: 0x0004D56E
		public virtual void Stop()
		{
			if (this.activeCommand != null)
			{
				this.activeCommand.IsExecuting = false;
				this.activeCommand.OnStopExecuting();
			}
			this.jumpToCommandIndex = int.MaxValue;
			this.ReturnToIdle();
		}

		// Token: 0x0600740E RID: 29710 RVA: 0x002ACBD0 File Offset: 0x002AADD0
		public virtual List<Block> GetConnectedBlocks()
		{
			List<Block> result = new List<Block>();
			for (int i = 0; i < this.commandList.Count; i++)
			{
				Command command = this.commandList[i];
				if (command != null)
				{
					command.GetConnectedBlocks(ref result);
				}
			}
			return result;
		}

		// Token: 0x0600740F RID: 29711 RVA: 0x0004F3A6 File Offset: 0x0004D5A6
		public virtual Type GetPreviousActiveCommandType()
		{
			if (this.previousActiveCommandIndex >= 0 && this.previousActiveCommandIndex < this.commandList.Count)
			{
				return this.commandList[this.previousActiveCommandIndex].GetType();
			}
			return null;
		}

		// Token: 0x06007410 RID: 29712 RVA: 0x0004F3DC File Offset: 0x0004D5DC
		public virtual int GetPreviousActiveCommandIndent()
		{
			if (this.previousActiveCommandIndex >= 0 && this.previousActiveCommandIndex < this.commandList.Count)
			{
				return this.commandList[this.previousActiveCommandIndex].IndentLevel;
			}
			return -1;
		}

		// Token: 0x06007411 RID: 29713 RVA: 0x002ACC18 File Offset: 0x002AAE18
		public virtual void UpdateIndentLevels()
		{
			int num = 0;
			for (int i = 0; i < this.commandList.Count; i++)
			{
				Command command = this.commandList[i];
				if (!(command == null))
				{
					if (command.CloseBlock())
					{
						num--;
					}
					num = Math.Max(num, 0);
					command.IndentLevel = num;
					if (command.OpenBlock())
					{
						num++;
					}
				}
			}
		}

		// Token: 0x06007412 RID: 29714 RVA: 0x002ACC7C File Offset: 0x002AAE7C
		public virtual int GetLabelIndex(string labelKey)
		{
			if (labelKey.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.commandList.Count; i++)
			{
				Label label = this.commandList[i] as Label;
				if (label != null && string.Compare(label.Key, labelKey, true) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040065C9 RID: 26057
		[SerializeField]
		protected int itemId = -1;

		// Token: 0x040065CA RID: 26058
		[FormerlySerializedAs("sequenceName")]
		[Tooltip("The name of the block node as displayed in the Flowchart window")]
		[SerializeField]
		public string blockName = "New Block";

		// Token: 0x040065CB RID: 26059
		[TextArea(2, 5)]
		[Tooltip("Description text to display under the block node")]
		[SerializeField]
		protected string description = "";

		// Token: 0x040065CC RID: 26060
		[Tooltip("An optional Event Handler which can execute the block when an event occurs")]
		[SerializeField]
		protected EventHandler eventHandler;

		// Token: 0x040065CD RID: 26061
		[SerializeField]
		public List<Command> commandList = new List<Command>();

		// Token: 0x040065CE RID: 26062
		protected ExecutionState executionState;

		// Token: 0x040065CF RID: 26063
		protected Command activeCommand;

		// Token: 0x040065D0 RID: 26064
		protected Action lastOnCompleteAction;

		// Token: 0x040065D1 RID: 26065
		protected int previousActiveCommandIndex = -1;

		// Token: 0x040065D2 RID: 26066
		protected int jumpToCommandIndex = -1;

		// Token: 0x040065D3 RID: 26067
		protected int executionCount;

		// Token: 0x040065D4 RID: 26068
		protected bool executionInfoSet;
	}
}
