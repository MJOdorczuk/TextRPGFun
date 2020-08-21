module Commands.Examine

open Types
open Player.Operations
open RoomsAndPassages.Operations
open Actors.Types
open Actors.Operations
open Utils

let GeneralExaminationAction (state : GameState) : ActionResult =
    match state with
    | Exploration (room, info) -> 
        let direction = PlayerDirection info
        Success (state, ExamineRoom room direction)
    | _ -> Failure "Can only examine room on free movement"

let ExamineActor (actor : Actor) : string =
    ActorName actor + " - " + ActorDescription actor + "\n"

let ExamineActorAction (name : string) (state : GameState) : ActionResult =
    match state with
    | Exploration (room, _) ->
        let actor =
            room
            |> RoomActors
            |?> List.tryFind (fun actor -> ActorName actor = name)
        match actor with
        | Some (Some actor) -> Success (state, ExamineActor actor)
        | _ -> Failure "No such object here.\n"
    | _ -> Failure "Can only examine room on free movement"

let (|ExamineCommand|_|) (command : string) (state : GameState) : ActionResult option =
    match command with
    | Utils.Regex "examine all" _ -> Some (GeneralExaminationAction state)
    | Utils.Regex "examine (.+)" [name] -> Some (ExamineActorAction name state)
    | _ -> None