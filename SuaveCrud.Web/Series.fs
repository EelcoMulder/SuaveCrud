namespace SuaveCrud.BusinessLogic

open SuaveCrud.Database
open SuaveCrud.Types
open DatabaseContext

module Series =

    let getSerie id = 
        let ctx = getContext
        let s = query {
            for p in ctx.Dbo.Series do
            where (p.Id = id)
            select (Some p)
            exactlyOneOrDefault
        }
        (ctx, s)

    let getSeries = 
        let ctx = getContext
        query { for c in ctx.Dbo.Series do
                sortBy c.Name
                select c } 
        |> Seq.map (fun x -> x.MapTo<Serie>())

    let addSerie (s: Serie) = 
        let ctx = getContext
        let addSerie = ctx.Dbo.Series.Create()
        addSerie.Name <- s.Name
        addSerie.Description <- s.Description
        ctx.SubmitUpdates()
        "Serie added"

    let updateSerie (id: int, serie: Serie) =
        let (c, s) = getSerie id
        match s with
            | Some storedSerie ->
                storedSerie.Name <- serie.Name
                storedSerie.Description <- serie.Description
                c.SubmitUpdates()
                "Serie updated"
            | None -> "Serie not found"
        
    let deleteSerie (id: int) = 
        let (c, s) = getSerie id
        match s with
            | Some storedSerie ->
                storedSerie.Delete()
                c.SubmitUpdates()
                "Serie deleted"
            | None -> "Serie not found"                            