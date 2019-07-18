using System;
using System.Text;

namespace Sort
{
    class Program
    {
        private static void SelectionSort(int[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var min = i;

                for (var j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[min])
                        min = j;
                }

                array.Swap(i, min);
            }
        }

        private static void BubbleSort(int[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                for (var j =  0; j < array.Length - 1; j++)
                {
                    if (array[i] < array[j])
                        array.Swap(i, j);
                }
            }
        }

        private static void CocktailSort(int[] array)
        {                      
            var left = 0;
            var rigth = array.Length - 1;

            do
            {
                for (var i = left; i < rigth; i++)
                {
                    if (array[i + 1] < array[i])
                        array.Swap(i + 1, i);
                }
                rigth--;

                for (int i = rigth; i > left; i--)
                {
                    if (array[i - 1] > array[i])
                        array.Swap(i - 1, i);
                }
                left++;

            } while (left <= rigth);
        }

        private static void GnomeSort(int[] array)
        {
            var i = 1;
            while (i < array.Length)
            {
                if (i == 0 || array[i - 1] <= array[i])
                {
                    i++; 
                }
                else
                {
                    array.Swap(i - 1, i);
                    i--;                                          
                }                
            }
        }

        private static void InsertionSort(int[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                var j = i;
                var temp = array[i];
                while (j > 0 && temp < array[j - 1])
                {
                    array[j] = array[j - 1];
                    j--;
                }

                array[j] = temp;
            }              
        }        

        private static void MergeSort(int[] array, int start, int end)
        {
            if (end - start <= 1)
                return;
            
            var mid = start + (end - start) / 2;
            MergeSort(array, start, mid);
            MergeSort(array, mid, end);
            Merge(array, start, mid, end);            
        }        

        private static void ShellSort(int[] array)
        {
            var offset = array.Length / 2;
            while (offset > 0)
            {
                for (var i = 0; i < array.Length - offset; i++)
                {
                    var j = i;
                    while (j >= 0 && array[j] > array[j + offset])
                    {
                        array.Swap(j, j + offset);                        
                        j -= offset;
                    }
                }

                offset /= 2;
            }
        }
               
        private static void HeapSort(int[] array)
        {
            #region build heap
            for (var i = array.Length / 2 - 1; i >= 0; i--)
            {
                Shift(array, i, array.Length);
            }
            #endregion

            #region sort
            for (var i = array.Length - 1; i >= 1; i--)
            {
                array.Swap(0, i);
                Shift(array, 0, i);
            }
            #endregion
        }
        
        private static void QuickSort(int[] array, int start, int end)
        {
            if (start < end)
            {
                var pivot = GetPivot(array, start, end);
                QuickSort(array, start, pivot - 1);
                QuickSort(array, pivot + 1, end);
            }
        }

        static void Main(string[] args)
        {
            var array = new int[] { 8, 2, 3, 5, 6, 1, 7, 4, 0, 9 };
            
            //SelectionSort(array);
            //BubbleSort(array);
            //CocktailSort(array);
            //GnomeSort(array);
            //InsertionSort(array);
            //MergeSort(array, 0, array.Length);
            //ShellSort(array);
            //HeapSort(array);
            QuickSort(array, 0, array.Length - 1);
            array.Output();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void Shift(int[] array, int curIndex, int curLength)
        {
            var maxChildIndex = 0;

            while (2 * curIndex + 1 < curLength)
            {
                maxChildIndex = (2 * curIndex + 1 == curLength - 1) || (array[2 * curIndex + 1] > array[2 * curIndex + 2])
                    ? 2 * curIndex + 1
                    : 2 * curIndex + 2;

                if (array[curIndex] >= array[maxChildIndex])
                    break;
                
                array.Swap(curIndex, maxChildIndex);
                curIndex = maxChildIndex;
            }
        }

        private static int GetPivot(int[] array, int start, int end)
        {
            var pivot = start;

            for (var i = start; i <= end; i++)
            {
                if (array[i] <= array[end])
                    array.Swap(pivot++, i);
            }

            return --pivot;
        }

        private static void Merge(int[] array, int start, int mid, int end)
        {
            var result = new int[end - start];
            var l = 0;
            var r = 0;
            var i = 0;

            while (l < mid - start && r < end - mid)
            {
                result[i++] = array[start + l] < array[mid + r]
                    ? array[start + l++]
                    : array[mid + r++];
            }

            while (r < end - mid)
                result[i++] = array[mid + r++];

            while (l < mid - start)
                result[i++] = array[start + l++];

            Array.Copy(result, 0, array, start, result.Length);
        }
    }

    public static class ArrayExtensions
    {
        public static void Swap(this int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static void Output(this int[] items)
        {
            var output = new StringBuilder();
            foreach (var item in items)
                output.Append(item + " ");

            Console.WriteLine(output);
        }
    }
}