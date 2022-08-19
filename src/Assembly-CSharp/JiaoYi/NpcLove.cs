using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JiaoYi
{
	// Token: 0x02000732 RID: 1842
	public class NpcLove : MonoBehaviour
	{
		// Token: 0x06003AB1 RID: 15025 RVA: 0x00193E28 File Offset: 0x00192028
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

		// Token: 0x06003AB2 RID: 15026 RVA: 0x00193F90 File Offset: 0x00192190
		public void ShowLove()
		{
			if (!this._isInit)
			{
				this.InitNpcLove(JiaoYiUIMag.Inst.NpcId);
				this._isInit = true;
			}
			this.Panel.SetActive(true);
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x00193FBD File Offset: 0x001921BD
		public void HideLove()
		{
			this.Panel.SetActive(false);
		}

		// Token: 0x040032E2 RID: 13026
		public GameObject Temp;

		// Token: 0x040032E3 RID: 13027
		public Transform Parent;

		// Token: 0x040032E4 RID: 13028
		public GameObject Panel;

		// Token: 0x040032E5 RID: 13029
		private bool _isInit;
	}
}
