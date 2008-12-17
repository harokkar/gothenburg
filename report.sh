#!/bin/sh

#check that no overwrite, copy old log etc.

echo "\n* Running unit tests and saving report to /report ... \n"

cd test
#make GothenburgTest.dll
ln -s ../src/gothenburg.exe gothenburg.exe
nunit-console GothenburgTest.dll | tee ../report/UnitTest_Report.txt
cd ..


echo "* Creating code coverage report ... \n"
cd src/
mono --debug --profile=cov:gothenburg ./gothenburg.exe | tee ../report/CodeCoverage_Report.txt
cd ..
echo "\n"

echo "* Profiling the application ... \n"
cd src/
mono --profile=default:stat,alloc,file=../report/Profiling_Report.txt gothenburg.exe
cd ..
echo "\n"
