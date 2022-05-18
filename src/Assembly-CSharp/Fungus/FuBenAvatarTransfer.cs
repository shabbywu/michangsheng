using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013C2 RID: 5058
	[CommandInfo("YSFuBen", "FuBenAvatarTransfer", "角色传送", 0)]
	[AddComponentMenu("")]
	public class FuBenAvatarTransfer : Command
	{
		// Token: 0x06007B4D RID: 31565 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007B4E RID: 31566 RVA: 0x002C3844 File Offset: 0x002C1A44
		public override void OnEnter()
		{
			Tools.instance.getPlayer().fubenContorl[this.ScenceName].NowIndex = this.MapID.Value;
			if (AllMapManage.instance != null && AllMapManage.instance.mapIndex.ContainsKey(this.MapID.Value))
			{
				AllMapManage.instance.mapIndex[this.MapID.Value].AvatarMoveToThis();
				WASDMove.Inst.IsMoved = true;
				this.wait();
				base.Invoke("removeWait", 0.8f);
			}
			this.Continue();
		}

		// Token: 0x06007B4F RID: 31567 RVA: 0x002C37E4 File Offset: 0x002C19E4
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06007B50 RID: 31568 RVA: 0x002C3814 File Offset: 0x002C1A14
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06007B51 RID: 31569 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B52 RID: 31570 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x040069ED RID: 27117
		[Tooltip("场景名称")]
		[SerializeField]
		protected string ScenceName;

		// Token: 0x040069EE RID: 27118
		[Tooltip("传送到的地点的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MapID;
	}
}
