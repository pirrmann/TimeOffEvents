module TimeOff.Tests

open Expecto
open EventStorage

let Given events = events
let When command events = events, command
let Then expected message (events: RequestEvent list, command) =
    let store = InMemoryStore.Create<UserId, RequestEvent>()
    for event in events do
      let stream = store.GetStream event.Request.UserId
      stream.Append [event]
    let result = Logic.decide store command
    Expect.equal result expected message

open System

let creationTests =
  testList "Creation tests" [
    test "A request is created" {
      let request = {
        UserId = 1
        RequestId = Guid.Empty
        Start = { Date = DateTime(2018, 12, 28); HalfDay = AM }
        End = { Date = DateTime(2018, 12, 28); HalfDay = PM } }

      Given [ ]
      |> When (RequestTimeOff request)
      |> Then (Ok [RequestCreated request]) "The request should have been created"
    }
  ]

let validationTests =
  testList "Validation tests" [
    test "A request is validated" {
      let request = {
        UserId = 1
        RequestId = Guid.Empty
        Start = { Date = DateTime(2018, 12, 28); HalfDay = AM }
        End = { Date = DateTime(2018, 12, 28); HalfDay = PM } }

      Given [ RequestCreated request ]
      |> When (ValidateRequest (1, Guid.Empty))
      |> Then (Ok [RequestValidated request]) "The request should have been validated"
    }
  ]

let tests =
  testList "All tests" [
    creationTests
    validationTests
  ]