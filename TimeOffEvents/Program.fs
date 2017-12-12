module TimeOff.TestsRunner

open Expecto

[<EntryPoint>]
let main args =
  runTestsWithArgs { defaultConfig with ``parallel`` = false } args Tests.tests