using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200049C RID: 1180
public class MainUISetName : MonoBehaviour
{
	// Token: 0x06001F5C RID: 8028 RVA: 0x00019E4E File Offset: 0x0001804E
	private void Awake()
	{
		this.RandomName();
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void Init()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x0010E41C File Offset: 0x0010C61C
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

	// Token: 0x06001F5F RID: 8031 RVA: 0x0010E490 File Offset: 0x0010C690
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

	// Token: 0x06001F60 RID: 8032 RVA: 0x00019E56 File Offset: 0x00018056
	public void ReturnMethod()
	{
		base.gameObject.SetActive(false);
		MainUIMag.inst.selectAvatarPanel.Init();
		MainUIMag.inst.createAvatarPanel.Close();
	}

	// Token: 0x04001ADC RID: 6876
	public InputField xinInputField;

	// Token: 0x04001ADD RID: 6877
	public InputField minInputField;
}
