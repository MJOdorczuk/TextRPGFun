module Actors.Operations

open Actors.Types

let ActorTextData (actor : Actor) : ActorTextData =
    match actor with
    | Item (data) -> data
    | Static (data) -> data
    | Container (data, _) -> data

let ActorDataName ((name, _) : ActorTextData) : Name =
    name

let ActorName (actor : Actor) : Name =
    actor
    |> ActorTextData
    |> ActorDataName

let ActorDataDescription ((_, description) : ActorTextData) : Description =
    description

let ActorDescription (actor : Actor) : Description =
    actor
    |> ActorTextData
    |> ActorDataDescription

let AsCollectable (actor : Actor) : CollectableActor option =
    match actor with
    | Item item -> Some item
    | _ -> None

let AsStatic (actor : Actor) : StaticActor option =
    match actor with
    | Static actor -> Some actor
    | _ -> None

let AsContainer (actor : Actor) : ContainerActor option =
    match actor with
    | Container container -> Some container
    | _ -> None

let SelectCollectable (actors : Actor list) : CollectableActor list =
    actors
    |> List.map AsCollectable
    |> List.choose id

let SelectStatics (actors : Actor list) : StaticActor list =
    actors
    |> List.map AsStatic
    |> List.choose id

let SelectConainers (actors : Actor list) : ContainerActor list =
    actors
    |> List.map AsContainer
    |> List.choose id

let FilterCollectables (actors : Actor list) : Actor list =
    let predicate actor = 
        match AsCollectable actor with
        | Some _ -> false
        | _ -> true
    List.filter predicate actors

let CountActors (actors : Actor list) : (int * Name * Description) list =
    actors
    |> List.groupBy ActorName
    |> List.map (fun (name, group) -> group.Length, name, ActorDescription group.Head)

let GroupActorsByName (actors : Actor list) (name : Name) : Actor list =
    actors
    |> List.groupBy ActorName
    |> List.tryFind (fun (actorName, _) -> name = actorName)
    |> Option.map (fun (_, actors) -> actors)
    |> Option.defaultValue []