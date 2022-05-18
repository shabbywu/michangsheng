using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012E6 RID: 4838
	public class Parallax : MonoBehaviour
	{
		// Token: 0x060075D6 RID: 30166 RVA: 0x0005048C File Offset: 0x0004E68C
		protected virtual void Start()
		{
			this.startPosition = base.transform.position;
			if (this.backgroundSprite == null)
			{
				this.backgroundSprite = base.gameObject.GetComponentInParent<SpriteRenderer>();
			}
		}

		// Token: 0x060075D7 RID: 30167 RVA: 0x002B0FE4 File Offset: 0x002AF1E4
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

		// Token: 0x040066D0 RID: 26320
		[SerializeField]
		protected SpriteRenderer backgroundSprite;

		// Token: 0x040066D1 RID: 26321
		[SerializeField]
		protected Vector2 parallaxScale = new Vector2(0.25f, 0f);

		// Token: 0x040066D2 RID: 26322
		[SerializeField]
		protected float accelerometerScale = 0.5f;

		// Token: 0x040066D3 RID: 26323
		protected Vector3 startPosition;

		// Token: 0x040066D4 RID: 26324
		protected Vector3 acceleration;

		// Token: 0x040066D5 RID: 26325
		protected Vector3 velocity;
	}
}
