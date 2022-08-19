using System;
using System.Collections.Generic;
using System.Text;
using GUIPackage;
using JSONClass;

// Token: 0x020001B9 RID: 441
public static class CheckData
{
	// Token: 0x06001264 RID: 4708 RVA: 0x0006FB29 File Offset: 0x0006DD29
	public static void CheckTask(int taskID)
	{
		if (CheckData.Check())
		{
			PreloadManager.Inst.TaskDone(taskID);
			return;
		}
		PreloadManager.LogException(string.Format("在检查JSON数据时出现如下错误，已阻止游戏继续加载:\n{0}", CheckData.log));
		PreloadManager.Inst.TaskDone(taskID);
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x0006FB5D File Offset: 0x0006DD5D
	public static bool Check()
	{
		CheckData.log = new StringBuilder();
		CheckData.CheckItem();
		CheckData.CheckSkill();
		CheckData.CheckStaticSkill();
		CheckData.CheckBuff();
		return CheckData.log.Length <= 0;
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x0006FB90 File Offset: 0x0006DD90
	public static void CheckItem()
	{
		foreach (_ItemJsonData itemJsonData in _ItemJsonData.DataList)
		{
			try
			{
				if (itemJsonData.id < jsonData.QingJiaoItemIDSegment)
				{
					if (itemJsonData.seid != null)
					{
						if (itemJsonData.seid.Count <= 0)
						{
							goto IL_264;
						}
						if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
						{
							using (List<int>.Enumerator enumerator2 = itemJsonData.seid.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									int num = enumerator2.Current;
									if (num < jsonData.instance.EquipSeidJsonData.Length)
									{
										JSONObject jsonobject = jsonData.instance.EquipSeidJsonData[num];
										if (jsonobject.list.Count != 0 && !jsonobject.HasField(itemJsonData.id.ToString()))
										{
											CheckData.log.AppendLine(string.Format("物品表id为{0}的装备{1}定义了seid{2}，但是装备seid{3}表中没有此物品的对应数据", new object[]
											{
												itemJsonData.id,
												itemJsonData.name,
												num,
												num
											}));
										}
									}
									else
									{
										CheckData.log.AppendLine(string.Format("物品表id为{0}的装备{1}定义的seid{2}是不存在的seid，请使用表中已有的seid", itemJsonData.id, itemJsonData.name, num));
									}
								}
								goto IL_264;
							}
						}
						using (List<int>.Enumerator enumerator2 = itemJsonData.seid.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								int num2 = enumerator2.Current;
								if (num2 < jsonData.instance.ItemsSeidJsonData.Length)
								{
									JSONObject jsonobject2 = jsonData.instance.ItemsSeidJsonData[num2];
									if (jsonobject2.list.Count != 0 && !jsonobject2.HasField(itemJsonData.id.ToString()))
									{
										CheckData.log.AppendLine(string.Format("物品表id为{0}的物品{1}定义了seid{2}，但是物品seid{3}表中没有此物品的对应数据", new object[]
										{
											itemJsonData.id,
											itemJsonData.name,
											num2,
											num2
										}));
									}
								}
								else
								{
									CheckData.log.AppendLine(string.Format("物品表id为{0}的物品{1}定义的seid{2}是不存在的seid，请使用表中已有的seid", itemJsonData.id, itemJsonData.name, num2));
								}
							}
							goto IL_264;
						}
					}
					CheckData.log.AppendLine(string.Format("物品表id为{0}的物品{1} seid为null，请检查配表", itemJsonData.id, itemJsonData.name));
					IL_264:
					if (itemJsonData.type == 3)
					{
						float num3 = 0f;
						if (float.TryParse(itemJsonData.desc, out num3))
						{
							if (!SkillDatebase.instence.Dict.ContainsKey((int)num3))
							{
								CheckData.log.AppendLine(string.Format("物品表id为{0}的神通书籍《{1}》绑定的神通{2}不存在，请检查配表", itemJsonData.id, itemJsonData.name, (int)num3));
							}
						}
						else
						{
							CheckData.log.AppendLine(string.Format("物品表id为{0}的神通书籍《{1}》没有绑定神通，请检查配表", itemJsonData.id, itemJsonData.name));
						}
						if (itemJsonData.seid != null && itemJsonData.seid.Count > 0 && itemJsonData.seid[0] != 0 && itemJsonData.seid[0] != 1 && itemJsonData.seid[0] != 30)
						{
							CheckData.log.AppendLine(string.Format("物品表id为{0}的神通书籍《{1}》的seid必须配1，请检查配表", itemJsonData.id, itemJsonData.name));
						}
					}
					if (itemJsonData.type == 4)
					{
						float num4 = 0f;
						if (float.TryParse(itemJsonData.desc, out num4))
						{
							if (!SkillStaticDatebase.instence.Dict.ContainsKey((int)num4))
							{
								CheckData.log.AppendLine(string.Format("物品表id为{0}的功法书籍《{1}》绑定的功法{2}不存在，请检查配表", itemJsonData.id, itemJsonData.name, (int)num4));
							}
						}
						else
						{
							CheckData.log.AppendLine(string.Format("物品表id为{0}的功法书籍《{1}》没有绑定功法，请检查配表", itemJsonData.id, itemJsonData.name));
						}
						if (itemJsonData.seid != null && itemJsonData.seid.Count > 0 && itemJsonData.seid[0] != 0 && itemJsonData.seid[0] != 2 && itemJsonData.seid[0] != 30)
						{
							CheckData.log.AppendLine(string.Format("物品表id为{0}的功法书籍《{1}》的seid必须配2，请检查配表", itemJsonData.id, itemJsonData.name));
						}
					}
				}
			}
			catch (Exception arg)
			{
				CheckData.log.AppendLine(string.Format("检查物品表时出现意外错误:\n{0}", arg));
			}
		}
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x00070088 File Offset: 0x0006E288
	public static void CheckSkill()
	{
		foreach (_skillJsonData skillJsonData in _skillJsonData.DataList)
		{
			try
			{
				if (skillJsonData.seid != null)
				{
					if (skillJsonData.seid.Count <= 0)
					{
						goto IL_143;
					}
					using (List<int>.Enumerator enumerator2 = skillJsonData.seid.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int num = enumerator2.Current;
							if (num < jsonData.instance.SkillSeidJsonData.Length)
							{
								JSONObject jsonobject = jsonData.instance.SkillSeidJsonData[num];
								if (jsonobject.list.Count != 0 && !jsonobject.HasField(skillJsonData.id.ToString()))
								{
									CheckData.log.AppendLine(string.Format("神通表id为{0}的神通{1}定义了seid{2}，但是神通seid{3}表中没有此神通的对应数据", new object[]
									{
										skillJsonData.id,
										skillJsonData.name,
										num,
										num
									}));
								}
							}
							else
							{
								CheckData.log.AppendLine(string.Format("神通表id为{0}的神通{1}定义的seid{2}是不存在的seid，请使用表中已有的seid", skillJsonData.id, skillJsonData.name, num));
							}
						}
						goto IL_143;
					}
				}
				CheckData.log.AppendLine(string.Format("神通表id为{0}的神通{1} seid为null，请检查配表", skillJsonData.id, skillJsonData.name));
				IL_143:
				if (skillJsonData.Skill_Lv < 1 || skillJsonData.Skill_Lv > 5)
				{
					CheckData.log.AppendLine(string.Format("神通表id为{0}的神通{1}的Skill_Lv {2}必须在1-5之间，请检查配表", skillJsonData.id, skillJsonData.name, skillJsonData.Skill_Lv));
				}
				if (skillJsonData.skill_Cast != null && skillJsonData.skill_CastType != null)
				{
					if (skillJsonData.skill_Cast.Count != skillJsonData.skill_CastType.Count)
					{
						CheckData.log.AppendLine(string.Format("神通表id为{0}的神通{1}的灵气消耗类型(skill_CastType)与灵气消耗数量(skill_Cast)数组长度不一致，请检查配表", skillJsonData.id, skillJsonData.name));
					}
				}
				else
				{
					CheckData.log.AppendLine(string.Format("神通表id为{0}的神通{1}的灵气消耗异常，请检查配表", skillJsonData.id, skillJsonData.name));
				}
				if (skillJsonData.TuJianType != 0 && skillJsonData.TuJianType != 6 && skillJsonData.TuJianType != 8)
				{
					CheckData.log.AppendLine(string.Format("神通表id为{0}的神通{1}的图鉴类型必须配0(不显示)或6(神通)或8(秘术)，当前值:{2}，请检查配表", skillJsonData.id, skillJsonData.name, skillJsonData.TuJianType));
				}
			}
			catch (Exception arg)
			{
				CheckData.log.AppendLine(string.Format("检查神通表时出现意外错误:\n{0}", arg));
			}
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x00070364 File Offset: 0x0006E564
	public static void CheckStaticSkill()
	{
		foreach (StaticSkillJsonData staticSkillJsonData in StaticSkillJsonData.DataList)
		{
			try
			{
				if (staticSkillJsonData.seid != null)
				{
					if (staticSkillJsonData.seid.Count <= 0)
					{
						goto IL_143;
					}
					using (List<int>.Enumerator enumerator2 = staticSkillJsonData.seid.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int num = enumerator2.Current;
							if (num < jsonData.instance.StaticSkillSeidJsonData.Length)
							{
								JSONObject jsonobject = jsonData.instance.StaticSkillSeidJsonData[num];
								if (jsonobject.list.Count != 0 && !jsonobject.HasField(staticSkillJsonData.id.ToString()))
								{
									CheckData.log.AppendLine(string.Format("功法表id为{0}的功法{1}定义了seid{2}，但是功法seid{3}表中没有此功法的对应数据", new object[]
									{
										staticSkillJsonData.id,
										staticSkillJsonData.name,
										num,
										num
									}));
								}
							}
							else
							{
								CheckData.log.AppendLine(string.Format("功法表id为{0}的功法{1}定义的seid{2}是不存在的seid，请使用表中已有的seid", staticSkillJsonData.id, staticSkillJsonData.name, num));
							}
						}
						goto IL_143;
					}
				}
				CheckData.log.AppendLine(string.Format("功法表id为{0}的功法{1} seid为null，请检查配表", staticSkillJsonData.id, staticSkillJsonData.name));
				IL_143:
				if (staticSkillJsonData.Skill_Lv < 1 || staticSkillJsonData.Skill_Lv > 5)
				{
					CheckData.log.AppendLine(string.Format("功法表id为{0}的功法{1}的Skill_Lv {2}必须在1-5之间，请检查配表", staticSkillJsonData.id, staticSkillJsonData.name, staticSkillJsonData.Skill_Lv));
				}
				if (staticSkillJsonData.TuJianType != 0 && staticSkillJsonData.TuJianType != 7)
				{
					CheckData.log.AppendLine(string.Format("神通表id为{0}的神通{1}的图鉴类型必须配0(不显示)或7(功法)，当前值:{2}，请检查配表", staticSkillJsonData.id, staticSkillJsonData.name, staticSkillJsonData.TuJianType));
				}
			}
			catch (Exception arg)
			{
				CheckData.log.AppendLine(string.Format("检查功法表时出现意外错误:\n{0}", arg));
			}
		}
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x000705C0 File Offset: 0x0006E7C0
	public static void CheckBuff()
	{
		foreach (_BuffJsonData buffJsonData in _BuffJsonData.DataList)
		{
			try
			{
				if (buffJsonData.seid != null)
				{
					if (buffJsonData.seid.Count <= 0)
					{
						continue;
					}
					using (List<int>.Enumerator enumerator2 = buffJsonData.seid.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int num = enumerator2.Current;
							if (num < jsonData.instance.BuffSeidJsonData.Length)
							{
								JSONObject jsonobject = jsonData.instance.BuffSeidJsonData[num];
								if (jsonobject.list.Count != 0 && !jsonobject.HasField(buffJsonData.buffid.ToString()))
								{
									CheckData.log.AppendLine(string.Format("Buff表id为{0}的Buff{1}定义了seid{2}，但是Buff seid{3}表中没有此Buff的对应数据", new object[]
									{
										buffJsonData.buffid,
										buffJsonData.name,
										num,
										num
									}));
								}
							}
							else
							{
								CheckData.log.AppendLine(string.Format("Buff表id为{0}的Buff{1}定义的seid{2}是不存在的seid，请使用表中已有的seid", buffJsonData.buffid, buffJsonData.name, num));
							}
						}
						continue;
					}
				}
				CheckData.log.AppendLine(string.Format("Buff表id为{0}的Buff{1} seid为null，请检查配表", buffJsonData.buffid, buffJsonData.name));
			}
			catch (Exception arg)
			{
				CheckData.log.AppendLine(string.Format("检查Buff表时出现意外错误:\n{0}", arg));
			}
		}
	}

	// Token: 0x04000D07 RID: 3335
	public static StringBuilder log;
}
