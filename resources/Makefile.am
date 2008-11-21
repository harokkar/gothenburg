NULL =

initial_files = \
		gbg_raw.db \
		gbg.cfg \
		$(NULL)


EXTRA_DIST = \
	$(initial_files)

###############################################################################

install-gbg-home-datadir: touch ~/.gbg/test

install-data-local: install-gbg-home-datadir
uninstall-local: echo "foo"
