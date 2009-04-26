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

using Gothenburg.AssetProvider;

namespace GothenburgTest
{
	using NUnit.Framework;

	//foo variable should be declared as an instance of the test class
	/*[SetUp] public void SetUp() { foo = new Foo(); }
	[TearDown] public void TearDown() { foo.cleanUp(); }*/

	[TestFixture]
	public class AssetProviderTests
	{
    Gothenburg.AssetProvider.Tomboy tomboy = new Gothenburg.AssetProvider.Tomboy ();

		[Test]
		public void TomboyCreate_Test ()
		{
      
			Assert.AreEqual(tomboy.create(), "foo");
		}
	}
}
