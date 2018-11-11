namespace Client

open Elmish.Browser.UrlParser

/// The different pages of the application. If you add a new page, then add an entry here.
[<RequireQualifiedAccess>]
type Page =
    | Home
    | Login
    | About

module Pages =
    let toPath =
        function
        | Page.Home -> "#home"
        | Page.About -> "#about"
        | Page.Login -> "#login"

    /// The URL is turned into a Result.
    let pageParser : Parser<Page -> Page,_> =
        oneOf [
            map Page.Home (s "home")
            map Page.Login (s "login")
            map Page.About (s "about")
        ]

    let urlParser location =
        match parseHash pageParser location with
        | Some page -> Some page
        | None -> parsePath (oneOf [ map Page.Home (s "") ]) location
