module Commands.Turn

open Types
open Player.Operations

let TurnAction (state : GameState) (turnDirection : TurnDirection) : ActionResult =
    match state with
    | Exploration (room, info) ->
        let direction = GetDirectionAfterTurn (PlayerDirection info) turnDirection
        Success (Exploration (room, ChangeDirection info direction), "Woosh!")
    | Sleeping _ -> Failure "You can turn to the other side while asleep"
    | _ -> Failure "You are not free to move now"

let (|TurnCommand|_|) (command : string) (state : GameState) : ActionResult option =
    match command with
    | Utils.Regex "turn left" _ -> Some (TurnAction state Left)
    | Utils.Regex "turn right" _ -> Some (TurnAction state Right)
    | Utils.Regex "turn back" _ -> Some (TurnAction state Back)
    | Utils.Regex "turn (.+)" [text] -> Some (Failure (text + " is not a viable direction."))
    | _ -> None