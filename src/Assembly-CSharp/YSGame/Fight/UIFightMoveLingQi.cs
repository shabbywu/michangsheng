using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000E02 RID: 3586
	public class UIFightMoveLingQi : MonoBehaviour
	{
		// Token: 0x060056A0 RID: 22176 RVA: 0x00241200 File Offset: 0x0023F400
		private void Update()
		{
			if (this.IsStart)
			{
				this.cd -= Time.deltaTime;
				if (this.isShowAnim)
				{
					float num = Mathf.Lerp(0f, 1f, Mathf.Clamp(1f - this.cd, 0f, 1f));
					this.LingQiImage.color = new Color(1f, 1f, 1f, num);
					this.LingQiImage.transform.localScale = new Vector3(num, num, num);
				}
				if (this.cd < 0f)
				{
					this.OnEnd();
					return;
				}
			}
			else
			{
				this.deadTime -= Time.deltaTime;
				if (this.deadTime < 0f && UIFightPanel.Inst.MoveLingQiList.Count > 10)
				{
					UIFightPanel.Inst.MoveLingQiList.Remove(this);
					Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x002412F8 File Offset: 0x0023F4F8
		public void SetData(LingQiType lingQiType, Vector3 start, Vector3 end, Action callback = null, int count = 1, bool showAnim = false, float particleSpeed = 5f)
		{
			if (lingQiType == LingQiType.Count)
			{
				Debug.LogError("灵气动画异常：尝试设置空灵气动画");
				return;
			}
			this.LastLingQiType = lingQiType;
			this.LastLingQiCount = count;
			this.Particle2.startColor = UIFightMoveLingQi.pColors[(int)lingQiType];
			this.Particle2.emission.rateOverTime = (float)Mathf.Clamp(count * 5, 0, 100);
			base.transform.position = new Vector3(start.x, start.y, start.z - 5f);
			this.ParticleEnd.position = end;
			this.ParticleEnd.localPosition = new Vector3(this.ParticleEnd.localPosition.x, this.ParticleEnd.localPosition.y, 0f);
			this.isShowAnim = showAnim;
			if (this.isShowAnim)
			{
				this.LingQiImage.sprite = UIFightPanel.Inst.LingQiImageDatas[(int)lingQiType].Normal;
				this.LingQiImage.transform.position = this.ParticleEnd.position;
			}
			this.OnMoveEnd = callback;
			this.cd = 0.2f;
			this.PAL.speed = particleSpeed;
			if (particleSpeed >= 0f)
			{
				ParticleSystem.MainModule main = this.Particle2.main;
				ParticleSystem.MinMaxCurve startLifetime = main.startLifetime;
				startLifetime.constant = this.particleAnimTime;
				main.startLifetime = startLifetime;
			}
			else
			{
				ParticleSystem.MainModule main2 = this.Particle2.main;
				ParticleSystem.MinMaxCurve startLifetime2 = main2.startLifetime;
				startLifetime2.constant = this.particleAnimTime / 2f;
				main2.startLifetime = startLifetime2;
			}
			this.Particle1.Play();
			this.IsStart = true;
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x002414AC File Offset: 0x0023F6AC
		private void OnEnd()
		{
			this.IsStart = false;
			this.deadTime = 10f;
			this.LingQiImage.color = new Color(1f, 1f, 1f, 0f);
			this.LingQiImage.transform.localScale = Vector3.zero;
			if (this.OnMoveEnd != null)
			{
				this.OnMoveEnd();
			}
		}

		// Token: 0x04005621 RID: 22049
		private static List<Color> pColors = new List<Color>
		{
			new Color(1f, 0.8862745f, 0.02745098f),
			new Color(0.33333334f, 1f, 0.02745098f),
			new Color(0.02745098f, 0.73333335f, 1f),
			new Color(1f, 0.09803922f, 0.02745098f),
			new Color(0.36078432f, 0.20392157f, 0.0627451f),
			new Color(0.2784314f, 0f, 0.33333334f)
		};

		// Token: 0x04005622 RID: 22050
		public particleAttractorLinear PAL;

		// Token: 0x04005623 RID: 22051
		public ParticleSystem Particle1;

		// Token: 0x04005624 RID: 22052
		public ParticleSystem Particle2;

		// Token: 0x04005625 RID: 22053
		public Image LingQiImage;

		// Token: 0x04005626 RID: 22054
		public Transform ParticleEnd;

		// Token: 0x04005627 RID: 22055
		private Action OnMoveEnd;

		// Token: 0x04005628 RID: 22056
		private float cd;

		// Token: 0x04005629 RID: 22057
		[HideInInspector]
		public bool IsStart;

		// Token: 0x0400562A RID: 22058
		private bool isShowAnim;

		// Token: 0x0400562B RID: 22059
		public LingQiType LastLingQiType;

		// Token: 0x0400562C RID: 22060
		public int LastLingQiCount;

		// Token: 0x0400562D RID: 22061
		private float deadTime;

		// Token: 0x0400562E RID: 22062
		private float particleAnimTime = 0.66f;
	}
}
