using System;

class Program
{
    static void Main()
    {
        double moment=0.2;
        double field=50*Math.Pow(10,-6);
        double time =0.2;
        
        double inertiaH = 1/12.0*1.11*(0.3*0.3+0.1*0.1);
        
        int count = 1;
        
        Delta(moment, field, Math.PI/2, inertiaH, time, ref count);
    }
    
    static void Delta
    ( double moment, 
    double field, 
    double delta, 
    double inertia,
    double time, 
    ref int count)
    {
        while( count<=100)
        {
        double torque = Math.Sin(Math.PI-delta)*field*moment;
        double aAccel = torque/inertia;
        double dDelta = 0.5*aAccel*time*time;
        Console.Write("[{0}] ", count);
        Console.WriteLine("{0:F5}", (delta-dDelta)*180/Math.PI);
        count++;
        Delta(moment, field, delta - dDelta , inertia, time, ref count);
        }
    }
}