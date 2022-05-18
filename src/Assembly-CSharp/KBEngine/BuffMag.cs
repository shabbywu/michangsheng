using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02001012 RID: 4114
	public class BuffMag
	{
		// Token: 0x06006257 RID: 25175 RVA: 0x000441F2 File Offset: 0x000423F2
		public BuffMag(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x0027418C File Offset: 0x0027238C
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

		// Token: 0x06006259 RID: 25177 RVA: 0x00274250 File Offset: 0x00272450
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
					return;
				}
				transform.transform.GetComponentInChildren<Animator>().Play(type);
			}
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x00044206 File Offset: 0x00042406
		public void PlayBuffAdd(string Buff)
		{
			if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			this._playEffect(Buff, "Start");
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x0004422E File Offset: 0x0004242E
		public void PlayBuffTarget(string Buff)
		{
			if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			this._playEffect(Buff, "Target");
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x00044256 File Offset: 0x00042456
		public void PlayBuffRemove(string Buff)
		{
			if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			this._playEffect(Buff, "Remove");
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x002742D0 File Offset: 0x002724D0
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

		// Token: 0x0600625E RID: 25182 RVA: 0x00274334 File Offset: 0x00272534
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

		// Token: 0x0600625F RID: 25183 RVA: 0x0004427E File Offset: 0x0004247E
		public bool HasBuffNumGraterY(int buffID, int num)
		{
			return this.GetBuffHasYTime(buffID, num) > 0;
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x00274370 File Offset: 0x00272570
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

		// Token: 0x06006261 RID: 25185 RVA: 0x0027439C File Offset: 0x0027259C
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

		// Token: 0x06006262 RID: 25186 RVA: 0x002743E0 File Offset: 0x002725E0
		public List<List<int>> GetAllBuffByType(int type)
		{
			return this.entity.bufflist.FindAll(delegate(List<int> aa)
			{
				int num = aa[2];
				return jsonData.instance.BuffJsonData[num.ToString()]["bufftype"].I == type;
			});
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x0004428E File Offset: 0x0004248E
		public int getBuffTypeNum(int type)
		{
			return this.getAllBuffByType(type).Count;
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x00274418 File Offset: 0x00272618
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

		// Token: 0x06006265 RID: 25189 RVA: 0x00274450 File Offset: 0x00272650
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

		// Token: 0x06006266 RID: 25190 RVA: 0x002744DC File Offset: 0x002726DC
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

		// Token: 0x06006267 RID: 25191 RVA: 0x002745A4 File Offset: 0x002727A4
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

		// Token: 0x06006268 RID: 25192 RVA: 0x00274610 File Offset: 0x00272810
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

		// Token: 0x06006269 RID: 25193 RVA: 0x00274674 File Offset: 0x00272874
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

		// Token: 0x0600626A RID: 25194 RVA: 0x002746D0 File Offset: 0x002728D0
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

		// Token: 0x0600626B RID: 25195 RVA: 0x00274784 File Offset: 0x00272984
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

		// Token: 0x0600626C RID: 25196 RVA: 0x002747E4 File Offset: 0x002729E4
		public int getHuDun()
		{
			List<List<int>> buffByID = this.getBuffByID(5);
			if (buffByID.Count > 0)
			{
				return buffByID[0][2];
			}
			return 0;
		}

		// Token: 0x04005CDF RID: 23775
		public Avatar entity;
	}
}
