module Commands.Collect

open Types
open Player.Operations
open RoomsAndPassages.Operations
open Actors.Types
open Actors.Operations
open Utils

let GeneralCollectionAction (state : GameState) : ActionResult =
    match state with
    | Exploration (room, info) ->
        let items = 
            room
            |> RoomActors
            |?> SelectCollectable
        match items with
        | None
        | Some [] -> Failure "There is nothing you can collect here"
        | Some items -> 
            let newInfo = 
                items
                |> List.fold Equip info
            let collectedListMessage =
                items
                |> List.groupBy (fun item -> ActorName (Item item))
                |> List.map (fun (name, items) -> (name, items.Length))
                |> List.fold (fun message (name, count) -> 
                    message + " - " + name + " x" + string count + "\n") "You collected:\n"
            RemoveCollectables room
            Success (Exploration (room, newInfo), collectedListMessage)
    | _ -> Failure "Can only collect items on free movement"

let CollectItem (item : CollectableActor) (info : PlayerInfo) : PlayerInfo =
    Equip info item

let SingleCollectionAction (name : string) (state : GameState) : ActionResult =
    match state with
    | Exploration (room, info) ->
        match ExtractActors room (fun actor -> ActorName actor = name) with
        | [] -> Failure "There is nothing named so"
        | actors ->
            match SelectCollectable actors with
            | [] -> Failure "It is impossible to take it"
            | item::tail ->
                let reminder = 
                    tail
                    |> List.map Item
                AddRoomActors room reminder
                Success (Exploration (room, Equip info item), 
                    "You collected a " + ActorName (Item item))
    | _ -> Failure "Can only collect items on free movement"

let EveryCollectionAction (name : string) (state : GameState) : ActionResult =
    match state with
    | Exploration (room, info) ->
        match ExtractActors room (fun actor -> ActorName actor = name) with
        | [] -> Failure "There is nothing named so"
        | actors ->
            match SelectCollectable actors with
            | [] -> Failure "It is impossible to take it"
            | item::tail ->
                let newInfo = 
                    item::tail
                    |> List.fold Equip info
                Success (Exploration (room, newInfo), 
                    "You collected " + 
                    ActorName (Item item) + 
                    " x" + 
                    string (item::tail).Length + 
                    "\n")
    | _ -> Failure "Can only collect items on free movement"

let (|CollectCommand|_|) (command : string) (state : GameState) : ActionResult option =
    match command with
    | Utils.Regex "collect all" _ -> Some (GeneralCollectionAction state)
    | Utils.Regex "collect every (.+)" [name] -> Some (EveryCollectionAction name state)
    | Utils.Regex "collect (.+)" [name] -> Some (SingleCollectionAction name state)
    | _ -> None