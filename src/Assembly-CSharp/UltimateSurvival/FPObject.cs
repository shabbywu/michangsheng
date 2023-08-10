using UnityEngine;

namespace UltimateSurvival;

public class FPObject : PlayerBehaviour
{
	public Message Draw = new Message();

	public Message Holster = new Message();

	[Header("General")]
	[SerializeField]
	private string m_ObjectName = "Unnamed";

	[SerializeField]
	[Range(15f, 100f)]
	private int m_TargetFOV = 45;

	protected ItemProperty.Value m_Durability;

	public bool IsEnabled { get; private set; }

	public string ObjectName => m_ObjectName;

	public SavableItem CorrespondingItem { get; private set; }

	public int TargetFOV => m_TargetFOV;

	public float LastDrawTime { get; private set; }

	protected virtual void Awake()
	{
		((Component)this).gameObject.SetActive(true);
		((Component)this).gameObject.SetActive(false);
	}

	public virtual void On_Draw(SavableItem correspondingItem)
	{
		IsEnabled = true;
		CorrespondingItem = correspondingItem;
		LastDrawTime = Time.time;
		m_Durability = correspondingItem.GetPropertyValue("Durability");
		Draw.Send();
	}

	public virtual void On_Holster()
	{
		IsEnabled = false;
		CorrespondingItem = null;
		Holster.Send();
	}
}
