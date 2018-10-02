module TimeOff.TestsRunner

open Expecto

[<EntryPoint>]
let main args =
  runTestsInAssembly { defaultConfig with ``parallel`` = false } args