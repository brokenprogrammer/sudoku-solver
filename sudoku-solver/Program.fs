module SudokuSolver

open System

type Row<'T> = 'T list
type Matrix<'T> = Row<'T> list

type Digit = Char
type Choices = Digit list
type Grid = Matrix<Digit>

let undefined<'T> : 'T = failwith "Not implemented yet"

let rows = (fun x -> x)

// TODO: Försök förstår hur denna fungerar..
//  Oskar Mendel 2018-03-07
let cols l =
    l |> List.mapi (fun a row -> row |> List.mapi (fun b cell -> l.[b].[a]))

let boxs l =
    let rec split list = 
        match list with
            | [] -> []
            | x::y::z::xs -> [x;y;z;] :: split xs
    l 
    |> List.map split 
    |> split 
    |> List.map cols
    |> List.map (List.map List.concat)
    |> List.concat

let someSudoku : Grid = 
    [['5'; '3'; '0';  '0'; '7'; '0';  '0'; '0'; '0'];
     ['6'; '0'; '0';  '1'; '9'; '5';  '0'; '0'; '0'];
     ['0'; '9'; '8';  '0'; '0'; '0';  '0'; '6'; '0'];

     ['8'; '0'; '0';  '0'; '6'; '0';  '0'; '0'; '3'];
     ['4'; '0'; '0';  '8'; '0'; '3';  '0'; '0'; '1'];
     ['7'; '0'; '0';  '0'; '2'; '0';  '0'; '0'; '6'];

     ['0'; '6'; '0';  '0'; '0'; '0';  '2'; '8'; '0'];
     ['0'; '0'; '0';  '4'; '1'; '9';  '0'; '0'; '5'];
     ['0'; '0'; '0';  '0'; '8'; '0';  '0'; '7'; '0']]

let solvedSudoku =
    [['1'; '2'; '8';  '3'; '4'; '5';  '6'; '9'; '7'];
     ['5'; '3'; '4';  '6'; '7'; '9';  '2'; '1'; '8'];
     ['6'; '7'; '9';  '1'; '8'; '2';  '5'; '4'; '3'];

     ['2'; '1'; '6';  '4'; '3'; '8';  '7'; '5'; '9'];
     ['4'; '8'; '5';  '7'; '9'; '1';  '3'; '2'; '6'];
     ['3'; '9'; '7';  '5'; '2'; '6';  '4'; '8'; '1'];

     ['7'; '6'; '2';  '9'; '1'; '4';  '8'; '3'; '5'];
     ['9'; '4'; '3';  '8'; '5'; '7';  '1'; '6'; '2'];
     ['8'; '5'; '1';  '2'; '6'; '3';  '9'; '7'; '4']]

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
    List.forall noDuplicates (rows g) &&
    List.forall noDuplicates (cols g) &&
    List.forall noDuplicates (boxs g)

let solve g : Grid list = 
    g |> choices |> expand |> List.filter valid

[<EntryPoint>]
let main argv =
    printfn "Hello World"
    0
