using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Random r = new Random();
        int[] array = new int[10000];
        for( int i = 0 ; i < array.Length; i++)
        {
            array[i] = r.Next(10);
        }
        int[] sorted = InsertSortIncreasing(array);
        TimeSpan bt;
        TimeSpan lt;
        Console.WriteLine(CheckSort(sorted));
        bool terminate = false;
        int value = 0;
        int result = -1;
        int tr;
        while(!terminate)
        {
            Console.Write("Search for: ");
            value = int.Parse(Console.ReadLine());
            result = BinarySearch(array, value, out bt);
            LinearSearch( array, value, out lt);
            if(result != -1)
            {
                Console.WriteLine("Found {0} at {1}", value, result);
                Console.WriteLine("Time used:\nLinear: {0}\nBinary{1}", lt, bt);
            }
            else
            {
                Console.WriteLine("Cannot find {0}", value);
                Console.WriteLine("Time used:\nLinear: {0}\nBinary{1}", lt, bt);
            }
            Console.Write("\nenter 42 to exit");
            int.TryParse(Console.ReadLine(), out tr);
            if( tr == 42)
            terminate = true;
            Console.Clear();
        }
    }
    
    static int LinearSearch ( int[] array, int value, out TimeSpan time)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        for( int i = 0; i< array.Length; i++)
        {
            if(array[i] == value)
            {
                watch.Stop();
                time = watch.Elapsed;
                return i;
            }
        }
        watch.Stop();
        time = watch.Elapsed;
        return -1;
    }
    
    static int BinarySearch ( int[] array, int value, out TimeSpan time)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        int left = 0;
        int right = array.Length-1;
        int middle = -1;
        
        while(left <= right)
        {
            middle = (left+right)/2;
            if (array[middle] == value)
            {
                watch.Stop();
                time = watch.Elapsed;
                return middle;
            }
            else if(array[middle]>value)
            right = middle - 1;
            else
            left = middle + 1;
        }
        
        watch.Stop();
        time = watch.Elapsed;        
        return -1;
    }
    
    static int[] InsertSortIncreasing ( int[] array )
    {
        int[] result = array;
        for( int i = 1; i < result.Length; i++ )
        {
            int temp = result[i];
            for(int j = i; j > 0 ; j--)
            {
                if(result[j-1]<temp)
                {
                    result[j] = temp;
                    break;
                }
                else
                {
                    result[j] = result[j-1];
                }
            }
        }
        return result;
    }
    
    static bool CheckSort (int[] array)
    {
        for ( int i = 1 ; i < array.Length; i++)
        {
            if ( array[i] < array[i-1])
            return false;
        }
        return true;
    }
}