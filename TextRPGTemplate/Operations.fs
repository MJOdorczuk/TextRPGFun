module Operations

open Types
open Utils
open RoomsAndPassages.Operations

let GeneralStateDescription (state : GameState) : string =
    match state with
    | Exploration (room, _) ->
        let locationName =
            room
            |> RoomName
            |> someStringOrEmpty
        "You are on the way.\n" +
        "Your current location is " + locationName + ".\n"
    | Sleeping _ -> 
        "You are asleep.\n"