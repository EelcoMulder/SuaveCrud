namespace SuaveCrud.Database
open FSharp.Data.Sql

module DatabaseContext =
    [<Literal>]
    let ConnectionString = @"Server=.\SQLExpress;Database=Series;Integrated Security=SSPI;"
    type SqlProvider = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, ConnectionString> 
    let getContext = 
        SqlProvider.GetDataContext()