Imports System.Runtime.CompilerServices

Module StringExtensions
    <Extension()>
    Public Sub ConsoleGreen(ByVal aString As String)
        aString.ColoredWriteLine(ConsoleColor.Green)
    End Sub

    <Extension()>
    Public Sub ConsoleRed(ByVal aString As String)
        aString.ColoredWriteLine(ConsoleColor.Red)
    End Sub

    <Extension()>
    Public Sub ConsoleYellow(ByVal aString As String)
        aString.ColoredWriteLine(ConsoleColor.Yellow)
    End Sub

    <Extension()>
    Public Sub ColoredWriteLine(ByVal aString As String, ByVal color As ConsoleColor)
        Console.ForegroundColor = color
        Console.WriteLine(aString)
        Console.ResetColor()
    End Sub

End Module
