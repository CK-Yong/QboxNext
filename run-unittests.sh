#!/bin/sh

# https://confluence.atlassian.com/bitbucket/test-reporting-in-pipelines-939708543.html

find . -type f -name '*Tests.csproj' -exec dotnet test --no-build --logger 'trx;LogFileName=../test-results/results.trx' --filter TestCategory!='Integration' "{}" \;

# Compile reports

export PATH="$PATH:/root/.dotnet/tools"
dotnet tool install -g trx2junit

trx2junit **/test-results/*.trx
