using System;
using UnityEngine;

namespace KBEngine;

public class Entity
{
	public Vector3 _entityLastLocalPos = new Vector3(0f, 0f, 0f);

	public Vector3 _entityLastLocalDir = new Vector3(0f, 0f, 0f);

	public int id;

	public string className = "";

	public Vector3 position = new Vector3(0f, 0f, 0f);

	public Vector3 direction = new Vector3(0f, 0f, 0f);

	public float velocity;

	public bool isOnGround = true;

	public object renderObj;

	public bool inWorld;

	public bool isControlled;

	public bool inited;

	public static void clear()
	{
	}

	public virtual object getDefinedProperty(string name)
	{
		return null;
	}

	public void destroy()
	{
		detachComponents();
		onDestroy();
	}

	public virtual void onDestroy()
	{
	}

	public bool isPlayer()
	{
		return id == KBEngineApp.app.entity_id;
	}

	public virtual void onRemoteMethodCall(MemoryStream stream)
	{
	}

	public virtual void onUpdatePropertys(MemoryStream stream)
	{
	}

	public virtual void onGetBase()
	{
	}

	public virtual void onGetCell()
	{
	}

	public virtual void onLoseCell()
	{
	}

	public virtual void onComponentsEnterworld()
	{
	}

	public virtual void onComponentsLeaveworld()
	{
	}

	public virtual EntityCall getBaseEntityCall()
	{
		return null;
	}

	public virtual EntityCall getCellEntityCall()
	{
		return null;
	}

	public virtual void __init__()
	{
	}

	public virtual void callPropertysSetMethods()
	{
	}

	public virtual void attachComponents()
	{
	}

	public virtual void detachComponents()
	{
	}

	public void baseCall(string methodname, params object[] arguments)
	{
		if (KBEngineApp.app.currserver == "loginapp")
		{
			Dbg.ERROR_MSG(className + "::baseCall(" + methodname + "), currserver=!" + KBEngineApp.app.currserver);
			return;
		}
		ScriptModule value = null;
		if (!EntityDef.moduledefs.TryGetValue(className, out value))
		{
			Dbg.ERROR_MSG("entity::baseCall:  entity-module(" + className + ") error, can not find from EntityDef.moduledefs");
			return;
		}
		Method value2 = null;
		if (!value.base_methods.TryGetValue(methodname, out value2))
		{
			Dbg.ERROR_MSG(className + "::baseCall(" + methodname + "), not found method!");
			return;
		}
		ushort methodUtype = value2.methodUtype;
		if (arguments.Length != value2.args.Count)
		{
			Dbg.ERROR_MSG(className + "::baseCall(" + methodname + "): args(" + arguments.Length + "!= " + value2.args.Count + ") size is error!");
			return;
		}
		EntityCall baseEntityCall = getBaseEntityCall();
		baseEntityCall.newCall();
		baseEntityCall.bundle.writeUint16(0);
		baseEntityCall.bundle.writeUint16(methodUtype);
		try
		{
			for (int i = 0; i < value2.args.Count; i++)
			{
				if (value2.args[i].isSameType(arguments[i]))
				{
					value2.args[i].addToStream(baseEntityCall.bundle, arguments[i]);
					continue;
				}
				throw new Exception("arg" + i + ": " + value2.args[i].ToString());
			}
		}
		catch (Exception ex)
		{
			Dbg.ERROR_MSG(className + "::baseCall(method=" + methodname + "): args is error(" + ex.Message + ")!");
			baseEntityCall.bundle = null;
			return;
		}
		baseEntityCall.sendCall(null);
	}

