NULL =

gbg_folder = $(HOME)/.gothenburg

home_files = \
		gbg_raw.db \
		gbg.cfg
 		$(NULL)

EXTRA_DIST = \
	$(home_files)

install-gbg-home-datadir:
	if test -d $(gbg_folder); \
	then \
		echo "Folder already exists";\
  else \
		echo "Creating directory ~./gothenburg"; \
		mkdir $(gbg_folder); \
		cp $(home_files) $(gbg_folder); \
		mv $(gbg_folder)/gbg_raw.db $(gbg_folder)/gbg.db; \
	fi  

install-data-local: install-gbg-home-datadir
uninstall-local:
	echo "foo"
