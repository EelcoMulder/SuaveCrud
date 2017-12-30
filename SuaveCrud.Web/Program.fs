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
    updateSerie (id, putBody)
    OK("Updated")
  ) 

let handPostSerie = 
  request (fun r -> 
    let newSerie = fromJson<Serie> r
    OK (addSerie newSerie |> toJson)
  ) >=> Writers.setMimeType "application/json; charset=utf-8"

let handleDeleteSerie id = 
  deleteSerie id
  OK("Deleted")

let appWebPart =
  choose
    [ GET >=> choose
        [ path "/api/series" >=> Writers.setMimeType "application/json; charset=utf-8" >=> OK(toJson getSerie)
          pathScan "/api/series/%d" (fun id -> Writers.setMimeType "application/json; charset=utf-8" >=> OK(getSerie(id) |> toJson)) ] 
      POST >=> choose
        [ path "/api/series" >=> handPostSerie ] 
      PUT >=> choose
        [ pathScan "/api/series/%d" handlePutSerie ]
      DELETE >=> choose
        [ pathScan "/api/series/%d" handleDeleteSerie ] ]
          
[<EntryPoint>]
let main argv =

  let conf = 
    { defaultConfig with
            bindings = [ HttpBinding.create HTTP IPAddress.Loopback <| getPort argv ] }

  startWebServer conf appWebPart
  0
