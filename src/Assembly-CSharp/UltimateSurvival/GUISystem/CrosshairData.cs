using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200064E RID: 1614
	[Serializable]
	public class CrosshairData
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06003364 RID: 13156 RVA: 0x0016914E File Offset: 0x0016734E
		public string ItemName
		{
			get
			{
				return this.m_ItemName;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x00169156 File Offset: 0x00167356
		public bool HideWhenAiming
		{
			get
			{
				return this.m_HideWhenAiming;
			}
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x00014667 File Offset: 0x00012867
		public static implicit operator bool(CrosshairData cd)
		{
			return cd != null;
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x00169160 File Offset: 0x00167360
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

		// Token: 0x06003368 RID: 13160 RVA: 0x001692A4 File Offset: 0x001674A4
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

		// Token: 0x04002D9F RID: 11679
		[SerializeField]
		private string m_ItemName;

		// Token: 0x04002DA0 RID: 11680
		[SerializeField]
		private bool m_HideWhenAiming = true;

		// Token: 0x04002DA1 RID: 11681
		[SerializeField]
		private Color m_NormalColor = Color.white;

		// Token: 0x04002DA2 RID: 11682
		[SerializeField]
		private Color m_OnEntityColor = Color.red;

		// Token: 0x04002DA3 RID: 11683
		[SerializeField]
		private CrosshairType m_Type;

		// Token: 0x04002DA4 RID: 11684
		[SerializeField]
		private Image m_Image;

		// Token: 0x04002DA5 RID: 11685
		[SerializeField]
		private Sprite m_Sprite;

		// Token: 0x04002DA6 RID: 11686
		[SerializeField]
		private Vector2 m_Size = new Vector2(64f, 64f);

		// Token: 0x04002DA7 RID: 11687
		[SerializeField]
		private DynamicCrosshair m_Crosshair;

		// Token: 0x04002DA8 RID: 11688
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_IdleDistance = 32f;

		// Token: 0x04002DA9 RID: 11689
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_CrouchDistance = 24f;

		// Token: 0x04002DAA RID: 11690
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_WalkDistance = 36f;

		// Token: 0x04002DAB RID: 11691
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_RunDistance = 48f;

		// Token: 0x04002DAC RID: 11692
		[SerializeField]
		[Clamp(0f, 256f)]
		private float m_JumpDistance = 54f;
	}
}
