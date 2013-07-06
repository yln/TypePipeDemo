using System;

namespace TypePipeDemo
{
  class Program
  {
    static void Main ()
    {
      var computer = new Computer();

      computer.SurfTheWeb ("http://www.commitLogsFromLastNight.com");
      Console.WriteLine();

      Console.WriteLine ("The meaning of life is {0}.", computer.ComputeMeaningOfLife());
      Console.WriteLine ("The meaning of life is STILL {0}.", computer.ComputeMeaningOfLife());
      Console.WriteLine();
    }
  }
}
