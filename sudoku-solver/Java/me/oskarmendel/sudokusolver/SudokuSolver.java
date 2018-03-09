package me.oskarmendel.sudokusolver;

public class SudokuSolver {
	
	static int unsolvedSudoku[][] = {
			{5, 3, 0,  0, 7, 0,  0, 0, 0},
			{6, 0, 0,  1, 9, 5,  0, 0, 0},
			{0, 9, 8,  0, 0, 0,  0, 6, 0},
			{8, 0, 0,  0, 6, 0,  0, 0, 3},
			{4, 0, 0,  8, 0, 3,  0, 0, 1},
			{7, 0, 0,  0, 2, 0,  0, 0, 6},
			{0, 6, 0,  0, 0, 0,  2, 8, 0},
			{0, 0, 0,  4, 1, 9,  0, 0, 5},
			{0, 0, 0,  0, 8, 0,  0, 7, 0}
		};
	
	//TODO: Pretty printing?
	//TODO: Allow for user input.
	
	public static void main(String[] args) {
		if (solve(0, 0, unsolvedSudoku) && isValid(unsolvedSudoku)) {
			for (int row = 0; row < unsolvedSudoku.length; row++) {
				for (int col = 0; col < unsolvedSudoku[row].length; col++) {
					System.out.print(unsolvedSudoku[row][col]);
				}
				System.out.println("");
			}
		} else {
			System.out.println("No valid solution was found.");
		}
	}
	
	public static boolean solve(int row, int col, int[][] sudoku) 
	{
		// Base case, if row number 9 was reached we increment the column
		//	and if the column reaches 9 the end of the sudoku has been reached 
		//		and the solution was found.
		if (row == 9) 
		{
			row = 0;
			col++;
			
			if (col == 9) 
			{
				return true;
			}
		}
		
		if (sudoku[row][col] != 0) 
		{
			return solve(row + 1, col, sudoku);
		}
		
		for (int i = 0; i <= 9; i++) 
		{
			if (isValidPlacement(row, col, i, sudoku)) 
			{
				sudoku[row][col] = i;
				if (solve(row+1, col, sudoku)) 
				{
					return true;
				}
			}
		}
		
		// Backtracking cause no valid placement was found.
		sudoku[row][col] = 0;
		return false;
	}
	
	public static boolean isValidPlacement(int row, int col, int value, int[][] sudoku) 
	{
		// Testing value against row.
		for (int r = 0; r < sudoku.length; r++) 
		{
			if (value == sudoku[r][col]) 
			{
				return false;
			}
		}
		
		// Testing value against column.
		for (int c = 0; c < sudoku[row].length; c++) 
		{
			if (value == sudoku[row][c]) 
			{
				return false;
			}
		}
		
		// Testing against 3x3 box.
		for (int r = 0; r < sudoku.length / 3; r++) 
		{
			for (int c = 0; r < sudoku[row].length / 3; r++) 
			{
				if (value == sudoku[r + ((row / 3) * 3)][c + ((col / 3) * 3)]) 
				{
					return false;
				}
			}
		}
		
		return true;
	}
	
	public static boolean isValid(int[][] sudoku) 
	{
		if (noDuplicates(sudoku) && noDuplicates(getColumns(sudoku)) && noDuplicates(getBoxes(sudoku))) {
			return true;
		}
		
		return false;
	}
	
	public static boolean noDuplicates(int[][] sudoku) {
		
		for (int row = 0; row < sudoku.length; row++) {
			for (int col = 0; col < sudoku[row].length; col++) {
				int testing = sudoku[row][col];
				for (int val = col+1; val < sudoku[row].length; val++) {
					if (testing == sudoku[row][val]) {
						return false;
					}
				}
			}
		}
		
		return true;
	}
	
	public static int[][] getColumns(int[][] sudoku)
	{
		int colums[][] = new int[9][9];
		for (int row = 0; row < sudoku.length; row++) {
			for (int col = 0; col < sudoku[row].length; col++) {
				colums[col][row] = sudoku[row][col];
			}
		}
		
		return colums;
	}
	
	public static int[][] getBoxes(int[][] sudoku) 
	{
		int boxes[][] = new int[sudoku.length][sudoku[0].length];
		
		int rowStart = 0;
		int colStart = 0;
		int tRow = 0;
		for (int yMul = 1; yMul <= 3; yMul++) {
			colStart = 0;
			for (int xMul = 1; xMul <= 3; xMul++) {
				for (int row = rowStart; row < 3 * yMul; row++) {
					for (int col = colStart; col < 3 * xMul; col++) {
						boxes[tRow][col - colStart + ((row - rowStart) * 3)] = sudoku[row][col];
					}
				}
				colStart += 3;
				tRow++;
			}
			rowStart += 3;
		}
		
		return boxes;
	}

}
