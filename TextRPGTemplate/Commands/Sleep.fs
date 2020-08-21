module Commands.Sleep

open Types

let SleepAction (state : GameState) : ActionResult =
    match state with
    | Exploration (room, info) -> Success (Sleeping (room, info), "Zzz")
    | _ -> Failure "Can only go asleep on free movement"

let (|SleepCommand|_|) (command : string) (state : GameState) : ActionResult option =
    match command with
    | Utils.Regex "sleep" _ -> Some (SleepAction state)
    | _ -> None