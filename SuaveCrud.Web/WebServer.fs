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
          |> OK // move this to elsewhere
          >=> Writers.setMimeType "application/json; charset=utf-8"

    let fromJson<'a> (req: HttpRequest) =
      let json = req.rawForm |> System.Text.Encoding.UTF8.GetString
      Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a  
              
    let getPort (argv: string[]) =
      match Array.length argv with
        | 0 -> 8080us
        | _ -> argv.[0] |> uint16