using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "设置相机追随玩家", "设置相机追随玩家", 0)]
[AddComponentMenu("")]
public class CmdSetCameraFollowPlayer : Command
{
	[Tooltip("是否追随")]
	[SerializeField]
	protected bool IsFollow = true;

	public override void OnEnter()
	{
		CamaraFollow camaraFollow = Object.FindObjectOfType<CamaraFollow>();
		if ((Object)(object)camaraFollow != (Object)null)
		{
			camaraFollow.follwPlayer = IsFollow;
		}
		else
		{
			Debug.LogError((object)"设置镜头追随出错，没有找到CamaraFollow对象");
		}
		Continue();
	}
}
