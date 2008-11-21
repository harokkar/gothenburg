/*****
 * Gothenburg - A project-centric view on your GNOME Desktop       
 *                                                                   
 *        Copyright (C) 2008 Clemens N. Buss
 *                                                                   
 * This program is free software; you can redistribute it and/or     
 * modify it under the terms of the GNU General Public License       
 * as published by the Free Software Foundation; either version 3    
 * of the License, or any later version.                             
 *                                                                   
 *                                                                   
 * This program is distributed in the hope that it will be useful,   
 * but WITHOUT ANY WARRANTY; without even the implied warranty of    
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the     
 * GNU General Public License for more details.                      
 *                                                                   
 * You should have received a copy of the GNU General Public License 
 * along with this program; if not, write to the Free Software       
 * Foundation, Inc., 51 Franklin Street, Boston,            
 * MA  02110-1301, USA.                                              
 ****/


using System;
using Mono.Data.Sqlite;
using NDesk.DBus;
using org.freedesktop.DBus;

//using System.IO;
//using System.Collections.Generic;
//using System.Data;

namespace Gothenburg
{
namespace AssetProvider
{
	public interface IAssetProvider
	{
		void init ();
		//GIcon icon {set; get;}	//TODO: The Icon of the provider, e.g. the yellow thingie for tomboy ...
		void open(string resourcelink);
		//void edit(string resourcelink);
		string create();   //Gives back the resourcelink async when done?!
		string get_primary_info(string resourcelink);
	 	string get_secondary_info(string resourcelink);
		//GIcon get_preview_icon(string resourcelink); //TODO: More hairy
		bool add_tag(string resourcelink, string tag);
		bool rem_tag(string resourcelink, string tag);

		int retrieve_by_tag(string tag);
		string retrieve_by_name(string name);
		
		string version ();
	}

  public class Tomboy : IAssetProvider
	{
		public void init()
		{
			remote = GetRemoteControl ();
		}
		
		public void open(string resourcelink)
		{
			remote.DisplayNote (resourcelink);
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
	
		public string retrieve_by_name(string name)
		{
			return remote.FindNote (name);
		}
		
		public string version ()
		{
			return remote.Version ();
		}
		
		RemoteControl remote;				//TODO: Perhaps use a constructor
		static org.freedesktop.DBus.IBus sBus;
		static ObjectPath TomboyPath = new ObjectPath ("/org/gnome/Tomboy/RemoteControl");
		static string TomboyNamespace = "org.gnome.Tomboy";
		
		public static RemoteControl GetRemoteControl()
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
		
		[Interface ("org.gnome.Tomboy.RemoteControl")]
		public interface RemoteControl : Introspectable
		{
			//RemoteControl ();
			string Version ();
			bool DisplayNote (string uri);
			string FindNote (string linked_title);
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

	

	// See: /tomboy-0.12.0/Tomboy$ vim RemoteControl.cs 


	//conn = openTomboyConnection()

	

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
}
}
