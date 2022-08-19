using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F0F RID: 3855
	[CommandInfo("YSFuBen", "FuBenAvatarTransfer", "角色传送", 0)]
	[AddComponentMenu("")]
	public class FuBenAvatarTransfer : Command
	{
		// Token: 0x06006D64 RID: 28004 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006D65 RID: 28005 RVA: 0x002A31D4 File Offset: 0x002A13D4
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

		// Token: 0x06006D66 RID: 28006 RVA: 0x002A327C File Offset: 0x002A147C
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06006D67 RID: 28007 RVA: 0x002A32AC File Offset: 0x002A14AC
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06006D68 RID: 28008 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D69 RID: 28009 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B26 RID: 23334
		[Tooltip("场景名称")]
		[SerializeField]
		protected string ScenceName;

		// Token: 0x04005B27 RID: 23335
		[Tooltip("传送到的地点的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MapID;
	}
}
