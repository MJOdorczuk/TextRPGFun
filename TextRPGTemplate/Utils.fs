module Utils

open System.Text.RegularExpressions

let (|?>) (maybe : 'a option) (f : 'a -> 'b) : 'b option =
    match maybe with
    | Some value -> Some (f value)
    | None -> None

let someStringOrEmpty (maybe : string option) : string =
    match maybe with
    | Some text -> text
    | None -> ""

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success 
    then Some(List.tail [for g in m.Groups -> g.Value])
    else None