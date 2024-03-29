using System.Diagnostics;

namespace Algorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinearSearch();
            BinarySearch(); // faster than linear
            BubbleSort();
            InsertionSort(); // slightly faster than bubble and selection sort
            SelectionSort();
            QuickSort(); // uses recursion -> unstable, good for a lot of data, less complex than merge sort
            MergeSort(); // uses recursion, good for a lot of data
            HeapSort(); 
            CountingSort();
            RadixSort();
        }
        public static void LinearSearch()
        {
            Stopwatch sp = new Stopwatch();
            int key = 10;
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            sp.Start();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == key)
                {
                    Console.WriteLine($"{key} is at index {i} in the array!");
                    sp.Stop();
                    Console.WriteLine(sp.Elapsed + " Linear Search");
                }
            }

            // faster way?
            sp.Restart();
            int index = Array.IndexOf(arr, key);
            Console.WriteLine($"{key} is at index {index} in the array!");
            sp.Stop();
            Console.WriteLine(sp.Elapsed);


        }
        public static void BinarySearch()
        {
            Stopwatch sp = new Stopwatch();
            int key = 23;
            int[] arr = new int[] { 2,5,8,12,16,23,38,56,72,91 };
            var l = 0;
            var r = arr.Length;
            sp.Start();
            while (l <= r)
            {
                var x = l + r / 2;
                if (arr[x] == key)
                {
                    Console.WriteLine($"{key} is at index {x} of array!");
                    sp.Stop();
                    Console.WriteLine(sp.Elapsed + " Binary Search");
                    break;
                }
                if (arr[x] < key)
                {
                    l = x + 1;
                }
                else 
                {
                    r = x - 1;
                }
            }
        }
        public static void BubbleSort()
        {
            Stopwatch sp = new Stopwatch();
            int[] unsorted = new int[] { 8, 5, 7, 3, 1, 4, 9, 10, 2, 6 };
            sp.Start();
            for (int i = 0; i <= unsorted.Length- 2; i++)
            {
                for (int j = 0; j <= unsorted.Length-2; j++)
                {
                    if (unsorted[j] > unsorted[j + 1])
                    {
                        var temp = unsorted[j + 1];
                        unsorted[j + 1] = unsorted[j];
                        unsorted[j] = temp;
                    }
                }
            }
            sp.Stop ();
            Console.WriteLine(string.Join(", ", unsorted));
            Console.WriteLine(sp.Elapsed + " Bubble Sort");
        }
        public static void InsertionSort()
        {
            Stopwatch sp = new Stopwatch();
            int[] unsorted = new int[] { 8, 5, 7, 3, 1, 4, 9, 10, 2, 6 };
            sp.Start();
            for (int i = 1; i <= unsorted.Length - 1; i++)
            {
                var key = unsorted[i];
                var j = i - 1;
                while (j >= 0 && unsorted[j] > key)
                {
                    var temp = unsorted[j + 1];
                    unsorted[j + 1] = unsorted[j];
                    unsorted[j] = temp;
                    j = j - 1;
                }
            }
            sp.Stop();
            Console.WriteLine(string.Join(", ", unsorted));
            Console.WriteLine(sp.Elapsed + " Insertion Sort");
        }
        public static void SelectionSort()
        {
            Stopwatch sp = new Stopwatch();
            int[] unsorted = new int[] { 8, 5, 7, 3, 1, 4, 9, 10, 2, 6 };
            var min = int.MinValue;
            sp.Start();
            for (int i = 0; i < unsorted.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < unsorted.Length; j++)
                {
                    if (unsorted[j] < unsorted[min])
                    {
                        min = j; // keeps track of minimal value throughout the loops
                    }
                }
                if (min != i)
                {
                    var temp = unsorted[i];
                    unsorted[i] = unsorted[min];
                    unsorted[min] = temp;
                }
            }
            sp.Stop();
            Console.WriteLine(string.Join(", ", unsorted));
            Console.WriteLine(sp.Elapsed + " Selection Sort");
        }
        public static void QuickSort()
        {
            Stopwatch sp = new Stopwatch();
            int[] unsorted = new int[] { 8, 5, 7, 3, 1, 4, 9, 10, 2, 6 };
            static int[] Recursion(int[] unsorted, int l, int r)
            {
                var i = l;
                var j = r;
                var pivot = unsorted[l]; // pivot point always at the start of the arr
                while (i <= j)
                {
                    while (unsorted[i] < pivot)
                    {
                        i++;
                    }
                    while (unsorted[j] > pivot)
                    {
                        j--;
                    }
                    if (i <= j)
                    {
                        var temp = unsorted[i];
                        unsorted[i] = unsorted[j];
                        unsorted[j] = temp;
                        i++;
                        j--;
                    }
                }
                if (l < j)
                {
                    Recursion(unsorted, l, j);
                }
                if (r > i)
                {
                    Recursion(unsorted, i, r);
                }
                return unsorted;
            }

            sp.Start();
            var sorted = Recursion(unsorted, 0, unsorted.Length - 1);
            sp.Stop();

            Console.WriteLine(string.Join(", ", sorted));
            Console.WriteLine(sp.Elapsed + " Quick Sort");
        }
        public static void MergeSort()
        {
            Stopwatch sp = new Stopwatch();
            int[] unsorted = new int[] { 8, 5, 7, 3, 1, 4, 9, 10, 2, 6, 11, 15, 13, 12, 14, 20, 16, 19, 17, 18 };
            sp.Start();
            var sorted = SortArray(unsorted, 0, unsorted.Length - 1);
            sp.Stop();

            static int[] SortArray(int[] array, int left, int right)
            {
                if (left < right)
                {
                    int middle = left + (right - left) / 2;
                    SortArray(array, left, middle);
                    SortArray(array, middle + 1, right);

                    Merge(array, left, middle, right);
                }
                return array;
            }
            static void Merge(int[] array, int left, int middle, int right)
            {
                var leftArrayLength = middle - left + 1; 
                var rightArrayLength = right - middle;
                var leftTempArray = new int[leftArrayLength]; // defining temporary arrays to hold data during the sorting process
                var rightTempArray = new int[rightArrayLength];
                
                int i, j;

                for (i = 0; i < leftArrayLength; ++i) // copying data into said temp arrays
                    leftTempArray[i] = array[left + i];
                for (j = 0; j < rightArrayLength; ++j)
                    rightTempArray[j] = array[middle + 1 + j];

                i = 0;
                j = 0;

                int k = left;

                while (i < leftArrayLength && j < rightArrayLength) // sorting and merging 
                {
                    if (leftTempArray[i] <= rightTempArray[j])
                    {
                        array[k++] = leftTempArray[i++];
                    }
                    else
                    {
                        array[k++] = rightTempArray[j++];
                    }
                }
                while (i < leftArrayLength) // copying any remaining elements 
                {
                    array[k++] = leftTempArray[i++];
                }
                while (j < rightArrayLength)
                {
                    array[k++] = rightTempArray[j++];
                }
            }
            Console.WriteLine(string.Join(", ", sorted));
            Console.WriteLine(sp.Elapsed + " Merge Sort");
        }
        public static void HeapSort()
        {
            Stopwatch sp = new Stopwatch();
            int[] unsorted = new int[] { 8, 5, 7, 3, 1, 4, 9, 10, 2, 6, 11, 15, 13, 12, 14, 20, 16, 19, 17, 18 };
            sp.Start();
            var sorted = SortArray(unsorted, unsorted.Length);
            sp.Stop();

            static int[] SortArray(int[] array, int size)
            {
                if (size <= 1)
                {
                    return array;
                }
                for (int i = (size / 2) - 1; i >= 0; i--)
                {
                    Heapify(array, size, i);
                }

                for (int i = size - 1; i >= 0; i--)
                {
                    var temp = array[0];
                    array[0] = array[i];
                    array[i] = temp;

                    Heapify(array, i, 0);
                }
                return array;
            }
            static void Heapify(int[] array, int size, int index)
            {
                var largestIndex = index; // set index of the maximum element to the current array index
                var leftChild = 2 * index + 1; // implementing left child
                var rightChild = 2 * index + 2; // implementing right child

                if (leftChild < size && array[leftChild] > array[largestIndex]) // check if left child > root ? root = left child
                {
                    largestIndex = leftChild;
                }
                if (rightChild < size && array[rightChild] > array[largestIndex]) // check if the right child > root
                {
                    largestIndex = rightChild;
                }
                if (largestIndex != index) // if the max index is != index ? switch places and recursively call upon the function 
                {
                    var tempVar = array[index];
                    array[index] = array[largestIndex];
                    array[largestIndex] = tempVar;

                    Heapify(array, size, largestIndex);
                }
            }
            Console.WriteLine(string.Join(", ", sorted));
            Console.WriteLine(sp.Elapsed + " Heap Sort");
        } // creates a binary heap structure (binary tree) from the elements that need to be sorted
         public static void CountingSort()
        {
            Stopwatch sp = new Stopwatch();
            int[] unsorted = new int[] { 8, 5, 7, 3, 1, 4, 9, 10, 2, 6, 11, 15, 13, 12, 14, 20, 16, 19, 17, 18 };
            sp.Start();
            var maxVal = unsorted.Max();
            var occurrences = Enumerable.Range(0, maxVal + 1).Select(x => 0).ToArray();
            var arr = new int[maxVal];
            for (int i = 0; i < unsorted.Length; i++)
            {
                occurrences[unsorted[i]]++; // checks the occurences of each element 
            }
            for (int i = 1; i <= maxVal; i++)
            {
                occurrences[i] += occurrences[i - 1];
            }
            for (int i = unsorted.Length - 1; i >= 0; i--)
            {
                arr[occurrences[unsorted[i]] - 1] = unsorted[i];
                occurrences[unsorted[i]]--;
            }
            for (int i = 0; i < unsorted.Length; i++)
            {
                unsorted[i] = arr[i];
            }
            sp.Stop();
            Console.WriteLine(string.Join(", ", unsorted));
            Console.WriteLine(sp.Elapsed + "Counting Sort");
        }
         public static void RadixSort() //t is a non-comparative sorting algorithm, meaning that it does not compare the values of the numbers being sorted. However, it uses counting sort as its “inner algorithm” to perform the sorting process. 
        {
            Stopwatch sp = new Stopwatch();
            int[] unsorted = new int[] { 8, 5, 7, 3, 1, 4, 9, 11, 2, 6, 11, 15, 13, 12, 14, 21, 16, 19, 17, 18 };
            sp.Start();
            var maxVal = unsorted.Max();
            for (int i = 1; maxVal / i > 0; i+= 10)
                CountingSort(unsorted, unsorted.Length, i);
            sp.Stop();
            Console.WriteLine(string.Join(", ", unsorted));
            Console.WriteLine(sp.Elapsed + " Radix Sort");

            static void CountingSort(int[] array, int size, int exponent)
            {
                var output = new int[size];

                var occurrences = Enumerable.Range(0, 10).Select(x => 0).ToArray();

                for (int i = 0; i < size; i++)
                    occurrences[(array[i] / exponent) % 10]++;

                for (int i = 1; i < 10; i++)
                    occurrences[i] += occurrences[i - 1];

                for (int i = size - 1; i >= 0; i--)
                {
                    output[occurrences[(array[i] / exponent) % 10] - 1] = array[i];
                    occurrences[(array[i] / exponent) % 10]--;
                }

                for (int i = 0; i < size; i++)
                    array[i] = output[i];
            }
        }
        
    }
}