	public void cellCall(string methodname, params object[] arguments)
	{
		if (KBEngineApp.app.currserver == "loginapp")
		{
			Dbg.ERROR_MSG(className + "::cellCall(" + methodname + "), currserver=!" + KBEngineApp.app.currserver);
			return;
		}
		ScriptModule value = null;
		if (!EntityDef.moduledefs.TryGetValue(className, out value))
		{
			Dbg.ERROR_MSG("entity::cellCall:  entity-module(" + className + ") error, can not find from EntityDef.moduledefs!");
			return;
		}
		Method value2 = null;
		if (!value.cell_methods.TryGetValue(methodname, out value2))
		{
			Dbg.ERROR_MSG(className + "::cellCall(" + methodname + "), not found method!");
			return;
		}
		ushort methodUtype = value2.methodUtype;
		if (arguments.Length != value2.args.Count)
		{
			Dbg.ERROR_MSG(className + "::cellCall(" + methodname + "): args(" + arguments.Length + "!= " + value2.args.Count + ") size is error!");
			return;
		}
		EntityCall cellEntityCall = getCellEntityCall();
		if (cellEntityCall == null)
		{
			Dbg.ERROR_MSG(className + "::cellCall(" + methodname + "): no cell!");
			return;
		}
		cellEntityCall.newCall();
		cellEntityCall.bundle.writeUint16(0);
		cellEntityCall.bundle.writeUint16(methodUtype);
		try
		{
			for (int i = 0; i < value2.args.Count; i++)
			{
				if (value2.args[i].isSameType(arguments[i]))
				{
					value2.args[i].addToStream(cellEntityCall.bundle, arguments[i]);
					continue;
				}
				throw new Exception("arg" + i + ": " + value2.args[i].ToString());
			}
		}
		catch (Exception ex)
		{
			Dbg.ERROR_MSG(className + "::cellCall(" + methodname + "): args is error(" + ex.Message + ")!");
			cellEntityCall.bundle = null;
			return;
		}
		cellEntityCall.sendCall(null);
	}

	public void enterWorld()
	{
		inWorld = true;
		try
		{
			onEnterWorld();
			onComponentsEnterworld();
		}
		catch (Exception ex)
		{
			Dbg.ERROR_MSG(className + "::onEnterWorld: error=" + ex.ToString());
		}
		Event.fireOut("onEnterWorld", this);
	}

	public virtual void onEnterWorld()
	{
	}

	public void leaveWorld()
	{
		inWorld = false;
		try
		{
			onLeaveWorld();
			onComponentsLeaveworld();
		}
		catch (Exception ex)
		{
			Dbg.ERROR_MSG(className + "::onLeaveWorld: error=" + ex.ToString());
		}
		Event.fireOut("onLeaveWorld", this);
	}

	public virtual void onLeaveWorld()
	{
	}

	public virtual void enterSpace()
	{
		inWorld = true;
		try
		{
			onEnterSpace();
		}
		catch (Exception ex)
		{
			Dbg.ERROR_MSG(className + "::onEnterSpace: error=" + ex.ToString());
		}
		Event.fireOut("onEnterSpace", this);
		Event.fireOut("set_position", this);
		Event.fireOut("set_direction", this);
	}

	public virtual void onEnterSpace()
	{
	}

	public virtual void leaveSpace()
	{
		inWorld = false;
		try
		{
			onLeaveSpace();
		}
		catch (Exception ex)
		{
			Dbg.ERROR_MSG(className + "::onLeaveSpace: error=" + ex.ToString());
		}
		Event.fireOut("onLeaveSpace", this);
	}

	public virtual void onLeaveSpace()
	{
	}

	public virtual void onPositionChanged(Vector3 oldValue)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (isPlayer())
		{
			KBEngineApp.app.entityServerPos(position);
		}
		if (inWorld)
		{
			Event.fireOut("set_position", this);
		}
	}

	public virtual void onUpdateVolatileData()
	{
	}

	public virtual void onDirectionChanged(Vector3 oldValue)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (inWorld)
		{
			direction.x = direction.x * 360f / ((float)Math.PI * 2f);
			direction.y = direction.y * 360f / ((float)Math.PI * 2f);
			direction.z = direction.z * 360f / ((float)Math.PI * 2f);
			Event.fireOut("set_direction", this);
		}
		else
		{
			direction = oldValue;
		}
	}

	public virtual void onControlled(bool isControlled_)
	{
	}
}
