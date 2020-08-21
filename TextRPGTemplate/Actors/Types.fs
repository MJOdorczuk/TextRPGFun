module Actors.Types

type Name = string
type Description = string

type ActorTextData = Name * Description

type CollectableActor = ActorTextData
type StaticActor = ActorTextData
type ContainerActor = ActorTextData * CollectableActor list

type Actor =
    | Item of CollectableActor
    | Static of StaticActor
    | Container of ContainerActor