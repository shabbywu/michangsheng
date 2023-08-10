using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fight;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YSGame;

public class World : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__42_0;

		internal void _003Cset_state_003Eb__42_0()
		{
			GameOver();
			YSFuncList.Ints.Continue();
		}
	}

	public static World _instance;

	private GameObject terrain;

	public GameObject terrainPerfab;

	private GameObject player;

	public GameObject otherPlayerPerfab;

	public GameObject gatePerfab;

	public GameObject avatarPerfab;

	public GameObject snowBallPerfab;

	public GameObject droppedItemPerfab;

	private Dictionary<string, GameObject> allGameEntity = new Dictionary<string, GameObject>();

	private bool isFirstPos = true;

	private UI_Target ui_target;

	private UI_Target ui_targetPlayer;

	public static ItemDataBaseList inventoryItemList;

	public static World instance
	{
		get
		{
			if ((Object)(object)_instance != (Object)null)
			{
				return _instance;
			}
			_instance = new World();
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	static World()
	{
	}

	private void Awake()
	{
	}

	public void init()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		inventoryItemList = (ItemDataBaseList)(object)Resources.Load("ItemDatabase");
		GameObject val = new GameObject("World");
		Object.DontDestroyOnLoad((Object)val);
		instance = val.AddComponent<World>();
		instance.terrainPerfab = (GameObject)Resources.Load("Terrain");
		instance.otherPlayerPerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/Character");
		instance.gatePerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/Gate");
		instance.avatarPerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/Character");
		instance.snowBallPerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/snowBall");
		instance.droppedItemPerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/droppedItem");
		instance.allGameEntity.Add("Zombie", (GameObject)Resources.Load("Effect/Prefab/gameEntity/Zombie"));
		instance.allGameEntity.Add("In-Game GUI", (GameObject)Resources.Load("Effect/Prefab/gameUI/In-Game GUI"));
		instance.allGameEntity.Add("_Game Controller", (GameObject)Resources.Load("Effect/Prefab/gameUI/_Game Controller"));
	}

	private void Start()
	{
		GameObject val = GameObject.FindGameObjectWithTag("Target");
		if (Object.op_Implicit((Object)(object)val))
		{
			ui_target = val.GetComponent<UI_Target>();
		}
		Event.registerOut("addSpaceGeometryMapping", this, "addSpaceGeometryMapping");
		Event.registerOut("onAvatarEnterWorld", this, "onAvatarEnterWorld");
		Event.registerOut("onEnterWorld", this, "onEnterWorld");
		Event.registerOut("onLeaveWorld", this, "onLeaveWorld");
		Event.registerOut("set_position", this, "set_position");
		Event.registerOut("set_direction", this, "set_direction");
		Event.registerOut("updatePosition", this, "updatePosition");
		Event.registerOut("set_name", this, "set_entityName");
		Event.registerOut("set_state", this, "set_state");
		Event.registerOut("set_HP", this, "set_HP");
		Event.registerOut("set_HP_Max", this, "set_HP_Max");
		Event.registerOut("set_Buffs", this, "set_Buffs");
		Event.registerOut("recvDamage", this, "recvDamage");
		Event.registerOut("onReqItemList", this, "onReqItemList");
		Event.registerOut("set_equipWeapon", this, "set_equipWeapon");
		Event.registerOut("setSkillButton", this, "setSkillButton");
		Event.registerOut("recvSkill", this, "recvSkill");
		Event.registerOut("goToHome", this, "goToHome");
	}

	public void set_Buffs(Entity entity, List<ushort> oldValue, List<ushort> newValue)
	{
		if (oldValue.Count() > newValue.Count() || entity.renderObj == null)
		{
			return;
		}
		List<ushort> list = newValue.Except(oldValue).ToList();
		list.AddRange(oldValue.Except(newValue).ToList());
		foreach (ushort item in list)
		{
			displayBuff(entity, item);
		}
	}

	public void displayBuff(Entity entity, int buffid)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		string str = jsonData.instance.BuffJsonData[string.Concat(buffid)]["skillEffect"].str;
		if (str != "")
		{
			_ = ((GameObject)entity.renderObj).transform.position;
			Object.Destroy(Object.Instantiate(ResManager.inst.LoadSkillEffect(str), ((GameObject)entity.renderObj).transform), jsonData.instance.BuffJsonData[string.Concat(buffid)]["totaltime"].n);
		}
	}

	public void goToHome()
	{
		SceneManager.LoadScene("Mainmenu");
		SceneManager.LoadSceneAsync("homeScene", (LoadSceneMode)1);
		Object.Destroy((Object)(object)((Component)instance).gameObject);
	}

	private void OnDestroy()
	{
		instance = null;
		Event.deregisterOut(this);
	}

	private void Update()
	{
	}

	public void onAvatarEnterWorld(ulong rndUUID, int eid, Avatar avatar)
	{
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		if (!avatar.isPlayer())
		{
			return;
		}
		createPlayer();
		Debug.Log((object)"loading scene...");
		object obj = avatar.state;
		if (obj != null)
		{
			set_state(avatar, obj);
		}
		object name = avatar.name;
		if (name != null)
		{
			set_entityName(avatar, (string)name);
		}
		object obj2 = avatar.HP;
		if (obj2 != null)
		{
			set_HP(avatar, obj2);
		}
		object obj3 = avatar.HP_Max;
		if (obj3 != null)
		{
			set_HP_Max(avatar, obj3);
		}
		GameObject val = GameObject.FindGameObjectWithTag("TargetPlayer");
		if (Object.op_Implicit((Object)(object)val))
		{
			ui_targetPlayer = val.GetComponent<UI_Target>();
		}
		if (Object.op_Implicit((Object)(object)ui_targetPlayer))
		{
			ui_targetPlayer.GE_target = player.GetComponent<GameEntity>();
		}
		GameObject val2 = GameObject.FindGameObjectWithTag("Canvas");
		GameObject val3 = null;
		if ((Object)(object)val2.transform.Find("Panel - State") != (Object)null)
		{
			val3 = ((Component)val2.transform.Find("Panel - State")).gameObject;
		}
		if ((Object)(object)val3 != (Object)null)
		{
			UI_AvatarState component = val3.GetComponent<UI_AvatarState>();
			object obj4 = avatar.attack_Max;
			if (obj4 != null)
			{
				component.setAttackMax((int)obj4);
			}
			object obj5 = avatar.attack_Min;
			if (obj5 != null)
			{
				component.setAttackMin((int)obj5);
			}
			object obj6 = avatar.defence;
			if (obj6 != null)
			{
				component.setDefence((int)obj6);
			}
			object obj7 = avatar.rating;
			if (obj7 != null)
			{
				component.setRating((int)obj7);
			}
			object obj8 = avatar.dodge;
			if (obj8 != null)
			{
				component.setDodge((int)obj8);
			}
			object obj9 = avatar.strength;
			if (obj9 != null)
			{
				component.setStrength((int)obj9);
			}
			object obj10 = avatar.dexterity;
			if (obj10 != null)
			{
				component.setDexterity((int)obj10);
			}
			object obj11 = avatar.stamina;
			if (obj11 != null)
			{
				component.setStamina((int)obj11);
			}
			object obj12 = avatar.exp;
			if (obj12 != null)
			{
				component.setExp((ulong)obj12);
			}
			object obj13 = avatar.level;
			if (obj13 != null)
			{
				component.setLevel((ushort)obj13);
			}
		}
		object obj14 = avatar.equipWeapon;
		if (obj14 != null)
		{
			set_equipWeapon(avatar, (int)obj14);
		}
		SkillBox.inst.initSkillDisplay();
		GameObject obj15 = GameObject.Find("Male_Player");
		GameObject val4 = (GameObject)KBEngineApp.app.player().renderObj;
		obj15.transform.parent = val4.transform;
		obj15.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	public void onEnterWorld(Entity entity)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		float num = entity.position.y;
		if (entity.isOnGround)
		{
			num = 0f;
		}
		if (entity.className == "Gate")
		{
			entity.renderObj = Object.Instantiate<GameObject>(gatePerfab, new Vector3(entity.position.x, num, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
			((GameObject)entity.renderObj).GetComponent<GameEntity>().entityDisable();
		}
		else if (entity.className == "Monster" || entity.className == "Pet")
		{
			createMonster(entity);
		}
		else if (entity.className == "DroppedItem")
		{
			entity.renderObj = Object.Instantiate<GameObject>(droppedItemPerfab, new Vector3(entity.position.x, num, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
		}
		else if (entity.className == "Avatar")
		{
			creatAvater(entity);
		}
		else if (entity.className == "NPC")
		{
			createNPC(entity);
		}
		else if (entity.className == "Build")
		{
			CreatBuild(entity);
		}
		((Object)(GameObject)entity.renderObj).name = entity.className + "_" + entity.id;
		set_position(entity);
		set_direction(entity);
		object definedProperty = entity.getDefinedProperty("state");
		if (definedProperty != null)
		{
			set_state(entity, definedProperty);
		}
		object definedProperty2 = entity.getDefinedProperty("name");
		if (definedProperty2 != null)
		{
			set_entityName(entity, (string)definedProperty2);
		}
		object definedProperty3 = entity.getDefinedProperty("HP");
		if (definedProperty3 != null)
		{
			set_HP(entity, definedProperty3);
		}
		object definedProperty4 = entity.getDefinedProperty("HP_Max");
		if (definedProperty4 != null)
		{
			set_HP_Max(entity, definedProperty4);
		}
		object definedProperty5 = entity.getDefinedProperty("equipWeapon");
		if (definedProperty5 != null)
		{
			set_equipWeapon(entity, (int)definedProperty5);
		}
	}

	public void createMonster(Entity entity)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		createInstantiate("Effect/Prefab/gameEntity/Monster/Monster", entity, "Monster");
		createRoleScript<MonsterAddScript>(entity);
		GameObject val = (GameObject)entity.renderObj;
		if (entity.className == "Monster")
		{
			val.GetComponent<GameEntity>().canAttack = true;
		}
	}

	public void createNPC(Entity entity)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = (GameObject)Resources.Load("Effect/Prefab/gameEntity/NPC/NPC1/NPC1_1");
		entity.renderObj = Object.Instantiate<GameObject>(val, new Vector3(entity.position.x, entity.position.y, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
	}

	public void creatAvater(Entity entity)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		createInstantiate("Effect/Prefab/gameEntity/Avater/Avater", entity, "Avater");
		createRoleScript<AvaterAddScript>(entity);
		((GameObject)entity.renderObj).GetComponent<GameEntity>().scale = new Vector3(0.75f, 0.75f, 0.75f);
	}

	public void createRoleScript<T>(Entity entity) where T : BaseAddScript
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		object definedProperty = entity.getDefinedProperty("roleTypeCell");
		object definedProperty2 = entity.getDefinedProperty("roleSurfaceCall");
		BaseAddScript component = ((GameObject)entity.renderObj).GetComponent<T>();
		if ((Object)(object)component == (Object)null)
		{
			((GameObject)entity.renderObj).AddComponent<T>();
			component = ((GameObject)entity.renderObj).GetComponent<T>();
		}
		component.nowRoleType = (int)(uint)definedProperty;
		component.nowRoleFace = (ushort)definedProperty2;
		component.entity = entity;
	}

	public void createInstantiate(string patch, Entity entity, string entitytype)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		float y = entity.position.y;
		_ = entity.isOnGround;
		Avatar avatar = (Avatar)entity;
		uint roleTypeCell = avatar.roleTypeCell;
		ushort roleSurfaceCall = avatar.roleSurfaceCall;
		string text = patch + roleTypeCell + "/" + entitytype + roleTypeCell + "_" + roleSurfaceCall;
		string text2 = patch + (49 + avatar.Sex) + "/" + entitytype + (49 + avatar.Sex) + "_" + roleSurfaceCall;
		GameObject val = (GameObject)Resources.Load((roleTypeCell == 0) ? text2 : text);
		entity.renderObj = (object)new GameObject(entitytype + "_" + entity.id);
		GameObject obj = Object.Instantiate<GameObject>(val, new Vector3(entity.position.x, y, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
		obj.transform.parent = ((GameObject)entity.renderObj).transform;
		obj.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	public void CreatBuild(Entity entity)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		object definedProperty = entity.getDefinedProperty("BuildId");
		if (definedProperty != null)
		{
			UltimateSurvival.MonoSingleton<InventoryController>.Instance.Database.FindItemById((int)definedProperty, out var itemData);
			entity.renderObj = Object.Instantiate<GameObject>(itemData.WorldObject, new Vector3(entity.position.x, entity.position.y, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
		}
	}

	public void onLeaveWorld(Entity entity)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		if (entity.renderObj != null)
		{
			GameEntity component = ((GameObject)entity.renderObj).GetComponent<GameEntity>();
			if ((Object)(object)getUITarget().GE_target == (Object)(object)component)
			{
				getUITarget().deactivate();
			}
			Object.Destroy((Object)(GameObject)entity.renderObj);
			entity.renderObj = null;
		}
	}

	public void set_entityName(Entity entity, object v)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (entity.renderObj != null)
		{
			((GameObject)entity.renderObj).GetComponent<GameEntity>().entity_name = (string)v;
		}
	}

	public void set_position(Entity entity)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (entity.renderObj != null)
		{
			Vector3 position = entity.position;
			((GameObject)entity.renderObj).GetComponent<GameEntity>().destPosition = position;
			((GameObject)entity.renderObj).GetComponent<GameEntity>().position = position;
		}
	}

	public void set_direction(Entity entity)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (entity.renderObj != null)
		{
			_ = (GameObject)entity.renderObj;
			((GameObject)entity.renderObj).GetComponent<GameEntity>().destDirection = new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
		}
	}

	public UI_Target getUITarget()
	{
		if ((Object)(object)ui_target == (Object)null)
		{
			GameObject val = GameObject.FindGameObjectWithTag("Target");
			if (Object.op_Implicit((Object)(object)val))
			{
				ui_target = val.GetComponent<UI_Target>();
			}
		}
		return ui_target;
	}

	public void set_HP(Entity entity, object v)
	{
	}

	public void set_HP_Max(Entity entity, object v)
	{
	}

	public static void GameOver()
	{
		if ((Object)(object)RoundManager.instance != (Object)null && RoundManager.instance.gameOverSwitch == 0)
		{
			RoundManager.instance.gameOverSwitch = 1;
			Tools.instance.getPlayer();
			if (RoundManager.instance.StaticRoundNum < 1)
			{
				RoundManager.instance.StaticRoundNum = 1;
			}
			GlobalValue.Set(403, RoundManager.instance.StaticRoundNum, "World.GameOver");
			FightResultMag.inst.ShowVictory();
		}
	}

	public void set_state(Entity entity, object v)
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		if (entity.renderObj == null)
		{
			return;
		}
		if ((sbyte)v == 1)
		{
			((Component)((GameObject)entity.renderObj).transform.GetChild(0)).GetComponent<Animator>().speed = 1f;
			((Component)((GameObject)entity.renderObj).transform.GetChild(0)).GetComponent<Animator>().Play("Dead");
			Queue<UnityAction> queue = new Queue<UnityAction>();
			object obj = _003C_003Ec._003C_003E9__42_0;
			if (obj == null)
			{
				UnityAction val = delegate
				{
					GameOver();
					YSFuncList.Ints.Continue();
				};
				_003C_003Ec._003C_003E9__42_0 = val;
				obj = (object)val;
			}
			UnityAction item = (UnityAction)obj;
			queue.Enqueue(item);
			YSFuncList.Ints.AddFunc(queue);
		}
		else
		{
			((Component)((GameObject)entity.renderObj).transform.GetChild(0)).GetComponent<Animator>().speed = 1f;
			((Component)((GameObject)entity.renderObj).transform.GetChild(0)).GetComponent<Animator>().SetFloat("Speed", 0f);
			((Component)((GameObject)entity.renderObj).transform.GetChild(0)).GetComponent<Animator>().Play("Idle");
		}
	}

	public void createPlayer()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)player != (Object)null || KBEngineApp.app.entity_type != "Avatar")
		{
			return;
		}
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar == null)
		{
			Debug.Log((object)"wait create(palyer)!");
			return;
		}
		createInstantiate("Effect/Prefab/gameEntity/Avater/Avater", avatar, "Avater");
		player = (GameObject)avatar.renderObj;
		createRoleScript<AvaterAddScript>(avatar);
		initPlayer(player);
		avatar.renderObj = player;
		((Component)Camera.main).GetComponent<SmoothFollow>().target = player.transform;
		for (int i = 0; i < Camera.allCameras.Length; i++)
		{
			if ((Object)(object)((Component)Camera.allCameras[i]).GetComponent<MapFollow>() != (Object)null)
			{
				((Component)Camera.allCameras[i]).GetComponent<MapFollow>().target = player.transform;
			}
		}
		((GameObject)avatar.renderObj).GetComponent<GameEntity>().isPlayer = true;
	}

	private void initPlayer(GameObject player)
	{
		if (!((Object)(object)player == (Object)null))
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar == null)
			{
				Debug.Log((object)"wait create(palyer)!");
			}
			else
			{
				avatar.reqItemList();
			}
		}
	}

	public void addSpaceGeometryMapping(string respath)
	{
		Debug.Log((object)("loading scene(" + respath + ")..."));
		MonoBehaviour.print((object)("scene(" + respath + "), spaceID=" + KBEngineApp.app.spaceID));
		_ = (Object)(object)terrain == (Object)null;
	}

	public void updatePosition(Entity entity)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (entity.renderObj != null)
		{
			GameEntity component = ((GameObject)entity.renderObj).GetComponent<GameEntity>();
			component.destPosition = entity.position;
			component.isOnGround = entity.isOnGround;
		}
	}

	public void recvDamage(Entity entity, Entity attacker, int skillID, int damageType, int damage)
	{
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Expected O, but got Unknown
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Expected O, but got Unknown
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Expected O, but got Unknown
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		if (((Object)(object)RoundManager.instance != (Object)null && RoundManager.instance.IsVirtual) || skillID == 0)
		{
			return;
		}
		if (damage < 0)
		{
			Object obj = Object.Instantiate(ResManager.inst.LoadSkillEffect("huifu"));
			GameObject val = (GameObject)(object)((obj is GameObject) ? obj : null);
			if (SceneEx.NowSceneName == "YSNewTianJieFight")
			{
				val.transform.localPosition = new Vector3(-2.42f, 3.37f, 1f);
			}
			else if (entity == attacker)
			{
				if (entity.isPlayer())
				{
					val.transform.localPosition = new Vector3(-7.5f, 3.41f, 1f);
				}
				else
				{
					val.transform.localPosition = new Vector3(2.7f, 3.41f, 1f);
				}
			}
			else if (attacker.isPlayer())
			{
				val.transform.localPosition = new Vector3(-7.5f, 3.41f, 1f);
			}
			else
			{
				val.transform.localPosition = new Vector3(2.7f, 3.41f, 1f);
			}
		}
		Queue<UnityAction> queue = new Queue<UnityAction>();
		Skill sk = SkillBox.inst.get(skillID);
		GameObject val2 = (GameObject)attacker.renderObj;
		GameObject entityEntity = (GameObject)entity.renderObj;
		if (sk != null && jsonData.instance.skillJsonData[skillID.ToString()]["script"].str == "SkillAttack" && entity == attacker && damage < 0)
		{
			UnityAction item = (UnityAction)delegate
			{
				entityEntity.GetComponentInChildren<AvatarShowHpDamage>().show(damage);
				((MonoBehaviour)this).Invoke("continuFunc", 0.1f);
			};
			queue.Enqueue(item);
		}
		else if (sk != null && entity != attacker)
		{
			Vector3 val3 = entity.position - attacker.position;
			UnityAction item2 = (UnityAction)delegate
			{
				sk.displaySkill(attacker, entity);
			};
			UnityAction item3 = (UnityAction)delegate
			{
				if (damage > 0)
				{
					Transform child2 = entityEntity.transform.GetChild(0);
					if ((Object)(object)child2 == (Object)null)
					{
						YSFuncList.Ints.Continue();
						return;
					}
					Animator component2 = ((Component)child2).GetComponent<Animator>();
					if ((Object)(object)component2 == (Object)null)
					{
						YSFuncList.Ints.Continue();
						return;
					}
					component2.Play("Hit", -1, 0f);
				}
				YSFuncList.Ints.Continue();
			};
			queue.Enqueue(item2);
			queue.Enqueue(item3);
			if ((Object)(object)TianJieManager.Inst != (Object)null && entity.isPlayer())
			{
				entityEntity.GetComponentInChildren<AvatarShowHpDamage>().SpecialShow(damage);
			}
			else
			{
				UnityAction item4 = (UnityAction)delegate
				{
					entityEntity.GetComponentInChildren<AvatarShowHpDamage>().show(damage);
					((MonoBehaviour)this).Invoke("continuFunc", 0.1f);
				};
				queue.Enqueue(item4);
			}
			if (attacker.isPlayer())
			{
				val2.transform.LookAt(new Vector3(val2.transform.position.x + val3.x, val2.transform.position.y, val2.transform.position.z + val3.z));
			}
		}
		else if (entity == attacker)
		{
			UnityAction item5 = (UnityAction)delegate
			{
				sk.displaySkill(attacker, entity);
			};
			UnityAction item6 = (UnityAction)delegate
			{
				if (damage > 0)
				{
					Transform child = entityEntity.transform.GetChild(0);
					if ((Object)(object)child == (Object)null)
					{
						YSFuncList.Ints.Continue();
						return;
					}
					Animator component = ((Component)child).GetComponent<Animator>();
					if ((Object)(object)component == (Object)null)
					{
						YSFuncList.Ints.Continue();
						return;
					}
					component.Play("Hit", -1, 0f);
				}
				YSFuncList.Ints.Continue();
			};
			UnityAction item7 = (UnityAction)delegate
			{
				if ((Object)(object)entityEntity.GetComponentInChildren<AvatarShowHpDamage>() != (Object)null)
				{
					entityEntity.GetComponentInChildren<AvatarShowHpDamage>().show(damage);
				}
				((MonoBehaviour)this).Invoke("continuFunc", 0.1f);
			};
			queue.Enqueue(item5);
			queue.Enqueue(item6);
			queue.Enqueue(item7);
		}
		YSFuncList.Ints.AddFunc(queue);
		YSFuncList.Ints.Start();
	}

	public void continuFunc()
	{
		YSFuncList.Ints.Continue();
	}

	public void recvSkill(int attackerID, int skillID)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Entity entity = KBEngineApp.app.entities[attackerID];
		Skill skill = SkillBox.inst.get(skillID);
		if (skill != null)
		{
			GameObject val = (GameObject)entity.renderObj;
			((Component)val.transform.GetChild(0)).GetComponent<Animator>().Play("Punch", -1, 0f);
			((Component)val.transform.GetChild(0)).GetComponent<Animator>().Update(0f);
			skill.displaySkill(entity);
		}
	}

	public void onReqItemList(Dictionary<ulong, ITEM_INFO> itemList, Dictionary<ulong, ITEM_INFO> equipItemDict)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		GameObject val = (GameObject)KBEngineApp.app.player().renderObj;
		Inventory inventory = null;
		Inventory inventory2 = null;
		if ((Object)(object)val != (Object)null)
		{
			inventory = val.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
			inventory2 = val.GetComponent<PlayerInventory>().characterSystem.GetComponent<Inventory>();
		}
		if ((Object)(object)inventory != (Object)null)
		{
			foreach (ulong key in itemList.Keys)
			{
				ITEM_INFO iTEM_INFO = itemList[key];
				int itemId = iTEM_INFO.itemId;
				ulong uUID = iTEM_INFO.UUID;
				int itemIndex = iTEM_INFO.itemIndex;
				uint itemCount = iTEM_INFO.itemCount;
				inventory.addItemToInventory(itemId, uUID, (int)itemCount, itemIndex);
				inventory.updateItemList();
				inventory.stackableSettings();
			}
		}
		if (!((Object)(object)inventory2 != (Object)null))
		{
			return;
		}
		foreach (ulong key2 in equipItemDict.Keys)
		{
			ITEM_INFO iTEM_INFO2 = equipItemDict[key2];
			int itemId2 = iTEM_INFO2.itemId;
			ulong uUID2 = iTEM_INFO2.UUID;
			int itemIndex2 = iTEM_INFO2.itemIndex;
			inventory2.addItemToInventory(itemId2, uUID2, 1, itemIndex2);
			inventory2.updateItemList();
			inventory2.stackableSettings();
		}
	}

	public void set_equipWeapon(Entity dst, int itemId)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (dst.renderObj != null)
		{
			if (itemId == -1)
			{
				((GameObject)dst.renderObj).GetComponent<EquipWeapon>().clearWeapon();
			}
			else
			{
				((GameObject)dst.renderObj).GetComponent<EquipWeapon>().equipWeapon(itemId);
			}
		}
	}

	public void setSkillButton()
	{
		UI_MainUI.inst.setSkill();
	}
}
