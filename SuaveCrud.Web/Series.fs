namespace SuaveCrud.BusinessLogic

open SuaveCrud.Database
open SuaveCrud.Types
open DatabaseContext

module Series =
    let getSeries = 
        let ctx = getContext
        query { for c in ctx.Dbo.Series do
                sortBy c.Name
                select c } 
        |> Seq.map (fun x -> x.MapTo<Serie>())

    let addSerie (s: Serie) = 
        let ctx  = getContext
        let addSerie = ctx.Dbo.Series.Create()
        addSerie.Name <- s.Name
        addSerie.Description <- s.Description
        ctx.SubmitUpdates()
        "Serie added"