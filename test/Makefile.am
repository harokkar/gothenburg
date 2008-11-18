#bin_PROGRAMS = GothenburgTest.dll

TARGET = GothenburgTest.dll

CSFLAGS =				\
	-debug				\
	-define:DEBUG			\
	-target:library

CSFILES =				\
	$(srcdir)/db_operationsTests.cs
	

ASSEMBLIES =							\
	$(NUNIT_LIBS)						\
	-r:$(top_builddir)/src/gothenburg.exe			

#MONO_PATH = $(top_builddir)/Tomboy:$(top_builddir)/Tomboy/Plugins

RESSOURCES =

$(TARGET): $(CSFILES) $(top_builddir)/src/gothenburg.exe
	$(GMCS) -out:$@ $(CSFLAGS) $(CSFILES) $(ASSEMBLIES) $(RESSOURCES)

test: $(TARGET)
	MONO_PATH=$(MONO_PATH) $(NUNIT) $(TARGET)	#/nologo

EXTRA_DIST = 				\
	$(CSFILES)

CLEANFILES = 				\
	$(TARGET)			\
	$(TARGET).mdb			\
	TestResult.xml

#.PHONY: test
