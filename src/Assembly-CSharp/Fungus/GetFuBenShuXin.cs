using System;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013C5 RID: 5061
	[CommandInfo("YSFuBen", "GetFuBenShuXin", "获取随机副本属性", 0)]
	[AddComponentMenu("")]
	public class GetFuBenShuXin : Command
	{
		// Token: 0x06007B62 RID: 31586 RVA: 0x002C3A20 File Offset: 0x002C1C20
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

		// Token: 0x06007B63 RID: 31587 RVA: 0x002C37E4 File Offset: 0x002C19E4
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06007B64 RID: 31588 RVA: 0x002C3814 File Offset: 0x002C1A14
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06007B65 RID: 31589 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B66 RID: 31590 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x040069F4 RID: 27124
		[Tooltip("获取副本属性")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public IntegerVariable ShuXin;

		// Token: 0x040069F5 RID: 27125
		[Tooltip("获取副本类型")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public IntegerVariable LeiXin;
	}
}
