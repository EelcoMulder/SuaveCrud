open System.Net
open SuaveCrud.Infrastructure
open WebServer
open Suave
open Suave.Filters
open Suave.Operators
open Successful
open SuaveCrud.BusinessLogic
open Series
open SuaveCrud.Types

// make generic
let handlePutSerie id = 
  request (fun r ->
    let putBody = fromJson<Serie> r
    OK (updateSerie (id, putBody))
  ) 

let appWebPart =
  choose
    [ GET >=> choose
        [ path "/api/series" >=> toJson getSeries 
          path "/api/series/%d" >=> (fun id -> toJson getSerie id) ] 
      POST >=> choose
        [ path "/api/series" >=> request( fromJson<Serie> >> addSerie >> OK )  ] 
      PUT >=> choose
        [ pathScan "/api/series/%d" handlePutSerie ]
      DELETE >=> choose
        [ pathScan "/api/series/%d" (fun id -> OK (deleteSerie id)) ] ]
          
[<EntryPoint>]
let main argv =

  let conf = 
    { defaultConfig with
            bindings = [ HttpBinding.create HTTP IPAddress.Loopback <| getPort argv ] }

  startWebServer conf appWebPart
  0
