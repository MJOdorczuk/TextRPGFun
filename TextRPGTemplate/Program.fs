open Controls
open StartingStates.State1.Data

let entryInfo = "
To go ahead type \"go\"
To examine the current room \"examine all\"
To examine particullar object \"examine <OBJECT_NAME>\"
To turn type \"turn\" and than \"back/left/right\"
To go asleep type \"sleep\"
To wake up type \"wake up\"
To exit the game type \"exit\"
"

[<EntryPoint>]
let main _ =
    printfn "%s" entryInfo
    Play gameState
    0
