module Controls

open Types
open Operations
open Commands.CommandList


let rec Play (state : GameState) : unit =
    printfn "\n\n\n%s" (GeneralStateDescription state)
    let command = System.Console.ReadLine ()
    match MatchCommand state command with
    | Success (state, info) ->
        printfn "%s" info
        Play state
    | Failure info ->
        printfn "%s" info
        Play state
    | Interrupt -> ()
