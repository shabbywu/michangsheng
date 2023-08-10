using KBEngine;
using UnityEngine;

public class CharacterHUD : MonoBehaviour
{
	public GUISkin Skin;

	public bool AlwayShow;

	public Texture2D Bar_bg;

	public Texture2D Bar_hp;

	public Texture2D Bar_Rhp;

	public Texture2D Bar_sp;

	public Texture2D Bar_target;

	private CharacterStatus character;

	private GameEntity gameEntity;

	public bool isTarget;

	public GameObject FloatingText;

	private void Start()
	{
		((Behaviour)this).enabled = true;
		if (Object.op_Implicit((Object)(object)((Component)this).gameObject.GetComponent<GameEntity>()))
		{
			gameEntity = ((Component)this).gameObject.GetComponent<GameEntity>();
		}
	}

	public void AddFloatingText(Vector3 pos, string text)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)FloatingText))
		{
			GameObject val = Object.Instantiate<GameObject>(FloatingText, pos, ((Component)this).transform.rotation);
			if (Object.op_Implicit((Object)(object)val.GetComponent<FloatingText>()))
			{
				val.GetComponent<FloatingText>().Text = text;
			}
			Object.Destroy((Object)(object)val, 1f);
		}
	}

	private void OnGUI()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = Camera.main.WorldToScreenPoint(((Component)this).transform.position);
		if ((Object)(object)Skin != (Object)null)
		{
			GUI.skin = Skin;
		}
		GameObject val2 = (GameObject)KBEngineApp.app.player().renderObj;
		if (!Object.op_Implicit((Object)(object)gameEntity) || !Object.op_Implicit((Object)(object)val2) || !(Vector3.Distance(((Component)this).gameObject.transform.position, val2.transform.position) < 20f) || (!AlwayShow && gameEntity.hp >= gameEntity.hpMax))
		{
			return;
		}
		GUI.BeginGroup(new Rect(val.x - 42f, (float)Screen.height - val.y + 20f, 104f, 60f));
		GUI.DrawTexture(new Rect(0f, 0f, 104f, 9f), (Texture)(object)Bar_bg);
		if (gameEntity.canAttack)
		{
			GUI.DrawTexture(new Rect(2f, 2f, 100f / (float)gameEntity.hpMax * (float)gameEntity.hp, 5f), (Texture)(object)Bar_Rhp);
		}
		else
		{
			GUI.DrawTexture(new Rect(2f, 2f, 100f / (float)gameEntity.hpMax * (float)gameEntity.hp, 5f), (Texture)(object)Bar_hp);
		}
		if (isTarget && (Object)(object)World.instance.getUITarget().GE_target != (Object)null)
		{
			if ((Object)(object)((Component)World.instance.getUITarget().GE_target).gameObject == (Object)(object)((Component)this).gameObject)
			{
				GUI.DrawTexture(new Rect(36f, 20f, 32f, 32f), (Texture)(object)Bar_target);
			}
			else
			{
				isTarget = false;
			}
		}
		if (gameEntity.spMax > 0)
		{
			GUI.DrawTexture(new Rect(0f, 9f, 104f, 7f), (Texture)(object)Bar_bg);
			GUI.DrawTexture(new Rect(2f, 9f, 100f / (float)gameEntity.sp * (float)gameEntity.spMax, 5f), (Texture)(object)Bar_sp);
		}
		GUI.EndGroup();
	}
}
