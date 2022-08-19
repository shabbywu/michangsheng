using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200032E RID: 814
public class MainUISetName : MonoBehaviour
{
	// Token: 0x06001C0A RID: 7178 RVA: 0x000C8EFB File Offset: 0x000C70FB
	private void Awake()
	{
		this.RandomName();
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public void Init()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x000C8F04 File Offset: 0x000C7104
	public void RandomName()
	{
		bool flag = NpcJieSuanManager.inst.getRandomInt(1, 100) > 50;
		string text;
		string text2;
		string input;
		do
		{
			text = jsonData.instance.RandomFirstName();
			if (flag)
			{
				text2 = jsonData.instance.RandomManLastName();
			}
			else
			{
				text2 = jsonData.instance.RandomWomenLastName();
			}
			input = text + text2;
		}
		while (!Tools.instance.CheckBadWord(input));
		this.xinInputField.text = text;
		this.minInputField.text = text2;
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x000C8F78 File Offset: 0x000C7178
	public void NextMethod()
	{
		string text = this.xinInputField.text + this.minInputField.text;
		if (text.Length > 10)
		{
			UIPopTip.Inst.Pop("名称字数过长", PopTipIconType.叹号);
			return;
		}
		if (string.IsNullOrWhiteSpace(text))
		{
			UIPopTip.Inst.Pop("没有填写名字", PopTipIconType.叹号);
			return;
		}
		if (!Tools.instance.CheckBadWord(text))
		{
			UIPopTip.Inst.Pop("名称不合法,请换个名称", PopTipIconType.叹号);
			return;
		}
		MainUIPlayerInfo.inst.firstName = this.xinInputField.text;
		MainUIPlayerInfo.inst.lastName = this.minInputField.text;
		MainUIPlayerInfo.inst.playerName = text;
		if (jsonData.instance.AvatarRandomJsonData.HasField("1"))
		{
			jsonData.instance.AvatarRandomJsonData["1"].SetField("Name", text);
		}
		base.gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.setFacePanel.Init();
		MainUIMag.inst.createAvatarPanel.facePanel.SetActive(true);
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x000C9092 File Offset: 0x000C7292
	public void ReturnMethod()
	{
		base.gameObject.SetActive(false);
		MainUIMag.inst.selectAvatarPanel.Init();
		MainUIMag.inst.createAvatarPanel.Close();
	}

	// Token: 0x040016A6 RID: 5798
	public InputField xinInputField;

	// Token: 0x040016A7 RID: 5799
	public InputField minInputField;
}
