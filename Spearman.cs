using System;

class Program
{
    static void Main()
    {
        double [] a = new double [5] {1,2,3,4,5};
        double [] b = new double [5] {10,9,7,5,2};
        double rho = Correlation.Spearman(a, b);
        Console.WriteLine("Spearman Rho is {0}", rho);
    }
}

class Correlation
{
    public static double Spearman(double[] x, double[] y)
    {   
        if( x.Length == y.Length)
        {
            int[] xRank = SortRankDescend(x);
            int[] yRank = SortRankDescend(y);
            int[] rankDifference= new int[x.Length];
            double rho;
            
            for ( int i = 0; i < rankDifference.Length; i++)
            {
                rankDifference[i] = (xRank[i] - yRank[i])*(xRank[i] - yRank[i]);
            }
            
            int sumDifference = 0;
            
            foreach( int i in rankDifference)
            {
                sumDifference =+i;
            }
            int n  = rankDifference.Length;
            rho = 1 - 6.0*sumDifference/(n*(n*n-1));
            return rho;
            
            
        }
        else
        {
            Console.WriteLine("Set sizes must be equal");
            return 0;
        }
    }
    
    public static double arrayMean (int[] x)
    {
        double mean;
        int sum = 0;
        foreach( int i in x)
        {
            sum += i;
        }
        mean = 10.0*sum/x.Length;
        return mean;
    }
    
    public static int[] SortRankDescend(double[] toSort)
    {
        double[] sorted = new double [toSort.Length];
        int[] rank = new int [toSort.Length];
        
        for( int j = 0; j <sorted.Length;j++)
        {
            double max = 0;
            int maxPosition=0;
            for( int i = 0;  i < toSort.Length; i++)
            {
                if(j!=0)
                {
                    if(toSort[i]> max && sorted[j-1]>toSort[i])
                    {
                        max = toSort[i];
                        maxPosition = i;
                    }
                }
                else
                {
                    if(toSort[i]> max)
                    {
                        max = toSort[i];
                        maxPosition = i;
                    }                
                }
            }
            rank[j] = maxPosition+1;
            sorted[j] = max;
        }  
        Console.WriteLine();
        Console.WriteLine("ToSort");
        foreach( double i in toSort)
        {
            Console.Write( i + " ");
        }
        Console.WriteLine();
        
        Console.WriteLine("Sorting");
        
        foreach( double i in sorted)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        
        Console.WriteLine("rank");
        foreach( int i in rank)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        
        return rank;
    }
    // public static void SortRank(double[] toSort, out double[] sorted, out int[] rank)
    // {
        // sorted = new double [toSort.Length];
        // rank = new int [toSort.Length];
        
        // for( int j = 0; j <sorted.Length;j++)
        // {
            // double max = 0;
            // int maxPosition=0;
            // for( int i = 0;  i < toSort.Length; i++)
            // {
                // if(j!=0)
                // {
                    // if(toSort[i]> max && sorted[j-1]>toSort[i])
                    // {
                        // max = toSort[i];
                        // maxPosition = i;
                    // }
                // }
                // else
                // {
                    // if(toSort[i]> max)
                    // {
                        // max = toSort[i];
                        // maxPosition = i;
                    // }                
                // }
            // }
            // rank[j] = maxPosition;
            // sorted[j] = max;
        // }  
    // }
}