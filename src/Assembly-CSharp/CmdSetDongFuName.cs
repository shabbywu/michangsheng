using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "让玩家设置洞府名字", "让玩家设置洞府名字", 0)]
[AddComponentMenu("")]
public class CmdSetDongFuName : Command
{
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int dongFuID;

	public override void OnEnter()
	{
		OpenInputBox();
		Continue();
	}

	public void OpenInputBox()
	{
		UInputBox.Show("为洞府命名", delegate(string s)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				OpenInputBox();
			}
			else if (s.Length > 6)
			{
				UIPopTip.Inst.Pop("名字太长了");
				OpenInputBox();
			}
			else
			{
				DongFuManager.SetDongFuName(dongFuID, s);
			}
		});
	}
}
