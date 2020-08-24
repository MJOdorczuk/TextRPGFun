module RoomsAndPassages.Operations

open Types
open Utils
open Player.Operations
open Actors.Types
open Actors.Operations

let RoomInfoName (info : RoomInfo) : string =
    info.Name

let RoomInfoDescription (info : RoomInfo) : string =
    info.Description

let RoomInfoActors  (info : RoomInfo) : Actor list =
    info.Actors

let ChangeInfoActors (actors : Actor list) (info : RoomInfo) : unit =
    info.Actors <- actors

let ExtractRoomInfo (room : Room) : RoomInfo option =
    match room with
    | Dummy -> None
    | Hall (_, _, _, _, info) -> Some info

let RoomName (room : Room) : string option =
    room
    |> ExtractRoomInfo
    |?> RoomInfoName

let RoomDescription (room : Room) : string option =
    room
    |> ExtractRoomInfo
    |?> RoomInfoDescription

let RoomActors (room : Room) : Actor list option =
    room
    |> ExtractRoomInfo
    |?> RoomInfoActors

let FindActors (room : Room) (name : Name) : Actor list option =
    room
    |> RoomActors
    |?> List.filter (fun actor -> ActorName actor = name)

let SetRoomActors (room : Room) (actors : Actor list) : unit =
    room
    |> ExtractRoomInfo
    |?> ChangeInfoActors actors
    |> ignore  

let AddRoomActors (room : Room) (actors : Actor list) : unit =
    room
    |> RoomActors
    |?> (@) actors
    |?> SetRoomActors room
    |> ignore
    
let ExtractActors (room : Room) (predicate : Actor -> bool) : Actor list =
    let actors = 
        room
        |> RoomActors
        |?> List.filter predicate
        |> Option.defaultValue []
    let reminder =
        room
        |> RoomActors
        |?> List.filter (predicate >> not)
        |> Option.defaultValue []
    SetRoomActors room reminder
    actors

let RemoveCollectables (room : Room) : unit =
    room
    |> RoomActors
    |?> FilterCollectables
    |?> SetRoomActors room
    |> ignore

let GetPassage (direction : Direction) (room : Room) : Passage option =
    match room with
    | Dummy -> None
    | Hall (north, west, south, east, _) ->
        match direction with
        | North -> Some north
        | West -> Some west
        | South -> Some south
        | East -> Some east

let CrossPassage (room : Room) (passage : Passage) : Room =
    passage.GoThrough room

let GetRoom (room : Room) (direction : Direction) : Room =
    match GetPassage direction room with
    | None -> Dummy
    | Some passage -> CrossPassage room passage

let GetRoomAhead (info : PlayerInfo) (room : Room) : Room =
    info
    |> PlayerDirection
    |> GetRoom room

let ExamineRoom (room : Room) (direction : Direction) : string =
    let frontPassage = GetPassage (GetDirectionAfterTurn direction NoTurn) room
    let leftPassage = GetPassage (GetDirectionAfterTurn direction Left) room
    let rightPassage = GetPassage (GetDirectionAfterTurn direction Right) room
    let frontView = match frontPassage with
                    | None -> ""
                    | Some passage -> "In front you see " + passage.Description + "\n"
    let leftView = match leftPassage with
                   | None -> ""
                   | Some passage -> "To the left you see " + passage.Description + "\n"
    let rightView = match rightPassage with
                    | None -> ""
                    | Some passage -> "To the right you see " + passage.Description + "\n"
    let actors = RoomActors room
    let actorsDescription = 
        match actors with
        | None
        | Some [] -> "It is empty here.\n"
        | _ -> "There are:\n" +
               (actors
               |> Option.defaultValue []
               |> CountActors
               |> List.map (fun (count, name, _) -> 
                               "\t" + name + " x" + string count + "\n")
               |> List.reduce (+))
    frontView +
    leftView +
    rightView +
    actorsDescription