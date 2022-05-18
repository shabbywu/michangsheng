using System;
using Fungus;
using UnityEngine;

// Token: 0x020002F3 RID: 755
[CommandInfo("YSPlayer", "设置相机追随玩家", "设置相机追随玩家", 0)]
[AddComponentMenu("")]
public class CmdSetCameraFollowPlayer : Command
{
	// Token: 0x060016D7 RID: 5847 RVA: 0x000CBAA4 File Offset: 0x000C9CA4
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

	// Token: 0x0400123B RID: 4667
	[Tooltip("是否追随")]
	[SerializeField]
	protected bool IsFollow = true;
}
