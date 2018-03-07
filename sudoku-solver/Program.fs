module SudokuSolver

open System

type Row<'T> = 'T list
type Matrix<'T> = Row<'T> list

type Digit = Char
type Choices = Digit list
type Grid = Matrix<Digit>

let undefined<'T> : 'T = failwith "Not implemented yet"

let rows = (fun x -> x)
//let cols = List.foldBack 

let someSudoku : Grid = 
    [['1'; '2';]; ['2'; '3'; '4';]]

let digits : Digit list = ['1'..'9']

let isBlank = (=) '0'

let choices (g : Grid) : Matrix<Choices> = 
    let choice d =
        match d with
            | ('0') -> digits
            | _ -> d :: []
    List.map (List.map choice) g

let expand (m : Matrix<Choices>) : Grid list =
    let op xs yss = [for x in xs do for ys in yss do yield x::ys]
    let cp xss =
        List.foldBack op xss [[]]
    (List.map cp) m |> cp

let rec noDuplicates l : bool =
    match l with
        | [] -> true
        | x::xs -> (not (List.contains x xs)) && noDuplicates xs

let valid (g : Grid) : bool =
    List.forall noDuplicates (rows g)
    //List.forall noDuplicates (cols g) &&
    //List.forall noDuplicates (boxs g)

[<EntryPoint>]
let main argv =
    printfn "Hello World"
    0
