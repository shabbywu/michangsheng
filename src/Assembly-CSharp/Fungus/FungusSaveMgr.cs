using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000FAB RID: 4011
	[Serializable]
	public class FungusSaveMgr
	{
		// Token: 0x06006FD6 RID: 28630 RVA: 0x002A8130 File Offset: 0x002A6330
		public void SetCommand(Command command)
		{
			if (!this.IsCanSet(command))
			{
				return;
			}
			this.CurCommand = command;
		}

		// Token: 0x06006FD7 RID: 28631 RVA: 0x002A8143 File Offset: 0x002A6343
		public void ClearCommand()
		{
			if (this.CurCommand is Menu)
			{
				return;
			}
			this.CurCommand = null;
		}

		// Token: 0x06006FD8 RID: 28632 RVA: 0x002A815A File Offset: 0x002A635A
		public void ClearMenu()
		{
			this.CurCommand = null;
		}

		// Token: 0x06006FD9 RID: 28633 RVA: 0x002A8163 File Offset: 0x002A6363
		public bool IsCanSet(Command command)
		{
			return !(command is INoCommand) && (this.CurCommand == null || !(this.CurCommand is Menu));
		}

		// Token: 0x06006FDA RID: 28634 RVA: 0x002A818F File Offset: 0x002A638F
		public bool IsEnd()
		{
			return this.CurCommand == null;
		}

		// Token: 0x06006FDB RID: 28635 RVA: 0x002A81A4 File Offset: 0x002A63A4
		public void SaveData()
		{
			this.SaveFungusData = new FungusData();
			if (this.IsEnd())
			{
				this.SaveFungusData.IsNeedLoad = false;
				return;
			}
			this.SaveFungusData.Flowchart = this.CurCommand.GetFlowchart();
			this.SaveFungusData.Block = this.CurCommand.ParentBlock;
			this.SaveFungusData.TalkName = this.CurCommand.GetFlowchart().GetParentName();
			this.SaveFungusData.BlockName = this.CurCommand.ParentBlock.blockName;
			this.SaveFungusData.CommandName = this.CurCommand.GetType().Name;
			this.SaveFungusData.CommandIndex = this.SaveFungusData.Block.commandList.IndexOf(this.CurCommand);
			this.SaveFungusData.TalkIsEnd = false;
			this.SaveFungusData.Save();
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x002A828C File Offset: 0x002A648C
		public void Load()
		{
			if (this.SaveFungusData != null && this.SaveFungusData.IsNeedLoad)
			{
				Flowchart flowchart = null;
				GameObject gameObject = null;
				switch (this.SaveFungusData.TalkType)
				{
				case 0:
					gameObject = ResManager.inst.LoadTalk("TalkPrefab/" + this.SaveFungusData.TalkName).Inst(null);
					break;
				case 1:
					gameObject = GameObject.Find("AllMap/LevelsWorld0/" + this.SaveFungusData.TalkName);
					break;
				case 2:
					gameObject = GameObject.Find(this.SaveFungusData.TalkName);
					break;
				}
				PanelMamager.inst.StartCoroutine(this.Later());
				if (gameObject == null)
				{
					Debug.LogError("加载Talk出错,talkName:" + this.SaveFungusData.TalkName);
					return;
				}
				flowchart = gameObject.GetComponentInChildren<Flowchart>();
				if (flowchart == null)
				{
					Debug.LogError("加载Talk出错,talkName:" + this.SaveFungusData.TalkName);
					return;
				}
				Block block = flowchart.FindBlock(this.SaveFungusData.BlockName);
				if (block == null)
				{
					Debug.LogError("当前talk：" + this.SaveFungusData.TalkName + "不存在block" + this.SaveFungusData.BlockName);
					return;
				}
				if (block.CommandList.Count > this.SaveFungusData.CommandIndex)
				{
					if (block.CommandList[this.SaveFungusData.CommandIndex].GetType().Name == this.SaveFungusData.CommandName)
					{
						foreach (string key in this.SaveFungusData.Floats.Keys)
						{
							flowchart.SetFloatVariable(key, this.SaveFungusData.Floats[key]);
						}
						foreach (string key2 in this.SaveFungusData.Ints.Keys)
						{
							flowchart.SetIntegerVariable(key2, this.SaveFungusData.Ints[key2]);
						}
						foreach (string key3 in this.SaveFungusData.Strings.Keys)
						{
							flowchart.SetStringVariable(key3, this.SaveFungusData.Strings[key3]);
						}
						foreach (string key4 in this.SaveFungusData.Bools.Keys)
						{
							flowchart.SetBooleanVariable(key4, this.SaveFungusData.Bools[key4]);
						}
						this.StopTalkName = this.SaveFungusData.TalkName;
						flowchart.ExecuteBlock(block, this.SaveFungusData.CommandIndex, null);
						return;
					}
					Debug.LogError(string.Concat(new string[]
					{
						"当前talk：",
						this.SaveFungusData.TalkName,
						"的block",
						this.SaveFungusData.BlockName,
						"，命令冲突原先命令为",
						this.SaveFungusData.CommandName,
						",现在命令为",
						block.CommandList[this.SaveFungusData.CommandIndex].GetType().Name
					}));
					return;
				}
				else
				{
					Debug.LogError(string.Concat(new string[]
					{
						"当前talk：",
						this.SaveFungusData.TalkName,
						"的block",
						this.SaveFungusData.BlockName,
						",命令行数错误"
					}));
				}
			}
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x002A8694 File Offset: 0x002A6894
		public void LoadTalk()
		{
			if (Tools.instance.IsInDF)
			{
				return;
			}
			this.IsNeedStop = true;
			PanelMamager.inst.StartCoroutine(this.CheckCanLoad());
		}

		// Token: 0x06006FDE RID: 28638 RVA: 0x002A86BB File Offset: 0x002A68BB
		public IEnumerator CheckCanLoad()
		{
			while (!SceneManager.GetActiveScene().name.Equals(Tools.jumpToName, StringComparison.CurrentCultureIgnoreCase))
			{
				yield return new WaitForEndOfFrame();
			}
			this.Load();
			this.IsNeedStop = false;
			yield break;
		}

		// Token: 0x06006FDF RID: 28639 RVA: 0x002A86CA File Offset: 0x002A68CA
		public IEnumerator Later()
		{
			yield return new WaitForSeconds(1f);
			yield break;
		}

		// Token: 0x04005C51 RID: 23633
		[NonSerialized]
		public Command CurCommand;

		// Token: 0x04005C52 RID: 23634
		public bool IsNeedStop;

		// Token: 0x04005C53 RID: 23635
		public FungusData SaveFungusData = new FungusData();

		// Token: 0x04005C54 RID: 23636
		[NonSerialized]
		public bool LastIsEnd = true;

		// Token: 0x04005C55 RID: 23637
		[NonSerialized]
		public string StopTalkName;
	}
}
