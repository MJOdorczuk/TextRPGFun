module Commands.Exit

open Types

let (|ExitCommand|_|) (command : string) (state : GameState) : ActionResult option =
    match command with
    | Utils.Regex "exit" _ -> Some Interrupt
    | _ -> None