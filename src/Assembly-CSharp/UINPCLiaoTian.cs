using Fungus;
using UnityEngine;
using UnityEngine.Events;

public class UINPCLiaoTian : MonoBehaviour
{
	public static UINPCLiaoTian Inst;

	private UINPCData npc;

	private bool _inited;

	public RectTransform ContentRT;

	public GameObject SVItemPrefab;

	public Flowchart NPCFlowchart;

	private GameObject nowChatObj;

	[HideInInspector]
	public UnityAction onChatExit;

	private void Awake()
	{
		Inst = this;
	}

	private void Update()
	{
	}

	public void RefreshUI()
	{
		Init();
	}

	public void OnChatExit()
	{
		Object.Destroy((Object)(object)nowChatObj, 2f);
		if (onChatExit != null)
		{
			onChatExit.Invoke();
		}
	}

	public void ShowTalk(int avatarid, string msg, UnityAction onExit = null)
	{
		nowChatObj = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCLiaoTianTalk"));
		Say obj = (Say)((Component)nowChatObj.transform.Find("Flowchart")).GetComponent<Flowchart>().FindBlock("Splash").CommandList[0];
		obj.pubAvatarIntID = avatarid;
		obj.SetStandardText(msg);
		onChatExit = onExit;
	}

	private void Init()
	{
		if (!_inited)
		{
			_inited = true;
		}
	}
}
