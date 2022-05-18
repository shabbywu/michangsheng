using System;
using GUIPackage;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001447 RID: 5191
	[CommandInfo("YSTools", "OpenKeFang", "打开客房界面", 0)]
	[AddComponentMenu("")]
	public class OpenKeFang : Command
	{
		// Token: 0x06007D69 RID: 32105 RVA: 0x00054D2C File Offset: 0x00052F2C
		public override void OnEnter()
		{
			this.NewUI();
			this.Continue();
		}

		// Token: 0x06007D6A RID: 32106 RVA: 0x00054D3A File Offset: 0x00052F3A
		public void NewUI()
		{
			UIBiGuanPanel.Inst.OpenBiGuan(this.BiGuanType);
		}

		// Token: 0x06007D6B RID: 32107 RVA: 0x002C671C File Offset: 0x002C491C
		public void OldUI()
		{
			XiuLian biguan = Singleton.biguan;
			if (biguan != null)
			{
				biguan.GetComponentInChildren<XiuLian>().open();
				biguan.transform.Find("Win/Layer/Content3/BiGuan2").GetComponentInChildren<UIBiGuan>().biguanType = this.BiGuanType;
			}
		}

		// Token: 0x06007D6C RID: 32108 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D6D RID: 32109 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AE1 RID: 27361
		[Tooltip("闭关界面的闭关速度")]
		[SerializeField]
		protected int BiGuanType = 1;
	}
}
