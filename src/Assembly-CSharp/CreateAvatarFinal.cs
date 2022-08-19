using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003EA RID: 1002
public class CreateAvatarFinal : MonoBehaviour
{
	// Token: 0x0600205E RID: 8286 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600205F RID: 8287 RVA: 0x000E3B0C File Offset: 0x000E1D0C
	private void Update()
	{
		int num = 0;
		foreach (object obj in this.zizhiLabel.transform)
		{
			((Transform)obj).GetComponent<UILabel>().text = Tools.getStr("xibieFight" + num) + CreateAvatarMag.inst.lingenUI.createLingen[num];
			num++;
		}
		this.ShenpinText.text = "";
		using (List<JSONObject>.Enumerator enumerator2 = jsonData.instance.CreateAvatarJsonData.list.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				JSONObject _Temp = enumerator2.Current;
				createAvatarChoice createAvatarChoice = CreateAvatarMag.inst.tianfuUI.getSelectChoice.Find((createAvatarChoice aa) => _Temp["id"].I == aa.id);
				if (createAvatarChoice != null && createAvatarChoice.id > 5)
				{
					UILabel shenpinText = this.ShenpinText;
					shenpinText.text = shenpinText.text + createAvatarChoice.descInfo + "\n\n";
				}
			}
		}
		UILabel shenpinText2 = this.ShenpinText;
		shenpinText2.text += "十六岁那年，你意外捡到了一把满是锈迹的钝剑，无意间唤醒了其中沉睡的老者灵魂。在老者的指引下，长生之途的大门缓缓为你敞开——";
	}

	// Token: 0x04001A48 RID: 6728
	public GameObject zizhiLabel;

	// Token: 0x04001A49 RID: 6729
	public UILabel ShenpinText;
}
