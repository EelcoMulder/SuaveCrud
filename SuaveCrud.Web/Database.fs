namespace SuaveCrud.Database
open FSharp.Data.Sql
open FSharp.Data.Sql.Transactions

module DatabaseContext =
    [<Literal>]
    let ConnectionString = @"Server=.\SQLExpress;Database=Series;Integrated Security=SSPI;"
    type SqlProvider = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, ConnectionString> 
    let getContext = 
        SqlProvider.GetDataContext( {Timeout = System.TimeSpan.MaxValue; IsolationLevel = IsolationLevel.DontCreateTransaction})