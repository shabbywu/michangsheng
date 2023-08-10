using Fungus;
using UnityEngine;

public class DeathCtr : MonoBehaviour
{
	private void Start()
	{
		Object.Destroy((Object)(object)((Component)SayDialog.GetSayDialog()).gameObject);
	}
}
