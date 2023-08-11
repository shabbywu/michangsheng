using System;
using UnityEngine;
using script.YarnEditor.Manager;

namespace script.YarnEditor.Component.TriggerData;

[Serializable]
public class TriggerConfig
{
	public bool IsNull = true;

	public int Type;

	public bool IsValue;

	public string NpcId = "";

	public string SceneName = "";

	public string Path;

	public string ModPath;

	public string ValueId;

	public string Value;

	public bool CanTrigger(string value)
	{
		if (IsNull)
		{
			return false;
		}
		if (Type == 0)
		{
			if (NpcId == "" || NpcId.Length < 1)
			{
				return false;
			}
			try
			{
				if (IsValue)
				{
					NpcId = StoryManager.Inst.GetGoalValue(NpcId);
				}
				else
				{
					NpcId = NPCEx.NPCIDToNew(int.Parse(NpcId)).ToString();
				}
			}
			catch (Exception ex)
			{
				Debug.Log((object)ex);
				StoryManager.Inst.LogError(ex.Message);
				return false;
			}
			if (value != NpcId)
			{
				return false;
			}
		}
		else if (Type == 1)
		{
			if (SceneName == "" || SceneName.Length < 1)
			{
				return false;
			}
			if (IsValue)
			{
				SceneName = StoryManager.Inst.GetGoalValue(SceneName);
			}
			if (SceneName != value)
			{
				return false;
			}
		}
		if ((ValueId != "" || ValueId.Length > 0) && StoryManager.Inst.GetGoalValue(ValueId) != value)
		{
			return false;
		}
		return true;
	}
}
