using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FF1 RID: 4081
	public abstract class SpawnPointBase : Entity
	{
		// Token: 0x060060B6 RID: 24758 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x00043129 File Offset: 0x00041329
		public SpawnPointBase()
		{
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x00043144 File Offset: 0x00041344
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_SpawnPointBase(this.id, this.className);
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x0004315D File Offset: 0x0004135D
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_SpawnPointBase(this.id, this.className);
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x00043176 File Offset: 0x00041376
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x0004317F File Offset: 0x0004137F
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x00043187 File Offset: 0x00041387
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x060060C3 RID: 24771 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x0026A984 File Offset: 0x00268B84
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["SpawnPoint"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x0026AA04 File Offset: 0x00268C04
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["SpawnPoint"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				ushort properUtype2 = property.properUtype;
				switch (properUtype2)
				{
				case 40000:
				{
					Vector3 position = this.position;
					this.position = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onPositionChanged(position);
						}
					}
					else if (this.inWorld)
					{
						this.onPositionChanged(position);
					}
					break;
				}
				case 40001:
				{
					Vector3 direction = this.direction;
					this.direction = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onDirectionChanged(direction);
						}
					}
					else if (this.inWorld)
					{
						this.onDirectionChanged(direction);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				default:
					switch (properUtype2)
					{
					case 41003:
					{
						string oldValue = this.name;
						this.name = stream.readUnicode();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onNameChanged(oldValue);
							}
						}
						else if (this.inWorld)
						{
							this.onNameChanged(oldValue);
						}
						break;
					}
					case 41004:
					{
						uint oldValue2 = this.uid;
						this.uid = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUidChanged(oldValue2);
							}
						}
						else if (this.inWorld)
						{
							this.onUidChanged(oldValue2);
						}
						break;
					}
					case 41005:
					{
						uint oldValue3 = this.utype;
						this.utype = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUtypeChanged(oldValue3);
							}
						}
						else if (this.inWorld)
						{
							this.onUtypeChanged(oldValue3);
						}
						break;
					}
					case 41006:
					{
						uint oldValue4 = this.modelID;
						this.modelID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelIDChanged(oldValue4);
							}
						}
						else if (this.inWorld)
						{
							this.onModelIDChanged(oldValue4);
						}
						break;
					}
					case 41007:
					{
						byte oldValue5 = this.modelScale;
						this.modelScale = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelScaleChanged(oldValue5);
							}
						}
						else if (this.inWorld)
						{
							this.onModelScaleChanged(oldValue5);
						}
						break;
					}
					}
					break;
				}
			}
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x0026ACD8 File Offset: 0x00268ED8
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["SpawnPoint"].idpropertys;
			Vector3 direction = this.direction;
			Property property = idpropertys[2];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			uint oldValue = this.modelID;
			Property property2 = idpropertys[4];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue);
			}
			byte oldValue2 = this.modelScale;
			Property property3 = idpropertys[5];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue2);
			}
			string oldValue3 = this.name;
			Property property4 = idpropertys[6];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue3);
			}
			Vector3 position = this.position;
			Property property5 = idpropertys[1];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue4 = this.uid;
			Property property6 = idpropertys[7];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue4);
			}
			uint oldValue5 = this.utype;
			Property property7 = idpropertys[8];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue5);
					return;
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue5);
			}
		}

		// Token: 0x04005BD9 RID: 23513
		public EntityBaseEntityCall_SpawnPointBase baseEntityCall;

		// Token: 0x04005BDA RID: 23514
		public EntityCellEntityCall_SpawnPointBase cellEntityCall;

		// Token: 0x04005BDB RID: 23515
		public uint modelID;

		// Token: 0x04005BDC RID: 23516
		public byte modelScale = 30;

		// Token: 0x04005BDD RID: 23517
		public string name = "";

		// Token: 0x04005BDE RID: 23518
		public uint uid;

		// Token: 0x04005BDF RID: 23519
		public uint utype;
	}
}
