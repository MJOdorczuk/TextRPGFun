module Player.Operations

open Types
open Actors.Types

let PlayerDirection ((direction, _) : PlayerInfo) : Direction =
    direction

let ChangeDirection ((_, equipment) : PlayerInfo) (direction : Direction) : PlayerInfo =
    direction, equipment

let PlayerEquipment ((_, equipment) : PlayerInfo) : Equipment =
    equipment

let ChangeEquipment ((direction, _) : PlayerInfo) (equipment : Equipment) : PlayerInfo =
    direction, equipment

let Equip (item : CollectableActor) (info : PlayerInfo) : PlayerInfo =
    let equipment = PlayerEquipment info
    ChangeEquipment info (item::equipment)

let GetDirectionAfterTurn (direction : Direction) (turnDirection : TurnDirection) : Direction =
    match direction, turnDirection with
    | _, NoTurn -> direction
    | North, Left
    | South, Right
    | East, Back -> West
    | North, Right
    | South, Left
    | West, Back -> East
    | North, Back
    | West, Left
    | East, Right -> South
    | South, Back
    | West, Right
    | East, Left -> North