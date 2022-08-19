using System;
using KBEngine;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class CharacterHUD : MonoBehaviour
{
	// Token: 0x06000B03 RID: 2819 RVA: 0x00042B5F File Offset: 0x00040D5F
	private void Start()
	{
		base.enabled = true;
		if (base.gameObject.GetComponent<GameEntity>())
		{
			this.gameEntity = base.gameObject.GetComponent<GameEntity>();
		}
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x00042B8C File Offset: 0x00040D8C
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

	// Token: 0x06000B05 RID: 2821 RVA: 0x00042BE4 File Offset: 0x00040DE4
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

	// Token: 0x04000727 RID: 1831
	public GUISkin Skin;

	// Token: 0x04000728 RID: 1832
	public bool AlwayShow;

	// Token: 0x04000729 RID: 1833
	public Texture2D Bar_bg;

	// Token: 0x0400072A RID: 1834
	public Texture2D Bar_hp;

	// Token: 0x0400072B RID: 1835
	public Texture2D Bar_Rhp;

	// Token: 0x0400072C RID: 1836
	public Texture2D Bar_sp;

	// Token: 0x0400072D RID: 1837
	public Texture2D Bar_target;

	// Token: 0x0400072E RID: 1838
	private CharacterStatus character;

	// Token: 0x0400072F RID: 1839
	private GameEntity gameEntity;

	// Token: 0x04000730 RID: 1840
	public bool isTarget;

	// Token: 0x04000731 RID: 1841
	public GameObject FloatingText;
}
