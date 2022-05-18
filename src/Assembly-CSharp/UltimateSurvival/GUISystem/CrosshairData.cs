using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200094D RID: 2381
	[Serializable]
	public class CrosshairData
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06003CDA RID: 15578 RVA: 0x0002BDDA File Offset: 0x00029FDA
		public string ItemName
		{
			get
			{
				return this.m_ItemName;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x0002BDE2 File Offset: 0x00029FE2
		public bool HideWhenAiming
		{
			get
			{
				return this.m_HideWhenAiming;
			}
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000079B2 File Offset: 0x00005BB2
		public static implicit operator bool(CrosshairData cd)
		{
			return cd != null;
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x001B2104 File Offset: 0x001B0304
		public void Update(PlayerEventHandler player)
		{
			RaycastData raycastData = player.RaycastData.Get();
			bool flag = false;
			if (raycastData && raycastData.HitInfo.collider && raycastData.HitInfo.collider.GetComponent<HitBox>())
			{
				flag = true;
			}
			if (this.m_Type == CrosshairType.Dynamic && this.m_Crosshair)
			{
				this.m_Crosshair.SetColor(flag ? this.m_OnEntityColor : this.m_NormalColor);
				float num = this.m_IdleDistance;
				if (player.Crouch.Active)
				{
					num = this.m_CrouchDistance;
				}
				else if (player.Walk.Active)
				{
					num = this.m_WalkDistance;
				}
				else if (player.Run.Active)
				{
					num = this.m_RunDistance;
				}
				else if (player.Jump.Active)
				{
					num = this.m_JumpDistance;
				}
				this.m_Crosshair.SetDistance(Mathf.Lerp(this.m_Crosshair.Distance, num, Time.deltaTime * 10f));
				return;
			}
			if (this.m_Type == CrosshairType.Simple && this.m_Image)
			{
				this.m_Image.color = (flag ? this.m_OnEntityColor : this.m_NormalColor);
			}
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x001B2248 File Offset: 0x001B0448
		public void SetActive(bool active)
		{
			if (this.m_Type == CrosshairType.Dynamic && this.m_Crosshair)
			{
				this.m_Crosshair.SetActive(active);
				return;
			}
			if (this.m_Type == CrosshairType.Simple && this.m_Image)
			{
				this.m_Image.enabled = active;
				this.m_Image.sprite = this.m_Sprite;
				this.m_Image.rectTransform.sizeDelta = this.m_Size;
			}
		}

		// Token: 0x04003716 RID: 14102
		[SerializeField]
		private string m_ItemName;

		// Token: 0x04003717 RID: 14103
		[SerializeField]
		private bool m_HideWhenAiming = true;

		// Token: 0x04003718 RID: 14104
		[SerializeField]
		private Color m_NormalColor = Color.white;

		// Token: 0x04003719 RID: 14105
		[SerializeField]
		private Color m_OnEntityColor = Color.red;

		// Token: 0x0400371A RID: 14106
		[SerializeField]
		private CrosshairType m_Type;

		// Token: 0x0400371B RID: 14107
		[SerializeField]
		private Image m_Image;

		// Token: 0x0400371C RID: 14108
		[SerializeField]
		private Sprite m_Sprite;

		// Token: 0x0400371D RID: 14109
		[SerializeField]
		private Vector2 m_Size = new Vector2(64f, 64f);

		// Token: 0x0400371E RID: 14110
		[SerializeField]
		private DynamicCrosshair m_Crosshair;

		// Token: 0x0400371F RID: 14111
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_IdleDistance = 32f;

		// Token: 0x04003720 RID: 14112
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_CrouchDistance = 24f;

		// Token: 0x04003721 RID: 14113
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_WalkDistance = 36f;

		// Token: 0x04003722 RID: 14114
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_RunDistance = 48f;

		// Token: 0x04003723 RID: 14115
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_JumpDistance = 54f;
	}
}
