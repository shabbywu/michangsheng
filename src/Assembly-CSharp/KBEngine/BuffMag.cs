using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C71 RID: 3185
	public class BuffMag
	{
		// Token: 0x060057DF RID: 22495 RVA: 0x0024804F File Offset: 0x0024624F
		public BuffMag(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x00248064 File Offset: 0x00246264
		private void _PlayBuffEffect(string name)
		{
			Object @object = ResManager.inst.LoadBuffEffect(name);
			GameObject gameObject = (GameObject)this.entity.renderObj;
			if (gameObject.transform.Find("Buff_" + name) == null && @object != null)
			{
				GameObject gameObject2 = (GameObject)Object.Instantiate(@object);
				gameObject2.transform.parent = gameObject.transform;
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
				gameObject2.transform.name = "Buff_" + name;
			}
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x00248128 File Offset: 0x00246328
		private GameObject PlayTianJieBuffEffect(string name)
		{
			string text = "tianjie" + name;
			Object @object = ResManager.inst.LoadBuffEffect(text);
			if (((GameObject)this.entity.renderObj).transform.Find("Buff_" + text) == null && @object != null)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(@object);
				gameObject.transform.parent = TianJieEffectManager.Inst.PlayerTransform.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
				gameObject.transform.name = "Buff_" + text;
				return gameObject;
			}
			return null;
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x00248204 File Offset: 0x00246404
		private void _playEffect(string BuffName, string type)
		{
			GameObject gameObject = (GameObject)this.entity.renderObj;
			if (gameObject == null)
			{
				return;
			}
			this._PlayBuffEffect(BuffName);
			Transform transform = gameObject.transform.Find("Buff_" + BuffName);
			if (transform != null)
			{
				if (transform.transform.GetComponentInChildren<Animator>() == null)
				{
					Debug.Log("粒子特效,没有animator");
				}
				else
				{
					transform.transform.GetComponentInChildren<Animator>().Play(type);
				}
			}
			if (TianJieEffectManager.Inst != null)
			{
				GameObject gameObject2 = this.PlayTianJieBuffEffect(BuffName);
				if (gameObject2 != null)
				{
					Animator componentInChildren = gameObject2.transform.GetComponentInChildren<Animator>();
					if (componentInChildren == null)
					{
						Debug.Log("粒子特效,没有animator");
						return;
					}
					componentInChildren.Play(type);
				}
			}
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x002482C8 File Offset: 0x002464C8
		public void PlayBuffAdd(string Buff)
		{
			if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			this._playEffect(Buff, "Start");
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x002482F0 File Offset: 0x002464F0
		public void PlayBuffTarget(string Buff)
		{
			if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			this._playEffect(Buff, "Target");
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x00248318 File Offset: 0x00246518
		public void PlayBuffRemove(string Buff)
		{
			if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			this._playEffect(Buff, "Remove");
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x00248340 File Offset: 0x00246540
		public bool HasBuff(int buffID)
		{
			foreach (List<int> list in this.entity.bufflist)
			{
				if (buffID == list[2])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x002483A4 File Offset: 0x002465A4
		public bool checkHasBuff(int buffID, Avatar avatar)
		{
			for (int i = 0; i < avatar.bufflist.Count; i++)
			{
				if (buffID == avatar.bufflist[i][2])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x002483DF File Offset: 0x002465DF
		public bool HasBuffNumGraterY(int buffID, int num)
		{
			return this.GetBuffHasYTime(buffID, num) > 0;
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x002483F0 File Offset: 0x002465F0
		public int GetBuffHasYTime(int buffID, int Y)
		{
			if (!this.HasBuff(buffID))
			{
				return 0;
			}
			int buffSum = this.GetBuffSum(buffID);
			if (buffSum >= Y)
			{
				return buffSum / Y;
			}
			return 0;
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x0024841C File Offset: 0x0024661C
		public List<List<int>> getAllBuffByType(int type)
		{
			List<int> buffList = new List<int>();
			return this.entity.bufflist.FindAll(delegate(List<int> aa)
			{
				int item = aa[2];
				int i = jsonData.instance.BuffJsonData[item.ToString()]["bufftype"].I;
				if (!buffList.Contains(item))
				{
					buffList.Add(item);
					if (i == type)
					{
						return true;
					}
				}
				return false;
			});
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x00248460 File Offset: 0x00246660
		public List<List<int>> GetAllBuffByType(int type)
		{
			return this.entity.bufflist.FindAll(delegate(List<int> aa)
			{
				int num = aa[2];
				return jsonData.instance.BuffJsonData[num.ToString()]["bufftype"].I == type;
			});
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x00248496 File Offset: 0x00246696
		public int getBuffTypeNum(int type)
		{
			return this.getAllBuffByType(type).Count;
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x002484A4 File Offset: 0x002466A4
		public int GetBuffSum(int buffID)
		{
			List<List<int>> buffByID = this.getBuffByID(buffID);
			int sumRound = 0;
			buffByID.ForEach(delegate(List<int> _aa)
			{
				sumRound += _aa[1];
			});
			return sumRound;
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x002484DC File Offset: 0x002466DC
		public bool HasXTypeBuff(int BuffType)
		{
			foreach (List<int> list in this.entity.bufflist)
			{
				if (BuffType == (int)jsonData.instance.BuffJsonData[list[2].ToString()]["bufftype"].n)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x00248568 File Offset: 0x00246768
		public bool HasBuffSeid(int buffSeid)
		{
			foreach (List<int> list in this.entity.bufflist)
			{
				int num = list[2];
				foreach (JSONObject jsonobject in jsonData.instance.BuffJsonData[string.Concat(num)]["seid"].list)
				{
					if (buffSeid == jsonobject.I)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x00248630 File Offset: 0x00246830
		public List<List<int>> getBuffByID(int BuffID)
		{
			List<List<int>> list = new List<List<int>>();
			foreach (List<int> list2 in this.entity.bufflist)
			{
				if (list2[2] == BuffID)
				{
					list.Add(list2);
				}
			}
			return list;
		}

		// Token: 0x060057F1 RID: 22513 RVA: 0x0024869C File Offset: 0x0024689C
		public List<int> GetBuffById(int buffId)
		{
			foreach (List<int> list in this.entity.bufflist)
			{
				if (list[2] == buffId)
				{
					return list;
				}
			}
			return null;
		}

		// Token: 0x060057F2 RID: 22514 RVA: 0x00248700 File Offset: 0x00246900
		public int GetBuffRoundByID(int BuffID)
		{
			List<List<int>> buffByID = this.getBuffByID(BuffID);
			int num = 0;
			foreach (List<int> list in buffByID)
			{
				num += list[1];
			}
			return num;
		}

		// Token: 0x060057F3 RID: 22515 RVA: 0x0024875C File Offset: 0x0024695C
		public List<List<int>> getBuffBySeid(int BuffSeid)
		{
			List<List<int>> list = new List<List<int>>();
			foreach (List<int> list2 in this.entity.bufflist)
			{
				int key = list2[2];
				foreach (int num in _BuffJsonData.DataDict[key].seid)
				{
					if (BuffSeid == num)
					{
						list.Add(list2);
					}
				}
			}
			return list;
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x00248810 File Offset: 0x00246A10
		public int getBuffIndex(List<int> buffinfo)
		{
			int num = 0;
			using (List<List<int>>.Enumerator enumerator = this.entity.bufflist.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == buffinfo)
					{
						return num;
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x00248870 File Offset: 0x00246A70
		public int getHuDun()
		{
			List<List<int>> buffByID = this.getBuffByID(5);
			if (buffByID.Count > 0)
			{
				return buffByID[0][2];
			}
			return 0;
		}

		// Token: 0x060057F6 RID: 22518 RVA: 0x002488A0 File Offset: 0x00246AA0
		public int RemoveBuff(int BuffID)
		{
			List<List<int>> buffByID = this.getBuffByID(BuffID);
			int num = 0;
			foreach (List<int> list in buffByID)
			{
				num += list[1];
				this.entity.spell.removeBuff(list);
			}
			return num;
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x0024890C File Offset: 0x00246B0C
		public static int RemoveBuff(Avatar target, int BuffID)
		{
			return target.buffmag.RemoveBuff(BuffID);
		}

		// Token: 0x040051F3 RID: 20979
		public Avatar entity;
	}
}
