using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E63 RID: 3683
	[ExecuteInEditMode]
	[RequireComponent(typeof(Flowchart))]
	[AddComponentMenu("")]
	public class Block : Node
	{
		// Token: 0x0600675E RID: 26462 RVA: 0x0028A221 File Offset: 0x00288421
		protected virtual void Awake()
		{
			this.SetExecutionInfo();
		}

		// Token: 0x0600675F RID: 26463 RVA: 0x0028A22C File Offset: 0x0028842C
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

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06006760 RID: 26464 RVA: 0x0028A286 File Offset: 0x00288486
		// (set) Token: 0x06006761 RID: 26465 RVA: 0x0028A28E File Offset: 0x0028848E
		public bool IsSelected { get; set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06006762 RID: 26466 RVA: 0x0028A297 File Offset: 0x00288497
		// (set) Token: 0x06006763 RID: 26467 RVA: 0x0028A29F File Offset: 0x0028849F
		public bool IsFiltered { get; set; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06006764 RID: 26468 RVA: 0x0028A2A8 File Offset: 0x002884A8
		// (set) Token: 0x06006765 RID: 26469 RVA: 0x0028A2B0 File Offset: 0x002884B0
		public bool IsControlSelected { get; set; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06006766 RID: 26470 RVA: 0x0028A2B9 File Offset: 0x002884B9
		public virtual ExecutionState State
		{
			get
			{
				return this.executionState;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06006767 RID: 26471 RVA: 0x0028A2C1 File Offset: 0x002884C1
		// (set) Token: 0x06006768 RID: 26472 RVA: 0x0028A2C9 File Offset: 0x002884C9
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

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06006769 RID: 26473 RVA: 0x0028A2D2 File Offset: 0x002884D2
		// (set) Token: 0x0600676A RID: 26474 RVA: 0x0028A2DA File Offset: 0x002884DA
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

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x0600676B RID: 26475 RVA: 0x0028A2E3 File Offset: 0x002884E3
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x0600676C RID: 26476 RVA: 0x0028A2EB File Offset: 0x002884EB
		// (set) Token: 0x0600676D RID: 26477 RVA: 0x0028A2F3 File Offset: 0x002884F3
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

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x0600676E RID: 26478 RVA: 0x0028A2FC File Offset: 0x002884FC
		public virtual Command ActiveCommand
		{
			get
			{
				return this.activeCommand;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x0600676F RID: 26479 RVA: 0x0028A304 File Offset: 0x00288504
		// (set) Token: 0x06006770 RID: 26480 RVA: 0x0028A30C File Offset: 0x0028850C
		public virtual float ExecutingIconTimer { get; set; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06006771 RID: 26481 RVA: 0x0028A315 File Offset: 0x00288515
		public virtual List<Command> CommandList
		{
			get
			{
				return this.commandList;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (set) Token: 0x06006772 RID: 26482 RVA: 0x0028A31D File Offset: 0x0028851D
		public virtual int JumpToCommandIndex
		{
			set
			{
				this.jumpToCommandIndex = value;
			}
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x0028A326 File Offset: 0x00288526
		public virtual Flowchart GetFlowchart()
		{
			return base.GetComponent<Flowchart>();
		}

		// Token: 0x06006774 RID: 26484 RVA: 0x0028A32E File Offset: 0x0028852E
		public virtual bool IsExecuting()
		{
			return this.executionState == ExecutionState.Executing;
		}

		// Token: 0x06006775 RID: 26485 RVA: 0x0028A339 File Offset: 0x00288539
		public virtual int GetExecutionCount()
		{
			return this.executionCount;
		}

		// Token: 0x06006776 RID: 26486 RVA: 0x0028A341 File Offset: 0x00288541
		public virtual void StartExecution()
		{
			base.StartCoroutine(this.Execute(0, null));
		}

		// Token: 0x06006777 RID: 26487 RVA: 0x0028A352 File Offset: 0x00288552
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

		// Token: 0x06006778 RID: 26488 RVA: 0x0028A36F File Offset: 0x0028856F
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

		// Token: 0x06006779 RID: 26489 RVA: 0x0028A39F File Offset: 0x0028859F
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

		// Token: 0x0600677A RID: 26490 RVA: 0x0028A3D8 File Offset: 0x002885D8
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

		// Token: 0x0600677B RID: 26491 RVA: 0x0028A420 File Offset: 0x00288620
		public virtual Type GetPreviousActiveCommandType()
		{
			if (this.previousActiveCommandIndex >= 0 && this.previousActiveCommandIndex < this.commandList.Count)
			{
				return this.commandList[this.previousActiveCommandIndex].GetType();
			}
			return null;
		}

		// Token: 0x0600677C RID: 26492 RVA: 0x0028A456 File Offset: 0x00288656
		public virtual int GetPreviousActiveCommandIndent()
		{
			if (this.previousActiveCommandIndex >= 0 && this.previousActiveCommandIndex < this.commandList.Count)
			{
				return this.commandList[this.previousActiveCommandIndex].IndentLevel;
			}
			return -1;
		}

		// Token: 0x0600677D RID: 26493 RVA: 0x0028A48C File Offset: 0x0028868C
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

		// Token: 0x0600677E RID: 26494 RVA: 0x0028A4F0 File Offset: 0x002886F0
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

		// Token: 0x04005866 RID: 22630
		[SerializeField]
		protected int itemId = -1;

		// Token: 0x04005867 RID: 22631
		[FormerlySerializedAs("sequenceName")]
		[Tooltip("The name of the block node as displayed in the Flowchart window")]
		[SerializeField]
		public string blockName = "New Block";

		// Token: 0x04005868 RID: 22632
		[TextArea(2, 5)]
		[Tooltip("Description text to display under the block node")]
		[SerializeField]
		protected string description = "";

		// Token: 0x04005869 RID: 22633
		[Tooltip("An optional Event Handler which can execute the block when an event occurs")]
		[SerializeField]
		protected EventHandler eventHandler;

		// Token: 0x0400586A RID: 22634
		[SerializeField]
		public List<Command> commandList = new List<Command>();

		// Token: 0x0400586B RID: 22635
		protected ExecutionState executionState;

		// Token: 0x0400586C RID: 22636
		protected Command activeCommand;

		// Token: 0x0400586D RID: 22637
		protected Action lastOnCompleteAction;

		// Token: 0x0400586E RID: 22638
		protected int previousActiveCommandIndex = -1;

		// Token: 0x0400586F RID: 22639
		protected int jumpToCommandIndex = -1;

		// Token: 0x04005870 RID: 22640
		protected int executionCount;

		// Token: 0x04005871 RID: 22641
		protected bool executionInfoSet;
	}
}
