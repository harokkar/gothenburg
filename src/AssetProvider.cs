//gmcs asset_provider.cs -r:System.Data.dll -r:Mono.Data.Sqlite.dll  `pkg-config --libs ndesk-dbus-1.0` `pkg-config --libs ndesk-dbus-glib-1.0` `pkg-config --libs gtk-sharp-2.0` `pkg-config --libs glib-sharp-2.0`   `pkg-config --libs gtk-dotnet-2.0` 

//gmcs asset_provider.cs -r:Mono.Data.Sqlite.dll  `pkg-config --libs ndesk-dbus-1.0` `pkg-config --libs ndesk-dbus-glib-1.0`



using System;
//using System.Collections.Generic;

//using System.Data;
using Mono.Data.Sqlite;

using NDesk.DBus;
using org.freedesktop.DBus;

//using System.IO;


namespace asset_provider
{
	public interface IAssetProvider
	{
		//GIcon icon {set; get;}	//The Icon of the provider, e.g. the yellow thingie for tomboy ...
	
		void open(string resourcelink);
		//void edit(string resourcelink);
		string create();   //Gives back the resourcelink async when done?!
		string get_primary_info(string resourcelink);
	 	string get_secondary_info(string resourcelink);
		//GIcon get_preview_icon(string resourcelink); //TODO: More hairy
		bool add_tag(string resourcelink, string tag);
		bool rem_tag(string resourcelink, string tag);

		int retrieve_by_tag(string tag);
		int retrieve_by_name(string name);
	}

	class tomboy : IAssetProvider
	{
		public void open(string resourcelink)
		{
	
		}

		public string create()
		{
			return "foo";
		}
	
		public string get_primary_info(string resourcelink)
		{
			return "foobar";
		}
	
	 	public string get_secondary_info(string resourcelink)
	 	{
	 		return "foobar";
	 	}

		public bool add_tag(string resourcelink, string tag)
		{
			return true;
		}
	
		public bool rem_tag(string resourcelink, string tag)
		{
			return true;
		}

		public int retrieve_by_tag(string tag)
		{
			return 1;
		}
	
		public int retrieve_by_name(string name)
		{
			return 2;
		}
	}

	// See: /tomboy-0.12.0/Tomboy$ vim RemoteControl.cs 

	//from dbus.mainloop.glib import DBusGMainLoop

	//def openTomboyConnection():
	//    dbus_loop = DBusGMainLoop()
	//    bus = dbus.SessionBus( mainloop=dbus_loop )
	//    tomboy_obj = bus.get_object( "org.gnome.Tomboy", "/org/gnome/Tomboy/RemoteControl")
	//    return dbus.Interface( tomboy_obj, "org.gnome.Tomboy.RemoteControl")

	//conn = openTomboyConnection()

	//def getProjectNoteURI(connection,name):
	//	if connection.FindNote(name) != "":
	//		return connection.FindNote(name)
	//	else:
	//		return False

	//def getProjectNotes(projectName):
	//	notesList = []
	//	for note in conn.ListAllNotes():
	//		if (conn.GetNoteTitle(note).lower().find(projectName.lower()) != -1) or (conn.GetNoteContents(note).lower().find(projectName.lower()) != -1):
	//			notesList.append(note)
	//	return notesList

	//def createProjectNote(connection,name):
	//	#FIXME: broken
	//	print "Nothing found, creating a new one"
	//	newNote=connection.CreateNamedNote(name)
	//	connection.DisplayNote(newNote)

	//def openProjectNote(noteURI):
	//	openTomboyConnection().DisplayNote(noteURI)



	/*namespace Tomboy
	{
		[Interface ("org.gnome.Tomboy.RemoteControl")]
		public class RemoteControl : MarshalByRefObject
		{
			public RemoteControl()
			{
			}
		
			public string Version ()
			{
				return null;
			}
		
			*/
	//		public string CreateTask (string categoryName, string taskName,
	//								  bool enterEditMode)
	//		{
	//			return null;
	//		}
	//		
	//		public string[] GetCategoryNames ()
	//		{
	//			return null;
	//		}
	//		
	//		public void ShowTasks ()
	//		{
	//		}
	//	}
	//}


	namespace Tomboy
	{
		[Interface ("org.gnome.Tomboy.RemoteControl")]
		public interface RemoteControl : Introspectable
		{
			//RemoteControl ();
			string Version ();
			bool DisplayNote (string uri);
			string FindNote (string linked_title);
		}
	}


	//# Display the Start Here note
	//tomboy.DisplayNote(tomboy.FindStartHereNote())

	//# Display the title of every note
	//for n in tomboy.ListAllNotes(): print tomboy.GetNoteTitle(n)

	//# Display the contents of the note called Test
	//print tomboy.GetNoteContents(tomboy.FindNote("Test"))

	//# Add a tag to the note called Test
	//tomboy.AddTagToNote(tomboy.FindNote("Test"), "sampletag")

	//# Display the titles of all notes with the tag 'sampletag'
	//for note in tomboy.GetAllNotesWithTag("sampletag"):
	//  print tomboy.GetNoteTitle(note)

	//# Print the XML data for the note called Test
	//print tomboy.GetNoteCompleteXml(tomboy.FindNote("Test"))


	//# Notify when a new note is created
	//def onNoteAdded(n): print "Note created: %s" % n
	//bus.add_signal_receiver(onNoteAdded,
	//  dbus_interface="org.gnome.Tomboy.RemoteControl",
	//  signal_name="NoteAdded")

	//# Notify when a note is saved
	//def onNoteSaved(n): print "Note %s saved!" % tomboy.GetNoteTitle(n)
	//bus.add_signal_receiver(onNoteSaved,
	//  dbus_interface="org.gnome.Tomboy.RemoteControl",
	//  signal_name="NoteSaved")

	//# Loop until manually terminated
	//gobject.MainLoop().run()


	public class Test
	{
		//static IBus bus;
		static org.freedesktop.DBus.IBus sBus;
		static ObjectPath TomboyPath = new ObjectPath ("/org/gnome/Tomboy/RemoteControl");
		static string TomboyNamespace = "org.gnome.Tomboy";
	
		public static Tomboy.RemoteControl GetTomboyRemoteControl()
		{
			Tomboy.RemoteControl remoteControl = null;
			try
			{
				if (Bus.Session.NameHasOwner (TomboyNamespace) == false)
					return null;
				remoteControl = Bus.Session.GetObject<Tomboy.RemoteControl> ( TomboyNamespace, TomboyPath);					
			}	
			catch (Exception e)
			{
				//Logger.Error ("Exception when getting Tomboy.RemoteControl: {0}", e.Message);
			}
			return remoteControl;
		}	
	
		public static void introspect()
		{
			if (Bus.Session.NameHasOwner (TomboyNamespace) == false)
				Console.WriteLine ("No");
			else
				sBus = Bus.Session.GetObject<org.freedesktop.DBus.IBus> ( TomboyNamespace, TomboyPath);
	
			string xmlData = sBus.Introspect ();
			Console.WriteLine ("xmlData: " + xmlData);
		}			
	}
}
