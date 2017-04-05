@echo off

cd %~dp0

.\nfaw-io\src\node.exe .\nfaw-io\src\server -c config.json
