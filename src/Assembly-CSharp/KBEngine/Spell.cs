using System;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using JSONClass;
using TuPo;
using UnityEngine;
using UnityEngine.Events;
using YSGame;
using YSGame.Fight;

namespace KBEngine;

public class Spell
{
	public enum buffList
	{
		NUM,
		ROUND,
		ID
	}

	public Entity entity;

	public Dictionary<int, int> UseSkillLateDict = new Dictionary<int, int>();

	public Spell(Entity avater)
	{
		entity = avater;
	}

	public void UseSkillLateAddBuff(int id, int count)
	{
		if (UseSkillLateDict == null)
		{
			UseSkillLateDict = new Dictionary<int, int>();
		}
		if (UseSkillLateDict.ContainsKey(id))
		{
			UseSkillLateDict[id] += count;
		}
		else
		{
			UseSkillLateDict.Add(id, count);
		}
	}

	public void VirtualspellSkill(int skillID, string uuid = "")
	{
		if (!jsonData.instance.skillJsonData.HasField(string.Concat(skillID)))
		{
			return;
		}
		if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillAttack")
		{
			foreach (KeyValuePair<int, Entity> entity in KBEngineApp.app.entities)
			{
				if (entity.Value != this.entity)
				{
					VirtualspellTarget(skillID, entity.Value.id, uuid);
				}
			}
		}
		else if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillSelf")
		{
			foreach (KeyValuePair<int, Entity> entity2 in KBEngineApp.app.entities)
			{
				if (entity2.Value == this.entity)
				{
					VirtualspellTarget(skillID, entity2.Value.id, uuid);
				}
			}
		}
		UIFightPanel.Inst.PlayerLingQiController.ResetPlayerLingQiCount();
	}

	private void VirtualspellTarget(int skillID, int targetID, string uuid = "")
	{
		foreach (GUIPackage.Skill item in ((Avatar)entity).skill)
		{
			if (uuid != "")
			{
				if (uuid == item.weaponuuid && skillID == item.skill_ID)
				{
					Entity receiver = KBEngineApp.app.entities[targetID];
					item.VirtualPuting(entity, receiver, 0, uuid);
					break;
				}
			}
			else if (skillID == item.skill_ID)
			{
				Entity receiver2 = KBEngineApp.app.entities[targetID];
				item.VirtualPuting(entity, receiver2);
				break;
			}
		}
	}

	public void spellSkill(int skillID, string uuid = "")
	{
		if (!jsonData.instance.skillJsonData.HasField(string.Concat(skillID)))
		{
			return;
		}
		if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillAttack")
		{
			foreach (KeyValuePair<int, Entity> entity in KBEngineApp.app.entities)
			{
				if (entity.Value != this.entity)
				{
					spellTarget(skillID, entity.Value.id, uuid);
				}
			}
		}
		else if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillSelf")
		{
			foreach (KeyValuePair<int, Entity> entity2 in KBEngineApp.app.entities)
			{
				if (entity2.Value == this.entity)
				{
					spellTarget(skillID, entity2.Value.id, uuid);
				}
			}
		}
		UIFightPanel.Inst.PlayerLingQiController.ResetPlayerLingQiCount();
	}

	private void spellTarget(int skillID, int targetID, string uuid = "")
	{
		foreach (GUIPackage.Skill item in ((Avatar)entity).skill)
		{
			if (uuid != null && uuid != "")
			{
				if (uuid == item.weaponuuid && skillID == item.skill_ID)
				{
					Entity receiver = KBEngineApp.app.entities[targetID];
					item.Puting(entity, receiver, 0, uuid);
					break;
				}
			}
			else if (skillID == item.skill_ID)
			{
				Entity receiver2 = KBEngineApp.app.entities[targetID];
				item.Puting(entity, receiver2);
				break;
			}
		}
	}

	public void addDBuff(int buffid, int time)
	{
		for (int i = 0; i < time; i++)
		{
			addDBuff(buffid);
		}
	}

