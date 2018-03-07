module SudokuSolver

open System

type Row<'T> = 'T list
type Matrix<'T> = Row<'T> list

type Digit = Char
type Choices = Digit list
type Grid = Matrix<Digit>

let undefined<'T> : 'T = failwith "Not implemented yet"

let rows = (fun x -> x)

let cols l = 
    let rec getCol l n =
        let dropOneCol = List.map (List.tail)
        match n with
            | 0 -> []
            | _ -> List.map (List.head) l :: getCol (dropOneCol l) (n-1)
    getCol l 9

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
    List.forall noDuplicates (cols g)
    //List.forall noDuplicates (boxs g)

[<EntryPoint>]
let main argv =
    printfn "Hello World"
    0
