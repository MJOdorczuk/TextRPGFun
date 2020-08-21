module Commands.WakeUp

open Types

let WakeUpAction (state : GameState) : ActionResult =
    match state with
    | Sleeping (room, info) -> Success (Exploration (room, info), "Good morning")
    | _ -> Failure "Can not awake when not asleep"

let (|WakeUpCommand|_|) (command : string) (state : GameState) : ActionResult option =
    match command with
    | Utils.Regex "wake up" _ -> Some (WakeUpAction state)
    | _ -> None