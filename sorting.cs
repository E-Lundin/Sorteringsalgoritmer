using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace Sorting
{
    class Program
    {
        // Returns a list of `elements`random integers  
        static List<int> genList(int elements)
        {
            List<int> list = new List<int>();
            Random rand = new Random();
            for (int i = 0; i < elements; i++)
            {
                list.Add(rand.Next(100000));
            }
            return list;
        }

        // Swaps position of elements at given indices
        static void Swap(List<int> list, int firstIndex, int secondIndex)
        {
            int temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        static int Partition(List<int> list, int left, int right)
        {
            int pivot = list[left];
            while (true)
            {

                while (list[left] < pivot)
                {
                    left++;
                }

                while (list[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (list[left] == list[right]) return right;

                    int temp = list[left];
                    list[left] = list[right];
                    list[right] = temp;


                }
                else
                {
                    return right;
                }
            }
        }

        static void Merge(int[] input, int left, int middle, int right)
        {
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            Array.Copy(input, left, leftArray, 0, middle - left + 1);
            Array.Copy(input, middle + 1, rightArray, 0, right - middle);

            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }

        static void BubbleSort(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        Swap(list, j, j + 1);
                    }
                }
            }
        }

        static void InsertionSort(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (list[j - 1] > list[j])
                    {
                        // Shift the position, smallest number to the left
                        Swap(list, j - 1, j);
                    }
                }
            }
        }

        static void SelectionSort(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                // Find the smallest element of the list
                int min_idx = i;
                for (int j = i + 1; j < list.Count - 1; j++)
                    if (list[j] < list[min_idx])
                    {
                        min_idx = j;
                    }

                // Replace the found smallest element at index 0
                Swap(list, min_idx, i);
            }
        }

        // Wrapper to comply with List<Action> lol
        static void MergesortWrapper(List<int> list)
        {
            int[] array = list.ToArray();
            MergeSort(array, 0, array.Length - 1);
        }

        static void MergeSort(int[] list, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(list, left, middle);
                MergeSort(list, middle + 1, right);

                Merge(list, left, middle, right);
            }
        }

        // Wrapper to comply with List<Action> lol
        static void QuicksortWrapper(List<int> list)
        {
            Quicksort(list, 0, list.Count - 1);
        }

        static void Quicksort(List<int> list, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(list, left, right);

                if (pivot > 1)
                {
                    Quicksort(list, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quicksort(list, pivot + 1, right);
                }
            }

        }

        // Evaluates all algorithms and prints the result
        static void Evaluate(int elements)
        {
            string[] names = { "Bubblesort", "InsertionSort", "SelectionSort", "Mergesort", "Quicksort"};
            int algoIdx = 0; // Index of the algorithm that's currently being evaluated
            Console.WriteLine("========================");
            Console.WriteLine("Evaluating " + elements + " elements:");

            // Generate a List of random integers
            // For optimal results, a copy of said list is used for every algorithm
            List<int> baseList = genList(elements);

            List<Action<List<int>>> algorithms = new List<Action<List<int>>>();
            algorithms.Add(BubbleSort);
            algorithms.Add(InsertionSort);
            algorithms.Add(SelectionSort);
            algorithms.Add(MergesortWrapper);
            algorithms.Add(QuicksortWrapper);

            foreach (Action<List<int>> func in algorithms)
            {
                // Create a copy of the original List
                List<int> newList = new List<int>(baseList);
                string algoName = names[algoIdx];
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                // Execute the algorithm
                func(newList);

                stopWatch.Stop();
                Console.WriteLine(algoName + " " + stopWatch.Elapsed.TotalSeconds + "s");
                algoIdx += 1;
            }

        }
        static void Main(string[] args)
        {
            // Easier to perceive powers of 10, hence the later conversion to Int32
            double[] elements = { 1e1, 1e2, 1e3, 1e4, 1e5};

            foreach (double element in elements)
            {
                int value = Convert.ToInt32(element);
                Evaluate(value);
            }
            Console.WriteLine("========================");

        }
    }
}
