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

//some pseudocode mockups out of the top of my head
//add tag für alle assets in tomboy+ other providers
//if(true)
//  add tag in db
//  undo step in db
//rem tags für alle betroffenen assets in tomboy+andere provider
//if(true)
//  rem tags in db
//  undo step in db
//
//   def getProjectNoteURI(connection,name):
//   def getProjectNotes(projectName):

using System;
using Mono.Data.Sqlite;
using NDesk.DBus;
using org.freedesktop.DBus;
using Gothenburg.AssetProvider;
using GLib;

namespace Gothenburg
{
  class Gothenburg
  {
    public static void Main()
    {
     Gtk.Application.Init ();
     BusG.Init ();
     new Gui ();

     Gtk.Application.Run ();
    }
  }
}
