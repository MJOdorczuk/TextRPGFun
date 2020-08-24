module Types

open Actors.Types

type RoomInfo (name : string, description : string, actors : Actor list) =
    member public __.Name = name
    member public __.Description = description
    member val Actors = actors with get, set

type Room =
    | Dummy
    | Hall of north : Passage * west : Passage * south : Passage * east : Passage * info : RoomInfo

and Passage (description : string) =
    member val Room1 = Dummy with get, set
    member val Room2 = Dummy with get, set
    member __.Description = description
    member public this.GoThrough (room : Room) =
        if room = this.Room1
        then this.Room2
        else this.Room1

type Direction =
    | North
    | West
    | South
    | East

type Equipment = CollectableActor list

type PlayerInfo = Direction * Equipment

type GameState =
    | Exploration of Room * PlayerInfo
    | Sleeping of Room * PlayerInfo

type TurnDirection =
    | Left
    | Right
    | Back
    | NoTurn

type Operation =
    | GeneralExamination
    | GameExit
    | Sleep
    | WakeUp
    | GoAhead
    | Turn of TurnDirection

type ActionResult =
    | Success of GameState * string
    | Failure of string
    | Interrupt
  