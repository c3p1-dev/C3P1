# C3P1

C3P1 is my personal website, written in C# with .Net9, Blazor and Blazorise.
It runs a HTTP server and must be used behind a reverse proxy. Websockets must be redirected.

The database are sqlite3 files, but can be easily switched to mariadb, postgresql, SQL server...
The sqlite3 files are not encrypted, but are not exposed to public.

C3P1 hosts a WebAPI on https://c3p1.net/api/.