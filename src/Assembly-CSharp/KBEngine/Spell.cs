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

namespace KBEngine
{
	// Token: 0x02000C7D RID: 3197
	public class Spell
	{
		// Token: 0x06005870 RID: 22640 RVA: 0x0024B75D File Offset: 0x0024995D
		public Spell(Entity avater)
		{
			this.entity = avater;
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x0024B778 File Offset: 0x00249978
		public void UseSkillLateAddBuff(int id, int count)
		{
			if (this.UseSkillLateDict == null)
			{
				this.UseSkillLateDict = new Dictionary<int, int>();
			}
			if (this.UseSkillLateDict.ContainsKey(id))
			{
				Dictionary<int, int> useSkillLateDict = this.UseSkillLateDict;
				useSkillLateDict[id] += count;
				return;
			}
			this.UseSkillLateDict.Add(id, count);
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x0024B7D0 File Offset: 0x002499D0
		public void VirtualspellSkill(int skillID, string uuid = "")
		{
			if (!jsonData.instance.skillJsonData.HasField(string.Concat(skillID)))
			{
				return;
			}
			if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillAttack")
			{
				using (Dictionary<int, Entity>.Enumerator enumerator = KBEngineApp.app.entities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Entity> keyValuePair = enumerator.Current;
						if (keyValuePair.Value != this.entity)
						{
							this.VirtualspellTarget(skillID, keyValuePair.Value.id, uuid);
						}
					}
					goto IL_136;
				}
			}
			if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillSelf")
			{
				foreach (KeyValuePair<int, Entity> keyValuePair2 in KBEngineApp.app.entities)
				{
					if (keyValuePair2.Value == this.entity)
					{
						this.VirtualspellTarget(skillID, keyValuePair2.Value.id, uuid);
					}
				}
			}
			IL_136:
			UIFightPanel.Inst.PlayerLingQiController.ResetPlayerLingQiCount();
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x0024B940 File Offset: 0x00249B40
		private void VirtualspellTarget(int skillID, int targetID, string uuid = "")
		{
			foreach (Skill skill in ((Avatar)this.entity).skill)
			{
				if (uuid != "")
				{
					if (uuid == skill.weaponuuid && skillID == skill.skill_ID)
					{
						Entity receiver = KBEngineApp.app.entities[targetID];
						skill.VirtualPuting(this.entity, receiver, 0, uuid);
						break;
					}
				}
				else if (skillID == skill.skill_ID)
				{
					Entity receiver2 = KBEngineApp.app.entities[targetID];
					skill.VirtualPuting(this.entity, receiver2, 0, "");
					break;
				}
			}
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x0024BA10 File Offset: 0x00249C10
		public void spellSkill(int skillID, string uuid = "")
		{
			if (!jsonData.instance.skillJsonData.HasField(string.Concat(skillID)))
			{
				return;
			}
			if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillAttack")
			{
				using (Dictionary<int, Entity>.Enumerator enumerator = KBEngineApp.app.entities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Entity> keyValuePair = enumerator.Current;
						if (keyValuePair.Value != this.entity)
						{
							this.spellTarget(skillID, keyValuePair.Value.id, uuid);
						}
					}
					goto IL_136;
				}
			}
			if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillSelf")
			{
				foreach (KeyValuePair<int, Entity> keyValuePair2 in KBEngineApp.app.entities)
				{
					if (keyValuePair2.Value == this.entity)
					{
						this.spellTarget(skillID, keyValuePair2.Value.id, uuid);
					}
				}
			}
			IL_136:
			UIFightPanel.Inst.PlayerLingQiController.ResetPlayerLingQiCount();
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x0024BB80 File Offset: 0x00249D80
		private void spellTarget(int skillID, int targetID, string uuid = "")
		{
			foreach (Skill skill in ((Avatar)this.entity).skill)
			{
				if (uuid != null && uuid != "")
				{
					if (uuid == skill.weaponuuid && skillID == skill.skill_ID)
					{
						Entity receiver = KBEngineApp.app.entities[targetID];
						skill.Puting(this.entity, receiver, 0, uuid);
						break;
					}
				}
				else if (skillID == skill.skill_ID)
				{
					Entity receiver2 = KBEngineApp.app.entities[targetID];
					skill.Puting(this.entity, receiver2, 0, "");
					break;
				}
			}
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x0024BC54 File Offset: 0x00249E54
		public void addDBuff(int buffid, int time)
		{
			for (int i = 0; i < time; i++)
			{
				this.addDBuff(buffid);
			}
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x0024BC75 File Offset: 0x00249E75
		public void getBuffFlag(int buffid, List<int> Buffflag)
		{
			Buffflag.Add(buffid);
			Buffflag.Add(1);
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x0024BC88 File Offset: 0x00249E88
		public List<int> addDBuff(int buffid)
		{
			if (!_BuffJsonData.DataDict.ContainsKey(buffid))
			{
				Debug.LogError(string.Format("无法添加id为{0}的buff，buff不存在，请检查配表", buffid));
				return new List<int>();
			}
			Avatar avatar = (Avatar)this.entity;
			List<int> list = new List<int>();
			this.getBuffFlag(buffid, list);
			avatar.spell.onBuffTickByType(11, list);
			if (list[1] == 0)
			{
				return null;
			}
			if (avatar.buffmag.HasBuffSeid(6))
			{
				foreach (List<int> list2 in avatar.buffmag.getBuffBySeid(6))
				{
					using (List<int>.Enumerator enumerator2 = BuffSeidJsonData6.DataDict[list2[2]].value1.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current == buffid)
							{
								return null;
							}
						}
					}
				}
			}
			List<int> list3 = new List<int>();
			list3.Add(1);
			list3.Add(jsonData.instance.BuffJsonData[string.Concat(buffid)]["totaltime"].I);
			list3.Add(buffid);
			int i = jsonData.instance.BuffJsonData[string.Concat(buffid)]["BuffType"].I;
			bool flag = false;
			int num = 0;
			if (i == 0)
			{
				for (int j = 0; j < avatar.bufflist.Count; j++)
				{
					if (avatar.bufflist[j][2] == buffid)
					{
						flag = true;
						List<int> list4 = avatar.bufflist[j];
						list4[1] = list4[1] + list3[1];
						num = j;
						break;
					}
				}
			}
			else if (i == 1)
			{
				for (int k = 0; k < avatar.bufflist.Count; k++)
				{
					if (avatar.bufflist[k][2] == buffid)
					{
						flag = true;
						avatar.bufflist[k][1] = list3[1];
						num = k;
						break;
					}
				}
			}
			else if (i == 2)
			{
				flag = true;
				avatar.bufflist.Add(list3);
			}
			else if (i == 3)
			{
				flag = true;
				num = avatar.bufflist.Count;
				avatar.bufflist.Add(list3);
			}
			if (!flag)
			{
				avatar.bufflist.Add(list3);
				num = avatar.bufflist.IndexOf(list3);
				avatar.buffmag.PlayBuffAdd(jsonData.instance.BuffJsonData[buffid.ToString()]["skillEffect"].str);
			}
			foreach (JSONObject jsonobject in jsonData.instance.BuffJsonData[string.Concat(buffid)]["seid"].list)
			{
				if (jsonobject.I == 7)
				{
					int value = BuffSeidJsonData7.DataDict[buffid].value1;
					if (avatar.bufflist[num][1] > value)
					{
						avatar.bufflist[num][1] = value;
					}
				}
				else if (jsonobject.I == 188)
				{
					int value2 = BuffSeidJsonData188.DataDict[avatar.bufflist[num][2]].value1;
					if (value2 != 1)
					{
						if (value2 == 2)
						{
							if (avatar.bufflist[num][1] > avatar.GetBaseDunSu())
							{
								avatar.bufflist[num][1] = avatar.GetBaseDunSu();
							}
						}
					}
					else if (avatar.bufflist[num][1] > avatar.GetBaseShenShi())
					{
						avatar.bufflist[num][1] = avatar.GetBaseShenShi();
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
			foreach (JSONObject jsonobject2 in jsonData.instance.BuffJsonData[string.Concat(buffid)]["seid"].list)
			{
				if ((int)jsonobject2.n == 48)
				{
					jsonData.instance.Buff[avatar.bufflist[num][2]].ListRealizeSeid48(48, avatar, avatar.bufflist[num], null);
				}
				if ((int)jsonobject2.n == 22)
				{
					jsonData.instance.Buff[avatar.bufflist[num][2]].ListRealizeSeid22(22, avatar, avatar.bufflist[num], null);
				}
			}
			List<int> flag2 = new List<int>();
			if ((int)jsonData.instance.BuffJsonData[buffid.ToString()]["trigger"].n == 12)
			{
				avatar.spell.onBuffTick(num, flag2, 0);
			}
			avatar.spell.onBuffTickByType(36, list);
			try
			{
				SteamChengJiu.ints.FightBuffOnceSetStat(avatar, avatar.bufflist[num][2], avatar.bufflist[num][1]);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.HuaShen && avatar.isPlayer() && BigTuPoResultIMag.Inst == null)
			{
				int buffSum = avatar.buffmag.GetBuffSum(3133);
				int buffSum2 = avatar.buffmag.GetBuffSum(3132);
				if (buffSum >= 100)
				{
					GlobalValue.SetTalk(1, 2, "Spell.addDBuff 化神检测");
					ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowSuccess(4);
				}
				else if (buffSum2 >= 100)
				{
					GlobalValue.SetTalk(1, 0, "Spell.addDBuff 化神检测");
					ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowFail(4, 0);
				}
			}
			if (UIFightPanel.Inst != null)
			{
				UIFightPanel.Inst.RefreshCD();
			}
			Event.fireOut("UpdataBuff", Array.Empty<object>());
			return list3;
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x0024C458 File Offset: 0x0024A658
		public List<int> addBuff(int buffid, int num)
		{
			if (!_BuffJsonData.DataDict.ContainsKey(buffid))
			{
				Debug.LogError(string.Format("无法添加id为{0}的buff，buff不存在，请检查配表", buffid));
				return new List<int>();
			}
			int buffType = _BuffJsonData.DataDict[buffid].BuffType;
			if (buffType == 3)
			{
				this.addDBuff(buffid, num);
				return null;
			}
			if (buffType == 1)
			{
				this.addDBuff(buffid);
				return null;
			}
			Avatar avatar = (Avatar)this.entity;
			List<int> list = new List<int>();
			this.getBuffFlag(buffid, list);
			avatar.spell.onBuffTickByType(11, list);
			if (list[1] == 0)
			{
				return null;
			}
			if (avatar.buffmag.HasBuffSeid(6))
			{
				foreach (List<int> list2 in avatar.buffmag.getBuffBySeid(6))
				{
					using (List<JSONObject>.Enumerator enumerator2 = jsonData.instance.BuffSeidJsonData[6][string.Concat(list2[2])]["value1"].list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if ((int)enumerator2.Current.n == buffid)
							{
								return null;
							}
						}
					}
				}
			}
			List<int> list3 = new List<int>();
			list3.Add(num);
			list3.Add(num);
			list3.Add(buffid);
			bool flag = false;
			int num2 = 0;
			if (buffType == 0)
			{
				for (int i = 0; i < avatar.bufflist.Count; i++)
				{
					if (avatar.bufflist[i][2] == buffid)
					{
						flag = true;
						List<int> list4 = avatar.bufflist[i];
						list4[1] = list4[1] + list3[1];
						num2 = i;
						break;
					}
				}
			}
			else if (buffType == 1)
			{
				for (int j = 0; j < avatar.bufflist.Count; j++)
				{
					if (avatar.bufflist[j][2] == buffid)
					{
						flag = true;
						avatar.bufflist[j][1] = list3[1];
						num2 = j;
						break;
					}
				}
			}
			else if (buffType == 2)
			{
				flag = true;
				avatar.bufflist.Add(list3);
			}
			if (!flag)
			{
				avatar.bufflist.Add(list3);
				num2 = avatar.bufflist.IndexOf(list3);
				avatar.buffmag.PlayBuffAdd(jsonData.instance.BuffJsonData[buffid.ToString()]["skillEffect"].str);
			}
			foreach (JSONObject jsonobject in jsonData.instance.BuffJsonData[string.Concat(buffid)]["seid"].list)
			{
				if ((int)jsonobject.n == 7)
				{
					int num3 = (int)jsonData.instance.BuffSeidJsonData[7][string.Concat(buffid)]["value1"].n;
					if (avatar.bufflist[num2][1] > num3)
					{
						avatar.bufflist[num2][1] = num3;
					}
				}
				else if (jsonobject.I == 188)
				{
					int value = BuffSeidJsonData188.DataDict[avatar.bufflist[num2][2]].value1;
					if (value != 1)
					{
						if (value == 2)
						{
							if (avatar.bufflist[num2][1] > avatar.GetBaseDunSu())
							{
								avatar.bufflist[num2][1] = avatar.GetBaseDunSu();
							}
						}
					}
					else if (avatar.bufflist[num2][1] > avatar.GetBaseShenShi())
					{
						avatar.bufflist[num2][1] = avatar.GetBaseShenShi();
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
			foreach (JSONObject jsonobject2 in jsonData.instance.BuffJsonData[string.Concat(buffid)]["seid"].list)
			{
				if ((int)jsonobject2.n == 48)
				{
					jsonData.instance.Buff[avatar.bufflist[num2][2]].ListRealizeSeid48(48, avatar, avatar.bufflist[num2], null);
				}
				if ((int)jsonobject2.n == 22)
				{
					jsonData.instance.Buff[avatar.bufflist[num2][2]].ListRealizeSeid22(22, avatar, avatar.bufflist[num2], null);
				}
			}
			List<int> flag2 = new List<int>();
			if ((int)jsonData.instance.BuffJsonData[buffid.ToString()]["trigger"].n == 12)
			{
				avatar.spell.onBuffTick(num2, flag2, 0);
			}
			avatar.spell.onBuffTickByType(36, list);
			try
			{
				SteamChengJiu.ints.FightBuffOnceSetStat(avatar, avatar.bufflist[num2][2], avatar.bufflist[num2][1]);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.HuaShen && avatar.isPlayer() && BigTuPoResultIMag.Inst == null)
			{
				int buffSum = avatar.buffmag.GetBuffSum(3133);
				int buffSum2 = avatar.buffmag.GetBuffSum(3132);
				if (buffSum >= 100)
				{
					GlobalValue.SetTalk(1, 2, "Spell.addDBuff 化神检测");
					ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowSuccess(4);
				}
				else if (buffSum2 >= 100)
				{
					GlobalValue.SetTalk(1, 0, "Spell.addDBuff 化神检测");
					ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowFail(4, 0);
				}
			}
			UIFightPanel.Inst.RefreshCD();
			Event.fireOut("UpdataBuff", Array.Empty<object>());
			return list3;
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x0024CC10 File Offset: 0x0024AE10
		public void removeBuff(List<int> buffid)
		{
			Avatar avatar = (Avatar)this.entity;
			List<int> list = new List<int>();
			this.getBuffFlag(buffid[2], list);
			avatar.spell.onBuffTickByType(32, list);
			jsonData.instance.Buff[buffid[2]].onDetach(avatar, buffid);
			buffid[1] = 0;
			if (avatar.isPlayer())
			{
				RoundManager.instance.curRemoveBuffId = buffid[2];
				RoundManager.EventFightTalk("RemoveBuff", null, null);
			}
			avatar.bufflist.Remove(buffid);
			using (List<JSONObject>.Enumerator enumerator = jsonData.instance.BuffJsonData[string.Concat(buffid[2])]["seid"].list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((int)enumerator.Current.n == 20)
					{
						foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[20][string.Concat(buffid[2])]["value1"].list)
						{
							foreach (List<int> buffid2 in avatar.buffmag.getBuffByID((int)jsonobject.n))
							{
								avatar.spell.removeBuff(buffid2);
							}
						}
					}
				}
			}
			Queue<UnityAction> queue = new Queue<UnityAction>();
			UnityAction item = delegate()
			{
				avatar.buffmag.PlayBuffRemove(jsonData.instance.BuffJsonData[buffid[2].ToString()]["skillEffect"].str);
				Event.fireOut("PlayBuffAnimaiton", new object[]
				{
					buffid
				});
				YSFuncList.Ints.Continue();
			};
			queue.Enqueue(item);
			YSFuncList.Ints.AddFunc(queue);
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x0024CE5C File Offset: 0x0024B05C
		public bool HasBuff(int buffID)
		{
			Avatar avatar = (Avatar)this.entity;
			for (int i = 0; i < avatar.bufflist.Count; i++)
			{
				if (avatar.bufflist[i][2] == buffID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x0024CEA3 File Offset: 0x0024B0A3
		public void removeAllBuff()
		{
			((Avatar)this.entity).bufflist.Clear();
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x0024CEBC File Offset: 0x0024B0BC
		public void BuffSeidRealizeCheck(int index, BuffLoopData buffLoopData)
		{
			Avatar avatar = (Avatar)this.entity;
			if (avatar.buffmag.HasBuffSeid(219))
			{
				int num = avatar.bufflist[index][2];
				foreach (List<int> list in avatar.buffmag.getBuffBySeid(219))
				{
					JSONObject seidJson = jsonData.instance.Buff[list[2]].getSeidJson(219);
					int i = seidJson["value1"].I;
					int i2 = seidJson["value2"].I;
					int i3 = seidJson["value3"].I;
					JSONObject json = seidJson["value4"];
					if (i3 == num)
					{
						float buffSum = (float)avatar.buffmag.GetBuffSum(i);
						float num2 = (float)avatar.HP_Max * ((float)i2 / 100f);
						if (buffSum < num2)
						{
							buffLoopData.SetSeid(Tools.JsonListToList(json));
						}
					}
				}
			}
			if (avatar.buffmag.HasBuffSeid(136))
			{
				int num3 = avatar.bufflist[index][2];
				foreach (List<int> list2 in avatar.buffmag.getBuffBySeid(136))
				{
					JSONObject seidJson2 = jsonData.instance.Buff[list2[2]].getSeidJson(136);
					if (seidJson2["value1"].I == num3)
					{
						buffLoopData.SetSeid(Tools.JsonListToList(seidJson2["value2"]));
					}
				}
			}
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x0024D0A4 File Offset: 0x0024B2A4
		public void onBuffTick(int index, List<int> flag = null, int type = 0)
		{
			Avatar avatar = (Avatar)this.entity;
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
			this.BuffSeidRealizeCheck(index, buffLoopData);
			List<int> flagClone = new List<int>();
			if (flag != null)
			{
				flag.ForEach(delegate(int i)
				{
					flagClone.Add(i);
				});
			}
			if (avatar.bufflist[index][2] == 1)
			{
				avatar.OtherAvatar.spell.onBuffTickByType(41);
			}
			jsonData.instance.Buff[avatar.bufflist[index][2]].onLoopTrigger(avatar, buffdata, flag, buffLoopData);
			if ((int)jsonData.instance.BuffJsonData[string.Concat(buffdata[2])]["removeTrigger"].n == 1)
			{
				List<int> buffdata2 = buffdata;
				buffdata2[1] = buffdata2[1] - 1;
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
			UnityAction item = delegate()
			{
				avatar.buffmag.PlayBuffTarget(jsonData.instance.BuffJsonData[buffdata[2].ToString()]["skillEffect"].str);
				Event.fireOut("PlayBuffAnimaiton", new object[]
				{
					buffdata
				});
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
				Debug.LogError(ex);
			}
			Event.fireOut("UpdataBuff", Array.Empty<object>());
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x0024D418 File Offset: 0x0024B618
		public void onBuffTickById(int id, List<int> flag = null, int type = 0)
		{
			Avatar avatar = (Avatar)this.entity;
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
				this.BuffSeidRealizeCheck(index, buffLoopData);
			}
			catch (Exception ex)
			{
				Debug.Log(ex);
			}
			List<int> flagClone = new List<int>();
			if (flag != null)
			{
				flag.ForEach(delegate(int i)
				{
					flagClone.Add(i);
				});
			}
			if (buffdata[2] == 1)
			{
				avatar.OtherAvatar.spell.onBuffTickByType(41);
			}
			jsonData.instance.Buff[buffdata[2]].onLoopTrigger(avatar, buffdata, flag, buffLoopData);
			if ((int)jsonData.instance.BuffJsonData[string.Concat(buffdata[2])]["removeTrigger"].n == 1)
			{
				List<int> buffdata2 = buffdata;
				buffdata2[1] = buffdata2[1] - 1;
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
			UnityAction item = delegate()
			{
				avatar.buffmag.PlayBuffTarget(jsonData.instance.BuffJsonData[buffdata[2].ToString()]["skillEffect"].str);
				Event.fireOut("PlayBuffAnimaiton", new object[]
				{
					buffdata
				});
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
				Debug.LogError(ex2);
			}
			Event.fireOut("UpdataBuff", Array.Empty<object>());
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x0024D774 File Offset: 0x0024B974
		public void AutoRemoveBuff()
		{
			Avatar avatar = (Avatar)this.entity;
			bool flag = false;
			List<List<int>> list = new List<List<int>>();
			foreach (List<int> list2 in avatar.bufflist)
			{
				if (list2[1] <= 0)
				{
					list.Add(list2);
				}
			}
			foreach (List<int> list3 in list)
			{
				if (list3[1] <= 0)
				{
					this.removeBuff(list3);
				}
				flag = true;
			}
			if (flag)
			{
				Event.fireOut("UpdataBuff", Array.Empty<object>());
			}
		}

		// Token: 0x06005881 RID: 22657 RVA: 0x0024D844 File Offset: 0x0024BA44
		public void onRemoveBuffByType(int type, int removeCount = 1)
		{
			Avatar avatar = (Avatar)this.entity;
			List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(145);
			int count = avatar.bufflist.Count;
			for (int i = 0; i < count; i++)
			{
				int num = (int)jsonData.instance.BuffJsonData[string.Concat(avatar.bufflist[i][2])]["removeTrigger"].n;
				if (type == num)
				{
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
							this.getBuffFlag(avatar.bufflist[i][2], list);
							avatar.spell.onBuffTickByType(32, list);
						}
						List<int> list2 = avatar.bufflist[i];
						list2[1] = list2[1] - removeCount;
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
						List<int> list2 = avatar.bufflist[i];
						list2[1] = list2[1] / 2;
					}
				}
			}
			Event.fireOut("UpdataBuff", Array.Empty<object>());
		}

		// Token: 0x06005882 RID: 22658 RVA: 0x0024DA90 File Offset: 0x0024BC90
		public void ONBuffTick(int buffindex, int type)
		{
			Avatar avatar = (Avatar)this.entity;
			if (jsonData.instance.BuffJsonData[string.Concat(avatar.bufflist[buffindex][2])]["trigger"].I == type)
			{
				avatar.spell.onBuffTick(buffindex, null, 0);
			}
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x0024DAF4 File Offset: 0x0024BCF4
		public void onBuffTickByType(int type)
		{
			Avatar avatar = (Avatar)this.entity;
			int count = avatar.bufflist.Count;
			for (int i = 0; i < count; i++)
			{
				int num = avatar.bufflist[i][2];
				if (jsonData.instance.BuffJsonData.ContainsKey(num.ToString()))
				{
					if (jsonData.instance.BuffJsonData[num.ToString()]["trigger"].I == type)
					{
						avatar.spell.onBuffTick(i, new List<int>(), 0);
					}
				}
				else
				{
					Debug.LogError(string.Format("Spell.onBuffTickByType({0})，BuffJsonData不存在buffid{1}", type, num));
				}
			}
			Event.fireOut("UpdataBuff", Array.Empty<object>());
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x0024DBBC File Offset: 0x0024BDBC
		public void onBuffTickByType(int type, List<int> flag)
		{
			try
			{
				Spell.<>c__DisplayClass23_0 CS$<>8__locals1 = new Spell.<>c__DisplayClass23_0();
				CS$<>8__locals1.avatar = (Avatar)this.entity;
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				int count = CS$<>8__locals1.avatar.bufflist.Count;
				List<List<int>> list = new List<List<int>>();
				int buff2;
				int buff;
				for (buff = 0; buff < count; buff = buff2 + 1)
				{
					if (count != CS$<>8__locals1.avatar.bufflist.Count)
					{
						buff = 0;
						count = CS$<>8__locals1.avatar.bufflist.Count;
					}
					if (list.FindAll((List<int> a) => a == CS$<>8__locals1.avatar.bufflist[buff]).Count <= 0)
					{
						list.Add(CS$<>8__locals1.avatar.bufflist[buff]);
						if (_BuffJsonData.DataDict[CS$<>8__locals1.avatar.bufflist[buff][2]].trigger == type)
						{
							if (dictionary.ContainsKey(CS$<>8__locals1.avatar.bufflist[buff][2]))
							{
								if (dictionary[CS$<>8__locals1.avatar.bufflist[buff][2]] != buff)
								{
									goto IL_77C;
								}
							}
							else
							{
								dictionary.Add(CS$<>8__locals1.avatar.bufflist[buff][2], buff);
							}
							int key = CS$<>8__locals1.avatar.bufflist[buff][2];
							CS$<>8__locals1.avatar.spell.onBuffTick(buff, flag, type);
							if (CS$<>8__locals1.avatar.isPlayer() && CS$<>8__locals1.avatar.fightTemp.LianQiBuffEquipDictionary.Keys.Count > 0 && CS$<>8__locals1.avatar.fightTemp.LianQiBuffEquipDictionary.ContainsKey(key))
							{
								List<JSONObject> list2 = CS$<>8__locals1.avatar.fightTemp.LianQiBuffEquipDictionary[key].list;
								for (int i = 0; i < list2.Count; i++)
								{
									if (list2[i]["id"].I == 62)
									{
										if ((float)CS$<>8__locals1.avatar.HP / (float)CS$<>8__locals1.avatar.HP_Max * 100f > list2[i]["value1"][0].n)
										{
											break;
										}
									}
									else
									{
										if (list2[i]["id"].I == 1)
										{
											for (int j = 0; j < list2[i]["value1"].Count; j++)
											{
												CS$<>8__locals1.avatar.recvDamage(CS$<>8__locals1.avatar, CS$<>8__locals1.avatar, 18005, -list2[i]["value2"][j].I, 0);
											}
										}
										if (list2[i]["id"].I == 5)
										{
											for (int k = 0; k < list2[i]["value1"].Count; k++)
											{
												CS$<>8__locals1.avatar.spell.addDBuff(list2[i]["value1"][k].I, list2[i]["value2"][k].I);
											}
										}
										if (list2[i]["id"].I == 17)
										{
											for (int l = 0; l < list2[i]["value1"].Count; l++)
											{
												CS$<>8__locals1.avatar.OtherAvatar.spell.addDBuff(list2[i]["value1"][l].I, list2[i]["value2"][l].I);
											}
										}
									}
								}
							}
							if (!CS$<>8__locals1.avatar.isPlayer() && RoundManager.instance.newNpcFightManager != null && RoundManager.instance.newNpcFightManager.LianQiBuffEquipDictionary.Keys.Count > 0 && RoundManager.instance.newNpcFightManager.LianQiBuffEquipDictionary.ContainsKey(key))
							{
								List<JSONObject> list3 = RoundManager.instance.newNpcFightManager.LianQiBuffEquipDictionary[key].list;
								for (int m = 0; m < list3.Count; m++)
								{
									if (list3[m]["id"].I == 62)
									{
										if ((float)CS$<>8__locals1.avatar.HP / (float)CS$<>8__locals1.avatar.HP_Max * 100f > list3[m]["value1"][0].n)
										{
											break;
										}
									}
									else
									{
										if (list3[m]["id"].I == 1)
										{
											for (int n = 0; n < list3[m]["value1"].Count; n++)
											{
												CS$<>8__locals1.avatar.recvDamage(CS$<>8__locals1.avatar, CS$<>8__locals1.avatar, 18005, -list3[m]["value2"][n].I, 0);
											}
										}
										if (list3[m]["id"].I == 5)
										{
											for (int num = 0; num < list3[m]["value1"].Count; num++)
											{
												CS$<>8__locals1.avatar.spell.addDBuff(list3[m]["value1"][num].I, list3[m]["value2"][num].I);
											}
										}
										if (list3[m]["id"].I == 17)
										{
											for (int num2 = 0; num2 < list3[m]["value1"].Count; num2++)
											{
												CS$<>8__locals1.avatar.OtherAvatar.spell.addDBuff(list3[m]["value1"][num2].I, list3[m]["value2"][num2].I);
											}
										}
									}
								}
							}
						}
					}
					IL_77C:
					buff2 = buff;
				}
				Event.fireOut("UpdataBuff", Array.Empty<object>());
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("{0}", arg));
			}
		}

		// Token: 0x04005202 RID: 20994
		public Entity entity;

		// Token: 0x04005203 RID: 20995
		public Dictionary<int, int> UseSkillLateDict = new Dictionary<int, int>();

		// Token: 0x02001623 RID: 5667
		public enum buffList
		{
			// Token: 0x0400715D RID: 29021
			NUM,
			// Token: 0x0400715E RID: 29022
			ROUND,
			// Token: 0x0400715F RID: 29023
			ID
		}
	}
}
