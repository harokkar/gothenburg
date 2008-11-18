//some pseudocode mockups out of the top of my head

//add tag für alle assets in tomboy+ other providers
//if(true)
//	add tag in db
//	undo step in db

//rem tags für alle betroffenen assets in tomboy+andere provider
//if(true)
//	rem tags in db
//	undo step in db

//gmcs gothenburg.cs db_operations.cs asset_provider.cs -r:Mono.Data.Sqlite.dll  `pkg-config --libs ndesk-dbus-1.0` `pkg-config --libs ndesk-dbus-glib-1.0`

using System;
//using System.Data;
using Mono.Data.Sqlite;
//using System.IO;
using asset_provider;
using db_operations;

using NDesk.DBus;
using org.freedesktop.DBus;

/*
public static void Main(string[] args)
	{
		string connectionString = "URI=file:~/DEV/codename_gothenburg/gbg/gbg.db, version=3";
		//string connectionString = "Data Source=file:~/DEV/codename_gothenburg/gbg/gbg.db, version=3"; new assembly should work FIXME
		
		connection = new SqliteConnection(connectionString);
		connection.Open();
		
		//Appropriate tables required to be created
		
		projects_list();
		//projectid_get("foobar2");
		//project_rem(10);
					
		connection.Close();
		connection = null;		
	}
*/

class Gothenburg
{	
	public static void Main(string[] args)
	{
				BusG.Init ();		
				asset_provider.Tomboy.RemoteControl remote = asset_provider.Test.GetTomboyRemoteControl ();
		
				Console.WriteLine ("Tomboy Version: " + remote.Version());
		
				string uri = remote.FindNote("as");
				remote.DisplayNote(uri);

				//?? Service tomboy = Service.Get(sBbus,"org.gnome.Tomboy.Remote");
	}
}
