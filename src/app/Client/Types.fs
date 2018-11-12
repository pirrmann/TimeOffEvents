module Client.Types

/// The composed set of messages that update the state of the application
type AppMsg =
    | HomeMsg of Home.Types.Msg

/// The composed model for the application, which is a single page state plus login information
type Model = {
        Home: Home.Types.Model
    }