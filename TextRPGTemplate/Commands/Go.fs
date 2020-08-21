module Commands.Go

open Types
open RoomsAndPassages.Operations

let GoAction (state : GameState) : ActionResult =
    match state with
    | Exploration (room, info) -> 
        match GetRoomAhead info room with
        | Hall _ as room -> Success (Exploration (room, info), "Tup tup tup")
        | Dummy -> Failure "No room ahead"
    | Sleeping _ -> Failure "Hold up you sleepwalker"
    | _ -> Failure "You are not free to move now"

let (|GoCommand|_|) (command : string) (state : GameState) : ActionResult option =
    match command with
    | Utils.Regex "go" _ -> Some (GoAction state)
    | _ -> None