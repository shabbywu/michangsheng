using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000588 RID: 1416
public class CreateAvatarFinal : MonoBehaviour
{
	// Token: 0x060023E5 RID: 9189 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060023E6 RID: 9190 RVA: 0x00125FB4 File Offset: 0x001241B4
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
				createAvatarChoice createAvatarChoice = CreateAvatarMag.inst.tianfuUI.getSelectChoice.Find((createAvatarChoice aa) => (int)_Temp["id"].n == aa.id);
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

	// Token: 0x04001EEC RID: 7916
	public GameObject zizhiLabel;

	// Token: 0x04001EED RID: 7917
	public UILabel ShenpinText;
}
