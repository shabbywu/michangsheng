using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E7B RID: 3707
	public class Parallax : MonoBehaviour
	{
		// Token: 0x060068EF RID: 26863 RVA: 0x0028EA3D File Offset: 0x0028CC3D
		protected virtual void Start()
		{
			this.startPosition = base.transform.position;
			if (this.backgroundSprite == null)
			{
				this.backgroundSprite = base.gameObject.GetComponentInParent<SpriteRenderer>();
			}
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x0028EA70 File Offset: 0x0028CC70
		protected virtual void Update()
		{
			if (this.backgroundSprite == null)
			{
				return;
			}
			Vector3 vector = Vector3.zero;
			Vector3 center = this.backgroundSprite.bounds.center;
			Vector3 position = Camera.main.transform.position;
			vector = center - position;
			vector.x *= this.parallaxScale.x;
			vector.y *= this.parallaxScale.y;
			vector.z = 0f;
			if (SystemInfo.supportsAccelerometer)
			{
				float num = Mathf.Max(this.parallaxScale.x, this.parallaxScale.y);
				this.acceleration = Vector3.SmoothDamp(this.acceleration, Input.acceleration, ref this.velocity, 0.1f);
				Vector3 vector2 = Quaternion.Euler(45f, 0f, 0f) * this.acceleration * num * this.accelerometerScale;
				vector += vector2;
			}
			base.transform.position = this.startPosition + vector;
		}

		// Token: 0x04005908 RID: 22792
		[SerializeField]
		protected SpriteRenderer backgroundSprite;

		// Token: 0x04005909 RID: 22793
		[SerializeField]
		protected Vector2 parallaxScale = new Vector2(0.25f, 0f);

		// Token: 0x0400590A RID: 22794
		[SerializeField]
		protected float accelerometerScale = 0.5f;

		// Token: 0x0400590B RID: 22795
		protected Vector3 startPosition;

		// Token: 0x0400590C RID: 22796
		protected Vector3 acceleration;

		// Token: 0x0400590D RID: 22797
		protected Vector3 velocity;
	}
}
