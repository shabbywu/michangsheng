using UnityEngine;
using UnityEngine.UI;

namespace PaiMai;

public class NpcUI : MonoBehaviour
{
	[SerializeField]
	private PlayerSetRandomFace NpcFace;

	[SerializeField]
	private Text NpcName;

	public void Init(int npcId)
	{
		NpcFace.SetNPCFace(npcId);
		NpcName.text = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"].Str;
	}
}
