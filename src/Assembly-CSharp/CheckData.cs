using System;
using System.Text;
using GUIPackage;
using JSONClass;

public static class CheckData
{
	public static StringBuilder log;

	public static void CheckTask(int taskID)
	{
		if (Check())
		{
			PreloadManager.Inst.TaskDone(taskID);
			return;
		}
		PreloadManager.LogException($"在检查JSON数据时出现如下错误，已阻止游戏继续加载:\n{log}");
		PreloadManager.Inst.TaskDone(taskID);
	}

	public static bool Check()
	{
		log = new StringBuilder();
		CheckItem();
		CheckSkill();
		CheckStaticSkill();
		CheckBuff();
		if (log.Length > 0)
		{
			return false;
		}
		return true;
	}

	public static void CheckItem()
	{
		foreach (_ItemJsonData data in _ItemJsonData.DataList)
		{
			try
			{
				if (data.id >= jsonData.QingJiaoItemIDSegment)
				{
					continue;
				}
				if (data.seid != null)
				{
					if (data.seid.Count > 0)
					{
						if (data.type == 0 || data.type == 1 || data.type == 2)
						{
							foreach (int item in data.seid)
							{
								if (item < jsonData.instance.EquipSeidJsonData.Length)
								{
									JSONObject jSONObject = jsonData.instance.EquipSeidJsonData[item];
									if (jSONObject.list.Count != 0 && !jSONObject.HasField(data.id.ToString()))
									{
										log.AppendLine($"物品表id为{data.id}的装备{data.name}定义了seid{item}，但是装备seid{item}表中没有此物品的对应数据");
									}
								}
								else
								{
									log.AppendLine($"物品表id为{data.id}的装备{data.name}定义的seid{item}是不存在的seid，请使用表中已有的seid");
								}
							}
						}
						else
						{
							foreach (int item2 in data.seid)
							{
								if (item2 < jsonData.instance.ItemsSeidJsonData.Length)
								{
									JSONObject jSONObject2 = jsonData.instance.ItemsSeidJsonData[item2];
									if (jSONObject2.list.Count != 0 && !jSONObject2.HasField(data.id.ToString()))
									{
										log.AppendLine($"物品表id为{data.id}的物品{data.name}定义了seid{item2}，但是物品seid{item2}表中没有此物品的对应数据");
									}
								}
								else
								{
									log.AppendLine($"物品表id为{data.id}的物品{data.name}定义的seid{item2}是不存在的seid，请使用表中已有的seid");
								}
							}
						}
					}
				}
				else
				{
					log.AppendLine($"物品表id为{data.id}的物品{data.name} seid为null，请检查配表");
				}
				if (data.type == 3)
				{
					float result = 0f;
					if (float.TryParse(data.desc, out result))
					{
						if (!SkillDatebase.instence.Dict.ContainsKey((int)result))
						{
							log.AppendLine($"物品表id为{data.id}的神通书籍《{data.name}》绑定的神通{(int)result}不存在，请检查配表");
						}
					}
					else
					{
						log.AppendLine($"物品表id为{data.id}的神通书籍《{data.name}》没有绑定神通，请检查配表");
					}
					if (data.seid != null && data.seid.Count > 0 && data.seid[0] != 0 && data.seid[0] != 1 && data.seid[0] != 30)
					{
						log.AppendLine($"物品表id为{data.id}的神通书籍《{data.name}》的seid必须配1，请检查配表");
					}
				}
				if (data.type != 4)
				{
					continue;
				}
				float result2 = 0f;
				if (float.TryParse(data.desc, out result2))
				{
					if (!SkillStaticDatebase.instence.Dict.ContainsKey((int)result2))
					{
						log.AppendLine($"物品表id为{data.id}的功法书籍《{data.name}》绑定的功法{(int)result2}不存在，请检查配表");
					}
				}
				else
				{
					log.AppendLine($"物品表id为{data.id}的功法书籍《{data.name}》没有绑定功法，请检查配表");
				}
				if (data.seid != null && data.seid.Count > 0 && data.seid[0] != 0 && data.seid[0] != 2 && data.seid[0] != 30)
				{
					log.AppendLine($"物品表id为{data.id}的功法书籍《{data.name}》的seid必须配2，请检查配表");
				}
			}
			catch (Exception arg)
			{
				log.AppendLine($"检查物品表时出现意外错误:\n{arg}");
			}
		}
	}

