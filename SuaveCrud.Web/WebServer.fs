namespace SuaveCrud.Infrastructure
open Newtonsoft.Json
open Newtonsoft.Json.Serialization
open Suave
open Operators
open Successful

module WebServer =
    let toJson v =
      let jsonSerializerSettings = JsonSerializerSettings()
      jsonSerializerSettings.ContractResolver <- CamelCasePropertyNamesContractResolver()
      JsonConvert.SerializeObject(v, jsonSerializerSettings)
          |> OK
          >=> Writers.setMimeType "application/json; charset=utf-8"


    let getPort (argv: string[]) =
      match Array.length argv with
        | 0 -> 8080us
        | _ -> argv.[0] |> uint16