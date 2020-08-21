module Commands.CommandList

open Types
open Commands.Examine
open Commands.Exit
open Commands.Sleep
open Commands.WakeUp
open Commands.Go
open Commands.Turn

let commands : (string -> GameState -> ActionResult option) list = [
    (|ExamineCommand|_|);
    (|ExitCommand|_|);
    (|SleepCommand|_|);
    (|WakeUpCommand|_|);
    (|GoCommand|_|);
    (|TurnCommand|_|)
    ]

let MatchCommand (state : GameState) (textCommand : string) : ActionResult =
    let maybeResult = 
        commands
        |> List.map (fun command -> command textCommand state)
        |> List.choose id
        |> List.tryHead
    match maybeResult with
    | Some result -> result
    | None -> Failure (textCommand + " is not a viable command")