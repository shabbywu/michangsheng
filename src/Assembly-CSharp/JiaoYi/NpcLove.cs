using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JiaoYi
{
	// Token: 0x02000A94 RID: 2708
	public class NpcLove : MonoBehaviour
	{
		// Token: 0x0600456F RID: 17775 RVA: 0x001DBAEC File Offset: 0x001D9CEC
		public void InitNpcLove(int npcId)
		{
			if (!jsonData.instance.AvatarBackpackJsonData[string.Concat(npcId)].HasField("XinQuType"))
			{
				jsonData.instance.MonstarCreatInterstingType(npcId);
			}
			List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[string.Concat(npcId)]["XinQuType"].list;
			if (list.Count > 0)
			{
				using (List<JSONObject>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JSONObject jsonobject = enumerator.Current;
						string str = (string)jsonData.instance.AllItemLeiXin[string.Concat(jsonobject["type"].I)]["name"];
						string str2 = jsonobject["percent"].I.ToString();
						Text component = this.Temp.Inst(this.Parent).GetComponent<Text>();
						component.SetText(str + "(+" + str2 + "%)");
						component.gameObject.SetActive(true);
					}
					return;
				}
			}
			this.Temp.Inst(this.Parent).SetActive(true);
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x000319A3 File Offset: 0x0002FBA3
		public void ShowLove()
		{
			if (!this._isInit)
			{
				this.InitNpcLove(JiaoYiUIMag.Inst.NpcId);
				this._isInit = true;
			}
			this.Panel.SetActive(true);
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x000319D0 File Offset: 0x0002FBD0
		public void HideLove()
		{
			this.Panel.SetActive(false);
		}

		// Token: 0x04003D9B RID: 15771
		public GameObject Temp;

		// Token: 0x04003D9C RID: 15772
		public Transform Parent;

		// Token: 0x04003D9D RID: 15773
		public GameObject Panel;

		// Token: 0x04003D9E RID: 15774
		private bool _isInit;
	}
}
