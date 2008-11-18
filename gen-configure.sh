#!/bin/sh

exit_on_failure()
{
    if [ "$1" != "0" ]
    then
	echo "$2 failed"
	echo "leaving....."
	exit
    fi
}
echo " ======== Developer =========="

echo "aclocal ==> aclocal.m4"
aclocal
exit_on_failure $? "aclocal"

echo "autoheader + configure.in ==> config.h.in"
autoheader
exit_on_failure $? "autoheader"

echo "autoconf + configure.in + aclocal.m4 ==> configure"
autoconf
exit_on_failure $? "autoconf"

echo "Creating empty files"
FILES="NEWS AUTHORS ChangeLog"
echo "   $FILES"
for i in $FILES
do
    touch $i
done

echo "automake Makefile.am  => Makefile.in"
automake --add-missing --copy
exit_on_failure $? "automake"

#  rm -fr aclocal.m4 AUTHORS autom4te.cache ChangeLog config.h config.h.in config.log config.status configure COPYING depcomp hola hola-0.1.tar.gz hola.o INSTALL install-sh Makefile Makefile.in missing NEWS stamp-h1 .deps