	public void getBuffFlag(int buffid, List<int> Buffflag)
	{
		Buffflag.Add(buffid);
		Buffflag.Add(1);
	}

	public List<int> addDBuff(int buffid)
	{
		if (!_BuffJsonData.DataDict.ContainsKey(buffid))
		{
			Debug.LogError((object)$"无法添加id为{buffid}的buff，buff不存在，请检查配表");
			return new List<int>();
		}
		Avatar avatar = (Avatar)entity;
		List<int> list = new List<int>();
		getBuffFlag(buffid, list);
		avatar.spell.onBuffTickByType(11, list);
		if (list[1] == 0)
		{
			return null;
		}
		if (avatar.buffmag.HasBuffSeid(6))
		{
			foreach (List<int> item in avatar.buffmag.getBuffBySeid(6))
			{
				foreach (int item2 in BuffSeidJsonData6.DataDict[item[2]].value1)
				{
					if (item2 == buffid)
					{
						return null;
					}
				}
			}
		}
		List<int> list2 = new List<int>();
		list2.Add(1);
		list2.Add(jsonData.instance.BuffJsonData[string.Concat(buffid)]["totaltime"].I);
		list2.Add(buffid);
		int i = jsonData.instance.BuffJsonData[string.Concat(buffid)]["BuffType"].I;
		bool flag = false;
		int num = 0;
		switch (i)
		{
		case 0:
		{
			for (int k = 0; k < avatar.bufflist.Count; k++)
			{
				if (avatar.bufflist[k][2] == buffid)
				{
					flag = true;
					avatar.bufflist[k][1] += list2[1];
					num = k;
					break;
				}
			}
			break;
		}
		case 1:
		{
			for (int j = 0; j < avatar.bufflist.Count; j++)
			{
				if (avatar.bufflist[j][2] == buffid)
				{
					flag = true;
					avatar.bufflist[j][1] = list2[1];
					num = j;
					break;
				}
			}
			break;
		}
		case 2:
			flag = true;
			avatar.bufflist.Add(list2);
			break;
		case 3:
			flag = true;
			num = avatar.bufflist.Count;
			avatar.bufflist.Add(list2);
			break;
		}
		if (!flag)
		{
			avatar.bufflist.Add(list2);
			num = avatar.bufflist.IndexOf(list2);
			avatar.buffmag.PlayBuffAdd(jsonData.instance.BuffJsonData[buffid.ToString()]["skillEffect"].str);
		}
		foreach (JSONObject item3 in jsonData.instance.BuffJsonData[string.Concat(buffid)]["seid"].list)
		{
			if (item3.I == 7)
			{
				int value = BuffSeidJsonData7.DataDict[buffid].value1;
				if (avatar.bufflist[num][1] > value)
				{
					avatar.bufflist[num][1] = value;
				}
			}
			else
			{
				if (item3.I != 188)
				{
					continue;
				}
				switch (BuffSeidJsonData188.DataDict[avatar.bufflist[num][2]].value1)
				{
				case 1:
					if (avatar.bufflist[num][1] > avatar.GetBaseShenShi())
					{
						avatar.bufflist[num][1] = avatar.GetBaseShenShi();
					}
					break;
				case 2:
					if (avatar.bufflist[num][1] > avatar.GetBaseDunSu())
					{
						avatar.bufflist[num][1] = avatar.GetBaseDunSu();
					}
					break;
				}
			}
		}
		if (avatar.bufflist[num][1] >= 2)
		{
			avatar.spell.ONBuffTick(num, 21);
		}
		if (avatar.bufflist[num][1] >= 3)
		{
			avatar.spell.ONBuffTick(num, 13);
		}
		if (avatar.bufflist[num][1] >= 4)
		{
			avatar.spell.ONBuffTick(num, 14);
		}
		if (avatar.bufflist[num][1] >= 5)
		{
			avatar.spell.ONBuffTick(num, 15);
		}
		if (avatar.bufflist[num][1] >= 6)
		{
			avatar.spell.ONBuffTick(num, 16);
		}
		if (avatar.bufflist[num][1] >= 10)
		{
			avatar.spell.ONBuffTick(num, 17);
		}
		jsonData.instance.Buff[buffid].onAttach(avatar, avatar.bufflist[num]);
		foreach (JSONObject item4 in jsonData.instance.BuffJsonData[string.Concat(buffid)]["seid"].list)
		{
			if ((int)item4.n == 48)
			{
				jsonData.instance.Buff[avatar.bufflist[num][2]].ListRealizeSeid48(48, avatar, avatar.bufflist[num], null);
			}
			if ((int)item4.n == 22)
			{
				jsonData.instance.Buff[avatar.bufflist[num][2]].ListRealizeSeid22(22, avatar, avatar.bufflist[num], null);
			}
		}
		List<int> flag2 = new List<int>();
		if ((int)jsonData.instance.BuffJsonData[buffid.ToString()]["trigger"].n == 12)
		{
			avatar.spell.onBuffTick(num, flag2);
		}
		avatar.spell.onBuffTickByType(36, list);
		try
		{
			SteamChengJiu.ints.FightBuffOnceSetStat(avatar, avatar.bufflist[num][2], avatar.bufflist[num][1]);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.HuaShen && avatar.isPlayer() && (Object)(object)BigTuPoResultIMag.Inst == (Object)null)
		{
			int buffSum = avatar.buffmag.GetBuffSum(3133);
			int buffSum2 = avatar.buffmag.GetBuffSum(3132);
			if (buffSum >= 100)
			{
				GlobalValue.SetTalk(1, 2, "Spell.addDBuff 化神检测");
				ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
					.ShowSuccess(4);
			}
			else if (buffSum2 >= 100)
			{
				GlobalValue.SetTalk(1, 0, "Spell.addDBuff 化神检测");
				ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
					.ShowFail(4);
			}
		}
		if ((Object)(object)UIFightPanel.Inst != (Object)null)
		{
			UIFightPanel.Inst.RefreshCD();
		}
		Event.fireOut("UpdataBuff");
		return list2;
	}

