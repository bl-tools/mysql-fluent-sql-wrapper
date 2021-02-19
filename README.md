mysql-fluent-sql-wrapper
===

Library provides the ability to call Stored Procedures, Functions, Raw SQL in MySqlDB. It is a wrapper over MySqlConnector data provider. Library created for the ability to use data provider in fluent notation mode.

```csharp
			//call stored procedure with returning data
            var products = new FluentSqlCommand(ConnectionString)
                .Procedure("storedProcedureName")
                .AddParameter("param1", "value1")
                .AddParameter("param2", "value2")
                .ExecRead((reader)=>
                     {
                         var result = new OutgoingDataModel
                         {
                             field1 = reader.GetString("field1"),
                             field2 = reader.GetString("field2"),
                             field3 = reader.GetInt32("field3"),
                             field4 = reader.GetInt32("field4"),
                             field5 = reader.GetDateTime("field5")
                         };
                         return result;
                     });
```