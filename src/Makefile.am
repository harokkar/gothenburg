#include $(top_srcdir)/Makefile.include
#SUBDIRS = . Addins

TARGET = gothenburg.exe

bin_SCRIPTS = $(TARGET)

#WRAPPER = gothenburg
#PANEL_WRAPPER = tomboy-panel

#if ENABLE_DBUS
DBUS_CSFLAGS = -define:ENABLE_DBUS
#	DBUS_CSFILES =        \
#	  $(srcdir)/RemoteControl.cs  \
#		  $(srcdir)/RemoteControlProxy.cs
#			endif
#			if FIXED_GTKSPELL
#				FIXED_GTKSPELL_CSFLAGS = -define:FIXED_GTKSPELL
#				endif

CSFLAGS =	\
	-debug	\
	-define:DEBUG	\
	-unsafe	\
	-target:exe	\
	$(DBUS_CSFLAGS)

# -r:Mono.Data.Sqlite.dll \
#$(FIXED_GTKSPELL_CSFLAGS) \
#$(CSC_DEFINES)

CSFILES =           \
	$(srcdir)/Gothenburg.cs       \
	$(srcdir)/AssetProvider.cs       \
	$(srcdir)/DBOperations.cs


ASSEMBLIES =    \
	-r:Mono.Posix \
	-r:Mono.Data.Sqlite.dll \
	$(DBUS_LIBS)

#gothenburg_exe_SOURCES = $(CSFILES)

#$(LINK_MONO_ADDINS)

$(TARGET): $(CSFILES)
	$(GMCS) -out:$@ $(CSFLAGS) $^ $(ASSEMBLIES) #$(RESOURCES)


EXTRA_DIST =	\
	$(CSFILES)
#	$(WRAPPER).in	\
#	$(PANEL_WRAPPER).in   \
#	$(TARGET).config.in   \
#	$(srcdir)/Defines.cs.in   \
#	$(srcdir)/Tomboy.addin.xml


CLEANFILES = \
	$(TARGET)
#	$(TARGET).config	\
#	$(TARGET).mdb	\
#	$(WRAPPER)	\
#	$(PANEL_WRAPPER)	\
#Defines.cs



#-----------
#dnl OLD CRAP

#gothenburg.exe: gothenburg.cs
#	$(GMCS) db_operations.cs asset_provider.cs "-r:System.Data.dll" "-r:Mono.Data.Sqlite.dll"  $(DBUS_LIBS) -out:$@ $<

#embedded = $(foreach res,$(resources), $(addprefix -resource:,$(res)),$(notdir $(res)))

#gmcs gothenburg.cs db_operations.cs asset_provider.cs -r:Mono.Data.Sqlite.dll  `pkg-config --libs ndesk-dbus-1.0` `pkg-config --libs ndesk-dbus-glib-1.0`pkg-config --libs ndesk-dbus-1.0`

#gbg.exe: db_operations.cs 
#	$(GMCS) $(MCS) $(embedded) -out:$@ $<

#gbg2.exe: asset_provider.cs
#	$(GMCS) "-r:System.Data.dll" "-r:Mono.Data.Sqlite.dll"  $(DBUS_LIBS) -out:$@ $<

#gbg.exe: db_operations.cs 
#  $(GMCS) "-r:System.Data.dll" "-r:Mono.Data.Sqlite.dll" -out:$@ $<


#gbg2.exe: asset_provider.cs
#	$(GMCS) "-r:System.Data.dll" "-r:Mono.Data.Sqlite.dll"  `pkg-config --lib ndesk-dbus-1.0` `pkg-config --libs ndesk-dbus-glib-1.0` `pkg-config --libs` `pkg-config --libs gtk-dotnet-2.0`  -out:$@ $<

#$(bush_build_sources) $(resources)
#	$(MCS) $(GTKSHARP_LIBS) $(embedded) -out:$@ $<
