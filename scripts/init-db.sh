#!/bin/bash
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "London4567!" -d master -i /scripts/init.sql