	public static void CheckSkill()
	{
		foreach (_skillJsonData data in _skillJsonData.DataList)
		{
			try
			{
				if (data.seid != null)
				{
					if (data.seid.Count > 0)
					{
						foreach (int item in data.seid)
						{
							if (item < jsonData.instance.SkillSeidJsonData.Length)
							{
								JSONObject jSONObject = jsonData.instance.SkillSeidJsonData[item];
								if (jSONObject.list.Count != 0 && !jSONObject.HasField(data.id.ToString()))
								{
									log.AppendLine($"神通表id为{data.id}的神通{data.name}定义了seid{item}，但是神通seid{item}表中没有此神通的对应数据");
								}
							}
							else
							{
								log.AppendLine($"神通表id为{data.id}的神通{data.name}定义的seid{item}是不存在的seid，请使用表中已有的seid");
							}
						}
					}
				}
				else
				{
					log.AppendLine($"神通表id为{data.id}的神通{data.name} seid为null，请检查配表");
				}
				if (data.Skill_Lv < 1 || data.Skill_Lv > 5)
				{
					log.AppendLine($"神通表id为{data.id}的神通{data.name}的Skill_Lv {data.Skill_Lv}必须在1-5之间，请检查配表");
				}
				if (data.skill_Cast != null && data.skill_CastType != null)
				{
					if (data.skill_Cast.Count != data.skill_CastType.Count)
					{
						log.AppendLine($"神通表id为{data.id}的神通{data.name}的灵气消耗类型(skill_CastType)与灵气消耗数量(skill_Cast)数组长度不一致，请检查配表");
					}
				}
				else
				{
					log.AppendLine($"神通表id为{data.id}的神通{data.name}的灵气消耗异常，请检查配表");
				}
				if (data.TuJianType != 0 && data.TuJianType != 6 && data.TuJianType != 8)
				{
					log.AppendLine($"神通表id为{data.id}的神通{data.name}的图鉴类型必须配0(不显示)或6(神通)或8(秘术)，当前值:{data.TuJianType}，请检查配表");
				}
			}
			catch (Exception arg)
			{
				log.AppendLine($"检查神通表时出现意外错误:\n{arg}");
			}
		}
	}

	public static void CheckStaticSkill()
	{
		foreach (StaticSkillJsonData data in StaticSkillJsonData.DataList)
		{
			try
			{
				if (data.seid != null)
				{
					if (data.seid.Count > 0)
					{
						foreach (int item in data.seid)
						{
							if (item < jsonData.instance.StaticSkillSeidJsonData.Length)
							{
								JSONObject jSONObject = jsonData.instance.StaticSkillSeidJsonData[item];
								if (jSONObject.list.Count != 0 && !jSONObject.HasField(data.id.ToString()))
								{
									log.AppendLine($"功法表id为{data.id}的功法{data.name}定义了seid{item}，但是功法seid{item}表中没有此功法的对应数据");
								}
							}
							else
							{
								log.AppendLine($"功法表id为{data.id}的功法{data.name}定义的seid{item}是不存在的seid，请使用表中已有的seid");
							}
						}
					}
				}
				else
				{
					log.AppendLine($"功法表id为{data.id}的功法{data.name} seid为null，请检查配表");
				}
				if (data.Skill_Lv < 1 || data.Skill_Lv > 5)
				{
					log.AppendLine($"功法表id为{data.id}的功法{data.name}的Skill_Lv {data.Skill_Lv}必须在1-5之间，请检查配表");
				}
				if (data.TuJianType != 0 && data.TuJianType != 7)
				{
					log.AppendLine($"神通表id为{data.id}的神通{data.name}的图鉴类型必须配0(不显示)或7(功法)，当前值:{data.TuJianType}，请检查配表");
				}
			}
			catch (Exception arg)
			{
				log.AppendLine($"检查功法表时出现意外错误:\n{arg}");
			}
		}
	}

	public static void CheckBuff()
	{
		foreach (_BuffJsonData data in _BuffJsonData.DataList)
		{
			try
			{
				if (data.seid != null)
				{
					if (data.seid.Count <= 0)
					{
						continue;
					}
					foreach (int item in data.seid)
					{
						if (item < jsonData.instance.BuffSeidJsonData.Length)
						{
							JSONObject jSONObject = jsonData.instance.BuffSeidJsonData[item];
							if (jSONObject.list.Count != 0 && !jSONObject.HasField(data.buffid.ToString()))
							{
								log.AppendLine($"Buff表id为{data.buffid}的Buff{data.name}定义了seid{item}，但是Buff seid{item}表中没有此Buff的对应数据");
							}
						}
						else
						{
							log.AppendLine($"Buff表id为{data.buffid}的Buff{data.name}定义的seid{item}是不存在的seid，请使用表中已有的seid");
						}
					}
					continue;
				}
				log.AppendLine($"Buff表id为{data.buffid}的Buff{data.name} seid为null，请检查配表");
			}
			catch (Exception arg)
			{
				log.AppendLine($"检查Buff表时出现意外错误:\n{arg}");
			}
		}
	}
}
