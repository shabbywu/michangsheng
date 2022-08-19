using System;
using Fungus;
using UnityEngine;

// Token: 0x020001E0 RID: 480
[CommandInfo("YSPlayer", "设置相机追随玩家", "设置相机追随玩家", 0)]
[AddComponentMenu("")]
public class CmdSetCameraFollowPlayer : Command
{
	// Token: 0x06001432 RID: 5170 RVA: 0x0008292C File Offset: 0x00080B2C
	public override void OnEnter()
	{
		CamaraFollow camaraFollow = Object.FindObjectOfType<CamaraFollow>();
		if (camaraFollow != null)
		{
			camaraFollow.follwPlayer = this.IsFollow;
		}
		else
		{
			Debug.LogError("设置镜头追随出错，没有找到CamaraFollow对象");
		}
		this.Continue();
	}

	// Token: 0x04000EFD RID: 3837
	[Tooltip("是否追随")]
	[SerializeField]
	protected bool IsFollow = true;
}
