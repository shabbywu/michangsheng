using UnityEngine;

public class CreateAvatarFinal : MonoBehaviour
{
	public GameObject zizhiLabel;

	public UILabel ShenpinText;

	private void Start()
	{
	}

	private void Update()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		foreach (Transform item in zizhiLabel.transform)
		{
			((Component)item).GetComponent<UILabel>().text = Tools.getStr("xibieFight" + num) + CreateAvatarMag.inst.lingenUI.createLingen[num];
			num++;
		}
		ShenpinText.text = "";
		foreach (JSONObject _Temp in jsonData.instance.CreateAvatarJsonData.list)
		{
			createAvatarChoice createAvatarChoice2 = CreateAvatarMag.inst.tianfuUI.getSelectChoice.Find((createAvatarChoice aa) => _Temp["id"].I == aa.id);
			if ((Object)(object)createAvatarChoice2 != (Object)null && createAvatarChoice2.id > 5)
			{
				UILabel shenpinText = ShenpinText;
				shenpinText.text = shenpinText.text + createAvatarChoice2.descInfo + "\n\n";
			}
		}
		ShenpinText.text += "十六岁那年，你意外捡到了一把满是锈迹的钝剑，无意间唤醒了其中沉睡的老者灵魂。在老者的指引下，长生之途的大门缓缓为你敞开——";
	}
}
