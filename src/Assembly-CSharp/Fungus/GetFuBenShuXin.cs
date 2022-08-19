using System;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F12 RID: 3858
	[CommandInfo("YSFuBen", "GetFuBenShuXin", "获取随机副本属性", 0)]
	[AddComponentMenu("")]
	public class GetFuBenShuXin : Command
	{
		// Token: 0x06006D79 RID: 28025 RVA: 0x002A34D8 File Offset: 0x002A16D8
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			int nowRandomFuBenID = Tools.instance.getPlayer().NowRandomFuBenID;
			JToken jtoken = player.RandomFuBenList[nowRandomFuBenID.ToString()];
			if (this.ShuXin != null)
			{
				this.ShuXin.Value = (int)jtoken["ShuXin"];
			}
			if (this.LeiXin != null)
			{
				this.LeiXin.Value = (int)jtoken["type"];
			}
			this.Continue();
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x002A356C File Offset: 0x002A176C
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06006D7B RID: 28027 RVA: 0x002A359C File Offset: 0x002A179C
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D7D RID: 28029 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B2D RID: 23341
		[Tooltip("获取副本属性")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public IntegerVariable ShuXin;

		// Token: 0x04005B2E RID: 23342
		[Tooltip("获取副本类型")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public IntegerVariable LeiXin;
	}
}
