module SudokuSolver

open System

type Box = Char
type Matrix = Box list list

let someSudoku : Matrix = 
    [['1'; '2';]; ['2'; '3';]]

[<EntryPoint>]
let main argv =
    printfn "Hello World"
    0
