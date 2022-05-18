using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class CharacterHUD : MonoBehaviour
{
	// Token: 0x06000BE6 RID: 3046 RVA: 0x0000DFCE File Offset: 0x0000C1CE
	private void Start()
	{
		base.enabled = true;
		if (base.gameObject.GetComponent<GameEntity>())
		{
			this.gameEntity = base.gameObject.GetComponent<GameEntity>();
		}
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x0009490C File Offset: 0x00092B0C
	public void AddFloatingText(Vector3 pos, string text)
	{
		if (this.FloatingText)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.FloatingText, pos, base.transform.rotation);
			if (gameObject.GetComponent<FloatingText>())
			{
				gameObject.GetComponent<FloatingText>().Text = text;
			}
			Object.Destroy(gameObject, 1f);
		}
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00094964 File Offset: 0x00092B64
	private void OnGUI()
	{
		Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.position);
		if (this.Skin != null)
		{
			GUI.skin = this.Skin;
		}
		GameObject gameObject = (GameObject)KBEngineApp.app.player().renderObj;
		if (this.gameEntity && gameObject && Vector3.Distance(base.gameObject.transform.position, gameObject.transform.position) < 20f && (this.AlwayShow || this.gameEntity.hp < this.gameEntity.hpMax))
		{
			GUI.BeginGroup(new Rect(vector.x - 42f, (float)Screen.height - vector.y + 20f, 104f, 60f));
			GUI.DrawTexture(new Rect(0f, 0f, 104f, 9f), this.Bar_bg);
			if (this.gameEntity.canAttack)
			{
				GUI.DrawTexture(new Rect(2f, 2f, 100f / (float)this.gameEntity.hpMax * (float)this.gameEntity.hp, 5f), this.Bar_Rhp);
			}
			else
			{
				GUI.DrawTexture(new Rect(2f, 2f, 100f / (float)this.gameEntity.hpMax * (float)this.gameEntity.hp, 5f), this.Bar_hp);
			}
			if (this.isTarget && World.instance.getUITarget().GE_target != null)
			{
				if (World.instance.getUITarget().GE_target.gameObject == base.gameObject)
				{
					GUI.DrawTexture(new Rect(36f, 20f, 32f, 32f), this.Bar_target);
				}
				else
				{
					this.isTarget = false;
				}
			}
			if (this.gameEntity.spMax > 0)
			{
				GUI.DrawTexture(new Rect(0f, 9f, 104f, 7f), this.Bar_bg);
				GUI.DrawTexture(new Rect(2f, 9f, 100f / (float)this.gameEntity.sp * (float)this.gameEntity.spMax, 5f), this.Bar_sp);
			}
			GUI.EndGroup();
		}
	}

	// Token: 0x040008D2 RID: 2258
	public GUISkin Skin;

	// Token: 0x040008D3 RID: 2259
	public bool AlwayShow;

	// Token: 0x040008D4 RID: 2260
	public Texture2D Bar_bg;

	// Token: 0x040008D5 RID: 2261
	public Texture2D Bar_hp;

	// Token: 0x040008D6 RID: 2262
	public Texture2D Bar_Rhp;

	// Token: 0x040008D7 RID: 2263
	public Texture2D Bar_sp;

	// Token: 0x040008D8 RID: 2264
	public Texture2D Bar_target;

	// Token: 0x040008D9 RID: 2265
	private CharacterStatus character;

	// Token: 0x040008DA RID: 2266
	private GameEntity gameEntity;

	// Token: 0x040008DB RID: 2267
	public bool isTarget;

	// Token: 0x040008DC RID: 2268
	public GameObject FloatingText;
}
