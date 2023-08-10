using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[Serializable]
public class FungusData
{
	[NonSerialized]
	public Flowchart Flowchart;

	[NonSerialized]
	public Block Block;

	public Dictionary<string, float> Floats;

	public Dictionary<string, int> Ints;

	public Dictionary<string, string> Strings;

	public Dictionary<string, bool> Bools;

	public string RealCommandName;

	public string TalkName;

	public string BlockName;

	public string LastBlockName;

	public string CommandName;

	public int FirstMenu = -1;

	public int CommandIndex;

	public bool TalkIsEnd = true;

	public bool IsNeedLoad;

	public int TalkType { get; private set; }

	public void Save()
	{
		if (TalkIsEnd || TalkName == "NPCJiaoHuTalk" || CommandName == "YSSaveGame")
		{
			IsNeedLoad = false;
			return;
		}
		FindTalkType();
		Floats = new Dictionary<string, float>();
		Ints = new Dictionary<string, int>();
		Strings = new Dictionary<string, string>();
		Bools = new Dictionary<string, bool>();
		foreach (Variable variable in Flowchart.Variables)
		{
			if (variable is FloatVariable)
			{
				Floats.Add(variable.Key, Flowchart.GetFloatVariable(variable.Key));
			}
			else if (variable is IntegerVariable)
			{
				Ints.Add(variable.Key, Flowchart.GetIntegerVariable(variable.Key));
			}
			else if (variable is StringVariable)
			{
				Strings.Add(variable.Key, Flowchart.GetStringVariable(variable.Key));
			}
			else if (variable is BooleanVariable)
			{
				Bools.Add(variable.Key, Flowchart.GetBooleanVariable(variable.Key));
			}
		}
		if (FirstMenu != -1)
		{
			CommandIndex = FirstMenu;
			CommandName = "Menu";
		}
		IsNeedLoad = true;
	}

	public void FindTalkType()
	{
		if ((Object)(object)ResManager.inst.LoadTalk("TalkPrefab/" + TalkName) == (Object)null)
		{
			TalkType = 1;
			if ((Object)(object)GameObject.Find("AllMap/LevelsWorld0/" + TalkName) != (Object)null)
			{
				TalkType = 1;
			}
			else
			{
				TalkType = 2;
			}
		}
		else
		{
			TalkType = 0;
		}
	}

	public bool IsNewBlock()
	{
		return LastBlockName != BlockName;
	}
}
