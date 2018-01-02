namespace SuaveCrud.Api
open Suave
open Successful
open Suave.Operators
open SuaveCrud.Infrastructure
open WebServer

module Api =
  type AddFunc<'a> = 'a -> unit -> 'a
  let handlePut<'a> (id: int, update) = 
    request (fun r ->
      let putBody = fromJson<'a> r
      update (id, putBody)
      OK("Updated")
    )

  let handPost<'a> (add: AddFunc<'a>) = 
    request (fun r -> 
      let item = fromJson<'a> r
      OK (add item |> toJson)
    ) >=> Writers.setMimeType "application/json; charset=utf-8"

  let handleDelete id delete = 
    delete id
    OK("Deleted")

  let handleGet id get = 
    OK(get(id) |> toJson) >=> Writers.setMimeType "application/json; charset=utf-8"

  let handleGetAll getAll = 
    OK(toJson getAll) >=> Writers.setMimeType "application/json; charset=utf-8"