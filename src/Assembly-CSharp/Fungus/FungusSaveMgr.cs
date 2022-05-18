using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02001461 RID: 5217
	[Serializable]
	public class FungusSaveMgr
	{
		// Token: 0x06007DC4 RID: 32196 RVA: 0x00055035 File Offset: 0x00053235
		public void SetCommand(Command command)
		{
			if (!this.IsCanSet(command))
			{
				return;
			}
			this.CurCommand = command;
		}

		// Token: 0x06007DC5 RID: 32197 RVA: 0x00055048 File Offset: 0x00053248
		public void ClearCommand()
		{
			if (this.CurCommand is Menu)
			{
				return;
			}
			this.CurCommand = null;
		}

		// Token: 0x06007DC6 RID: 32198 RVA: 0x0005505F File Offset: 0x0005325F
		public void ClearMenu()
		{
			this.CurCommand = null;
		}

		// Token: 0x06007DC7 RID: 32199 RVA: 0x00055068 File Offset: 0x00053268
		public bool IsCanSet(Command command)
		{
			return !(command is INoCommand) && (this.CurCommand == null || !(this.CurCommand is Menu));
		}

		// Token: 0x06007DC8 RID: 32200 RVA: 0x00055094 File Offset: 0x00053294
		public bool IsEnd()
		{
			return this.CurCommand == null;
		}

		// Token: 0x06007DC9 RID: 32201 RVA: 0x002C79BC File Offset: 0x002C5BBC
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

		// Token: 0x06007DCA RID: 32202 RVA: 0x002C7AA4 File Offset: 0x002C5CA4
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

		// Token: 0x06007DCB RID: 32203 RVA: 0x000550A7 File Offset: 0x000532A7
		public void LoadTalk()
		{
			if (Tools.instance.IsInDF)
			{
				return;
			}
			this.IsNeedStop = true;
			PanelMamager.inst.StartCoroutine(this.CheckCanLoad());
		}

		// Token: 0x06007DCC RID: 32204 RVA: 0x000550CE File Offset: 0x000532CE
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

		// Token: 0x06007DCD RID: 32205 RVA: 0x000550DD File Offset: 0x000532DD
		public IEnumerator Later()
		{
			yield return new WaitForSeconds(1f);
			yield break;
		}

		// Token: 0x04006B45 RID: 27461
		[NonSerialized]
		public Command CurCommand;

		// Token: 0x04006B46 RID: 27462
		public bool IsNeedStop;

		// Token: 0x04006B47 RID: 27463
		public FungusData SaveFungusData = new FungusData();

		// Token: 0x04006B48 RID: 27464
		[NonSerialized]
		public string StopTalkName;
	}
}
