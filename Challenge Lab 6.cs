/* Challenge Lab 6; Leet Code: Turn an image.
You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees (clockwise).

You have to rotate the image in-place, which means you have to modify the input 2D matrix directly. DO NOT allocate another 2D matrix and do the rotation.

Example 1:
Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
Output: [[7,4,1],[8,5,2],[9,6,3]]

Example 2:
Input: matrix = [[5,1,9,11],[2,4,8,10],[1You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees (clockwise).

You have to rotate the image in-place, which means you have to modify the input 2D matrix directly. DO NOT allocate another 2D matrix and do the rotation.

Example 1:
Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
Output: [[7,4,1],[8,5,2],[9,6,3]]

Example 2:
Input: matrix = [[5,1,9,11],[2,4,8,10],[13,3,6,7],[15,14,12,16]]
Output: [[15,13,2,5],[14,3,4,1],[12,6,8,9],[16,7,10,11]]3,3,6,7],[15,14,12,16]]
Output: [[15,13,2,5],[14,3,4,1],[12,6,8,9],[16,7,10,11]]
*/
namespace Challenge_Lab_6;

public class Program
{
    public static void Main(string[] args)
    {
        int[,] matrix1 = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        int[,] matrix2 = new int[,]
        {
            { 5, 1, 9, 11 },
            { 2, 4, 8, 10 },
            { 13, 3, 6, 7 },
            { 15, 14, 12, 16 }
        };

        Console.WriteLine("Original Matrix 1:");
        PrintMatrix(matrix1);
        RotateMatrix(matrix1);
        Console.WriteLine("Rotated Matrix 1:");
        PrintMatrix(matrix1);
        Console.WriteLine("Rotated using transposition and reversal:");
        RotateMatrixWithTransposition(matrix1);
        PrintMatrix(matrix1);

        Console.WriteLine("Original Matrix 2:");
        PrintMatrix(matrix2);
        RotateMatrix(matrix2);
        Console.WriteLine("Rotated Matrix 2:");
        PrintMatrix(matrix2);
        Console.WriteLine("Rotated using transposition and reversal:");
        RotateMatrixWithTransposition(matrix2);
        PrintMatrix(matrix2);
        Console.WriteLine();

        // User input for custom matrix
        Console.WriteLine("User Input Matrix:");

        int n;
        while (true)
        {
            Console.Write("Enter the size of the n x n matrix (positive integer): ");
            if (int.TryParse(Console.ReadLine(), out n) && n > 0)
            {
                break;
            }
            Console.WriteLine("Invalid input. Please enter a positive integer.");
        }

        int[,] userMatrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            int[] row = TakeUserIntArray(n);
            for (int j = 0; j < n; j++)
            {
                userMatrix[i, j] = row[j];
            }
        }

        Console.WriteLine("Original User Matrix:");
        PrintMatrix(userMatrix);
        RotateMatrix(userMatrix);
        Console.WriteLine("Rotated User Matrix:");
        PrintMatrix(userMatrix);
        Console.WriteLine("Rotated using transposition and reversal:");
        RotateMatrixWithTransposition(userMatrix);
        PrintMatrix(userMatrix);
        Console.WriteLine();
    }

    /// <summary> Rotates the given n x n matrix by 90 degrees clockwise in place.</summary>
    /// <param name="matrix">The n x n matrix to rotate.</param>
    public static void RotateMatrix(int[,] matrix)
    {
        int matrixLength = matrix.GetLength(0);

        // Process each concentric layer of the matrix
        for (int layer = 0; layer < matrixLength / 2; layer++) // Only need to process outer half of layers
        {
            // Move along the top edge of current layer, rotating 4 elements at a time
            for (int pos = layer; pos < matrixLength - layer - 1; pos++) // Stop before last corner to avoid double-processing
            {
                // Save the top element
                int temp = matrix[layer, pos];

                // Perform 4-way rotation: Top ← Left ← Bottom ← Right ← Top
                matrix[layer, pos] = matrix[matrixLength - pos - 1, layer];                    // Top ← Left
                matrix[matrixLength - pos - 1, layer] = matrix[matrixLength - layer - 1, matrixLength - pos - 1];   // Left ← Bottom  
                matrix[matrixLength - layer - 1, matrixLength - pos - 1] = matrix[pos, matrixLength - layer - 1];   // Bottom ← Right
                matrix[pos, matrixLength - layer - 1] = temp;                                 // Right ← Top
            }
        }
    }
    // Alternative method using transposition and row reversal
    public static void RotateMatrixWithTransposition(int[,] matrix)
    {
        int n = matrix.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++) // above the diagonal
            {
                // Swap elements across the diagonal
                int temporal = matrix[i, j];
                matrix[i, j] = matrix[j, i];
                matrix[j, i] = temporal;
            }
        }
        for (int i = 0; i < n; i++) // Reverse, two pointers
        {
            int left = 0, right = n - 1;
            while (left < right)
            {
                (matrix[i, left], matrix[i, right]) = (matrix[i, right], matrix[i, left]); // tuple notation
                left++;
                right--;
            }
        }
    }

    #region Matrix and Array management methods

    private static void PrintMatrix(int[,] matrix) // print matrix with automatic width formatting
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        // Determine the maximum width of any element in the matrix for padding
        int maxWidth = 0;
        foreach (int value in matrix)
        {
            maxWidth = Math.Max(maxWidth, value.ToString().Length);
        }

        for (int i = 0; i < rows; i++)
        {
            Console.Write("|"); // start of row
            for (int j = 0; j < cols; j++)
            {
                Console.Write($" {matrix[i, j].ToString().PadLeft(maxWidth)} |");
            }
            Console.WriteLine();
        }
    }

    private static int[] TakeUserIntArray(int length) // Prompts the user to enter a series of integers separated by spaces, parses them, and returns the resulting array.
    {
        while (true)
        {
            Console.Write($"Enter {length} integers separated by space: ");
            string? input = Console.ReadLine();
            if (input != null)
            {
                string[] parts = input.Split(' ');
                if (parts.Length != length)
                {
                    Console.WriteLine($"Please enter exactly {length} integers.");
                    continue;
                }
                int[] nums = new int[length];
                bool allValid = true;
                for (int i = 0; i < length; i++)
                {
                    if (int.TryParse(parts[i].Trim(), out int num))
                    {
                        nums[i] = num;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid integer: {parts[i]}");
                        allValid = false;
                        break;
                    }
                }
                if (allValid)
                {
                    Console.WriteLine($"You entered: [{string.Join(", ", nums)}]");
                    return nums;
                }
            }
            else
            {
                Console.WriteLine("No input provided.");
            }
        }
    }
    #endregion
}