using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F1 RID: 1521
	public class FPObject : PlayerBehaviour
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x0015D812 File Offset: 0x0015BA12
		// (set) Token: 0x060030E7 RID: 12519 RVA: 0x0015D81A File Offset: 0x0015BA1A
		public bool IsEnabled { get; private set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x0015D823 File Offset: 0x0015BA23
		public string ObjectName
		{
			get
			{
				return this.m_ObjectName;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x0015D82B File Offset: 0x0015BA2B
		// (set) Token: 0x060030EA RID: 12522 RVA: 0x0015D833 File Offset: 0x0015BA33
		public SavableItem CorrespondingItem { get; private set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x0015D83C File Offset: 0x0015BA3C
		public int TargetFOV
		{
			get
			{
				return this.m_TargetFOV;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x0015D844 File Offset: 0x0015BA44
		// (set) Token: 0x060030ED RID: 12525 RVA: 0x0015D84C File Offset: 0x0015BA4C
		public float LastDrawTime { get; private set; }

		// Token: 0x060030EE RID: 12526 RVA: 0x0015D855 File Offset: 0x0015BA55
		protected virtual void Awake()
		{
			base.gameObject.SetActive(true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x0015D86F File Offset: 0x0015BA6F
		public virtual void On_Draw(SavableItem correspondingItem)
		{
			this.IsEnabled = true;
			this.CorrespondingItem = correspondingItem;
			this.LastDrawTime = Time.time;
			this.m_Durability = correspondingItem.GetPropertyValue("Durability");
			this.Draw.Send();
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x0015D8A6 File Offset: 0x0015BAA6
		public virtual void On_Holster()
		{
			this.IsEnabled = false;
			this.CorrespondingItem = null;
			this.Holster.Send();
		}

		// Token: 0x04002B21 RID: 11041
		public Message Draw = new Message();

		// Token: 0x04002B22 RID: 11042
		public Message Holster = new Message();

		// Token: 0x04002B26 RID: 11046
		[Header("General")]
		[SerializeField]
		private string m_ObjectName = "Unnamed";

		// Token: 0x04002B27 RID: 11047
		[SerializeField]
		[Range(15f, 100f)]
		private int m_TargetFOV = 45;

		// Token: 0x04002B28 RID: 11048
		protected ItemProperty.Value m_Durability;
	}
}