	public List<int> addBuff(int buffid, int num)
	{
		if (!_BuffJsonData.DataDict.ContainsKey(buffid))
		{
			Debug.LogError((object)$"无法添加id为{buffid}的buff，buff不存在，请检查配表");
			return new List<int>();
		}
		int buffType = _BuffJsonData.DataDict[buffid].BuffType;
		switch (buffType)
		{
		case 3:
			addDBuff(buffid, num);
			return null;
		case 1:
			addDBuff(buffid);
			return null;
		default:
		{
			Avatar avatar = (Avatar)entity;
			List<int> list = new List<int>();
			getBuffFlag(buffid, list);
			avatar.spell.onBuffTickByType(11, list);
			if (list[1] == 0)
			{
				return null;
			}
			if (avatar.buffmag.HasBuffSeid(6))
			{
				foreach (List<int> item in avatar.buffmag.getBuffBySeid(6))
				{
					foreach (JSONObject item2 in jsonData.instance.BuffSeidJsonData[6][string.Concat(item[2])]["value1"].list)
					{
						if ((int)item2.n == buffid)
						{
							return null;
						}
					}
				}
			}
			List<int> list2 = new List<int>();
			list2.Add(num);
			list2.Add(num);
			list2.Add(buffid);
			bool flag = false;
			int num2 = 0;
			switch (buffType)
			{
			case 0:
			{
				for (int j = 0; j < avatar.bufflist.Count; j++)
				{
					if (avatar.bufflist[j][2] == buffid)
					{
						flag = true;
						avatar.bufflist[j][1] += list2[1];
						num2 = j;
						break;
					}
				}
				break;
			}
			case 1:
			{
				for (int i = 0; i < avatar.bufflist.Count; i++)
				{
					if (avatar.bufflist[i][2] == buffid)
					{
						flag = true;
						avatar.bufflist[i][1] = list2[1];
						num2 = i;
						break;
					}
				}
				break;
			}
			case 2:
				flag = true;
				avatar.bufflist.Add(list2);
				break;
			}
			if (!flag)
			{
				avatar.bufflist.Add(list2);
				num2 = avatar.bufflist.IndexOf(list2);
				avatar.buffmag.PlayBuffAdd(jsonData.instance.BuffJsonData[buffid.ToString()]["skillEffect"].str);
			}
			foreach (JSONObject item3 in jsonData.instance.BuffJsonData[string.Concat(buffid)]["seid"].list)
			{
				if ((int)item3.n == 7)
				{
					int num3 = (int)jsonData.instance.BuffSeidJsonData[7][string.Concat(buffid)]["value1"].n;
					if (avatar.bufflist[num2][1] > num3)
					{
						avatar.bufflist[num2][1] = num3;
					}
				}
				else
				{
					if (item3.I != 188)
					{
						continue;
					}
					switch (BuffSeidJsonData188.DataDict[avatar.bufflist[num2][2]].value1)
					{
					case 1:
						if (avatar.bufflist[num2][1] > avatar.GetBaseShenShi())
						{
							avatar.bufflist[num2][1] = avatar.GetBaseShenShi();
						}
						break;
					case 2:
						if (avatar.bufflist[num2][1] > avatar.GetBaseDunSu())
						{
							avatar.bufflist[num2][1] = avatar.GetBaseDunSu();
						}
						break;
					}
				}
			}
			if (avatar.bufflist[num2][1] >= 2)
			{
				avatar.spell.ONBuffTick(num2, 21);
			}
			if (avatar.bufflist[num2][1] >= 3)
			{
				avatar.spell.ONBuffTick(num2, 13);
			}
			if (avatar.bufflist[num2][1] >= 4)
			{
				avatar.spell.ONBuffTick(num2, 14);
			}
			if (avatar.bufflist[num2][1] >= 5)
			{
				avatar.spell.ONBuffTick(num2, 15);
			}
			if (avatar.bufflist[num2][1] >= 6)
			{
				avatar.spell.ONBuffTick(num2, 16);
			}
			if (avatar.bufflist[num2][1] >= 10)
			{
				avatar.spell.ONBuffTick(num2, 17);
			}
			jsonData.instance.Buff[buffid].onAttach(avatar, avatar.bufflist[num2]);
			foreach (JSONObject item4 in jsonData.instance.BuffJsonData[string.Concat(buffid)]["seid"].list)
			{
				if ((int)item4.n == 48)
				{
					jsonData.instance.Buff[avatar.bufflist[num2][2]].ListRealizeSeid48(48, avatar, avatar.bufflist[num2], null);
				}
				if ((int)item4.n == 22)
				{
					jsonData.instance.Buff[avatar.bufflist[num2][2]].ListRealizeSeid22(22, avatar, avatar.bufflist[num2], null);
				}
			}
			List<int> flag2 = new List<int>();
			if ((int)jsonData.instance.BuffJsonData[buffid.ToString()]["trigger"].n == 12)
			{
				avatar.spell.onBuffTick(num2, flag2);
			}
			avatar.spell.onBuffTickByType(36, list);
			try
			{
				SteamChengJiu.ints.FightBuffOnceSetStat(avatar, avatar.bufflist[num2][2], avatar.bufflist[num2][1]);
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex);
			}
			if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.HuaShen && avatar.isPlayer() && (Object)(object)BigTuPoResultIMag.Inst == (Object)null)
			{
				int buffSum = avatar.buffmag.GetBuffSum(3133);
				int buffSum2 = avatar.buffmag.GetBuffSum(3132);
				if (buffSum >= 100)
				{
					GlobalValue.SetTalk(1, 2, "Spell.addDBuff 化神检测");
					ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
						.ShowSuccess(4);
				}
				else if (buffSum2 >= 100)
				{
					GlobalValue.SetTalk(1, 0, "Spell.addDBuff 化神检测");
					ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
						.ShowFail(4);
				}
			}
			UIFightPanel.Inst.RefreshCD();
			Event.fireOut("UpdataBuff");
			return list2;
		}
		}
	}

	public void removeBuff(List<int> buffid)
	{
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		Avatar avatar = (Avatar)entity;
		List<int> list = new List<int>();
		getBuffFlag(buffid[2], list);
		avatar.spell.onBuffTickByType(32, list);
		jsonData.instance.Buff[buffid[2]].onDetach(avatar, buffid);
		buffid[1] = 0;
		if (avatar.isPlayer())
		{
			RoundManager.instance.curRemoveBuffId = buffid[2];
			RoundManager.EventFightTalk("RemoveBuff", null);
		}
		avatar.bufflist.Remove(buffid);
		foreach (JSONObject item2 in jsonData.instance.BuffJsonData[string.Concat(buffid[2])]["seid"].list)
		{
			if ((int)item2.n != 20)
			{
				continue;
			}
			foreach (JSONObject item3 in jsonData.instance.BuffSeidJsonData[20][string.Concat(buffid[2])]["value1"].list)
			{
				foreach (List<int> item4 in avatar.buffmag.getBuffByID((int)item3.n))
				{
					avatar.spell.removeBuff(item4);
				}
			}
		}
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = (UnityAction)delegate
		{
			avatar.buffmag.PlayBuffRemove(jsonData.instance.BuffJsonData[buffid[2].ToString()]["skillEffect"].str);
			Event.fireOut("PlayBuffAnimaiton", buffid);
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
	}

	public bool HasBuff(int buffID)
	{
		Avatar avatar = (Avatar)entity;
		for (int i = 0; i < avatar.bufflist.Count; i++)
		{
			if (avatar.bufflist[i][2] == buffID)
			{
				return true;
			}
		}
		return false;
	}

	public void removeAllBuff()
	{
		((Avatar)entity).bufflist.Clear();
	}

	public void BuffSeidRealizeCheck(int index, BuffLoopData buffLoopData)
	{
		Avatar avatar = (Avatar)entity;
		if (avatar.buffmag.HasBuffSeid(219))
		{
			int num = avatar.bufflist[index][2];
			foreach (List<int> item in avatar.buffmag.getBuffBySeid(219))
			{
				JSONObject seidJson = jsonData.instance.Buff[item[2]].getSeidJson(219);
				int i = seidJson["value1"].I;
				int i2 = seidJson["value2"].I;
				int i3 = seidJson["value3"].I;
				JSONObject json = seidJson["value4"];
				if (i3 == num)
				{
					int buffSum = avatar.buffmag.GetBuffSum(i);
					float num2 = (float)avatar.HP_Max * ((float)i2 / 100f);
					if ((float)buffSum < num2)
					{
						buffLoopData.SetSeid(Tools.JsonListToList(json));
					}
				}
			}
		}
		if (!avatar.buffmag.HasBuffSeid(136))
		{
			return;
		}
		int num3 = avatar.bufflist[index][2];
		foreach (List<int> item2 in avatar.buffmag.getBuffBySeid(136))
		{
			JSONObject seidJson2 = jsonData.instance.Buff[item2[2]].getSeidJson(136);
			if (seidJson2["value1"].I == num3)
			{
				buffLoopData.SetSeid(Tools.JsonListToList(seidJson2["value2"]));
			}
		}
	}

	public void onBuffTick(int index, List<int> flag = null, int type = 0)
	{
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		Avatar avatar = (Avatar)entity;
		List<int> buffdata = avatar.bufflist[index];
		if (type != 38 && avatar.OtherAvatar != null && index < avatar.bufflist.Count)
		{
			if (!avatar.BuffSeidFlag.ContainsKey(176))
			{
				avatar.BuffSeidFlag.Add(176, new Dictionary<int, int>());
			}
			int key = avatar.bufflist[index][2];
			if (!avatar.BuffSeidFlag[176].ContainsKey(key))
			{
				avatar.BuffSeidFlag[176].Add(key, 1);
			}
		}
		if (avatar.bufflist[index][1] <= 0 && type != 32)
		{
			return;
		}
		BuffLoopData buffLoopData = new BuffLoopData(1, jsonData.instance.Buff[avatar.bufflist[index][2]].seid);
		BuffSeidRealizeCheck(index, buffLoopData);
		List<int> flagClone = new List<int>();
		flag?.ForEach(delegate(int i)
		{
			flagClone.Add(i);
		});
		if (avatar.bufflist[index][2] == 1)
		{
			avatar.OtherAvatar.spell.onBuffTickByType(41);
		}
		jsonData.instance.Buff[avatar.bufflist[index][2]].onLoopTrigger(avatar, buffdata, flag, buffLoopData);
		if ((int)jsonData.instance.BuffJsonData[string.Concat(buffdata[2])]["removeTrigger"].n == 1)
		{
			buffdata[1]--;
		}
		if ((int)jsonData.instance.BuffJsonData[string.Concat(buffdata[2])]["removeTrigger"].n == 2)
		{
			buffdata[1] = 0;
		}
		if ((int)jsonData.instance.BuffJsonData[string.Concat(buffdata[2])]["removeTrigger"].n == 12)
		{
			buffdata[1] = (int)Math.Ceiling((double)buffdata[1] / 2.0);
		}
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = (UnityAction)delegate
		{
			avatar.buffmag.PlayBuffTarget(jsonData.instance.BuffJsonData[buffdata[2].ToString()]["skillEffect"].str);
			Event.fireOut("PlayBuffAnimaiton", buffdata);
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
		try
		{
			if (type != 38 && avatar.OtherAvatar != null)
			{
				avatar.OtherAvatar.spell.onBuffTickByType(38, flag);
				if (avatar.BuffSeidFlag.ContainsKey(176))
				{
					avatar.BuffSeidFlag[176].Clear();
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		Event.fireOut("UpdataBuff");
	}

	public void onBuffTickById(int id, List<int> flag = null, int type = 0)
	{
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Expected O, but got Unknown
		Avatar avatar = (Avatar)entity;
		List<int> buffdata = avatar.buffmag.GetBuffById(id);
		if (buffdata == null)
		{
			return;
		}
		int index = avatar.bufflist.IndexOf(buffdata);
		if (type != 38 && avatar.OtherAvatar != null)
		{
			if (!avatar.BuffSeidFlag.ContainsKey(176))
			{
				avatar.BuffSeidFlag.Add(176, new Dictionary<int, int>());
			}
			int key = buffdata[2];
			if (!avatar.BuffSeidFlag[176].ContainsKey(key))
			{
				avatar.BuffSeidFlag[176].Add(key, 1);
			}
		}
		if (buffdata[1] <= 0 && type != 32)
		{
			return;
		}
		BuffLoopData buffLoopData = new BuffLoopData(1, jsonData.instance.Buff[buffdata[2]].seid);
		try
		{
			BuffSeidRealizeCheck(index, buffLoopData);
		}
		catch (Exception ex)
		{
			Debug.Log((object)ex);
		}
		List<int> flagClone = new List<int>();
		flag?.ForEach(delegate(int i)
		{
			flagClone.Add(i);
		});
		if (buffdata[2] == 1)
		{
			avatar.OtherAvatar.spell.onBuffTickByType(41);
		}
		jsonData.instance.Buff[buffdata[2]].onLoopTrigger(avatar, buffdata, flag, buffLoopData);
		if ((int)jsonData.instance.BuffJsonData[string.Concat(buffdata[2])]["removeTrigger"].n == 1)
		{
			buffdata[1]--;
		}
		if ((int)jsonData.instance.BuffJsonData[string.Concat(buffdata[2])]["removeTrigger"].n == 2)
		{
			buffdata[1] = 0;
		}
		if ((int)jsonData.instance.BuffJsonData[string.Concat(buffdata[2])]["removeTrigger"].n == 2)
		{
			buffdata[1] = (int)Math.Ceiling((double)buffdata[1] / 2.0);
		}
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = (UnityAction)delegate
		{
			avatar.buffmag.PlayBuffTarget(jsonData.instance.BuffJsonData[buffdata[2].ToString()]["skillEffect"].str);
			Event.fireOut("PlayBuffAnimaiton", buffdata);
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
		try
		{
			if (type != 38 && avatar.OtherAvatar != null)
			{
				avatar.OtherAvatar.spell.onBuffTickByType(38, flag);
				if (avatar.BuffSeidFlag.ContainsKey(176))
				{
					avatar.BuffSeidFlag[176].Clear();
				}
			}
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)ex2);
		}
		Event.fireOut("UpdataBuff");
	}

	public void AutoRemoveBuff()
	{
		Avatar obj = (Avatar)entity;
		bool flag = false;
		List<List<int>> list = new List<List<int>>();
		foreach (List<int> item in obj.bufflist)
		{
			if (item[1] <= 0)
			{
				list.Add(item);
			}
		}
		foreach (List<int> item2 in list)
		{
			if (item2[1] <= 0)
			{
				removeBuff(item2);
			}
			flag = true;
		}
		if (flag)
		{
			Event.fireOut("UpdataBuff");
		}
	}

	public void onRemoveBuffByType(int type, int removeCount = 1)
	{
		Avatar avatar = (Avatar)entity;
		List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(145);
		int count = avatar.bufflist.Count;
		for (int i = 0; i < count; i++)
		{
			int num = (int)jsonData.instance.BuffJsonData[string.Concat(avatar.bufflist[i][2])]["removeTrigger"].n;
			if (type != num)
			{
				continue;
			}
			for (int j = 0; j < buffBySeid.Count; j++)
			{
				int num2 = buffBySeid[j][2];
				if (jsonData.instance.BuffSeidJsonData[145][string.Concat(num2)]["value1"].I == avatar.bufflist[i][2])
				{
					num = jsonData.instance.BuffSeidJsonData[145][string.Concat(num2)]["value2"].I;
				}
			}
			if (num == 2)
			{
				avatar.bufflist[i][1] = 0;
			}
			if (num == 3 || num == 5 || num == 9 || num == 10)
			{
				if (avatar.bufflist[i][1] - removeCount <= 0 && num == 9)
				{
					List<int> list = new List<int>();
					getBuffFlag(avatar.bufflist[i][2], list);
					avatar.spell.onBuffTickByType(32, list);
				}
				avatar.bufflist[i][1] -= removeCount;
				if (avatar.bufflist[i][1] < 0)
				{
					avatar.bufflist[i][1] = 0;
				}
			}
			if (num == 4 || num == 6)
			{
				avatar.bufflist[i][1] = 0;
			}
			if (num == 13 || num == 14)
			{
				avatar.bufflist[i][1] /= 2;
			}
		}
		Event.fireOut("UpdataBuff");
	}

	public void ONBuffTick(int buffindex, int type)
	{
		Avatar avatar = (Avatar)entity;
		if (jsonData.instance.BuffJsonData[string.Concat(avatar.bufflist[buffindex][2])]["trigger"].I == type)
		{
			avatar.spell.onBuffTick(buffindex);
		}
	}

	public void onBuffTickByType(int type)
	{
		Avatar avatar = (Avatar)entity;
		int count = avatar.bufflist.Count;
		for (int i = 0; i < count; i++)
		{
			int num = avatar.bufflist[i][2];
			if (jsonData.instance.BuffJsonData.ContainsKey(num.ToString()))
			{
				if (jsonData.instance.BuffJsonData[num.ToString()]["trigger"].I == type)
				{
					avatar.spell.onBuffTick(i, new List<int>());
				}
			}
			else
			{
				Debug.LogError((object)$"Spell.onBuffTickByType({type})，BuffJsonData不存在buffid{num}");
			}
		}
		Event.fireOut("UpdataBuff");
	}

	public void onBuffTickByType(int type, List<int> flag)
	{
		try
		{
			Avatar avatar = (Avatar)entity;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			int count = avatar.bufflist.Count;
			List<List<int>> list = new List<List<int>>();
			int num = 0;
			int buff;
			for (buff = 0; buff < count; buff++)
			{
				if (count != avatar.bufflist.Count)
				{
					buff = 0;
					count = avatar.bufflist.Count;
				}
				if (list.FindAll((List<int> a) => a == avatar.bufflist[buff]).Count > 0)
				{
					continue;
				}
				list.Add(avatar.bufflist[buff]);
				if (_BuffJsonData.DataDict[avatar.bufflist[buff][2]].trigger != type)
				{
					continue;
				}
				if (dictionary.ContainsKey(avatar.bufflist[buff][2]))
				{
					if (dictionary[avatar.bufflist[buff][2]] != buff)
					{
						continue;
					}
				}
				else
				{
					dictionary.Add(avatar.bufflist[buff][2], buff);
				}
				num = avatar.bufflist[buff][2];
				avatar.spell.onBuffTick(buff, flag, type);
				if (avatar.isPlayer() && avatar.fightTemp.LianQiBuffEquipDictionary.Keys.Count > 0 && avatar.fightTemp.LianQiBuffEquipDictionary.ContainsKey(num))
				{
					List<JSONObject> list2 = avatar.fightTemp.LianQiBuffEquipDictionary[num].list;
					for (int i = 0; i < list2.Count; i++)
					{
						if (list2[i]["id"].I == 62)
						{
							if ((float)avatar.HP / (float)avatar.HP_Max * 100f > list2[i]["value1"][0].n)
							{
								break;
							}
							continue;
						}
						if (list2[i]["id"].I == 1)
						{
							for (int j = 0; j < list2[i]["value1"].Count; j++)
							{
								avatar.recvDamage(avatar, avatar, 18005, -list2[i]["value2"][j].I);
							}
						}
						if (list2[i]["id"].I == 5)
						{
							for (int k = 0; k < list2[i]["value1"].Count; k++)
							{
								avatar.spell.addDBuff(list2[i]["value1"][k].I, list2[i]["value2"][k].I);
							}
						}
						if (list2[i]["id"].I == 17)
						{
							for (int l = 0; l < list2[i]["value1"].Count; l++)
							{
								avatar.OtherAvatar.spell.addDBuff(list2[i]["value1"][l].I, list2[i]["value2"][l].I);
							}
						}
					}
				}
				if (avatar.isPlayer() || RoundManager.instance.newNpcFightManager == null || RoundManager.instance.newNpcFightManager.LianQiBuffEquipDictionary.Keys.Count <= 0 || !RoundManager.instance.newNpcFightManager.LianQiBuffEquipDictionary.ContainsKey(num))
				{
					continue;
				}
				List<JSONObject> list3 = RoundManager.instance.newNpcFightManager.LianQiBuffEquipDictionary[num].list;
				for (int m = 0; m < list3.Count; m++)
				{
					if (list3[m]["id"].I == 62)
					{
						if ((float)avatar.HP / (float)avatar.HP_Max * 100f > list3[m]["value1"][0].n)
						{
							break;
						}
						continue;
					}
					if (list3[m]["id"].I == 1)
					{
						for (int n = 0; n < list3[m]["value1"].Count; n++)
						{
							avatar.recvDamage(avatar, avatar, 18005, -list3[m]["value2"][n].I);
						}
					}
					if (list3[m]["id"].I == 5)
					{
						for (int num2 = 0; num2 < list3[m]["value1"].Count; num2++)
						{
							avatar.spell.addDBuff(list3[m]["value1"][num2].I, list3[m]["value2"][num2].I);
						}
					}
					if (list3[m]["id"].I == 17)
					{
						for (int num3 = 0; num3 < list3[m]["value1"].Count; num3++)
						{
							avatar.OtherAvatar.spell.addDBuff(list3[m]["value1"][num3].I, list3[m]["value2"][num3].I);
						}
					}
				}
			}
			Event.fireOut("UpdataBuff");
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"{arg}");
		}
	}
}
