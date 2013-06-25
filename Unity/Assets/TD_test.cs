using TunerData;
namespace TDStruct
{
public class test : ITDStruct
	{
		public int ScriptID;
		public int QuestIdx;
		public int Jiangli;
		public int ChengFa;
		public int DuiHua;
		public int FaBuRen;
		public int FaBuRenId;
		public int JieShouRen;
		public string JieShouRenId;
		public float TargetScene;
		public int TargetX;
		public int TargetZ;
		public void Init(Row row)
		{
			ScriptID = row.GetField(0).GetInt();
			QuestIdx = row.GetField(1).GetInt();
			Jiangli = row.GetField(2).GetInt();
			ChengFa = row.GetField(3).GetInt();
			DuiHua = row.GetField(4).GetInt();
			FaBuRen = row.GetField(5).GetInt();
			FaBuRenId = row.GetField(6).GetInt();
			JieShouRen = row.GetField(7).GetInt();
			JieShouRenId = row.GetField(8).GetString();
			TargetScene = row.GetField(9).GetFloat();
			TargetX = row.GetField(10).GetInt();
			TargetZ = row.GetField(11).GetInt();
		}
	}
}
