module SudokuSolver

open System

type Digit = Char
type Row = Digit list
type Matrix = Row list

let someSudoku : Matrix = 
    [['1'; '2';]; ['2'; '3';]]

let digits : Box list = ['1'..'9']

let isBlank = (=) '0'

[<EntryPoint>]
let main argv =
    printfn "Hello World"
    0
