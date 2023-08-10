using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus;

[Serializable]
public class FungusSaveMgr
{
	[NonSerialized]
	public Command CurCommand;

	public bool IsNeedStop;

	public FungusData SaveFungusData = new FungusData();

	[NonSerialized]
	public bool LastIsEnd = true;

	[NonSerialized]
	public string StopTalkName;

	public void SetCommand(Command command)
	{
		if (IsCanSet(command))
		{
			CurCommand = command;
		}
	}

	public void ClearCommand()
	{
		if (!(CurCommand is Menu))
		{
			CurCommand = null;
		}
	}

	public void ClearMenu()
	{
		CurCommand = null;
	}

	public bool IsCanSet(Command command)
	{
		if (command is INoCommand)
		{
			return false;
		}
		if ((Object)(object)CurCommand == (Object)null)
		{
			return true;
		}
		if (CurCommand is Menu)
		{
			return false;
		}
		return true;
	}

	public bool IsEnd()
	{
		if ((Object)(object)CurCommand == (Object)null)
		{
			return true;
		}
		return false;
	}

	public void SaveData()
	{
		SaveFungusData = new FungusData();
		if (IsEnd())
		{
			SaveFungusData.IsNeedLoad = false;
			return;
		}
		SaveFungusData.Flowchart = CurCommand.GetFlowchart();
		SaveFungusData.Block = CurCommand.ParentBlock;
		SaveFungusData.TalkName = CurCommand.GetFlowchart().GetParentName();
		SaveFungusData.BlockName = CurCommand.ParentBlock.blockName;
		SaveFungusData.CommandName = ((object)CurCommand).GetType().Name;
		SaveFungusData.CommandIndex = SaveFungusData.Block.commandList.IndexOf(CurCommand);
		SaveFungusData.TalkIsEnd = false;
		SaveFungusData.Save();
	}

	public void Load()
	{
		if (SaveFungusData == null || !SaveFungusData.IsNeedLoad)
		{
			return;
		}
		Flowchart flowchart = null;
		GameObject val = null;
		switch (SaveFungusData.TalkType)
		{
		case 0:
			val = ResManager.inst.LoadTalk("TalkPrefab/" + SaveFungusData.TalkName).Inst();
			break;
		case 1:
			val = GameObject.Find("AllMap/LevelsWorld0/" + SaveFungusData.TalkName);
			break;
		case 2:
			val = GameObject.Find(SaveFungusData.TalkName);
			break;
		}
		((MonoBehaviour)PanelMamager.inst).StartCoroutine(Later());
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogError((object)("加载Talk出错,talkName:" + SaveFungusData.TalkName));
			return;
		}
		flowchart = val.GetComponentInChildren<Flowchart>();
		if ((Object)(object)flowchart == (Object)null)
		{
			Debug.LogError((object)("加载Talk出错,talkName:" + SaveFungusData.TalkName));
			return;
		}
		Block block = flowchart.FindBlock(SaveFungusData.BlockName);
		if ((Object)(object)block == (Object)null)
		{
			Debug.LogError((object)("当前talk：" + SaveFungusData.TalkName + "不存在block" + SaveFungusData.BlockName));
		}
		else if (block.CommandList.Count > SaveFungusData.CommandIndex)
		{
			if (((object)block.CommandList[SaveFungusData.CommandIndex]).GetType().Name == SaveFungusData.CommandName)
			{
				foreach (string key in SaveFungusData.Floats.Keys)
				{
					flowchart.SetFloatVariable(key, SaveFungusData.Floats[key]);
				}
				foreach (string key2 in SaveFungusData.Ints.Keys)
				{
					flowchart.SetIntegerVariable(key2, SaveFungusData.Ints[key2]);
				}
				foreach (string key3 in SaveFungusData.Strings.Keys)
				{
					flowchart.SetStringVariable(key3, SaveFungusData.Strings[key3]);
				}
				foreach (string key4 in SaveFungusData.Bools.Keys)
				{
					flowchart.SetBooleanVariable(key4, SaveFungusData.Bools[key4]);
				}
				StopTalkName = SaveFungusData.TalkName;
				flowchart.ExecuteBlock(block, SaveFungusData.CommandIndex);
			}
			else
			{
				Debug.LogError((object)("当前talk：" + SaveFungusData.TalkName + "的block" + SaveFungusData.BlockName + "，命令冲突原先命令为" + SaveFungusData.CommandName + ",现在命令为" + ((object)block.CommandList[SaveFungusData.CommandIndex]).GetType().Name));
			}
		}
		else
		{
			Debug.LogError((object)("当前talk：" + SaveFungusData.TalkName + "的block" + SaveFungusData.BlockName + ",命令行数错误"));
		}
	}

	public void LoadTalk()
	{
		if (!Tools.instance.IsInDF)
		{
			IsNeedStop = true;
			((MonoBehaviour)PanelMamager.inst).StartCoroutine(CheckCanLoad());
		}
	}

	public IEnumerator CheckCanLoad()
	{
		while (true)
		{
			Scene activeScene = SceneManager.GetActiveScene();
			if (((Scene)(ref activeScene)).name.Equals(Tools.jumpToName, StringComparison.CurrentCultureIgnoreCase))
			{
				break;
			}
			yield return (object)new WaitForEndOfFrame();
		}
		Load();
		IsNeedStop = false;
	}

	public IEnumerator Later()
	{
		yield return (object)new WaitForSeconds(1f);
	}
}
