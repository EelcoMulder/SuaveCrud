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
let getSeriesAsJson =
  getSeries |> toJson

let fromJson<'a> (req: HttpRequest) =
  let json = req.rawForm |> System.Text.Encoding.UTF8.GetString
  Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a

let appWebPart =
  choose
    [ GET >=> choose
        [ path "/api/series" >=> getSeriesAsJson ] 
      POST >=> choose
        [ path "/api/series" >=> request( fromJson<Serie> >> addSerie >> OK >> toJson )  ] ]
          
[<EntryPoint>]
let main argv =

  let conf = 
    { defaultConfig with
            bindings = [ HttpBinding.create HTTP IPAddress.Loopback <| getPort argv ] }

  startWebServer conf appWebPart
  0
