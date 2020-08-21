module StartingStates.Items.Currency

open Actors.Types

let commonSilverCoin = 
    Item (CollectableActor ("common silver coin", "It's just another common silver coin."))

let uncommonSilverCoin =
    Item (CollectableActor ("uncommon silver coin", "That is uncommon."))