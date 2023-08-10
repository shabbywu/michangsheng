using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage;

public class SkillDatebase : MonoBehaviour
{
	public static SkillDatebase instence;

	public List<Skill> skills = new List<Skill>();

	public Dictionary<int, Skill> dicSkills = new Dictionary<int, Skill>();

	public Dictionary<int, Dictionary<int, Skill>> Dict = new Dictionary<int, Dictionary<int, Skill>>();

	public void Awake()
	{
		instence = this;
	}

	private void OnDestroy()
	{
		instence = null;
	}

	public void Preload(int taskID)
	{
		Loom.RunAsync(delegate
		{
			LoadAsync(taskID);
		});
	}

	public void LoadAsync(int taskID)
	{
		try
		{
			foreach (JSONObject item in jsonData.instance._skillJsonData.list)
			{
				Skill skill = new Skill(item["id"].I, 0, 10);
				skills.Add(skill);
				dicSkills.Add(item["id"].I, skill);
				if (!Dict.ContainsKey(skill.SkillID))
				{
					Dict.Add(skill.SkillID, new Dictionary<int, Skill>());
				}
				if (!Dict[skill.SkillID].ContainsKey(skill.Skill_Lv))
				{
					ToolsEx.TryAdd(Dict[skill.SkillID], skill.Skill_Lv, skill);
				}
			}
			PreloadManager.Inst.TaskDone(taskID);
		}
		catch (Exception arg)
		{
			PreloadManager.IsException = true;
			PreloadManager.ExceptionData += $"{arg}\n";
			PreloadManager.Inst.TaskDone(taskID);
		}
	}
}
