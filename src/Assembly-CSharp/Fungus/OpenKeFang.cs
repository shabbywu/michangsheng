using System;
using GUIPackage;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F94 RID: 3988
	[CommandInfo("YSTools", "OpenKeFang", "打开客房界面", 0)]
	[AddComponentMenu("")]
	public class OpenKeFang : Command
	{
		// Token: 0x06006F82 RID: 28546 RVA: 0x002A7072 File Offset: 0x002A5272
		public override void OnEnter()
		{
			this.NewUI();
			this.Continue();
		}

		// Token: 0x06006F83 RID: 28547 RVA: 0x002A7080 File Offset: 0x002A5280
		public void NewUI()
		{
			UIBiGuanPanel.Inst.OpenBiGuan(this.BiGuanType);
		}

		// Token: 0x06006F84 RID: 28548 RVA: 0x002A7094 File Offset: 0x002A5294
		public void OldUI()
		{
			XiuLian biguan = Singleton.biguan;
			if (biguan != null)
			{
				biguan.GetComponentInChildren<XiuLian>().open();
				biguan.transform.Find("Win/Layer/Content3/BiGuan2").GetComponentInChildren<UIBiGuan>().biguanType = this.BiGuanType;
			}
		}

		// Token: 0x06006F85 RID: 28549 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F86 RID: 28550 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C12 RID: 23570
		[Tooltip("闭关界面的闭关速度")]
		[SerializeField]
		protected int BiGuanType = 1;
	}
}
