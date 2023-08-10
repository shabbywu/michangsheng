namespace KBEngine;

public class EntityCall
{
	public enum ENTITYCALL_TYPE
	{
		ENTITYCALL_TYPE_CELL,
		ENTITYCALL_TYPE_BASE
	}

	public int id;

	public string className = "";

	public ENTITYCALL_TYPE type;

	public Bundle bundle;

	public EntityCall(int eid, string ename)
	{
		id = eid;
		className = ename;
	}

	public virtual void __init__()
	{
	}

	public virtual bool isBase()
	{
		return type == ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}

	public virtual bool isCell()
	{
		return type == ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}

	public Bundle newCall()
	{
		if (bundle == null)
		{
			bundle = ObjectPool<Bundle>.createObject();
		}
		if (isCell())
		{
			bundle.newMessage(Messages.messages["Baseapp_onRemoteCallCellMethodFromClient"]);
		}
		else
		{
			bundle.newMessage(Messages.messages["Entity_onRemoteMethodCall"]);
		}
		bundle.writeInt32(id);
		return bundle;
	}

	public void sendCall(Bundle inbundle)
	{
		if (inbundle == null)
		{
			inbundle = bundle;
		}
		inbundle.send(KBEngineApp.app.networkInterface());
		if (inbundle == bundle)
		{
			bundle = null;
		}
	}

	public Bundle newCall(string methodName, ushort entitycomponentPropertyID = 0)
	{
		if (KBEngineApp.app.currserver == "loginapp")
		{
			Dbg.ERROR_MSG(className + "::newCall(" + methodName + "), currserver=!" + KBEngineApp.app.currserver);
			return null;
		}
		ScriptModule value = null;
		if (!EntityDef.moduledefs.TryGetValue(className, out value))
		{
			Dbg.ERROR_MSG(className + "::newCall: entity-module(" + className + ") error, can not find from EntityDef.moduledefs");
			return null;
		}
		Method method = null;
		method = ((!isCell()) ? value.base_methods[methodName] : value.cell_methods[methodName]);
		ushort methodUtype = method.methodUtype;
		newCall();
		bundle.writeUint16(entitycomponentPropertyID);
		bundle.writeUint16(methodUtype);
		return bundle;
	}
}
public struct ENTITYCALL
{
	private byte[] value;

	public byte this[int ID]
	{
		get
		{
			return value[ID];
		}
		set
		{
			this.value[ID] = value;
		}
	}

	private ENTITYCALL(byte[] value)
	{
		this.value = value;
	}

	public static implicit operator byte[](ENTITYCALL value)
	{
		return value.value;
	}

	public static implicit operator ENTITYCALL(byte[] value)
	{
		return new ENTITYCALL(value);
	}
}
