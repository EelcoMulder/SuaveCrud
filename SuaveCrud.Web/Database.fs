namespace SuaveCrud.Database
open FSharp.Data.Sql
open FSharp.Data.Sql.Transactions

module DatabaseContext =
    [<Literal>]
    let ConnectionString = @"Server=.\SQLExpress;Database=Series;Integrated Security=SSPI;"
    type SeriesSqlProvider = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, ConnectionString> 
    let getContext = 
        SeriesSqlProvider.GetDataContext( {Timeout = System.TimeSpan.MaxValue; IsolationLevel = IsolationLevel.DontCreateTransaction})