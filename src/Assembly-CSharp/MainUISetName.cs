using UnityEngine;
using UnityEngine.UI;

public class MainUISetName : MonoBehaviour
{
	public InputField xinInputField;

	public InputField minInputField;

	private void Awake()
	{
		RandomName();
	}

	public void Init()
	{
		((Component)this).gameObject.SetActive(true);
	}

	public void RandomName()
	{
		bool flag = NpcJieSuanManager.inst.getRandomInt(1, 100) > 50;
		string text;
		string text2;
		string input;
		do
		{
			text = jsonData.instance.RandomFirstName();
			text2 = ((!flag) ? jsonData.instance.RandomWomenLastName() : jsonData.instance.RandomManLastName());
			input = text + text2;
		}
		while (!Tools.instance.CheckBadWord(input));
		xinInputField.text = text;
		minInputField.text = text2;
	}

	public void NextMethod()
	{
		string text = xinInputField.text + minInputField.text;
		if (text.Length > 10)
		{
			UIPopTip.Inst.Pop("名称字数过长");
			return;
		}
		if (string.IsNullOrWhiteSpace(text))
		{
			UIPopTip.Inst.Pop("没有填写名字");
			return;
		}
		if (!Tools.instance.CheckBadWord(text))
		{
			UIPopTip.Inst.Pop("名称不合法,请换个名称");
			return;
		}
		MainUIPlayerInfo.inst.firstName = xinInputField.text;
		MainUIPlayerInfo.inst.lastName = minInputField.text;
		MainUIPlayerInfo.inst.playerName = text;
		if (jsonData.instance.AvatarRandomJsonData.HasField("1"))
		{
			jsonData.instance.AvatarRandomJsonData["1"].SetField("Name", text);
		}
		((Component)this).gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.setFacePanel.Init();
		MainUIMag.inst.createAvatarPanel.facePanel.SetActive(true);
	}

	public void ReturnMethod()
	{
		((Component)this).gameObject.SetActive(false);
		MainUIMag.inst.selectAvatarPanel.Init();
		MainUIMag.inst.createAvatarPanel.Close();
	}
}
