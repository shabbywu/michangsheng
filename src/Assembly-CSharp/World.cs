using System;
using System.Collections.Generic;
using System.Linq;
using Fight;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YSGame;

// Token: 0x02000659 RID: 1625
public class World : MonoBehaviour
{
	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x0600286F RID: 10351 RVA: 0x0001FA67 File Offset: 0x0001DC67
	// (set) Token: 0x06002870 RID: 10352 RVA: 0x0001FA8B File Offset: 0x0001DC8B
	public static World instance
	{
		get
		{
			if (World._instance != null)
			{
				return World._instance;
			}
			World._instance = new World();
			return World._instance;
		}
		set
		{
			World._instance = value;
		}
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x06002873 RID: 10355 RVA: 0x0013BED8 File Offset: 0x0013A0D8
	public void init()
	{
		World.inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
		GameObject gameObject = new GameObject("World");
		Object.DontDestroyOnLoad(gameObject);
		World.instance = gameObject.AddComponent<World>();
		World.instance.terrainPerfab = (GameObject)Resources.Load("Terrain");
		World.instance.otherPlayerPerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/Character");
		World.instance.gatePerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/Gate");
		World.instance.avatarPerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/Character");
		World.instance.snowBallPerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/snowBall");
		World.instance.droppedItemPerfab = (GameObject)Resources.Load("Effect/Prefab/gameEntity/droppedItem");
		World.instance.allGameEntity.Add("Zombie", (GameObject)Resources.Load("Effect/Prefab/gameEntity/Zombie"));
		World.instance.allGameEntity.Add("In-Game GUI", (GameObject)Resources.Load("Effect/Prefab/gameUI/In-Game GUI"));
		World.instance.allGameEntity.Add("_Game Controller", (GameObject)Resources.Load("Effect/Prefab/gameUI/_Game Controller"));
	}

	// Token: 0x06002874 RID: 10356 RVA: 0x0013C014 File Offset: 0x0013A214
	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Target");
		if (gameObject)
		{
			this.ui_target = gameObject.GetComponent<UI_Target>();
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

	// Token: 0x06002875 RID: 10357 RVA: 0x0013C174 File Offset: 0x0013A374
	public void set_Buffs(Entity entity, List<ushort> oldValue, List<ushort> newValue)
	{
		if (oldValue.Count<ushort>() > newValue.Count<ushort>())
		{
			return;
		}
		if (entity.renderObj != null)
		{
			List<ushort> list = newValue.Except(oldValue).ToList<ushort>();
			list.AddRange(oldValue.Except(newValue).ToList<ushort>());
			foreach (ushort buffid in list)
			{
				this.displayBuff(entity, (int)buffid);
			}
		}
	}

	// Token: 0x06002876 RID: 10358 RVA: 0x0013C1F8 File Offset: 0x0013A3F8
	public void displayBuff(Entity entity, int buffid)
	{
		string str = jsonData.instance.BuffJsonData[string.Concat(buffid)]["skillEffect"].str;
		if (str != "")
		{
			Vector3 position = ((GameObject)entity.renderObj).transform.position;
			Object.Destroy(Object.Instantiate(ResManager.inst.LoadSkillEffect(str), ((GameObject)entity.renderObj).transform), jsonData.instance.BuffJsonData[string.Concat(buffid)]["totaltime"].n);
		}
	}

	// Token: 0x06002877 RID: 10359 RVA: 0x0001FA93 File Offset: 0x0001DC93
	public void goToHome()
	{
		SceneManager.LoadScene("Mainmenu");
		SceneManager.LoadSceneAsync("homeScene", 1);
		Object.Destroy(World.instance.gameObject);
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x0001FABA File Offset: 0x0001DCBA
	private void OnDestroy()
	{
		World.instance = null;
		Event.deregisterOut(this);
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x0013C2A0 File Offset: 0x0013A4A0
	public void onAvatarEnterWorld(ulong rndUUID, int eid, Avatar avatar)
	{
		if (!avatar.isPlayer())
		{
			return;
		}
		this.createPlayer();
		Debug.Log("loading scene...");
		object obj = avatar.state;
		if (obj != null)
		{
			this.set_state(avatar, obj);
		}
		object name = avatar.name;
		if (name != null)
		{
			this.set_entityName(avatar, (string)name);
		}
		object obj2 = avatar.HP;
		if (obj2 != null)
		{
			this.set_HP(avatar, obj2);
		}
		object obj3 = avatar.HP_Max;
		if (obj3 != null)
		{
			this.set_HP_Max(avatar, obj3);
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("TargetPlayer");
		if (gameObject)
		{
			this.ui_targetPlayer = gameObject.GetComponent<UI_Target>();
		}
		if (this.ui_targetPlayer)
		{
			this.ui_targetPlayer.GE_target = this.player.GetComponent<GameEntity>();
		}
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("Canvas");
		GameObject gameObject3 = null;
		if (gameObject2.transform.Find("Panel - State") != null)
		{
			gameObject3 = gameObject2.transform.Find("Panel - State").gameObject;
		}
		if (gameObject3 != null)
		{
			UI_AvatarState component = gameObject3.GetComponent<UI_AvatarState>();
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
			this.set_equipWeapon(avatar, (int)obj14);
		}
		SkillBox.inst.initSkillDisplay();
		GameObject gameObject4 = GameObject.Find("Male_Player");
		GameObject gameObject5 = (GameObject)KBEngineApp.app.player().renderObj;
		gameObject4.transform.parent = gameObject5.transform;
		gameObject4.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0600287B RID: 10363 RVA: 0x0013C56C File Offset: 0x0013A76C
	public void onEnterWorld(Entity entity)
	{
		float num = entity.position.y;
		if (entity.isOnGround)
		{
			num = 0f;
		}
		if (entity.className == "Gate")
		{
			entity.renderObj = Object.Instantiate<GameObject>(this.gatePerfab, new Vector3(entity.position.x, num, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
			((GameObject)entity.renderObj).GetComponent<GameEntity>().entityDisable();
		}
		else if (entity.className == "Monster" || entity.className == "Pet")
		{
			this.createMonster(entity);
		}
		else if (entity.className == "DroppedItem")
		{
			entity.renderObj = Object.Instantiate<GameObject>(this.droppedItemPerfab, new Vector3(entity.position.x, num, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
		}
		else if (entity.className == "Avatar")
		{
			this.creatAvater(entity);
		}
		else if (entity.className == "NPC")
		{
			this.createNPC(entity);
		}
		else if (entity.className == "Build")
		{
			this.CreatBuild(entity);
		}
		((GameObject)entity.renderObj).name = entity.className + "_" + entity.id;
		this.set_position(entity);
		this.set_direction(entity);
		object definedProperty = entity.getDefinedProperty("state");
		if (definedProperty != null)
		{
			this.set_state(entity, definedProperty);
		}
		object definedProperty2 = entity.getDefinedProperty("name");
		if (definedProperty2 != null)
		{
			this.set_entityName(entity, (string)definedProperty2);
		}
		object definedProperty3 = entity.getDefinedProperty("HP");
		if (definedProperty3 != null)
		{
			this.set_HP(entity, definedProperty3);
		}
		object definedProperty4 = entity.getDefinedProperty("HP_Max");
		if (definedProperty4 != null)
		{
			this.set_HP_Max(entity, definedProperty4);
		}
		object definedProperty5 = entity.getDefinedProperty("equipWeapon");
		if (definedProperty5 != null)
		{
			this.set_equipWeapon(entity, (int)definedProperty5);
		}
	}

	// Token: 0x0600287C RID: 10364 RVA: 0x0013C7C0 File Offset: 0x0013A9C0
	public void createMonster(Entity entity)
	{
		this.createInstantiate("Effect/Prefab/gameEntity/Monster/Monster", entity, "Monster");
		this.createRoleScript<MonsterAddScript>(entity);
		GameObject gameObject = (GameObject)entity.renderObj;
		if (entity.className == "Monster")
		{
			gameObject.GetComponent<GameEntity>().canAttack = true;
		}
	}

	// Token: 0x0600287D RID: 10365 RVA: 0x0013C810 File Offset: 0x0013AA10
	public void createNPC(Entity entity)
	{
		GameObject gameObject = (GameObject)Resources.Load("Effect/Prefab/gameEntity/NPC/NPC1/NPC1_1");
		entity.renderObj = Object.Instantiate<GameObject>(gameObject, new Vector3(entity.position.x, entity.position.y, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x0013C88C File Offset: 0x0013AA8C
	public void creatAvater(Entity entity)
	{
		this.createInstantiate("Effect/Prefab/gameEntity/Avater/Avater", entity, "Avater");
		this.createRoleScript<AvaterAddScript>(entity);
		((GameObject)entity.renderObj).GetComponent<GameEntity>().scale = new Vector3(0.75f, 0.75f, 0.75f);
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x0013C8DC File Offset: 0x0013AADC
	public void createRoleScript<T>(Entity entity) where T : BaseAddScript
	{
		object definedProperty = entity.getDefinedProperty("roleTypeCell");
		object definedProperty2 = entity.getDefinedProperty("roleSurfaceCall");
		BaseAddScript baseAddScript = ((GameObject)entity.renderObj).GetComponent<T>();
		if (baseAddScript == null)
		{
			((GameObject)entity.renderObj).AddComponent<T>();
			baseAddScript = ((GameObject)entity.renderObj).GetComponent<T>();
		}
		baseAddScript.nowRoleType = (int)((uint)definedProperty);
		baseAddScript.nowRoleFace = (int)((ushort)definedProperty2);
		baseAddScript.entity = entity;
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x0013C968 File Offset: 0x0013AB68
	public void createInstantiate(string patch, Entity entity, string entitytype)
	{
		float y = entity.position.y;
		bool isOnGround = entity.isOnGround;
		Avatar avatar = (Avatar)entity;
		uint roleTypeCell = avatar.roleTypeCell;
		ushort roleSurfaceCall = avatar.roleSurfaceCall;
		string text = string.Concat(new object[]
		{
			patch,
			roleTypeCell,
			"/",
			entitytype,
			roleTypeCell,
			"_",
			roleSurfaceCall
		});
		string text2 = string.Concat(new object[]
		{
			patch,
			49 + avatar.Sex,
			"/",
			entitytype,
			49 + avatar.Sex,
			"_",
			roleSurfaceCall
		});
		GameObject gameObject = (GameObject)Resources.Load((roleTypeCell == 0U) ? text2 : text);
		entity.renderObj = new GameObject(entitytype + "_" + entity.id);
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, new Vector3(entity.position.x, y, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
		gameObject2.transform.parent = ((GameObject)entity.renderObj).transform;
		gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x0013CAE8 File Offset: 0x0013ACE8
	public void CreatBuild(Entity entity)
	{
		object definedProperty = entity.getDefinedProperty("BuildId");
		if (definedProperty != null)
		{
			ItemData itemData;
			UltimateSurvival.MonoSingleton<InventoryController>.Instance.Database.FindItemById((int)definedProperty, out itemData);
			entity.renderObj = Object.Instantiate<GameObject>(itemData.WorldObject, new Vector3(entity.position.x, entity.position.y, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
		}
	}

	// Token: 0x06002882 RID: 10370 RVA: 0x0013CB80 File Offset: 0x0013AD80
	public void onLeaveWorld(Entity entity)
	{
		if (entity.renderObj == null)
		{
			return;
		}
		GameEntity component = ((GameObject)entity.renderObj).GetComponent<GameEntity>();
		if (this.getUITarget().GE_target == component)
		{
			this.getUITarget().deactivate();
		}
		Object.Destroy((GameObject)entity.renderObj);
		entity.renderObj = null;
	}

	// Token: 0x06002883 RID: 10371 RVA: 0x0001FAC9 File Offset: 0x0001DCC9
	public void set_entityName(Entity entity, object v)
	{
		if (entity.renderObj != null)
		{
			((GameObject)entity.renderObj).GetComponent<GameEntity>().entity_name = (string)v;
		}
	}

	// Token: 0x06002884 RID: 10372 RVA: 0x0013CBDC File Offset: 0x0013ADDC
	public void set_position(Entity entity)
	{
		if (entity.renderObj == null)
		{
			return;
		}
		Vector3 position = entity.position;
		((GameObject)entity.renderObj).GetComponent<GameEntity>().destPosition = position;
		((GameObject)entity.renderObj).GetComponent<GameEntity>().position = position;
	}

	// Token: 0x06002885 RID: 10373 RVA: 0x0013CC28 File Offset: 0x0013AE28
	public void set_direction(Entity entity)
	{
		if (entity.renderObj == null)
		{
			return;
		}
		GameObject gameObject = (GameObject)entity.renderObj;
		((GameObject)entity.renderObj).GetComponent<GameEntity>().destDirection = new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x0013CC88 File Offset: 0x0013AE88
	public UI_Target getUITarget()
	{
		if (this.ui_target == null)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("Target");
			if (gameObject)
			{
				this.ui_target = gameObject.GetComponent<UI_Target>();
			}
		}
		return this.ui_target;
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x000042DD File Offset: 0x000024DD
	public void set_HP(Entity entity, object v)
	{
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x000042DD File Offset: 0x000024DD
	public void set_HP_Max(Entity entity, object v)
	{
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x0013CCC8 File Offset: 0x0013AEC8
	public void GameOver()
	{
		if (RoundManager.instance != null && RoundManager.instance.gameOverSwitch == 0)
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

	// Token: 0x0600288A RID: 10378 RVA: 0x0013CD40 File Offset: 0x0013AF40
	public void set_state(Entity entity, object v)
	{
		if (entity.renderObj != null)
		{
			if ((sbyte)v == 1)
			{
				((GameObject)entity.renderObj).transform.GetChild(0).GetComponent<Animator>().speed = 1f;
				((GameObject)entity.renderObj).transform.GetChild(0).GetComponent<Animator>().Play("Dead");
				Queue<UnityAction> queue = new Queue<UnityAction>();
				UnityAction item = delegate()
				{
					this.GameOver();
					YSFuncList.Ints.Continue();
				};
				queue.Enqueue(item);
				YSFuncList.Ints.AddFunc(queue);
				return;
			}
			((GameObject)entity.renderObj).transform.GetChild(0).GetComponent<Animator>().speed = 1f;
			((GameObject)entity.renderObj).transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 0f);
			((GameObject)entity.renderObj).transform.GetChild(0).GetComponent<Animator>().Play("Idle");
		}
	}

	// Token: 0x0600288B RID: 10379 RVA: 0x0013CE48 File Offset: 0x0013B048
	public void createPlayer()
	{
		if (this.player != null)
		{
			return;
		}
		if (KBEngineApp.app.entity_type != "Avatar")
		{
			return;
		}
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar == null)
		{
			Debug.Log("wait create(palyer)!");
			return;
		}
		this.createInstantiate("Effect/Prefab/gameEntity/Avater/Avater", avatar, "Avater");
		this.player = (GameObject)avatar.renderObj;
		this.createRoleScript<AvaterAddScript>(avatar);
		this.initPlayer(this.player);
		avatar.renderObj = this.player;
		Camera.main.GetComponent<SmoothFollow>().target = this.player.transform;
		for (int i = 0; i < Camera.allCameras.Length; i++)
		{
			if (Camera.allCameras[i].GetComponent<MapFollow>() != null)
			{
				Camera.allCameras[i].GetComponent<MapFollow>().target = this.player.transform;
			}
		}
		((GameObject)avatar.renderObj).GetComponent<GameEntity>().isPlayer = true;
	}

	// Token: 0x0600288C RID: 10380 RVA: 0x0013CF4C File Offset: 0x0013B14C
	private void initPlayer(GameObject player)
	{
		if (player == null)
		{
			return;
		}
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar == null)
		{
			Debug.Log("wait create(palyer)!");
			return;
		}
		avatar.reqItemList();
	}

	// Token: 0x0600288D RID: 10381 RVA: 0x0013CF88 File Offset: 0x0013B188
	public void addSpaceGeometryMapping(string respath)
	{
		Debug.Log("loading scene(" + respath + ")...");
		MonoBehaviour.print(string.Concat(new object[]
		{
			"scene(",
			respath,
			"), spaceID=",
			KBEngineApp.app.spaceID
		}));
		this.terrain == null;
	}

	// Token: 0x0600288E RID: 10382 RVA: 0x0001FAEE File Offset: 0x0001DCEE
	public void updatePosition(Entity entity)
	{
		if (entity.renderObj == null)
		{
			return;
		}
		GameEntity component = ((GameObject)entity.renderObj).GetComponent<GameEntity>();
		component.destPosition = entity.position;
		component.isOnGround = entity.isOnGround;
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x0013CFF0 File Offset: 0x0013B1F0
	public void recvDamage(Entity entity, Entity attacker, int skillID, int damageType, int damage)
	{
		if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
		{
			return;
		}
		if (skillID == 0)
		{
			return;
		}
		if (damage < 0)
		{
			GameObject gameObject = Object.Instantiate(ResManager.inst.LoadSkillEffect("huifu")) as GameObject;
			if (entity == attacker)
			{
				if (entity.isPlayer())
				{
					gameObject.transform.localPosition = new Vector3(-7.5f, 3.41f, 1f);
				}
				else
				{
					gameObject.transform.localPosition = new Vector3(2.7f, 3.41f, 1f);
				}
			}
			else if (attacker.isPlayer())
			{
				gameObject.transform.localPosition = new Vector3(-7.5f, 3.41f, 1f);
			}
			else
			{
				gameObject.transform.localPosition = new Vector3(2.7f, 3.41f, 1f);
			}
		}
		Queue<UnityAction> queue = new Queue<UnityAction>();
		Skill sk = SkillBox.inst.get(skillID);
		GameObject gameObject2 = (GameObject)attacker.renderObj;
		GameObject entityEntity = (GameObject)entity.renderObj;
		if (sk != null && jsonData.instance.skillJsonData[skillID.ToString()]["script"].str == "SkillAttack" && entity == attacker && damage < 0)
		{
			UnityAction item = delegate()
			{
				entityEntity.GetComponentInChildren<AvatarShowHpDamage>().show(damage, 0);
				this.Invoke("continuFunc", 0.1f);
			};
			queue.Enqueue(item);
		}
		else if (sk != null && entity != attacker)
		{
			Vector3 vector = entity.position - attacker.position;
			UnityAction item2 = delegate()
			{
				sk.displaySkill(attacker, entity);
			};
			UnityAction item3 = delegate()
			{
				if (damage > 0)
				{
					Transform child = entityEntity.transform.GetChild(0);
					if (child == null)
					{
						YSFuncList.Ints.Continue();
						return;
					}
					Animator component = child.GetComponent<Animator>();
					if (component == null)
					{
						YSFuncList.Ints.Continue();
						return;
					}
					component.Play("Hit", -1, 0f);
				}
				YSFuncList.Ints.Continue();
			};
			UnityAction item4 = delegate()
			{
				entityEntity.GetComponentInChildren<AvatarShowHpDamage>().show(damage, 0);
				this.Invoke("continuFunc", 0.1f);
			};
			queue.Enqueue(item2);
			queue.Enqueue(item3);
			queue.Enqueue(item4);
			if (attacker.isPlayer())
			{
				gameObject2.transform.LookAt(new Vector3(gameObject2.transform.position.x + vector.x, gameObject2.transform.position.y, gameObject2.transform.position.z + vector.z));
			}
		}
		else if (entity == attacker)
		{
			UnityAction item5 = delegate()
			{
				sk.displaySkill(attacker, entity);
			};
			UnityAction item6 = delegate()
			{
				if (damage > 0)
				{
					Transform child = entityEntity.transform.GetChild(0);
					if (child == null)
					{
						YSFuncList.Ints.Continue();
						return;
					}
					Animator component = child.GetComponent<Animator>();
					if (component == null)
					{
						YSFuncList.Ints.Continue();
						return;
					}
					component.Play("Hit", -1, 0f);
				}
				YSFuncList.Ints.Continue();
			};
			UnityAction item7 = delegate()
			{
				if (entityEntity.GetComponentInChildren<AvatarShowHpDamage>() != null)
				{
					entityEntity.GetComponentInChildren<AvatarShowHpDamage>().show(damage, 0);
				}
				this.Invoke("continuFunc", 0.1f);
			};
			queue.Enqueue(item5);
			queue.Enqueue(item6);
			queue.Enqueue(item7);
		}
		YSFuncList.Ints.AddFunc(queue);
		YSFuncList.Ints.Start();
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x000112BB File Offset: 0x0000F4BB
	public void continuFunc()
	{
		YSFuncList.Ints.Continue();
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x0013D2FC File Offset: 0x0013B4FC
	public void recvSkill(int attackerID, int skillID)
	{
		Entity entity = KBEngineApp.app.entities[attackerID];
		Skill skill = SkillBox.inst.get(skillID);
		if (skill != null)
		{
			GameObject gameObject = (GameObject)entity.renderObj;
			gameObject.transform.GetChild(0).GetComponent<Animator>().Play("Punch", -1, 0f);
			gameObject.transform.GetChild(0).GetComponent<Animator>().Update(0f);
			skill.displaySkill(entity);
		}
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x0013D378 File Offset: 0x0013B578
	public void onReqItemList(Dictionary<ulong, ITEM_INFO> itemList, Dictionary<ulong, ITEM_INFO> equipItemDict)
	{
		GameObject gameObject = (GameObject)KBEngineApp.app.player().renderObj;
		Inventory inventory = null;
		Inventory inventory2 = null;
		if (gameObject != null)
		{
			inventory = gameObject.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
			inventory2 = gameObject.GetComponent<PlayerInventory>().characterSystem.GetComponent<Inventory>();
		}
		if (inventory != null)
		{
			foreach (ulong key in itemList.Keys)
			{
				ITEM_INFO item_INFO = itemList[key];
				int itemId = item_INFO.itemId;
				ulong uuid = item_INFO.UUID;
				int itemIndex = item_INFO.itemIndex;
				uint itemCount = item_INFO.itemCount;
				inventory.addItemToInventory(itemId, uuid, (int)itemCount, itemIndex);
				inventory.updateItemList();
				inventory.stackableSettings();
			}
		}
		if (inventory2 != null)
		{
			foreach (ulong key2 in equipItemDict.Keys)
			{
				ITEM_INFO item_INFO2 = equipItemDict[key2];
				int itemId2 = item_INFO2.itemId;
				ulong uuid2 = item_INFO2.UUID;
				int itemIndex2 = item_INFO2.itemIndex;
				inventory2.addItemToInventory(itemId2, uuid2, 1, itemIndex2);
				inventory2.updateItemList();
				inventory2.stackableSettings();
			}
		}
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x0001FB20 File Offset: 0x0001DD20
	public void set_equipWeapon(Entity dst, int itemId)
	{
		if (dst.renderObj == null)
		{
			return;
		}
		if (itemId == -1)
		{
			((GameObject)dst.renderObj).GetComponent<EquipWeapon>().clearWeapon();
			return;
		}
		((GameObject)dst.renderObj).GetComponent<EquipWeapon>().equipWeapon(itemId);
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x0001FB5B File Offset: 0x0001DD5B
	public void setSkillButton()
	{
		UI_MainUI.inst.setSkill();
	}

	// Token: 0x0400222A RID: 8746
	public static World _instance;

	// Token: 0x0400222B RID: 8747
	private GameObject terrain;

	// Token: 0x0400222C RID: 8748
	public GameObject terrainPerfab;

	// Token: 0x0400222D RID: 8749
	private GameObject player;

	// Token: 0x0400222E RID: 8750
	public GameObject otherPlayerPerfab;

	// Token: 0x0400222F RID: 8751
	public GameObject gatePerfab;

	// Token: 0x04002230 RID: 8752
	public GameObject avatarPerfab;

	// Token: 0x04002231 RID: 8753
	public GameObject snowBallPerfab;

	// Token: 0x04002232 RID: 8754
	public GameObject droppedItemPerfab;

	// Token: 0x04002233 RID: 8755
	private Dictionary<string, GameObject> allGameEntity = new Dictionary<string, GameObject>();

	// Token: 0x04002234 RID: 8756
	private bool isFirstPos = true;

	// Token: 0x04002235 RID: 8757
	private UI_Target ui_target;

	// Token: 0x04002236 RID: 8758
	private UI_Target ui_targetPlayer;

	// Token: 0x04002237 RID: 8759
	public static ItemDataBaseList inventoryItemList;
}
