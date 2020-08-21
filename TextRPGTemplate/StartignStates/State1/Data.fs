module StartingStates.State1.Data

open Types
open StartingStates.Items.Currency

let mainRoomInfo : RoomInfo = 
    RoomInfo (
        "Main hall", 
        "It is a huge-ass hall with nothing inside",
        [commonSilverCoin; commonSilverCoin; commonSilverCoin])
let trapRoomInfo : RoomInfo = 
    RoomInfo (
        "", 
        "It's a trap", 
        [commonSilverCoin; uncommonSilverCoin])
    
let sideRoomInfo : RoomInfo = 
    RoomInfo (
        "Side room", 
        "It is a small side room", 
        [])

let fartherRoomInfo : RoomInfo = 
    RoomInfo (
        "Farther room", 
        "It is an even farther room", 
        [])


let solidBrickWall : Passage = Passage "solid brick wall"
let mainTrapPassage : Passage = Passage "promising looking door"
let mainSidePassage : Passage = Passage "small side door"
let sideFartherPassage : Passage = Passage "even smaller door"

let mainRoom : Room = Hall(mainTrapPassage, mainSidePassage, solidBrickWall, solidBrickWall, mainRoomInfo)
let trapRoom : Room = Hall(solidBrickWall, solidBrickWall, solidBrickWall, solidBrickWall, trapRoomInfo)
let sideRoom : Room = Hall(solidBrickWall, sideFartherPassage, solidBrickWall, mainSidePassage, sideRoomInfo)
let fartherRoom : Room = Hall(solidBrickWall, solidBrickWall, solidBrickWall, sideFartherPassage, fartherRoomInfo)

mainTrapPassage.Room1 <- mainRoom
mainTrapPassage.Room2 <- trapRoom
mainSidePassage.Room1 <- mainRoom
mainSidePassage.Room2 <- sideRoom
sideFartherPassage.Room1 <- sideRoom
sideFartherPassage.Room2 <- fartherRoom

let playerInfo : PlayerInfo = North, []
let gameState : GameState = Exploration (mainRoom, playerInfo)