using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Batbrain : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '2': // Normally, Batbrains are only used in just this folder
				case '1': // However, since there are more Origins Mission Zones use this object, we gotta expand a bit
				case '3':
				default:
					sprite = new Sprite(LevelData.GetSpriteSheet("MZ/Objects.gif").GetSection(37, 98, 14, 24), -7, -12);
					break;
				case '7':
					sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(52, 68, 14, 24), -7, -12);
					break;
			}
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
	}
}