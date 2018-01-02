open System.Net
open SuaveCrud.Infrastructure
open WebServer
open Suave
open Suave.Filters
open Suave.Operators
open SuaveCrud.BusinessLogic
open SuaveCrud.Types
open Series
open SuaveCrud.Api
open Api

let appWebPart =
  choose
    [ GET >=> choose
        [ path "/api/series" >=> handleGetAll getSeries
          pathScan "/api/series/%d" (fun id -> handleGet id getSerie) ] 
      POST >=> choose
        [ path "/api/series" >=> handPost<Serie>(addSerie) ] 
      PUT >=> choose
        [ pathScan "/api/series/%d" (fun id -> handlePut<Serie>(id, updateSerie)) ]
      DELETE >=> choose
        [ pathScan "/api/series/%d" (fun id -> handleDelete id deleteSerie)] ]
          
[<EntryPoint>]
let main argv =

  let conf = 
    { defaultConfig with
            bindings = [ HttpBinding.create HTTP IPAddress.Loopback <| getPort argv ] }

  startWebServer conf appWebPart
  0
