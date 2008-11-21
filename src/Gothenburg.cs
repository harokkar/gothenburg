//some pseudocode mockups out of the top of my head
//add tag für alle assets in tomboy+ other providers
//if(true)
//	add tag in db
//	undo step in db
//rem tags für alle betroffenen assets in tomboy+andere provider
//if(true)
//	rem tags in db
//	undo step in db
//
//	 def getProjectNoteURI(connection,name):
//	 def getProjectNotes(projectName):

using System;
using Mono.Data.Sqlite;
using NDesk.DBus;
using org.freedesktop.DBus;

namespace Gothenburg
{
  class Gothenburg
  { 
    public static void Main(string[] args)
    {
	    string connectionString = "URI=file:/home/harlan/DEV/codename_gothenburg/gbg/test/gbg.db, version=3";
	    //string connectionString = "Data Source=file:~/DEV/codename_gothenburg/gbg/gbg.db, version=3"; new assembly should work FIXME
      
      DBOperations dbo = new DBOperations (connectionString);
	
  	  //Appropriate tables required to be created
	
      //dbo.projects_list();
  
      Console.WriteLine (dbo.foo(1));

	    //projectid_get("foobar2");
	    //project_rem(10);
     

     AssetProvider.Tomboy tomboy = new AssetProvider.Tomboy ();  

     BusG.Init ();
     tomboy.init();  //RemoteControl remote = AssetProvider.Tomboy.GetRemoteControl ();

     Console.WriteLine ("Tomboy Version: " + tomboy.version());

     string uri = tomboy.retrieve_by_name("as");
     tomboy.open (uri);
                                 
    //?? Service tomboy = Service.Get(sBbus,"org.gnome.Tomboy.Remote");
    }
  }
}
