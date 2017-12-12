#I @"packages/FAKE/tools/"

#r "FakeLib.dll"

open Fake
open System.IO
open System.Diagnostics

// Properties
let buildDir = "./build/"

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir]
)

Target "BuildApp" (fun _ ->
   !! "**/*.fsproj"
     |> MSBuildRelease buildDir "Build"
     |> Log "AppBuild-Output: "
)

Target "Default" (fun _ ->
    trace "Building your Dojo project"
)

// Dependencies
"Clean"
  ==> "BuildApp"
  ==> "Default"

// start build
RunTargetOrDefault "Default"