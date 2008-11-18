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

echo " ======== User =========="
echo "configure"
./configure --prefix=/tmp/autotest

echo "make"
make
exit_on_failure $? "make"

echo "make dist"
make dist
exit_on_failure $? "make dist"

echo "make clean"
make clean
exit_on_failure $? "make clean"
