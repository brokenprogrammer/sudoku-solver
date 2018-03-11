(*
    MIT License

    Copyright (c) 2018 Oskar Mendel

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*)


(*
    TODO:
    Currently the algorithm used here is just a pure brute force
    it can be improved by a lot since it currently fails under
    out of memory exception. If some easy functions are added 
    that searches for solutions they can just be added within
    the solve function to improve it. I will add this at a later
    time.
    TODO: Might be a good idea to check out unit testing in F# by
    developing some tests for this application instead of using
    the constats someSudoku and solvedSudoku.
*)
module SudokuSolver

open System

type Row<'T> = 'T list
type Matrix<'T> = Row<'T> list

type Digit = Char
type Choices = Digit list
type Grid = Matrix<Digit>

let undefined<'T> : 'T = failwith "Not implemented yet"

let rows = (fun x -> x)

// For each row we map over each cell.
let cols l =
    l |> List.mapi (fun a row -> row |> List.mapi (fun b cell -> l.[b].[a]))

// Split the sudoku grid into boxes of size 3x3
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

// An unsolved sudoku grid to use for testing.
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

// A solved sudoku grid also used for testing.
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

// List of all the digits used when retrieving choices.
let digits : Digit list = ['1'..'9']

// Helper function to check if target cell is blank or not filled in.
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

// Checks that there is no duplicates within given list.
let rec noDuplicates l : bool =
    match l with
        | [] -> true
        | x::xs -> (not (List.contains x xs)) && noDuplicates xs

// Checks if given sudoku grid is a valid solution.
let valid (g : Grid) : bool =
    List.forall noDuplicates (rows g) &&
    List.forall noDuplicates (cols g) &&
    List.forall noDuplicates (boxs g)

// Solves a sudoku by getting all the different choices and filtering out
// the invalid ones.
let solve g : Grid list = 
    g |> choices |> expand |> List.filter valid

[<EntryPoint>]
let main argv =
    printfn "Hello World"
    0
