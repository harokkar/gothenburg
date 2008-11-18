//gmcs db_operations.cs -r:System.Data.dll -r:Mono.Data.Sqlite.dll

using System;
//using System.Data;
using Mono.Data.Sqlite;
//using System.IO;
//using asset_provider;

using NDesk.DBus;
using org.freedesktop.DBus;


namespace db_operations
{
	public class db_operations
	{
		private static SqliteConnection connection;
	
		public static void createdb()
		{
	
		}
		
		public int foo(int a)
		{
			if(a==1)
				return 2;
			else
				return 3;		
		}
	
		private static void listdb(string sqlstring, int field)
		{
			SqliteCommand cmd = connection.CreateCommand();
			cmd.CommandText = sqlstring;
			SqliteDataReader reader = cmd.ExecuteReader();	
				
			while(reader.Read())
			{
				string str = reader.GetString(field);
				Console.WriteLine(str + " ");
			}

			reader.Close();
			cmd.Dispose();
			cmd = null;
		}
	
		/*private object ExecuteScalar(string command)
		{
			object resultset;
			
			SqliteCommand cmd = connection.CreateCommand();
			cmd.CommandText = command;
			resultset = cmd.ExecuteScalar();
			return resultset;
		}*/
		
		private static int ExecuteNonQuery(string command)
		{
			SqliteCommand cmd = connection.CreateCommand();
			cmd.CommandText = command;
			return cmd.ExecuteNonQuery();
		}

		public static void projects_list()
		{	
			//string sql = "SELECT * FROM ViewProjects";
			string sql = "SELECT * " + "FROM project;";
			listdb(sql,1);
		}
	
		 
		public static void projectid_get(string projname)
		{	
			//SELECT * FROM ViewProjects WHERE name='foobar';
			string sql = "SELECT id " + "FROM project WHERE name='" + projname + "';";
			listdb(sql,0);
		}
	
		public static void tags_get(int projid)			//UNTESTED
		{	
			//SELECT * FROM tag WHERE ProjID=123;
			string sql = "SELECT * " + "FROM tag WHERE ProjID=" + projid + ";";
			listdb(sql,0);
		}
	
		public static int project_add(string name)
		{
			//INSERT INTO project values(NULL,'foobar',NULL,NULL,NULL);
			string sql = "INSERT INTO project values(NULL, '" + name + "', NULL, NULL, NULL);";
			return ExecuteNonQuery(sql);
		}
	
		public static int project_add_tag(int id, string tag)
		{
			//INSERT INTO tag values(ID,'tag');
			string sql = "INSERT INTO tag values(" + id + ", '" + tag + "');";
			return ExecuteNonQuery(sql);
		}
		public static int project_rem_tag(int id, string tag)
		{
			//DELETE FROM tag WHERE ProjID=123 AND tag='tagname';
			string sql = "DELETE FROM tag WHERE ProjID=" + id + "AND tag='" + tag + "';";
			return ExecuteNonQuery(sql);
		}
	
		public static int asset_add(int id, string restype, string reslink, string first, string second)
		{
			//INSERT INTO asset values(ProjID,'restype','reslink','first','second');
			string sql = "INSERT INTO asset values(" + id + ", '" + restype + "', '" + reslink + "', '" + first + "', '" + second +"');";
			return ExecuteNonQuery(sql);
		}
	
		public static int asset_rem(int id, string restype, string reslink)
		{
			//DELETE FROM asset WHERE ProjID=123 AND ressourcetype='type' AND ressourcelink='link';
			string sql = "DELETE FROM asset WHERE ProjID=" + id + "AND ressourcetype='" + restype + "' AND reslink='" + reslink + "';";
			return ExecuteNonQuery(sql);
		}
	
		public static int project_rem(int id)
		{
			string sql = "DELETE FROM tag WHERE ProjID=" + id + ";";
			sql += "DELETE FROM asset WHERE ProjID=" + id + ";";
			sql += "DELETE FROM project WHERE id=" + id + ";";
			//DBG: Console.WriteLine("ABC" + ExecuteNonQuery(sql));
			return ExecuteNonQuery(sql);
		}
	}
}
