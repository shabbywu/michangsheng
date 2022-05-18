using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008C5 RID: 2245
	public class FPObject : PlayerBehaviour
	{
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x00029F10 File Offset: 0x00028110
		// (set) Token: 0x060039C5 RID: 14789 RVA: 0x00029F18 File Offset: 0x00028118
		public bool IsEnabled { get; private set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060039C6 RID: 14790 RVA: 0x00029F21 File Offset: 0x00028121
		public string ObjectName
		{
			get
			{
				return this.m_ObjectName;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x00029F29 File Offset: 0x00028129
		// (set) Token: 0x060039C8 RID: 14792 RVA: 0x00029F31 File Offset: 0x00028131
		public SavableItem CorrespondingItem { get; private set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x00029F3A File Offset: 0x0002813A
		public int TargetFOV
		{
			get
			{
				return this.m_TargetFOV;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060039CA RID: 14794 RVA: 0x00029F42 File Offset: 0x00028142
		// (set) Token: 0x060039CB RID: 14795 RVA: 0x00029F4A File Offset: 0x0002814A
		public float LastDrawTime { get; private set; }

		// Token: 0x060039CC RID: 14796 RVA: 0x00029F53 File Offset: 0x00028153
		protected virtual void Awake()
		{
			base.gameObject.SetActive(true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x00029F6D File Offset: 0x0002816D
		public virtual void On_Draw(SavableItem correspondingItem)
		{
			this.IsEnabled = true;
			this.CorrespondingItem = correspondingItem;
			this.LastDrawTime = Time.time;
			this.m_Durability = correspondingItem.GetPropertyValue("Durability");
			this.Draw.Send();
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x00029FA4 File Offset: 0x000281A4
		public virtual void On_Holster()
		{
			this.IsEnabled = false;
			this.CorrespondingItem = null;
			this.Holster.Send();
		}

		// Token: 0x040033F7 RID: 13303
		public Message Draw = new Message();

		// Token: 0x040033F8 RID: 13304
		public Message Holster = new Message();

		// Token: 0x040033FC RID: 13308
		[Header("General")]
		[SerializeField]
		private string m_ObjectName = "Unnamed";

		// Token: 0x040033FD RID: 13309
		[SerializeField]
		[Range(15f, 100f)]
		private int m_TargetFOV = 45;

		// Token: 0x040033FE RID: 13310
		protected ItemProperty.Value m_Durability;
	}
}
